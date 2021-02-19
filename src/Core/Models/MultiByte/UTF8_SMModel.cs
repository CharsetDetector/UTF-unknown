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

using UtfUnknown.Core.Models;

namespace UtfUnknown.Core.Models.MultiByte
{
    public class UTF8_SMModel : StateMachineModel
    {
        private readonly static int[] UTF8_cls = {
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 00 - 07
            BitPackage.Pack4bits(1,1,1,1,1,1,0,0),  // 08 - 0f 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 10 - 17 
            BitPackage.Pack4bits(1,1,1,0,1,1,1,1),  // 18 - 1f 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 20 - 27 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 28 - 2f 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 30 - 37 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 38 - 3f 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 40 - 47 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 48 - 4f 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 50 - 57 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 58 - 5f 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 60 - 67 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 68 - 6f 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 70 - 77 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 78 - 7f 
            BitPackage.Pack4bits(2,2,2,2,3,3,3,3),  // 80 - 87 
            BitPackage.Pack4bits(4,4,4,4,4,4,4,4),  // 88 - 8f 
            BitPackage.Pack4bits(4,4,4,4,4,4,4,4),  // 90 - 97 
            BitPackage.Pack4bits(4,4,4,4,4,4,4,4),  // 98 - 9f 
            BitPackage.Pack4bits(5,5,5,5,5,5,5,5),  // a0 - a7 
            BitPackage.Pack4bits(5,5,5,5,5,5,5,5),  // a8 - af 
            BitPackage.Pack4bits(5,5,5,5,5,5,5,5),  // b0 - b7 
            BitPackage.Pack4bits(5,5,5,5,5,5,5,5),  // b8 - bf 
            BitPackage.Pack4bits(0,0,6,6,6,6,6,6),  // c0 - c7 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,6),  // c8 - cf 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,6),  // d0 - d7 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,6),  // d8 - df 
            BitPackage.Pack4bits(7,8,8,8,8,8,8,8),  // e0 - e7 
            BitPackage.Pack4bits(8,8,8,8,8,9,8,8),  // e8 - ef 
            BitPackage.Pack4bits(10,11,11,11,11,11,11,11),  // f0 - f7 
            BitPackage.Pack4bits(12,13,13,13,14,15,0,0)   // f8 - ff 
        };

        private readonly static int[] UTF8_st = {
            BitPackage.Pack4bits(ERROR,START,ERROR,ERROR,ERROR,ERROR,   12,   10),//00-07 
            BitPackage.Pack4bits(    9,   11,    8,    7,    6,    5,    4,    3),//08-0f 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR),//10-17 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR),//18-1f 
            BitPackage.Pack4bits(ITSME,ITSME,ITSME,ITSME,ITSME,ITSME,ITSME,ITSME),//20-27 
            BitPackage.Pack4bits(ITSME,ITSME,ITSME,ITSME,ITSME,ITSME,ITSME,ITSME),//28-2f 
            BitPackage.Pack4bits(ERROR,ERROR,    5,    5,    5,    5,ERROR,ERROR),//30-37 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR),//38-3f 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,    5,    5,    5,ERROR,ERROR),//40-47 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR),//48-4f 
            BitPackage.Pack4bits(ERROR,ERROR,    7,    7,    7,    7,ERROR,ERROR),//50-57 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR),//58-5f 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,    7,    7,ERROR,ERROR),//60-67 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR),//68-6f 
            BitPackage.Pack4bits(ERROR,ERROR,    9,    9,    9,    9,ERROR,ERROR),//70-77 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR),//78-7f 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,    9,    9,ERROR,ERROR),//80-87 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR),//88-8f 
            BitPackage.Pack4bits(ERROR,ERROR,   12,   12,   12,   12,ERROR,ERROR),//90-97 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR),//98-9f 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,ERROR,   12,ERROR,ERROR),//a0-a7 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR),//a8-af 
            BitPackage.Pack4bits(ERROR,ERROR,   12,   12,   12,ERROR,ERROR,ERROR),//b0-b7 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR),//b8-bf 
            BitPackage.Pack4bits(ERROR,ERROR,START,START,START,START,ERROR,ERROR),//c0-c7 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR) //c8-cf  
        };

        private readonly static int[] UTF8CharLenTable = 
            {0, 1, 0, 0, 0, 0, 2, 3, 3, 3, 4, 4, 5, 5, 6, 6 };
        
        public UTF8_SMModel() : base(
              new BitPackage(BitPackage.INDEX_SHIFT_4BITS, 
                         BitPackage.SHIFT_MASK_4BITS, 
                         BitPackage.BIT_SHIFT_4BITS,
                         BitPackage.UNIT_MASK_4BITS, UTF8_cls),
                         16,
              new BitPackage(BitPackage.INDEX_SHIFT_4BITS, 
                         BitPackage.SHIFT_MASK_4BITS, 
                         BitPackage.BIT_SHIFT_4BITS,
                         BitPackage.UNIT_MASK_4BITS, UTF8_st),
              UTF8CharLenTable, CodepageName.UTF8)
        {

        }
    }
}
