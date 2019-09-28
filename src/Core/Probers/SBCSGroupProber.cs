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

#region using languages

using UtfUnknown.Core.Models.SingleByte.Arabic;
using UtfUnknown.Core.Models.SingleByte.Bulgarian;
using UtfUnknown.Core.Models.SingleByte.Croatian;
using UtfUnknown.Core.Models.SingleByte.Czech;
using UtfUnknown.Core.Models.SingleByte.Danish;
using UtfUnknown.Core.Models.SingleByte.Esperanto;
using UtfUnknown.Core.Models.SingleByte.Estonian;
using UtfUnknown.Core.Models.SingleByte.Finnish;
using UtfUnknown.Core.Models.SingleByte.French;
using UtfUnknown.Core.Models.SingleByte.German;
using UtfUnknown.Core.Models.SingleByte.Greek;
using UtfUnknown.Core.Models.SingleByte.Hebrew;
using UtfUnknown.Core.Models.SingleByte.Hungarian;
using UtfUnknown.Core.Models.SingleByte.Irish;
using UtfUnknown.Core.Models.SingleByte.Italian;
using UtfUnknown.Core.Models.SingleByte.Latvian;
using UtfUnknown.Core.Models.SingleByte.Lithuanian;
using UtfUnknown.Core.Models.SingleByte.Maltese;
using UtfUnknown.Core.Models.SingleByte.Polish;
using UtfUnknown.Core.Models.SingleByte.Portuguese;
using UtfUnknown.Core.Models.SingleByte.Romanian;
using UtfUnknown.Core.Models.SingleByte.Russian;
using UtfUnknown.Core.Models.SingleByte.Slovak;
using UtfUnknown.Core.Models.SingleByte.Slovene;
using UtfUnknown.Core.Models.SingleByte.Spanish;
using UtfUnknown.Core.Models.SingleByte.Swedish;
using UtfUnknown.Core.Models.SingleByte.Thai;
using UtfUnknown.Core.Models.SingleByte.Turkish;
using UtfUnknown.Core.Models.SingleByte.Vietnamese;

#endregion using languages

namespace UtfUnknown.Core.Probers
{
    public class SBCSGroupProber : CharsetProber
    {
        private const int PROBERS_NUM = 100;
        private readonly CharsetProber[] _probers = new CharsetProber[PROBERS_NUM];
        private readonly bool[] _isActive = new bool[PROBERS_NUM];
        private int _bestGuess;
        private int _activeNum;

