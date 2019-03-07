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

using System;
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
        private CharsetProber[] probers = new CharsetProber[PROBERS_NUM];
        private bool[] isActive = new bool[PROBERS_NUM];
        private int bestGuess;
        private int activeNum;

        public SBCSGroupProber()
        {
            // Russian
            probers[0] = new SingleByteCharSetProber(new Windows_1251_RussianModel());
            probers[1] = new SingleByteCharSetProber(new Koi8r_Model());
            probers[2] = new SingleByteCharSetProber(new Iso_8859_5_RussianModel());
            probers[3] = new SingleByteCharSetProber(new X_Mac_Cyrillic_RussianModel());
            probers[4] = new SingleByteCharSetProber(new Ibm866_RussianModel());
            probers[5] = new SingleByteCharSetProber(new Ibm855_RussianModel());

            // Greek
            probers[6] = new SingleByteCharSetProber(new Iso_8859_7_GreekModel());
            probers[7] = new SingleByteCharSetProber(new Windows_1253_GreekModel());

            // Bulgarian
            probers[8] = new SingleByteCharSetProber(new Iso_8859_5_BulgarianModel());
            probers[9] = new SingleByteCharSetProber(new Windows_1251_BulgarianModel());

            // Hebrew
            HebrewProber hebprober = new HebrewProber();
            probers[10] = hebprober;
            // Logical  
            probers[11] = new SingleByteCharSetProber(new Windows_1255_HebrewModel(), false, hebprober);
            // Visual
            probers[12] = new SingleByteCharSetProber(new Windows_1255_HebrewModel(), true, hebprober);
            hebprober.SetModelProbers(probers[11], probers[12]);

            // Thai
            probers[13] = new SingleByteCharSetProber(new Tis_620_ThaiModel());
            probers[14] = new SingleByteCharSetProber(new Iso_8859_11_ThaiModel());

            // French
            probers[15] = new SingleByteCharSetProber(new Iso_8859_1_FrenchModel());
            probers[16] = new SingleByteCharSetProber(new Iso_8859_15_FrenchModel());
            probers[17] = new SingleByteCharSetProber(new Windows_1252_FrenchModel());

            // Spanish
            probers[18] = new SingleByteCharSetProber(new Iso_8859_1_SpanishModel());
            probers[19] = new SingleByteCharSetProber(new Iso_8859_15_SpanishModel());
            probers[20] = new SingleByteCharSetProber(new Windows_1252_SpanishModel());

            // Is the following still valid?
            // disable latin2 before latin1 is available, otherwise all latin1 
            // will be detected as latin2 because of their similarity
            // Hungarian
            probers[21] = new SingleByteCharSetProber(new Iso_8859_2_HungarianModel());
            probers[22] = new SingleByteCharSetProber(new Windows_1250_HungarianModel());

            // German
            probers[23] = new SingleByteCharSetProber(new Iso_8859_1_GermanModel());
            probers[24] = new SingleByteCharSetProber(new Windows_1252_GermanModel());

            // Esperanto
            probers[25] = new SingleByteCharSetProber(new Iso_8859_3_EsperantoModel());

            // Turkish
            probers[26] = new SingleByteCharSetProber(new Iso_8859_3_TurkishModel());
            probers[27] = new SingleByteCharSetProber(new Iso_8859_9_TurkishModel());

            // Arabic
            probers[28] = new SingleByteCharSetProber(new Iso_8859_6_ArabicModel());
            probers[29] = new SingleByteCharSetProber(new Windows_1256_ArabicModel());

            // Vietnamese
            probers[30] = new SingleByteCharSetProber(new Viscii_VietnameseModel());
            probers[31] = new SingleByteCharSetProber(new Windows_1258_VietnameseModel());

            // Danish
            probers[32] = new SingleByteCharSetProber(new Iso_8859_15_DanishModel());
            probers[33] = new SingleByteCharSetProber(new Iso_8859_1_DanishModel());
            probers[34] = new SingleByteCharSetProber(new Windows_1252_DanishModel());

            // Lithuanian
            probers[35] = new SingleByteCharSetProber(new Iso_8859_13_LithuanianModel());
            probers[36] = new SingleByteCharSetProber(new Iso_8859_10_LithuanianModel());
            probers[37] = new SingleByteCharSetProber(new Iso_8859_4_LithuanianModel());

            // Latvian
            probers[38] = new SingleByteCharSetProber(new Iso_8859_13_LatvianModel());
            probers[39] = new SingleByteCharSetProber(new Iso_8859_10_LatvianModel());
            probers[40] = new SingleByteCharSetProber(new Iso_8859_4_LatvianModel());

            // Portuguese
            probers[41] = new SingleByteCharSetProber(new Iso_8859_1_PortugueseModel());
            probers[42] = new SingleByteCharSetProber(new Iso_8859_9_PortugueseModel());
            probers[43] = new SingleByteCharSetProber(new Iso_8859_15_PortugueseModel());
            probers[44] = new SingleByteCharSetProber(new Windows_1252_PortugueseModel());

            // Maltese
            probers[45] = new SingleByteCharSetProber(new Iso_8859_3_MalteseModel());

            // Czech
            probers[46] = new SingleByteCharSetProber(new Windows_1250_CzechModel());
            probers[47] = new SingleByteCharSetProber(new Iso_8859_2_CzechModel());
            probers[48] = new SingleByteCharSetProber(new Mac_Centraleurope_CzechModel());
            probers[49] = new SingleByteCharSetProber(new Ibm852_CzechModel());

            // Slovak
            probers[50] = new SingleByteCharSetProber(new Windows_1250_SlovakModel());
            probers[51] = new SingleByteCharSetProber(new Iso_8859_2_SlovakModel());
            probers[52] = new SingleByteCharSetProber(new Mac_Centraleurope_SlovakModel());
            probers[53] = new SingleByteCharSetProber(new Ibm852_SlovakModel());

            // Polish
            probers[54] = new SingleByteCharSetProber(new Windows_1250_PolishModel());
            probers[55] = new SingleByteCharSetProber(new Iso_8859_2_PolishModel());
            probers[56] = new SingleByteCharSetProber(new Iso_8859_13_PolishModel());
            probers[57] = new SingleByteCharSetProber(new Iso_8859_16_PolishModel());
            probers[58] = new SingleByteCharSetProber(new Mac_Centraleurope_PolishModel());
            probers[59] = new SingleByteCharSetProber(new Ibm852_PolishModel());

            // Finnish
            probers[60] = new SingleByteCharSetProber(new Iso_8859_1_FinnishModel());
            probers[61] = new SingleByteCharSetProber(new Iso_8859_4_FinnishModel());
            probers[62] = new SingleByteCharSetProber(new Iso_8859_9_FinnishModel());
            probers[63] = new SingleByteCharSetProber(new Iso_8859_13_FinnishModel());
            probers[64] = new SingleByteCharSetProber(new Iso_8859_15_FinnishModel());
            probers[65] = new SingleByteCharSetProber(new Windows_1252_FinnishModel());

            // Italian
            probers[66] = new SingleByteCharSetProber(new Iso_8859_1_ItalianModel());
            probers[67] = new SingleByteCharSetProber(new Iso_8859_3_ItalianModel());
            probers[68] = new SingleByteCharSetProber(new Iso_8859_9_ItalianModel());
            probers[69] = new SingleByteCharSetProber(new Iso_8859_15_ItalianModel());
            probers[70] = new SingleByteCharSetProber(new Windows_1252_ItalianModel());

            // Croatian
            probers[71] = new SingleByteCharSetProber(new Windows_1250_CroatianModel());
            probers[72] = new SingleByteCharSetProber(new Iso_8859_2_CroatianModel());
            probers[73] = new SingleByteCharSetProber(new Iso_8859_13_CroatianModel());
            probers[74] = new SingleByteCharSetProber(new Iso_8859_16_CroatianModel());
            probers[75] = new SingleByteCharSetProber(new Mac_Centraleurope_CroatianModel());
            probers[76] = new SingleByteCharSetProber(new Ibm852_CroatianModel());

            // Estonian
            probers[77] = new SingleByteCharSetProber(new Windows_1252_EstonianModel());
            probers[78] = new SingleByteCharSetProber(new Windows_1257_EstonianModel());
            probers[79] = new SingleByteCharSetProber(new Iso_8859_4_EstonianModel());
            probers[80] = new SingleByteCharSetProber(new Iso_8859_13_EstonianModel());
            probers[81] = new SingleByteCharSetProber(new Iso_8859_15_EstonianModel());

            // Irish
            probers[82] = new SingleByteCharSetProber(new Iso_8859_1_IrishModel());
            probers[83] = new SingleByteCharSetProber(new Iso_8859_9_IrishModel());
            probers[84] = new SingleByteCharSetProber(new Iso_8859_15_IrishModel());
            probers[85] = new SingleByteCharSetProber(new Windows_1252_IrishModel());

            // Romanian
            probers[86] = new SingleByteCharSetProber(new Windows_1250_RomanianModel());
            probers[87] = new SingleByteCharSetProber(new Iso_8859_2_RomanianModel());
            probers[88] = new SingleByteCharSetProber(new Iso_8859_16_RomanianModel());
            probers[89] = new SingleByteCharSetProber(new Ibm852_RomanianModel());

            // Slovene
            probers[90] = new SingleByteCharSetProber(new Windows_1250_SloveneModel());
            probers[91] = new SingleByteCharSetProber(new Iso_8859_2_SloveneModel());
            probers[92] = new SingleByteCharSetProber(new Iso_8859_16_SloveneModel());
            probers[93] = new SingleByteCharSetProber(new Mac_Centraleurope_SloveneModel());
            probers[94] = new SingleByteCharSetProber(new Ibm852_SloveneModel());

            // Swedish
            probers[95] = new SingleByteCharSetProber(new Iso_8859_1_SwedishModel());
            probers[96] = new SingleByteCharSetProber(new Iso_8859_4_SwedishModel());
            probers[97] = new SingleByteCharSetProber(new Iso_8859_9_SwedishModel());
            probers[98] = new SingleByteCharSetProber(new Iso_8859_15_SwedishModel());
            probers[99] = new SingleByteCharSetProber(new Windows_1252_SwedishModel());

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

            if (newBuf.Length == 0)
                return state; // Nothing to see here, move on.

            for (int i = 0; i < PROBERS_NUM; i++)
            {
                if (isActive[i])
                {
                    ProbingState st = probers[i].HandleData(newBuf, 0, newBuf.Length);

                    if (st == ProbingState.FoundIt)
                    {
                        bestGuess = i;
                        state = ProbingState.FoundIt;
                        break;
                    }
                    else if (st == ProbingState.NotMe)
                    {
                        isActive[i] = false;
                        activeNum--;
                        if (activeNum <= 0)
                        {
                            state = ProbingState.NotMe;
                            break;
                        }
                    }
                }
            }

            return state;
        }

        public override float GetConfidence(StringBuilder status = null)
        {
            float bestConf = 0.0f, cf;

            switch (state)
            {
                case ProbingState.FoundIt:
                    return 0.99f; //sure yes

                case ProbingState.NotMe:
                    return 0.01f;  //sure no

                default:

                    if (status != null)
                    {
                        status.AppendLine($"Get confidence:");
                    }

                    for (int i = 0; i < PROBERS_NUM; i++)
                    {
                        if (isActive[i])
                        {
                            cf = probers[i].GetConfidence();
                            if (bestConf < cf)
                            {
                                bestConf = cf;
                                bestGuess = i;

                                if (status != null)
                                {
                                    status.AppendLine($"-- new match found: confidence {bestConf}, index {bestGuess}, charset {probers[i].GetCharsetName()}.");
                                }
                            }
                        }
                    }

                    if (status != null)
                    {
                        status.AppendLine($"Get confidence done.");
                    }

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
                if (probers[i] != null)
                {
                    if (!isActive[i])
                    {
                        status.AppendLine($" SBCS inactive: [{probers[i].GetCharsetName()}] (i.e. confidence is too low).");
                    }
                    else
                    {
                        var cfp = probers[i].GetConfidence();

                        status.AppendLine($" SBCS {cfp}: [{probers[i].GetCharsetName()}]");

                        status.AppendLine(probers[i].DumpStatus());
                    }
                }
            }

            status.AppendLine($" SBCS Group found best match [{probers[bestGuess].GetCharsetName()}] confidence {cf}.");

            return status.ToString();
        }

        public override void Reset()
        {
            activeNum = 0;

            for (int i = 0; i < PROBERS_NUM; i++)
            {
                if (probers[i] != null)
                {
                    probers[i].Reset();
                    isActive[i] = true;
                    ++activeNum;
                }
                else
                {
                    isActive[i] = false;
                }
            }

            bestGuess = -1;

            state = ProbingState.Detecting;
        }

        public override string GetCharsetName()
        {
            //if we have no answer yet
            if (bestGuess == -1)
            {
                GetConfidence();
                //no charset seems positive
                if (bestGuess == -1)
                    bestGuess = 0;
            }
            return probers[bestGuess].GetCharsetName();
        }
    }
}
