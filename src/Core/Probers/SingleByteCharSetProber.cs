/* ***** BEGIN LICENSE BLOCK *****
 * Version: MPL 1.1/GPL 2.0/LGPL 2.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is Mozilla Universal charset detector code.
 *
 * The Initial Developer of the Original Code is
 * Netscape Communications Corporation.
 * Portions created by the Initial Developer are Copyright (C) 2001
 * the Initial Developer. All Rights Reserved.
 *
 * Contributor(s):
 *          Shy Shalom <shooshX@gmail.com>
 *          Rudi Pettazzi <rudi.pettazzi@gmail.com> (C# port) 
 *
 * Alternatively, the contents of this file may be used under the terms of
 * either the GNU General Public License Version 2 or later (the "GPL"), or
 * the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
 * in which case the provisions of the GPL or the LGPL are applicable instead
 * of those above. If you wish to allow use of your version of this file only
 * under the terms of either the GPL or the LGPL, and not to allow others to
 * use your version of this file under the terms of the MPL, indicate your
 * decision by deleting the provisions above and replace them with the notice
 * and other provisions required by the GPL or the LGPL. If you do not delete
 * the provisions above, a recipient may use your version of this file under
 * the terms of any one of the MPL, the GPL or the LGPL.
 *
 * ***** END LICENSE BLOCK ***** */

using System.Text;

using UtfUnknown.Core.Models;

namespace UtfUnknown.Core.Probers
{
    public class SingleByteCharSetProber : CharsetProber
    {
        private const int SB_ENOUGH_REL_THRESHOLD = 1024;
        private const float POSITIVE_SHORTCUT_THRESHOLD = 0.95f;
        private const float NEGATIVE_SHORTCUT_THRESHOLD = 0.05f;
        private const int SYMBOL_CAT_ORDER = 250;
        private const int NUMBER_OF_SEQ_CAT = 4;
        private const int POSITIVE_CAT = NUMBER_OF_SEQ_CAT - 1;
        private const int PROBABLE_CAT = NUMBER_OF_SEQ_CAT - 2;
        private const int NEUTRAL_CAT = NUMBER_OF_SEQ_CAT - 3;
        private const int NEGATIVE_CAT = 0;

        protected SequenceModel model;

        // true if we need to reverse every pair in the model lookup        
        private readonly bool _reversed;

        // char order of last character
        private byte _lastOrder;

        private int _totalSeqs;
        private readonly int[] _seqCounters = new int[NUMBER_OF_SEQ_CAT];

        private int _totalChar;
        private int _ctrlChar;

        // characters that fall in our sampling range
        private int _freqChar;

        // Optional auxiliary prober for name decision. created and destroyed by the GroupProber
        private readonly CharsetProber _nameProber;

        public SingleByteCharSetProber(SequenceModel model)
            : this(model, false, null)
        { }

        public SingleByteCharSetProber(
            SequenceModel model, bool reversed, CharsetProber nameProber)
        {
            this.model = model;
            _reversed = reversed;
            _nameProber = nameProber;
            Reset();
        }

        public override ProbingState HandleData(byte[] buf, int offset, int len)
        {
            int max = offset + len;
            for (int i = offset; i < max; i++)
            {
                byte order = model.GetOrder(buf[i]);

                if (order < SYMBOL_CAT_ORDER)
                {
                    _totalChar++;
                }
                else if (order == SequenceModel.ILL)
                {
                    // When encountering an illegal codepoint,
                    // no need to continue analyzing data
                    state = ProbingState.NotMe;
                    break;
                }
                else if (order == SequenceModel.CTR)
                {
                    _ctrlChar++;
                }

                if (order < model.FreqCharCount)
                {
                    ++_freqChar;
                    if (_lastOrder < model.FreqCharCount)
                    {
                        ++_totalSeqs;
                        // reverse the order of the letters in the lookup
                        var pos = _reversed
                            ? order * model.FreqCharCount + _lastOrder
                            : _lastOrder * model.FreqCharCount + order;
                        var precedence = model.GetPrecedence(pos);
                        ++_seqCounters[precedence];
                    }
                }

                _lastOrder = order;
            }

            if (state != ProbingState.Detecting) return state;
            if (_totalSeqs <= SB_ENOUGH_REL_THRESHOLD) return state;

            float cf = GetConfidence();
            if (cf > POSITIVE_SHORTCUT_THRESHOLD)
            {
                state = ProbingState.FoundIt;
            }
            else if (cf < NEGATIVE_SHORTCUT_THRESHOLD)
            {
                state = ProbingState.NotMe;
            }

            return state;
        }

        public override string DumpStatus()
        {
            StringBuilder status = new StringBuilder();
            var cf = GetConfidence();
            var chName = GetCharsetName();
            status.AppendLine(
                $"  SBCS: {cf:0.00############} [{chName}]");
            return status.ToString();
        }

        public override float GetConfidence(StringBuilder status = null)
        {
            /*
            NEGATIVE_APPROACH
            if (totalSeqs > 0) {
                if (totalSeqs > seqCounters[NEGATIVE_CAT] * 10)
                    return (totalSeqs - seqCounters[NEGATIVE_CAT] * 10)/totalSeqs * freqChar / mTotalChar;
            }
            return 0.01f;
            */
            // POSITIVE_APPROACH
            float r;

            if (_totalSeqs <= 0) return 0.01f;

            r = 1.0f * _seqCounters[POSITIVE_CAT] / _totalSeqs / model.TypicalPositiveRatio;

            // Multiply by a ratio of positive sequences per characters.
            // This would help in particular to distinguish close winners.
            // Indeed if you add a letter, you'd expect the positive sequence count
            // to increase as well. If it doesn't, it may mean that this new codepoint
            // may not have been a letter, but instead a symbol (or some other
            // character). This could make the difference between very closely related
            // charsets used for the same language.
            var ratio = _seqCounters[POSITIVE_CAT] + _seqCounters[PROBABLE_CAT] / 4.0f;
            r = r * ratio / _totalChar;

            // The more control characters (proportionnaly to the size of the text), the
            // less confident we become in the current charset.
            r = r * (_totalChar - _ctrlChar) / _totalChar;
            r = r * _freqChar / _totalChar;
            if (r >= 1.0f) r = 0.99f;

            return r;
        }

        public override void Reset()
        {
            state = ProbingState.Detecting;
            _lastOrder = 255;
            for (int i = 0; i < NUMBER_OF_SEQ_CAT; i++)
            {
                _seqCounters[i] = 0;
            }
            _totalSeqs = 0;
            _totalChar = 0;
            _freqChar = 0;
        }

        public override string GetCharsetName()
        {
            return _nameProber?.GetCharsetName() ?? model.CharsetName;
        }
    }
}
