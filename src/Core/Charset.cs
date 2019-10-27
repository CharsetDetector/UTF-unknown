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
 * Contributor(s):
 *          UTF-Unknown Contributors (2019)
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
    /// <remarks>Based on https://github.com/dotnet/corefx/blob/cf28b7896a762f71c990a5896a160a4138d833c9/src/System.Text.Encoding.CodePages/src/System/Text/EncodingTable.Data.cs</remarks>
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

        internal const string BIG5 = "big5"; // or big5-hkscs, cn-big5, csbig5, x-x-big5

        internal const string EUC_TW = "euc-tw"; // TODO: not supported?

        internal const string GB18030 = "gb18030";

        internal const string HZ_GB_2312 = "hz-gb-2312";
        
        internal const string ISO_2022_CN = "iso-2022-ch"; // TODO: not supported?

        internal const string EUC_JP = "euc-jp"; // or x-euc, x-euc-jp, iso-2022-jpeuc, extended_unix_code_packed_format_for_japanese

        internal const string ISO_2022_JP = "iso-2022-jp";

        internal const string SHIFT_JIS = "shift-jis"; // or shift_jis, sjis, csshiftjis, cswindows31j, ms_kanji, x-sjis

        internal const string CP949 = "cp949"; // TODO: not supported? fix to ks-c-5601?

        internal const string KS_C_5601 = "ks_c_5601-1987"; // or korean, ks-c-5601, ks-c5601, ks_c_5601, ks_c_5601-1989, ks_c_5601_1987, ksc5601, ksc_5601, iso-ir-149, csksc56011987

        internal const string EUC_KR = "euc-kr"; // or iso-2022-kr-8, iso-2022-kr-8bit, cseuckr

        internal const string ISO_2022_KR = "iso-2022-kr"; // or iso-2022-kr-7, iso-2022-kr-7bit, csiso2022kr

        internal const string IBM852 = "ibm852"; // or cp852

        internal const string IBM855 = "ibm855"; // or cp855
        
        internal const string IBM866 = "ibm866"; // or cp866

        internal const string ISO_8859_1 = "iso-8859-1"; // TODO: not supported?

        internal const string ISO_8859_2 = "iso-8859-2"; // or iso8859-2, iso_8859-2, iso_8859-2:1987, iso-ir-101, l2, latin2, csisolatin2

        internal const string ISO_8859_3 = "iso-8859-3"; // or iso_8859-3, iso_8859-3:1988, iso-ir-109, l3, latin3, csisolatin3

        internal const string ISO_8859_4 = "iso-8859-4"; // or iso_8859-4, iso_8859-4:1988, iso-ir-110, l4, latin4, csisolatin4

        internal const string ISO_8859_5 = "iso-8859-5"; // or iso_8859-5, iso_8859-5:1988, iso-ir-144, cyrillic, csisolatincyrillic

        internal const string ISO_8859_6 = "iso-8859-6"; // or iso_8859-6, iso_8859-6:1987, iso-ir-127, arabic, csisolatinarabic, ecma-114

        internal const string ISO_8859_7 = "iso-8859-7"; // or iso_8859-7, iso_8859-7:1987, iso-ir-126, greek, greek8, csisolatingreek, ecma-118, elot_928

        internal const string ISO_8859_9 = "iso-8859-9"; // or iso-ir-148

        internal const string ISO_8859_10 = "iso-8859-10"; // TODO: not supported?
        
        internal const string ISO_8859_11 = "iso-8859-11"; // or tis-620, windows-874, dos-874

        internal const string ISO_8859_13 = "iso-8859-13";

        internal const string ISO_8859_15 = "iso-8859-15"; // or iso_8859-15, l9, latin9, csisolatin9
        
        internal const string ISO_8859_16 = "iso-8859-16"; // TODO: not supported

        internal const string WINDOWS_1250 = "windows-1250"; // or x-cp1250

        internal const string WINDOWS_1251 = "windows-1251"; // or x-cp1251

        internal const string WINDOWS_1252 = "windows-1252"; // or x-ansi
        
        internal const string WINDOWS_1253 = "windows-1253";
        
        internal const string WINDOWS_1255 = "windows-1255"; 

        internal const string WINDOWS_1256 = "windows-1256"; // or cp1256

        internal const string WINDOWS_1257 = "windows-1257";
        
        internal const string WINDOWS_1258 = "windows-1258";

        internal const string KOI8_R = "koi8-r"; // or koi, koi8, koi8r, cskoi8r

        internal const string X_MAC_CE = "MAC-CENTRALEUROPE"; // TODO: fix to x-mac-ce
        
        internal const string X_MAC_CYRILLIC = "x-mac-cyrillic";

        internal const string TIS_620 = "tis-620"; // TODO: equal iso-8859-11?

        internal const string VISCII = "viscii"; // TODO: not supported?
    }
}
