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

/*
* The following part was imported from https://gitlab.freedesktop.org/uchardet/uchardet
* The implementation of this feature was originally done on https://gitlab.freedesktop.org/uchardet/uchardet/blob/master/src/LangModels/LangHebrewModel.cpp
* and adjusted to language specific support.
*/

namespace UtfUnknown.Core.Models.SingleByte.Hebrew
{
    public class Windows_1255_HebrewModel : HebrewModel
    {
        // 255: Control characters that usually does not exist in any text
        // 254: Carriage/Return
        // 253: symbol (punctuation) that does not belong to word
        // 252: 0 - 9

        // Windows-1255 language model
        // Character Mapping Table:        
        private readonly static byte[]CHAR_TO_ORDER_MAP = {
            CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,RET,CTR,CTR,RET,CTR,CTR, /* 0X */
            CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR, /* 1X */
            SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM, /* 2X */
            NUM,NUM,NUM,NUM,NUM,NUM,NUM,NUM,NUM,NUM,SYM,SYM,SYM,SYM,SYM,SYM, /* 3X */
            SYM, 69, 91, 79, 80, 92, 89, 97, 90, 68,111,112, 82, 73, 95, 85, /* 4X */
             78,121, 86, 71, 67,102,107, 84,114,103,115,SYM,SYM,SYM,SYM,SYM, /* 5X */
            SYM, 50, 74, 60, 61, 42, 76, 70, 64, 53,105, 93, 56, 65, 54, 49, /* 6X */
             66,110, 51, 43, 44, 63, 81, 77, 98, 75,108,SYM,SYM,SYM,SYM,SYM, /* 7X */
            124,ILL,203,204,205, 40, 58,206,207,208,ILL,210,ILL,ILL,ILL,ILL, /* 8X */
            ILL, 83, 52, 47, 46, 72, 32, 94,216,113,ILL,109,ILL,ILL,ILL,ILL, /* 9X */
             34,116,222,118,100,223,224,117,119,104,125,225,226, 87, 99,227, /* AX */
            106,122,123,228, 55,229,230,101,231,232,120,233, 48, 39, 57,234, /* BX */
             30, 59, 41, 88, 33, 37, 36, 31, 29, 35,235, 62, 28,236,126,237, /* CX */
            238, 38, 45,239,240,241,242,243,127,ILL,ILL,ILL,ILL,ILL,ILL,ILL, /* DX */
              9,  8, 20, 16,  3,  2, 24, 14, 22,  1, 25, 15,  4, 11,  6, 23, /* EX */
             12, 19, 13, 26, 18, 27, 21, 17,  7, 10,  5,ILL,ILL,128, 96,ILL, /* FX */
        };
        /*X0  X1  X2  X3  X4  X5  X6  X7  X8  X9  XA  XB  XC  XD  XE  XF */

        public Windows_1255_HebrewModel() : base(CHAR_TO_ORDER_MAP, CodepageName.WINDOWS_1255)
        {
        }
    }
}