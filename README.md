[![Build status](https://ci.appveyor.com/api/projects/status/xr59ab52cav8vuph/branch/master?svg=true)](https://ci.appveyor.com/project/304NotModified/utf-unknown/branch/master)

<!-- 
[![codecov.io](https://codecov.io/github/UniversalCharsetDetector/ude/coverage.svg?branch=master)](https://codecov.io/github/UniversalCharsetDetector/ude?branch=master)
-->

# UTF Unknown

Detect character set for files, steams and other bytes.

Detection of character sets with a simple and redesigned interface.

This package is based on [Ude](https://github.com/errepi/ude),
which is a port of the [Mozilla Universal Charset Detector](https://mxr.mozilla.org/mozilla/source/extensions/universalchardet/).

      
The interface and other classes has been resigned so it's easier to use and better object oriented design (OOD). Unit tests and CI has been added.

Features:

- OOD
- Moved to netstandard
- Added more unit tests
- Builds on CI (AppVeyor)
- Strong named
- Documentation added

## Platform
.NET 4.0 and .NET Standard 1.3

## Usage

Use the static detectX methods from `CharsetDetector`.

```c#
// Detect from File
var result = CharsetDetector.DetectFromFile("c:/myfile.txt");
Encoding encoding = result.Detected.Encoding; //or result.Detected.EncodingName
float confidence = result.Detected.Confidence; //confidence between 0 and 1
var allDetails = result.Details;
// Detect from Stream
var result = CharsetDetector.DetectFromStream(stream);
// Detect from bytes
var result = CharsetDetector.DetectFromBytes(byteArray);

```

# Docs

The article "[A composite approach to language/encoding detection](http://www.mozilla.org/projects/intl/UniversalCharsetDetection.html)" describes the charsets detection algorithms implemented by the library.

The following charsets are supported:

* UTF-8
* UTF-16 (BE and LE)
* UTF-32 (BE and LE)
* windows-1252 (mostly equivalent to iso8859-1)
* windows-1251 and ISO-8859-5 (cyrillic)
* windows-1253 and ISO-8859-7 (greek)
* windows-1255 (logical hebrew. Includes ISO-8859-8-I and most of x-mac-hebrew)
* ISO-8859-8 (visual hebrew)
* Big-5
* gb18030 (superset of gb2312)
* HZ-GB-2312
* Shift-JIS
* EUC-KR, EUC-JP, EUC-TW
* ISO-2022-JP, ISO-2022-KR, ISO-2022-CN
* KOI8-R
* x-mac-cyrillic
* IBM855 and IBM866
* X-ISO-10646-UCS-4-3412 and X-ISO-10646-UCS-4-2413 (unusual BOM)
* ASCII



## License

The library is subject to the Mozilla Public License Version 1.1 (the "License"). Alternatively, it may be used under the terms of either the GNU General Public License Version 2 or later (the "GPL"), or the GNU Lesser General Public License Version 2.1 or later (the "LGPL").

Test data has been extracted from [Wikipedia](http://wikipedia.org) and [The Project Gutenberg](http://www.gutenberg.org/) books and is subject to their licenses.