        public SBCSGroupProber()
        {
            // Russian
            _probers[0] = new SingleByteCharSetProber(new Windows_1251_RussianModel());
            _probers[1] = new SingleByteCharSetProber(new Koi8r_Model());
            _probers[2] = new SingleByteCharSetProber(new Iso_8859_5_RussianModel());
            _probers[3] = new SingleByteCharSetProber(new X_Mac_Cyrillic_RussianModel());
            _probers[4] = new SingleByteCharSetProber(new Ibm866_RussianModel());
            _probers[5] = new SingleByteCharSetProber(new Ibm855_RussianModel());

            // Greek
            _probers[6] = new SingleByteCharSetProber(new Iso_8859_7_GreekModel());
            _probers[7] = new SingleByteCharSetProber(new Windows_1253_GreekModel());

            // Bulgarian
            _probers[8] = new SingleByteCharSetProber(new Iso_8859_5_BulgarianModel());
            _probers[9] = new SingleByteCharSetProber(new Windows_1251_BulgarianModel());

            // Hebrew
            HebrewProber hebprober = new HebrewProber();
            _probers[10] = hebprober;
            // Logical  
            _probers[11] = new SingleByteCharSetProber(new Windows_1255_HebrewModel(), false, hebprober);
            // Visual
            _probers[12] = new SingleByteCharSetProber(new Windows_1255_HebrewModel(), true, hebprober);
            hebprober.SetModelProbers(_probers[11], _probers[12]);

            // Thai
            _probers[13] = new SingleByteCharSetProber(new Tis_620_ThaiModel());
            _probers[14] = new SingleByteCharSetProber(new Iso_8859_11_ThaiModel());

            // French
            _probers[15] = new SingleByteCharSetProber(new Iso_8859_1_FrenchModel());
            _probers[16] = new SingleByteCharSetProber(new Iso_8859_15_FrenchModel());
            _probers[17] = new SingleByteCharSetProber(new Windows_1252_FrenchModel());

            // Spanish
            _probers[18] = new SingleByteCharSetProber(new Iso_8859_1_SpanishModel());
            _probers[19] = new SingleByteCharSetProber(new Iso_8859_15_SpanishModel());
            _probers[20] = new SingleByteCharSetProber(new Windows_1252_SpanishModel());

            // Is the following still valid?
            // disable latin2 before latin1 is available, otherwise all latin1 
            // will be detected as latin2 because of their similarity
            // Hungarian
            _probers[21] = new SingleByteCharSetProber(new Iso_8859_2_HungarianModel());
            _probers[22] = new SingleByteCharSetProber(new Windows_1250_HungarianModel());

            // German
            _probers[23] = new SingleByteCharSetProber(new Iso_8859_1_GermanModel());
            _probers[24] = new SingleByteCharSetProber(new Windows_1252_GermanModel());

            // Esperanto
            _probers[25] = new SingleByteCharSetProber(new Iso_8859_3_EsperantoModel());

            // Turkish
            _probers[26] = new SingleByteCharSetProber(new Iso_8859_3_TurkishModel());
            _probers[27] = new SingleByteCharSetProber(new Iso_8859_9_TurkishModel());

            // Arabic
            _probers[28] = new SingleByteCharSetProber(new Iso_8859_6_ArabicModel());
            _probers[29] = new SingleByteCharSetProber(new Windows_1256_ArabicModel());

            // Vietnamese
            _probers[30] = new SingleByteCharSetProber(new Viscii_VietnameseModel());
            _probers[31] = new SingleByteCharSetProber(new Windows_1258_VietnameseModel());

            // Danish
            _probers[32] = new SingleByteCharSetProber(new Iso_8859_15_DanishModel());
            _probers[33] = new SingleByteCharSetProber(new Iso_8859_1_DanishModel());
            _probers[34] = new SingleByteCharSetProber(new Windows_1252_DanishModel());

            // Lithuanian
            _probers[35] = new SingleByteCharSetProber(new Iso_8859_13_LithuanianModel());
            _probers[36] = new SingleByteCharSetProber(new Iso_8859_10_LithuanianModel());
            _probers[37] = new SingleByteCharSetProber(new Iso_8859_4_LithuanianModel());

            // Latvian
            _probers[38] = new SingleByteCharSetProber(new Iso_8859_13_LatvianModel());
            _probers[39] = new SingleByteCharSetProber(new Iso_8859_10_LatvianModel());
            _probers[40] = new SingleByteCharSetProber(new Iso_8859_4_LatvianModel());

            // Portuguese
            _probers[41] = new SingleByteCharSetProber(new Iso_8859_1_PortugueseModel());
            _probers[42] = new SingleByteCharSetProber(new Iso_8859_9_PortugueseModel());
            _probers[43] = new SingleByteCharSetProber(new Iso_8859_15_PortugueseModel());
            _probers[44] = new SingleByteCharSetProber(new Windows_1252_PortugueseModel());

            // Maltese
            _probers[45] = new SingleByteCharSetProber(new Iso_8859_3_MalteseModel());

            // Czech
            _probers[46] = new SingleByteCharSetProber(new Windows_1250_CzechModel());
            _probers[47] = new SingleByteCharSetProber(new Iso_8859_2_CzechModel());
            _probers[48] = new SingleByteCharSetProber(new Mac_Centraleurope_CzechModel());
            _probers[49] = new SingleByteCharSetProber(new Ibm852_CzechModel());

            // Slovak
            _probers[50] = new SingleByteCharSetProber(new Windows_1250_SlovakModel());
            _probers[51] = new SingleByteCharSetProber(new Iso_8859_2_SlovakModel());
            _probers[52] = new SingleByteCharSetProber(new Mac_Centraleurope_SlovakModel());
            _probers[53] = new SingleByteCharSetProber(new Ibm852_SlovakModel());

            // Polish
            _probers[54] = new SingleByteCharSetProber(new Windows_1250_PolishModel());
            _probers[55] = new SingleByteCharSetProber(new Iso_8859_2_PolishModel());
            _probers[56] = new SingleByteCharSetProber(new Iso_8859_13_PolishModel());
            _probers[57] = new SingleByteCharSetProber(new Iso_8859_16_PolishModel());
            _probers[58] = new SingleByteCharSetProber(new Mac_Centraleurope_PolishModel());
            _probers[59] = new SingleByteCharSetProber(new Ibm852_PolishModel());

            // Finnish
            _probers[60] = new SingleByteCharSetProber(new Iso_8859_1_FinnishModel());
            _probers[61] = new SingleByteCharSetProber(new Iso_8859_4_FinnishModel());
            _probers[62] = new SingleByteCharSetProber(new Iso_8859_9_FinnishModel());
            _probers[63] = new SingleByteCharSetProber(new Iso_8859_13_FinnishModel());
            _probers[64] = new SingleByteCharSetProber(new Iso_8859_15_FinnishModel());
            _probers[65] = new SingleByteCharSetProber(new Windows_1252_FinnishModel());

            // Italian
            _probers[66] = new SingleByteCharSetProber(new Iso_8859_1_ItalianModel());
            _probers[67] = new SingleByteCharSetProber(new Iso_8859_3_ItalianModel());
            _probers[68] = new SingleByteCharSetProber(new Iso_8859_9_ItalianModel());
            _probers[69] = new SingleByteCharSetProber(new Iso_8859_15_ItalianModel());
            _probers[70] = new SingleByteCharSetProber(new Windows_1252_ItalianModel());

            // Croatian
            _probers[71] = new SingleByteCharSetProber(new Windows_1250_CroatianModel());
            _probers[72] = new SingleByteCharSetProber(new Iso_8859_2_CroatianModel());
            _probers[73] = new SingleByteCharSetProber(new Iso_8859_13_CroatianModel());
            _probers[74] = new SingleByteCharSetProber(new Iso_8859_16_CroatianModel());
            _probers[75] = new SingleByteCharSetProber(new Mac_Centraleurope_CroatianModel());
            _probers[76] = new SingleByteCharSetProber(new Ibm852_CroatianModel());

            // Estonian
            _probers[77] = new SingleByteCharSetProber(new Windows_1252_EstonianModel());
            _probers[78] = new SingleByteCharSetProber(new Windows_1257_EstonianModel());
            _probers[79] = new SingleByteCharSetProber(new Iso_8859_4_EstonianModel());
            _probers[80] = new SingleByteCharSetProber(new Iso_8859_13_EstonianModel());
            _probers[81] = new SingleByteCharSetProber(new Iso_8859_15_EstonianModel());

            // Irish
            _probers[82] = new SingleByteCharSetProber(new Iso_8859_1_IrishModel());
            _probers[83] = new SingleByteCharSetProber(new Iso_8859_9_IrishModel());
            _probers[84] = new SingleByteCharSetProber(new Iso_8859_15_IrishModel());
            _probers[85] = new SingleByteCharSetProber(new Windows_1252_IrishModel());

            // Romanian
            _probers[86] = new SingleByteCharSetProber(new Windows_1250_RomanianModel());
            _probers[87] = new SingleByteCharSetProber(new Iso_8859_2_RomanianModel());
            _probers[88] = new SingleByteCharSetProber(new Iso_8859_16_RomanianModel());
            _probers[89] = new SingleByteCharSetProber(new Ibm852_RomanianModel());

            // Slovene
            _probers[90] = new SingleByteCharSetProber(new Windows_1250_SloveneModel());
            _probers[91] = new SingleByteCharSetProber(new Iso_8859_2_SloveneModel());
            _probers[92] = new SingleByteCharSetProber(new Iso_8859_16_SloveneModel());
            _probers[93] = new SingleByteCharSetProber(new Mac_Centraleurope_SloveneModel());
            _probers[94] = new SingleByteCharSetProber(new Ibm852_SloveneModel());

            // Swedish
            _probers[95] = new SingleByteCharSetProber(new Iso_8859_1_SwedishModel());
            _probers[96] = new SingleByteCharSetProber(new Iso_8859_4_SwedishModel());
            _probers[97] = new SingleByteCharSetProber(new Iso_8859_9_SwedishModel());
            _probers[98] = new SingleByteCharSetProber(new Iso_8859_15_SwedishModel());
            _probers[99] = new SingleByteCharSetProber(new Windows_1252_SwedishModel());

            Reset();
        }

