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

namespace UtfUnknown.Core.Analyzers
{
    /// <summary>
    /// Base class for the Character Distribution Method, used for 
    /// the CJK encodings
    /// </summary>
    public abstract class CharDistributionAnalyser
    {
        protected const float SURE_YES = 0.99f;
        protected const float SURE_NO = 0.01f;
        protected const int MINIMUM_DATA_THRESHOLD = 4;
        protected const int ENOUGH_DATA_THRESHOLD = 1024;

        // If this flag is set to true, detection is done and conclusion has been made
        protected bool done;

        // The number of characters whose frequency order is less than 512
        protected int freqChars;

        // Total character encounted.
        protected int totalChars;

        // Mapping table to get frequency order from char order (get from GetOrder())
        protected int[] charToFreqOrder;

        // This constant value varies from language to language. It is used in calculating confidence. 
        protected float typicalDistributionRatio;

        public CharDistributionAnalyser()
        {
            Reset();
        }

        /// <summary>
        /// Feed a block of data and do distribution analysis
        /// </summary>
        /// <remarks>
        /// we do not handle character base on its original encoding string, but 
        /// convert this encoding string to a number, here called order.
        /// This allow multiple encoding of a language to share one frequency table
        /// </remarks>
        /// <param name="buf">A <see cref="System.Byte"/></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public abstract int GetOrder(byte[] buf, int offset);

        /// <summary>
        /// Feed a character with known length 
        /// </summary>
        /// <param name="buf">A <see cref="System.Byte"/></param>
        /// <param name="offset">buf offset</param>
        /// <param name="charLen">1 of 2 char length?</param>
        public void HandleOneChar(byte[] buf, int offset, int charLen)
        {
            //we only care about 2-bytes character in our distribution analysis
            int order = (charLen == 2) ? GetOrder(buf, offset) : -1;
            if (order >= 0)
            {
                totalChars++;
                if (order < charToFreqOrder.Length)
                { // order is valid
                    if (512 > charToFreqOrder[order])
                        freqChars++;
                }
            }
        }

        public virtual void Reset()
        {
            done = false;
            totalChars = 0;
            freqChars = 0;
        }

        /// <summary>
        /// return confidence base on received data
        /// </summary>
        /// <returns></returns>
        public virtual float GetConfidence()
        {
            //if we didn't receive any character in our consideration range, or the
            // number of frequent characters is below the minimum threshold, return
            // negative answer
            if (totalChars <= 0 || freqChars <= MINIMUM_DATA_THRESHOLD)
                return SURE_NO;
            if (totalChars != freqChars)
            {
                float r = freqChars / ((totalChars - freqChars) * typicalDistributionRatio);
                if (r < SURE_YES)
                    return r;
            }
            //normalize confidence, (we don't want to be 100% sure)
            return SURE_YES;
        }

        //It is not necessary to receive all data to draw conclusion. For charset detection,
        // certain amount of data is enough
        public bool GotEnoughData()
        {
            return totalChars > ENOUGH_DATA_THRESHOLD;
        }
    }
}
