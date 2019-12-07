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
    /// This class defines the available codepage .NET Name.
    /// </summary>
    /// <remarks>Based on https://github.com/dotnet/corefx/blob/cf28b7896a762f71c990a5896a160a4138d833c9/src/System.Text.Encoding.CodePages/src/System/Text/EncodingTable.Data.cs</remarks>
    internal static class CodepageName
    {
        /// <summary>
        /// ASCII codepage name.
        /// </summary>
        internal const string ASCII = "ascii";

        /// <summary>
        /// UTF-7 codepage name.
        /// </summary>
        internal const string UTF7 = "utf-7";
        
        /// <summary>
        /// UTF-8 codepage name.
        /// </summary>
        internal const string UTF8 = "utf-8";

        /// <summary>
        /// UTF-16LE codepage name.
        /// </summary>
        internal const string UTF16_LE = "utf-16le";
        
        /// <summary>
        /// UTF-16BE codepage name.
        /// </summary>
        internal const string UTF16_BE = "utf-16be";
        
        /// <summary>
        /// UTF-32LE codepage name.
        /// </summary>
        internal const string UTF32_LE = "utf-32le";
        
        /// <summary>
        /// UTF-32BE codepage name.
        /// </summary>
        internal const string UTF32_BE = "utf-32be";
        
        /// <summary>
        /// EUC Japanese codepage name.
        /// </summary>
        /// <remarks>
        /// Are other aliases x-euc, x-euc-jp, iso-2022-jpeuc, extended_unix_code_packed_format_for_japanese in .NET
        /// </remarks>
        internal const string EUC_JP = "euc-jp";

        /// <summary>
        /// EUC Korean codepage name.
        /// </summary>
        /// <remarks>
        /// Are other aliases iso-2022-kr-8, iso-2022-kr-8bit, cseuckr in .NET
        /// </remarks>
        internal const string EUC_KR = "euc-kr";

        /// <summary>
        /// EUC Taiwan codepage name.
        /// </summary>
        /// <remarks>
        /// Not supported.
        /// </remarks>
        internal const string EUC_TW = "euc-tw";
        
        /// <summary>
        /// ISO 2022 Chinese codepage name.
        /// </summary>
        /// <remarks>
        /// Supported by alias is x-cp50227 (Codepage 50227) in. NET. Codepage identifier 50229 is currently unsupported (see for example https://github.com/microsoft/referencesource/blob/17b97365645da62cf8a49444d979f94a59bbb155/mscorlib/system/text/iso2022encoding.cs#L92).
        /// </remarks>
        internal const string ISO_2022_CN = "iso-2022-cn";
        
        /// <summary>
        /// ISO 2022 Korean codepage name.
        /// </summary>
        /// <remarks>
        /// Are other aliases iso-2022-kr-7, iso-2022-kr-7bit, csiso2022kr in .NET
        /// </remarks>
        internal const string ISO_2022_KR = "iso-2022-kr";

        /// <summary>
        /// ISO 2022 Japanese codepage name.
        /// </summary>
        internal const string ISO_2022_JP = "iso-2022-jp";

        /// <summary>
        /// ISO 2022 Simplified Chinese codepage name.
        /// </summary>
        /// <remarks>
        /// Other alias is cp50227.
        /// </remarks>
        internal const string X_CP50227 = "x-cp50227";
        
        /// <summary>
        /// Big5 codepage name.
        /// </summary>
        /// <remarks>
        /// Are other aliases big5-hkscs, cn-big5, csbig5, x-x-big5 in .NET
        /// </remarks>
        internal const string BIG5 = "big5";

        /// <summary>
        /// GB18030 codepage name.
        /// </summary>
        internal const string GB18030 = "gb18030";

        /// <summary>
        /// HZ-GB2312 codepage name.
        /// </summary>
        internal const string HZ_GB_2312 = "hz-gb-2312";

        /// <summary>
        /// Shift-JIS codepage name.
        /// </summary>
        /// <remarks>
        /// Are other aliases shift_jis, sjis, csshiftjis, cswindows31j, ms_kanji, x-sjis in .NET
        /// </remarks>
        internal const string SHIFT_JIS = "shift-jis";
        
        /// <summary>
        /// ANSI/OEM Korean codepage name.
        /// </summary>
        /// <remarks>
        /// Are other aliases korean, ks-c-5601, ks-c5601, ks_c_5601, ks_c_5601-1989, ks_c_5601_1987, ksc5601, ksc_5601, iso-ir-149, csksc56011987 in .NET
        /// </remarks>
        internal const string KS_C_5601_1987 = "ks_c_5601-1987";

        /// <summary>
        /// CP949 codepage name.
        /// </summary>
        /// <remarks>
        /// Not supported in .NET. A nearly identical version of cp949 is ks_c_5601-1987 (see https://lists.w3.org/Archives/Public/ietf-charsets/2002JulSep/0020.html)
        /// </remarks>
        internal const string CP949 = "cp949";
        
        /// <summary>
        /// OEM Latin-2 codepage name.
        /// </summary>
        /// <remarks>
        /// Is other alias cp852 in .NET
        /// </remarks>
        internal const string IBM852 = "ibm852";

        /// <summary>
        /// OEM Cyrillic (primarily Russian) codepage name.
        /// </summary>
        /// <remarks>
        /// Is other alias cp855 in .NET
        /// </remarks>
        internal const string IBM855 = "ibm855";
        
        /// <summary>
        /// OEM Cyrillic (primarily Russian) codepage name.
        /// </summary>
        /// <remarks>
        /// Is other alias cp866 in .NET
        /// </remarks>
        internal const string IBM866 = "ibm866";

        /// <summary>
        /// ISO 8859-1 Latin-1 Western European codepage name.
        /// </summary>
        internal const string ISO_8859_1 = "iso-8859-1";

        /// <summary>
        /// ISO 8859-2 Central European (Latin 2 Eastern European) codepage name.
        /// </summary>
        /// <remarks>
        /// Are other aliases iso8859-2, iso_8859-2, iso_8859-2:1987, iso-ir-101, l2, latin2, csisolatin2 in .NET
        /// </remarks>
        internal const string ISO_8859_2 = "iso-8859-2";

        /// <summary>
        /// ISO 8859-3 Latin-3 (South European) codepage name.
        /// </summary>
        /// <remarks>
        /// Are other aliases iso_8859-3, iso_8859-3:1988, iso-ir-109, l3, latin3, csisolatin3 in .NET
        /// </remarks>
        internal const string ISO_8859_3 = "iso-8859-3";

        /// <summary>
        /// ISO 8859-4 Baltic (Latin-4 North European) codepage name.
        /// </summary>
        /// <remarks>
        /// Are other aliases iso_8859-4, iso_8859-4:1988, iso-ir-110, l4, latin4, csisolatin4 in .NET
        /// </remarks>
        internal const string ISO_8859_4 = "iso-8859-4";

        /// <summary>
        /// ISO 8859-5 Cyrillic codepage name.
        /// </summary>
        /// <remarks>
        /// Are other aliases iso_8859-5, iso_8859-5:1988, iso-ir-144, cyrillic, csisolatincyrillic in .NET
        /// </remarks>
        internal const string ISO_8859_5 = "iso-8859-5";

        /// <summary>
        /// ISO 8859-6 Arabic codepage name.
        /// </summary>
        /// <remarks>
        /// Are other aliases iso_8859-6, iso_8859-6:1987, iso-ir-127, arabic, csisolatinarabic, ecma-114 in .NET
        /// </remarks>
        internal const string ISO_8859_6 = "iso-8859-6";

        /// <summary>
        /// ISO 8859-7 Greek codepage name.
        /// </summary>
        /// <remarks>
        /// Are other aliases iso_8859-7, iso_8859-7:1987, iso-ir-126, greek, greek8, csisolatingreek, ecma-118, elot_928 in .NET
        /// </remarks>
        internal const string ISO_8859_7 = "iso-8859-7";
        
        /// <summary>
        /// ISO 8859-8 Hebrew codepage name.
        /// </summary>
        /// <remarks>
        /// Are other aliases iso_8859-8, iso_8859-8:1988, iso-8859-8 visual, iso-ir-138, hebrew, logical, visual, csisolatinhebrew in .NET
        /// </remarks>
        internal const string ISO_8859_8 = "iso-8859-8";

        /// <summary>
        /// ISO 8859-9 Latin-5 Turkish codepage name.
        /// </summary>
        /// <remarks>
        /// Is other alias iso-ir-148 in .NET
        /// </remarks>
        internal const string ISO_8859_9 = "iso-8859-9";

        /// <summary>
        /// ISO 8859-10 Latin-6 Nordic codepage name.
        /// </summary>
        /// <remarks>
        /// Not supported.
        /// </remarks>
        internal const string ISO_8859_10 = "iso-8859-10";
        
        /// <summary>
        /// ANSI/OEM Thai codepage name.
        /// </summary>
        /// <remarks>
        /// Are other aliases tis-620, windows-874, dos-874 in .NET
        /// </remarks>
        internal const string ISO_8859_11 = "iso-8859-11";

        /// <summary>
        /// ISO 8859-13 Estonian (Latin 7 BalticRim) codepage name.
        /// </summary>
        internal const string ISO_8859_13 = "iso-8859-13";

        /// <summary>
        /// ISO 8859-15 Latin-9 (Western European) codepage name.
        /// </summary>
        /// <remarks>
        /// Are other aliases iso_8859-15, l9, latin9, csisolatin9 in .NET
        /// </remarks>
        internal const string ISO_8859_15 = "iso-8859-15";
        
        /// <summary>
        /// ISO 8859-16 codepage name.
        /// </summary>
        /// <remarks>
        /// Not supported.
        /// </remarks>
        internal const string ISO_8859_16 = "iso-8859-16";

        /// <summary>
        /// ANSI Central European codepage name.
        /// </summary>
        /// <remarks>
        /// Is other alias x-cp1250 in .NET
        /// </remarks>
        internal const string WINDOWS_1250 = "windows-1250";

        /// <summary>
        /// ANSI Cyrillic codepage name.
        /// </summary>
        /// <remarks>
        /// Is other alias x-cp1251 in .NET
        /// </remarks>
        internal const string WINDOWS_1251 = "windows-1251";

        /// <summary>
        /// ANSI Latin-1 codepage name.
        /// </summary>
        /// <remarks>
        /// Is other alias x-ansi in .NET
        /// </remarks>
        internal const string WINDOWS_1252 = "windows-1252";
        
        /// <summary>
        /// ANSI Greek codepage name.
        /// </summary>
        internal const string WINDOWS_1253 = "windows-1253";
        
        /// <summary>
        /// ANSI Hebrew codepage name.
        /// </summary>
        internal const string WINDOWS_1255 = "windows-1255";

        /// <summary>
        /// ANSI Arabic codepage name.
        /// </summary>
        /// <remarks>
        /// Is other alias cp1256 in .NET
        /// </remarks>
        internal const string WINDOWS_1256 = "windows-1256";

        /// <summary>
        /// ANSI Baltic codepage name.
        /// </summary>
        internal const string WINDOWS_1257 = "windows-1257";
        
        /// <summary>
        /// ANSI/OEM Vietnamese codepage name.
        /// </summary>
        internal const string WINDOWS_1258 = "windows-1258";

        /// <summary>
        /// MAC Latin-2 codepage name.
        /// </summary>
        internal const string X_MAC_CE = "x-mac-ce";
        
        /// <summary>
        /// Cyrillic (Mac) codepage name.
        /// </summary>
        internal const string X_MAC_CYRILLIC = "x-mac-cyrillic";

        /// <summary>
        /// Cyrillic (KOI8-R) codepage name.
        /// </summary>
        /// <remarks>
        /// Are other aliases koi, koi8, koi8r, cskoi8r in .NET
        /// </remarks>
        internal const string KOI8_R = "koi8-r";

        /// <summary>
        /// TIS-620 codepage name.
        /// </summary>
        /// <remarks>
        /// A nearly identical version of TIS-620 is iso-8859-11. The sole difference being that iso-8859-11 defines hex A0 as a non-breaking space, while TIS-620 leaves it undefined but reserved.
        /// </remarks>
        internal const string TIS_620 = "tis-620";

        /// <summary>
        /// VISCII codepage name.
        /// </summary>
        /// <remarks>
        /// Not supported. It's an unofficially-defined modified ASCII character encoding for using the Vietnamese language with computers.
        /// See https://en.wikipedia.org/wiki/VISCII
        /// </remarks>
        internal const string VISCII = "viscii";

        /// <summary>
        /// X-ISO-10646-UCS-4-3412 codepage name.
        /// </summary>
        /// <remarks>
        /// Not supported? ISO 10646 and Unicode only include big-endian and little-endian UCS-4/UTF-32, not middle-endian.
        /// See https://stackoverflow.com/a/21896370
        /// </remarks>
        internal const string X_ISO_10646_UCS_4_3412 = "X-ISO-10646-UCS-4-3412";

        /// <summary>
        /// X-ISO-10646-UCS-4-2143 codepage name.
        /// </summary>
        /// <remarks>
        /// Not supported? ISO 10646 and Unicode only include big-endian and little-endian UCS-4/UTF-32, not middle-endian.
        /// See https://stackoverflow.com/a/21896370
        /// </remarks>
        internal const string X_ISO_10646_UCS_4_2143 = "X-ISO-10646-UCS-4-2143";
    }
}
