[![Build status](https://ci.appveyor.com/api/projects/status/xr59ab52cav8vuph/branch/master?svg=true)](https://ci.appveyor.com/project/304NotModified/utf-unknown/branch/master)
[![NuGet Pre Release](https://img.shields.io/nuget/vpre/UTF.Unknown.svg)](https://www.nuget.org/packages/UTF.Unknown/)

<!--
[![codecov.io](https://codecov.io/github/UniversalCharsetDetector/ude/coverage.svg?branch=master)](https://codecov.io/github/UniversalCharsetDetector/ude?branch=master)
-->

<h1><img src="https://raw.githubusercontent.com/CharsetDetector/UTF-unknown/master/logo.png" width="40" height="40" /> UTF Unknown </h1>



Detect character set for files, steams and other bytes.

Detection of character sets with a simple and redesigned interface.

This package is based on [Ude](https://github.com/errepi/ude) and since version 2 also on [uchardet](https://gitlab.freedesktop.org/uchardet/uchardet),
which are ports of the [Mozilla Universal Charset Detector](https://mxr.mozilla.org/mozilla/source/extensions/universalchardet/).

The interface and other classes has been resigned so it's easier to use and better object oriented design (OOD). Unit tests and CI has been added.

Features:

- New API
- Moved to .NET Standard
- Added more unit tests
- Builds on CI (AppVeyor)
- Strong named
- Documentation added
- Multiple bugs from Ude fixed

## Supported Platforms
- .NET 4+, 
- .NET Standard 1.0 and .NET Standard 2
- .NET Core 2
- .NET Core 3

## Usage

Use the static detectX methods from `CharsetDetector`.

```c#
// Detect from File (NET standard 1.3+ or .NET 4+)
var result = CharsetDetector.DetectFromFile("c:/myfile.txt"); //or pass FileInfo
Encoding encoding = result.Detected.Encoding; //or result.Detected.EncodingName
float confidence = result.Detected.Confidence; //confidence between 0 and 1
var allDetails = result.Details;
// Detect from Stream (NET standard 1.3+ or .NET 4+)
var result = CharsetDetector.DetectFromStream(stream);
// Detect from bytes
var result = CharsetDetector.DetectFromBytes(byteArray);

```

# Docs

The article "[A composite approach to language/encoding detection](https://www-archive.mozilla.org/projects/intl/UniversalCharsetDetection.html)" describes the charsets detection algorithms implemented by the library.

<details>
  <summary>The following charsets are supported</summary>

|        Language         | Encodings                                                                                           |
|-------------------------|-----------------------------------------------------------------------------------------------------|
| International (Unicode) | UTF-8; UTF-16BE / UTF-16LE; UTF-32BE / UTF-32LE / X-ISO-10646-UCS-4-34121 / X-ISO-10646-UCS-4-21431 |
| Arabic                  | ISO-8859-6; WINDOWS-1256                                                                            |
| Bulgarian               | ISO-8859-5; WINDOWS-1251                                                                            |
| Chinese                 | ISO-2022-CN; BIG5; EUC-TW; GB18030; HZ-GB-2312                                                      |
| Croatian                | ISO-8859-2; ISO-8859-13; ISO-8859-16; WINDOWS-1250; IBM852; MAC-CENTRALEUROPE                       |
| Czech                   | WINDOWS-1250; ISO-8859-2; IBM852; MAC-CENTRALEUROPE                                                 |
| Danish                  | ISO-8859-1; ISO-8859-15; WINDOWS-1252                                                               |
| English                 | ASCII                                                                                               |
| Esperanto               | ISO-8859-3                                                                                          |
| Estonian                | ISO-8859-4; ISO-8859-13; ISO-8859-13; WINDOWS-1252; WINDOWS-1257                                    |
| Finnish                 | ISO-8859-1; ISO-8859-4; ISO-8859-9; ISO-8859-13; ISO-8859-15; WINDOWS-1252                          |
| French                  | ISO-8859-1; ISO-8859-15; WINDOWS-1252                                                               |
| German                  | ISO-8859-1; WINDOWS-1252                                                                            |
| Greek                   | ISO-8859-7; WINDOWS-1253                                                                            |
| Hebrew                  | ISO-8859-8; WINDOWS-1255                                                                            |
| Hungarian               | ISO-8859-2; WINDOWS-1250                                                                            |
| Irish Gaelic            | ISO-8859-1; ISO-8859-9; ISO-8859-15; WINDOWS-1252                                                   |
| Italian                 | ISO-8859-1; ISO-8859-3; ISO-8859-9; ISO-8859-15; WINDOWS-1252                                       |
| Japanese                | ISO-2022-JP; SHIFT_JIS; EUC-JP                                                                      |
| Korean                  | ISO-2022-KR; EUC-KR / UHC; WINDOWS-949                                                              |
| Lithuanian              | ISO-8859-4; ISO-8859-10; ISO-8859-13                                                                |
| Latvian                 | ISO-8859-4; ISO-8859-10; ISO-8859-13                                                                |
| Maltese                 | ISO-8859-3                                                                                          |
| Polish                  | ISO-8859-2; ISO-8859-13; ISO-8859-16; WINDOWS-1250; IBM852; MAC-CENTRALEUROPE                       |
| Portuguese              | ISO-8859-1; ISO-8859-9; ISO-8859-15; WINDOWS-1252                                                   |
| Romanian                | ISO-8859-2; ISO-8859-16; WINDOWS-1250; IBM852                                                       |
| Russian                 | ISO-8859-5; KOI8-R; WINDOWS-1251; MAC-CYRILLIC; IBM866; IBM855                                      |
| Slovak                  | WINDOWS-1250; ISO-8859-2; IBM852; MAC-CENTRALEUROPE                                                 |
| Slovene                 | ISO-8859-2; ISO-8859-16; WINDOWS-1250; IBM852; MAC-CENTRALEUROPE                                    |
| Spanish                 | ISO-8859-1; ISO-8859-15; WINDOWS-1252                                                               |
| Swedish                 | ISO-8859-1; ISO-8859-4; ISO-8859-9; ISO-8859-15; WINDOWS-1252                                       |
| Thai                    | TIS-620; ISO-8859-11                                                                                |
| Turkish                 | ISO-8859-3; ISO-8859-9                                                                              |
| Vietnamese              | VISCII; WINDOWS-1258                                                                                |
| Others                  | WINDOWS-1252                                                                                        |

</details>



## License

The library is subject to the Mozilla Public License Version 1.1 (the "License"). Alternatively, it may be used under the terms of either the GNU General Public License Version 2 or later (the "GPL"), or the GNU Lesser General Public License Version 2.1 or later (the "LGPL").

Test data has been extracted from [Wikipedia](https://wikipedia.org) and [The Project Gutenberg](https://www.gutenberg.org/) books and is subject to their licenses.
