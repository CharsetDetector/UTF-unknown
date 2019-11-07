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
 * The Original Code is Mozilla Communicator client code.
 *
 * The Initial Developer of the Original Code is
 * Netscape Communications Corporation.
 * Portions created by the Initial Developer are Copyright (C) 1998
 * the Initial Developer. All Rights Reserved.
 *
 * Contributor(s):
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
* The implementation of this feature was originally done on https://gitlab.freedesktop.org/uchardet/uchardet/blob/master/src/LangModels/LangRussianModel.cpp
* and adjusted to language specific support.
*/

namespace UtfUnknown.Core.Models.SingleByte.Russian
{
    public class Iso_8859_5_RussianModel : RussianModel
    {
        private readonly static byte[] CHAR_TO_ORDER_MAP = {
            CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,RET,CTR,CTR,RET,CTR,CTR, /* 0X */
            CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR, /* 1X */
            SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM, /* 2X */
            NUM,NUM,NUM,NUM,NUM,NUM,NUM,NUM,NUM,NUM,SYM,SYM,SYM,SYM,SYM,SYM, /* 3X */
            SYM,142,143,144,145,146,147,148,149,150,151,152, 74,153, 75,154, /* 4X */
            155,156,157,158,159,160,161,162,163,164,165,SYM,SYM,SYM,SYM,SYM, /* 5X */
            SYM, 71,172, 66,173, 65,174, 76,175, 64,176,177, 77, 72,178, 69, /* 6X */
             67,179, 78, 73,180,181, 79,182,183,184,185,SYM,SYM,SYM,SYM,SYM, /* 7X */
            191,192,193,194,195,196,197,198,199,200,201,202,203,204,205,206, /* 8X */
            207,208,209,210,211,212,213,214,215,216,217,218,219,220,221,222, /* 9X */
            223,224,225,226,227,228,229,230,231,232,233,234,235,236,237,238, /* AX */
             37, 44, 33, 46, 41, 48, 56, 51, 42, 60, 36, 49, 38, 31, 34, 35, /* BX */
             45, 32, 40, 52, 53, 55, 58, 50, 57, 63, 70, 62, 61, 47, 59, 43, /* CX */
              3, 21, 10, 19, 13,  2, 24, 20,  4, 23, 11,  8, 12,  5,  1, 15, /* DX */
              9,  7,  6, 14, 39, 26, 28, 22, 25, 29, 54, 18, 17, 30, 27, 16, /* EX */
            239, 68,240,241,242,243,244,245,246,247,248,249,250,251,NUM,CTR, /* FX */
        };
        /*X0  X1  X2  X3  X4  X5  X6  X7  X8  X9  XA  XB  XC  XD  XE  XF */

        public Iso_8859_5_RussianModel() : base(CHAR_TO_ORDER_MAP, CodepageName.ISO_8859_5)
        {
        }
    }
}