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
* The implementation of this feature was originally done on https://gitlab.freedesktop.org/uchardet/uchardet/blob/master/src/LangModels/LangIrishModel.cpp
* and adjusted to language specific support.
*/

namespace UtfUnknown.Core
{
    public class Windows_1252_IrishModel : IrishModel
    {
        // Generated by BuildLangModel.py
        // On: 2016-09-27 00:33:40.158624

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
          SYM,  0, 16,  8, 11,  5, 19, 12,  3,  1, 27, 25,  9, 13,  2, 10, /* 4X */
           21, 30,  4,  6,  7, 15, 23, 26, 29, 24, 28,SYM,SYM,SYM,SYM,SYM, /* 5X */
          SYM,  0, 16,  8, 11,  5, 19, 12,  3,  1, 27, 25,  9, 13,  2, 10, /* 6X */
           21, 30,  4,  6,  7, 15, 23, 26, 29, 24, 28,SYM,SYM,SYM,SYM,CTR, /* 7X */
          SYM,ILL,SYM, 75,SYM,SYM,SYM,SYM,SYM,SYM, 34,SYM, 76,ILL, 77,ILL, /* 8X */
          ILL,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM, 34,SYM, 78,ILL, 79, 80, /* 9X */
          SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM, /* AX */
          SYM,SYM,SYM,SYM,SYM, 81,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM, /* BX */
           82, 14, 83, 84, 33, 85, 86, 39, 35, 18, 42, 37, 87, 17, 88, 40, /* CX */
           89, 32, 43, 22, 90, 91, 38,SYM, 36, 92, 20, 93, 31, 94, 95, 96, /* DX */
           97, 14, 98, 99, 33,100,101, 39, 35, 18, 42, 37,102, 17,103, 40, /* EX */
          104, 32, 43, 22,105,106, 38,SYM, 36,107, 20,108, 31,109,110,111, /* FX */
        };
        /*X0  X1  X2  X3  X4  X5  X6  X7  X8  X9  XA  XB  XC  XD  XE  XF */

        public Windows_1252_IrishModel() : base(CHAR_TO_ORDER_MAP, "WINDOWS-1252")
        {
        }
    }
}
