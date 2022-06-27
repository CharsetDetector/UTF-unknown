[![Build status](https://ci.appveyor.com/api/projects/status/xr59ab52cav8vuph/branch/master?svg=true)](https://ci.appveyor.com/project/304NotModified/utf-unknown/branch/master)
[![NuGet Pre Release](https://img.shields.io/nuget/vpre/UTF.Unknown.svg)](https://www.nuget.org/packages/UTF.Unknown/)

<!--
[![codecov.io](https://codecov.io/github/UniversalCharsetDetector/ude/coverage.svg?branch=master)](https://codecov.io/github/UniversalCharsetDetector/ude?branch=master)
-->

<h1><img src="https://raw.githubusercontent.com/CharsetDetector/UTF-unknown/master/logo.png" width="40" height="40" /> UTF Unknown </h1>



Detect character set for files, streams and other bytes.

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

- .NET 5+
- .NET Standard 1.0+
- .NET Core 3.0+
- .NET Framework 4.0+

__Remarks:__
You can still register your [`EncodingProvider`](https://docs.microsoft.com/ru-ru/dotnet/api/system.text.encodingprovider) so that the  [`Encoding.GetEncoding(...)`](https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding.getencoding?view=netcore-3.0) method first tries to find in it.

## Usage

Use the static detectX methods from `CharsetDetector`.

```c#
// Detect from File (NET standard 1.3+ or .NET 4+)
DetectionResult result = CharsetDetector.DetectFromFile("path/to/file.txt"); // or pass FileInfo

// Detect from Stream (NET standard 1.3+ or .NET 4+)
result = CharsetDetector.DetectFromStream(stream);

// Detect from bytes
results = CharsetDetector.DetectFromBytes(byteArray);

// Get the best Detection
DetectionDetail resultDetected = results.Detected;

// Get the alias of the found encoding
string encodingName = resultDetected.EncodingName;

// Get the System.Text.Encoding of the found encoding (can be null if not available)
Encoding encoding = resultDetected.Encoding;

// Get the confidence of the found encoding (between 0 and 1)
float confidence = resultDetected.Confidence;

// Get all the details of the result
IList<DetectionDetail> allDetails = result.Details;
```

# Docs

The article "[A composite approach to language/encoding detection](https://www-archive.mozilla.org/projects/intl/UniversalCharsetDetection.html)" describes the charsets detection algorithms implemented by the library.

## The following charsets are supported

__Encodings with BOM:__ `utf-7`, `utf-8`, `utf-16be`/`utf-16le`, `utf-32be`/`utf-32le`, `X-ISO-10646-UCS-4-34121`/`X-ISO-10646-UCS-4-21431`, `gb18030`.

__Encodings without BOM are presented in the table, separated by languages:__

|         Language        |  Encodings                                                                             |
|-------------------------|----------------------------------------------------------------------------------------|
| International (Unicode) | `utf-8`                                                                                |
| Arabic                  | `iso-8859-6`, `windows-1256`                                                           |
| Bulgarian               | `iso-8859-5`, `windows-1251`                                                           |
| Chinese                 | `iso-2022-cn`, `big5`, `euc-tw`, `gb18030`, `hz-gb-2312`                               |
| Croatian                | `iso-8859-2`, `iso-8859-13`, `iso-8859-16`, `windows-1250`, `ibm852`, `x-mac-ce`       |
| Czech                   | `windows-1250`, `iso-8859-2`, `ibm852`, `x-mac-ce`                                     |
| Danish                  | `iso-8859-1`, `iso-8859-15`, `windows-1252`                                            |
| English                 | `ascii`                                                                                |
| Esperanto               | `iso-8859-3`                                                                           |
| Estonian                | `iso-8859-4`, `iso-8859-13`, `iso-8859-13`, `windows-1252`, `windows-1257`             |
| Finnish                 | `iso-8859-1`, `iso-8859-4`, `iso-8859-9`, `iso-8859-13`, `iso-8859-15`, `windows-1252` |
| French                  | `iso-8859-1`, `iso-8859-15`, `windows-1252`                                            |
| German                  | `iso-8859-1`, `windows-1252`                                                           |
| Greek                   | `iso-8859-7`, `windows-1253`                                                           |
| Hebrew                  | `iso-8859-8`, `windows-1255`                                                           |
| Hungarian               | `iso-8859-2`, `windows-1250`                                                           |
| Irish Gaelic            | `iso-8859-1`, `iso-8859-9`, `iso-8859-15`, `windows-1252`                              |
| Italian                 | `iso-8859-1`, `iso-8859-3`, `iso-8859-9`, `iso-8859-15`, `windows-1252`                |
| Japanese                | `iso-2022-jp`, `shift-jis`, `euc-jp`                                                   |
| Korean                  | `iso-2022-kr`, `euc-kr`/`uhc`, `cp949`                                                 |
| Lithuanian              | `iso-8859-4`, `iso-8859-10`, `iso-8859-13`                                             |
| Latvian                 | `iso-8859-4`, `iso-8859-10`, `iso-8859-13`                                             |
| Maltese                 | `iso-8859-3`                                                                           |
| Polish                  | `iso-8859-2`, `iso-8859-13`, `iso-8859-16`, `windows-1250`, `ibm852`, `x-mac-ce`       |
| Portuguese              | `iso-8859-1`, `iso-8859-9`, `iso-8859-15`, `windows-1252`                              |
| Romanian                | `iso-8859-2`, `iso-8859-16`, `windows-1250`, `ibm852`                                  |
| Russian                 | `iso-8859-5`, `koi8-r`, `windows-1251`, `x-mac-cyrillic`, `ibm855`, `ibm866`           |
| Slovak                  | `windows-1250`, `iso-8859-2`, `ibm852`, `x-mac-ce`                                     |
| Slovene                 | `iso-8859-2`, `iso-8859-16`, `windows-1250`, `ibm852`, `x-mac-ce`                      |
| Spanish                 | `iso-8859-1`, `iso-8859-15`, `windows-1252`                                            |
| Swedish                 | `iso-8859-1`, `iso-8859-4`, `iso-8859-9`, `iso-8859-15`, `windows-1252`                |
| Thai                    | `tis-620`, `iso-8859-11`                                                               |
| Turkish                 | `iso-8859-3`, `iso-8859-9`                                                             |
| Vietnamese              | `viscii`, `windows-1258`                                                               |
| Others                  | `windows-1252`                                                                         |

</details>

__Remarks:__
For some aliases of encoding not available: `cp949`, `iso-2022-cn`, `euc-tw`, `iso-8859-10`, `iso-8859-16`, `viscii`, `X-ISO-10646-UCS-4-34121`/`X-ISO-10646-UCS-4-21431`. Some of them have been offered a suitable replacement for the return result by  `DetectionDetail.Encoding`:
- `cp949`: use `ks_c_5601-1987`
- `iso-2022-cn`: use `x-cp50227`

## License

The library is subject to the Mozilla Public License Version 1.1 (the "License"). Alternatively, it may be used under the terms of either the GNU General Public License Version 2 or later (the "GPL"), or the GNU Lesser General Public License Version 2.1 or later (the "LGPL").

Test data has been extracted from [Wikipedia](https://wikipedia.org) and [The Project Gutenberg](https://www.gutenberg.org/) books and is subject to their licenses.
