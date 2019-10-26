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
 * The Original Code is mozilla.org code.
 *
 * The Initial Developer of the Original Code is
 * Netscape Communications Corporation.
 * Portions created by the Initial Developer are Copyright (C) 1998
 * the Initial Developer. All Rights Reserved.
 *
 * Contributor(s):
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

namespace UtfUnknown.Core
{
    /// <summary>
    /// charset helper
    /// </summary>
    internal static class Charset
    {
        internal const string ASCII = "ascii";

        internal const string UTF8 = "utf-8";

        internal const string UTF16_LE = "utf-16le";
        
        internal const string UTF16_BE = "utf-16be";
        
        internal const string UTF32_LE = "utf-32le";
        
        internal const string UTF32_BE = "utf-32be";

        internal const string X_ISO_10646_UCS_4_3412 = "X-ISO-10646-UCS-4-3412"; // TODO: not supported?

        internal const string X_ISO_10646_UCS_4_2143 = "X-ISO-10646-UCS-4-2143"; // TODO: not supported?

        internal const string BIG5 = "big5";

        internal const string EUC_TW = "euc-tw"; // TODO: not supported?

        internal const string GB18030 = "gb18030";

        internal const string HZ_GB_2312 = "hz-gb-2312";
        
        internal const string ISO_2022_CN = "iso-2022-ch"; // TODO: not supported?

        internal const string EUC_JP = "euc-jp";

        internal const string ISO_2022_JP = "iso-2022-jp";

        internal const string SHIFT_JIS = "shift-jis"; // or shift_jis, sjis

        internal const string CP949 = "cp949"; // TODO: not supported?

        internal const string KS_C_5601_1987 = "ks_c_5601-1987"; // TODO: cp949?

        internal const string EUC_KR = "euc-kr";

        internal const string ISO_2022_KR = "iso-2022-kr"; // TODO: or iso-2022-kr-7, iso-2022-kr-7bit?
    }
}