        public override ProbingState HandleData(byte[] buf, int offset, int len)
        {
            // apply filter to original buffer, and we got new buffer back
            // depend on what script it is, we will feed them the new buffer 
            // we got after applying proper filter
            // this is done without any consideration to KeepEnglishLetters
            // of each prober since as of now, there are no probers here which
            // recognize languages with English characters.

            byte[] newBuf = FilterWithoutEnglishLetters(buf, offset, len);
            if (newBuf.Length == 0) return state;

            for (int i = 0; i < PROBERS_NUM; i++)
            {
                if (!_isActive[i]) continue;

                ProbingState st = _probers[i].HandleData(newBuf, 0, newBuf.Length);
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
                    return 0.99f; //sure yes

                case ProbingState.NotMe:
                    return 0.01f;  //sure no

                default:

                    status?.AppendLine($"Get confidence:");
                    float cf;
                    for (int i = 0; i < PROBERS_NUM; i++)
                    {
                        if (!_isActive[i]) continue;

                        cf = _probers[i].GetConfidence();
                        if (cf <= bestConf) continue;

                        bestConf = cf;
                        _bestGuess = i;

                        var chName = _probers[i].GetCharsetName();
                        status?.AppendLine(
                            $"-- new match found: confidence {bestConf}, index {_bestGuess}, charset {chName}.");
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
            status.AppendLine(" SBCS Group Prober --------begin status");
            for (int i = 0; i < PROBERS_NUM; i++)
            {
                if (_probers[i] == null) continue;

                var chName = _probers[i].GetCharsetName();
                if (!_isActive[i])
                {
                    status.AppendLine(
                        $" SBCS inactive: [{chName}] (i.e. confidence is too low).");
                }
                else
                {
                    var cfp = _probers[i].GetConfidence();
                    var dumpStatus = _probers[i].DumpStatus();
                    status.AppendLine($" SBCS {cfp}: [{chName}]");
                    status.AppendLine(dumpStatus);
                }
            }

            var bestChName = _probers[_bestGuess].GetCharsetName();
            status.AppendLine(
                $" SBCS Group found best match [{bestChName}] confidence {cf}.");

            return status.ToString();
        }

        public override void Reset()
        {
            _activeNum = 0;

            for (int i = 0; i < PROBERS_NUM; i++)
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

        public override string GetCharsetName()
        {
            //if we have no answer yet
            if (_bestGuess == -1)
            {
                GetConfidence();
                //no charset seems positive
                if (_bestGuess == -1)
                    _bestGuess = 0;
            }
            return _probers[_bestGuess].GetCharsetName();
        }
    }
}
