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

/*
* The following part was imported from https://gitlab.freedesktop.org/uchardet/uchardet
* The implementation of this feature was originally done on https://gitlab.freedesktop.org/uchardet/uchardet/blob/master/src/LangModels/LangHungarianModel.cpp
* and adjusted to language specific support.
*/

namespace UtfUnknown.Core.Models.SingleByte.Hungarian;

public abstract class HungarianModel : SequenceModel
{
    // Model Table:
    // Total sequences: 1084
    // First 512 sequences: 0.9748272224933486
    // Next 512 sequences (512-1024): 0.024983863604162403
    // Rest: 0.0001889139024889644
    // Negative sequences: TODO
    private readonly static byte[] LANG_MODEL = {
        3,3,3,3,3,3,3,3,3,3,3,2,3,3,2,3,3,3,3,3,3,3,3,3,3,3,1,0,2,2,0,0,
        3,2,3,3,3,3,3,3,2,3,3,2,3,3,2,3,3,3,3,3,3,3,3,3,3,0,0,2,2,1,2,1,
        3,3,3,3,3,3,3,3,3,3,3,3,3,2,3,3,2,3,3,3,3,2,3,2,2,3,3,3,3,3,2,2,
        3,3,3,3,3,3,3,3,3,3,3,3,3,2,3,3,2,3,2,3,3,3,2,3,2,2,3,3,3,3,3,2,
        3,3,3,3,3,2,3,3,3,3,2,3,3,3,3,3,3,3,3,3,3,2,3,3,3,3,3,3,3,3,2,2,
        3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,2,3,3,2,3,3,3,3,3,3,2,
        3,3,3,3,3,2,3,3,3,3,3,3,2,3,3,3,3,3,3,3,2,2,2,3,3,3,2,3,2,2,2,2,
        3,3,3,3,3,3,3,3,3,3,3,3,3,3,2,3,3,3,1,3,3,3,2,3,3,2,3,0,2,2,2,2,
        3,2,3,3,3,3,3,2,2,3,3,2,3,3,0,3,3,3,2,3,3,3,2,3,3,0,2,0,0,0,0,0,
        3,3,3,3,3,3,3,3,3,3,2,3,2,2,3,3,2,2,2,3,2,2,2,2,2,3,3,2,3,3,2,2,
        3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,2,3,2,3,2,2,2,3,3,3,3,3,2,2,
        1,2,3,3,3,3,3,3,2,3,3,0,3,3,2,3,3,3,2,2,2,3,3,3,2,0,0,0,2,0,0,0,
        3,3,3,2,3,2,2,3,3,2,3,3,3,2,3,3,2,2,2,3,2,3,2,2,2,2,3,2,2,2,2,3,
        3,3,3,3,3,3,3,3,3,3,2,3,2,3,3,3,2,3,3,3,3,2,3,2,2,3,3,2,3,2,2,2,
        0,1,3,3,3,3,3,2,2,3,3,0,3,3,2,3,3,3,0,0,2,3,2,3,0,0,0,0,0,2,0,0,
        3,3,2,3,3,3,2,3,3,2,2,3,2,1,3,3,3,2,2,3,1,2,2,2,2,2,3,3,3,2,2,2,
        3,3,3,3,2,3,3,3,3,2,2,3,3,2,3,2,2,3,2,3,2,2,3,2,2,3,3,3,3,2,2,2,
        3,3,2,2,2,2,2,3,3,2,0,3,0,2,3,2,2,2,1,2,2,0,2,1,2,3,2,3,3,2,2,2,
        3,3,3,3,2,2,3,3,3,2,3,3,3,2,3,3,2,3,1,3,3,2,2,2,2,2,2,2,2,2,2,3,
        3,2,3,3,3,3,3,2,2,3,2,3,3,3,0,3,3,2,2,2,2,2,2,3,2,0,0,0,1,0,0,0,
        3,3,2,2,2,2,2,3,3,2,0,3,2,2,2,2,2,2,2,3,2,0,2,2,2,2,2,2,3,2,2,2,
        3,3,3,3,3,3,2,3,3,2,2,3,1,2,3,2,2,2,2,3,2,3,3,3,2,2,2,2,3,3,2,0,
        3,3,3,2,2,2,3,2,3,2,2,3,2,2,3,2,3,2,0,3,2,2,2,2,2,2,3,0,2,2,3,2,
        3,3,2,3,2,2,2,3,3,3,3,2,2,2,3,2,2,2,2,2,3,0,0,2,2,2,2,0,3,0,0,0,
        3,3,2,2,2,3,2,3,3,0,0,2,2,2,3,2,2,2,2,3,0,2,2,2,2,3,2,3,2,3,2,2,
        2,0,3,3,3,3,3,0,0,3,3,0,2,3,0,3,3,3,0,0,2,2,2,2,1,0,0,0,0,0,0,0,
        2,2,3,3,3,3,3,3,2,3,3,2,3,3,2,3,3,3,0,0,2,3,3,2,2,2,0,0,1,2,2,0,
        2,2,3,3,3,3,2,3,2,3,3,2,2,2,2,3,3,2,0,0,2,2,3,2,2,1,0,0,1,2,1,0,
        0,2,3,2,2,3,3,2,2,2,3,0,3,3,0,2,2,3,0,2,1,2,3,2,2,0,0,0,0,0,0,0,
        0,0,3,2,3,2,3,0,0,3,2,0,2,3,0,0,2,2,0,0,1,0,2,0,0,0,0,0,0,0,0,0,
        2,2,3,3,3,2,3,0,0,2,2,0,0,3,0,2,2,2,0,0,2,2,3,2,1,0,0,0,0,0,0,0,
        2,2,2,2,3,2,2,2,0,3,2,0,2,2,0,2,2,3,0,2,2,0,2,2,2,0,0,0,0,0,0,0,
    };

    public HungarianModel(byte[] charToOrderMap, string name)
        : base(charToOrderMap, LANG_MODEL, 32, 0.9748272224933486f,
            false, name)
    {
    }
}