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

using UtfUnknown.Core.Probers.MultiByte;
using UtfUnknown.Core.Probers.MultiByte.Chinese;
using UtfUnknown.Core.Probers.MultiByte.Japanese;
using UtfUnknown.Core.Probers.MultiByte.Korean;

namespace UtfUnknown.Core.Probers
{
    /// <summary>
    /// Multi-byte charsets probers
    /// </summary>
    public class MBCSGroupProber : CharsetProber
    {
        private const int PROBERS_NUM = 8;

        private readonly CharsetProber[] _probers = new CharsetProber[PROBERS_NUM];
        private readonly bool[] _isActive = new bool[PROBERS_NUM];
        private int _bestGuess;
        private int _activeNum;

        public MBCSGroupProber()
        {
            _probers[0] = new UTF8Prober();
            _probers[1] = new SJISProber();
            _probers[2] = new EUCJPProber();
            _probers[3] = new GB18030Prober();
            _probers[4] = new EUCKRProber();
            _probers[5] = new CP949Prober();
            _probers[6] = new Big5Prober();
            _probers[7] = new EUCTWProber();

            Reset();
        }

        public override string GetCharsetName()
        {
            if (_bestGuess == -1)
            {
                GetConfidence();
                if (_bestGuess == -1) _bestGuess = 0;
            }

            return _probers[_bestGuess].GetCharsetName();
        }

        public override void Reset()
        {
            _activeNum = 0;

            for (int i = 0; i < _probers.Length; i++)
            {
                if (_probers[i] != null)
                {
                    _probers[i].Reset();
                    _isActive[i] = true;
                    ++_activeNum;
                }
                else
                {
                    _isActive[i] = false;
                }
            }

            _bestGuess = -1;
            state = ProbingState.Detecting;
        }

        public override ProbingState HandleData(byte[] buf, int offset, int len)
        {
            // do filtering to reduce load to probers
            byte[] highbyteBuf = new byte[len];
            int hptr = 0;
            //assume previous is not ascii, it will do no harm except add some noise
            bool isKeepNext = true;
            int max = offset + len;

            for (int i = offset; i < max; i++)
            {
                if ((buf[i] & 0x80) != 0)
                {
                    highbyteBuf[hptr++] = buf[i];
                    isKeepNext = true;
                }
                else
                {
                    //if previous is highbyte, keep this even it is a ASCII
                    if (!isKeepNext) continue;
                    highbyteBuf[hptr] = buf[i];
                    ++hptr;
                    isKeepNext = false;
                }
            }

            for (int i = 0; i < _probers.Length; i++)
            {
                if (!_isActive[i]) continue;

                var st = _probers[i].HandleData(highbyteBuf, 0, hptr);
                if (st == ProbingState.FoundIt)
                {
                    _bestGuess = i;
                    state = ProbingState.FoundIt;
                    break;
                }

                if (st != ProbingState.NotMe) continue;

                _isActive[i] = false;
                _activeNum--;
                if (_activeNum > 0) continue;

                state = ProbingState.NotMe;
                break;
            }

            return state;
        }

        public override float GetConfidence(StringBuilder status = null)
        {
            float bestConf = 0.0f;

            switch (state)
            {
                case ProbingState.FoundIt:
                    return 0.99f;

                case ProbingState.NotMe:
                    return 0.01f;

                default:
                    status?.AppendLine($"Get confidence:");
                    for (int i = 0; i < PROBERS_NUM; i++)
                    {
                        if (!_isActive[i]) continue;

                        var cf = _probers[i].GetConfidence();
                        if (cf <= bestConf) continue;

                        bestConf = cf;
                        _bestGuess = i;

                        status?.AppendLine(
                            $"-- new match found: confidence {bestConf}, index {_bestGuess}, charset {_probers[i].GetCharsetName()}.");
                    }
                    status?.AppendLine($"Get confidence done.");
                    break;
            }

            return bestConf;
        }

        public override string DumpStatus()
        {
            StringBuilder status = new StringBuilder();
            float cf = GetConfidence(status);
            status.AppendLine(" MBCS Group Prober --------begin status");
            for (int i = 0; i < PROBERS_NUM; i++)
            {
                if (_probers[i] == null) continue;

                var chName = _probers[i].GetCharsetName();
                if (!_isActive[i])
                {
                    status.AppendLine(
                        $" MBCS inactive: {chName} (i.e. confidence is too low).");
                }
                else
                {
                    var cfp = _probers[i].GetConfidence();
                    var dumpStatus = _probers[i].DumpStatus();
                    status.AppendLine($" MBCS {cfp}: [{chName}]");
                    status.AppendLine(dumpStatus);
                }
            }

            var bestChName = _probers[_bestGuess].GetCharsetName();
            status.AppendLine(
                $" MBCS Group found best match [{bestChName}] confidence {cf}.");
            return status.ToString();
        }
    }
}
