﻿/* ***** BEGIN LICENSE BLOCK *****
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
* The implementation of this feature was originally done on https://gitlab.freedesktop.org/uchardet/uchardet/blob/master/src/LangModels/LangSlovakModel.cpp
* and adjusted to language specific support.
*/

namespace UtfUnknown.Core.Models.SingleByte.Slovak
{
    internal class Mac_Centraleurope_SlovakModel : SlovakModel
    {
        // Generated by BuildLangModel.py
        // On: 2016-09-21 13:33:10.331339

        // Character Mapping Table:
        // ILL: illegal character.
        // CTR: control character specific to the charset.
        // RET: carriage/return.
        // SYM: symbol (punctuation) that does not belong to word.
        // NUM: 0 - 9.

        // Other characters are ordered by probabilities
        // (0 is the most common character in the language).

        // Orders are generic to a language. So the codepoint with order X in
        // CHARSET1 maps to the same character as the codepoint with the same
        // order X in CHARSET2 for the same language.
        // As such, it is possible to get missing order. For instance the
        // ligature of 'o' and 'e' exists in ISO-8859-15 but not in ISO-8859-1
        // even though they are both used for French. Same for the euro sign.

        private static byte[] CHAR_TO_ORDER_MAP = {
          CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,RET,CTR,CTR,RET,CTR,CTR, /* 0X */
          CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR, /* 1X */
          SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM, /* 2X */
          NUM,NUM,NUM,NUM,NUM,NUM,NUM,NUM,NUM,NUM,SYM,SYM,SYM,SYM,SYM,SYM, /* 3X */
          SYM,  1, 20, 15, 11,  2, 29, 30, 17,  4, 18,  7, 10, 12,  3,  0, /* 4X */
           13, 40,  6,  8,  5, 14,  9, 37, 34, 19, 16,SYM,SYM,SYM,SYM,SYM, /* 5X */
          SYM,  1, 20, 15, 11,  2, 29, 30, 17,  4, 18,  7, 10, 12,  3,  0, /* 6X */
           13, 40,  6,  8,  5, 14,  9, 37, 34, 19, 16,SYM,SYM,SYM,SYM,CTR, /* 7X */
           38, 90, 91, 25, 92, 43, 46, 21, 93, 24, 38, 24, 47, 47, 25, 94, /* 8X */
           95, 39, 23, 39, 96, 97, 98, 35, 99, 32, 43,100, 27, 41, 41, 46, /* 9X */
          SYM,SYM,101,SYM,SYM,SYM,SYM, 58,SYM,SYM,SYM,102,SYM,SYM,103,104, /* AX */
          105, 57,SYM,SYM, 57,106,SYM,SYM, 49,107,108, 33, 33, 42, 42,109, /* BX */
          110, 52,SYM,SYM, 52, 36,SYM,SYM,SYM,SYM,SYM, 36, 50,111, 50, 53, /* CX */
          SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM, 53, 44, 44, 45,SYM,SYM, 45,112, /* DX */
          113, 28,SYM,SYM, 28,114,115, 21, 31, 31, 23, 26, 26,116, 35, 32, /* EX */
          117, 48, 27, 48, 60, 60,118,119, 22, 22,120, 61, 49, 61,121,SYM, /* FX */
        };
        /*X0  X1  X2  X3  X4  X5  X6  X7  X8  X9  XA  XB  XC  XD  XE  XF */

        public Mac_Centraleurope_SlovakModel() : base(CHAR_TO_ORDER_MAP, CodepageName.X_MAC_CE)
        {
        }
    }
}
