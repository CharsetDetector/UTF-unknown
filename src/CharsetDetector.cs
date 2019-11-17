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
 *          J. Verdurmen
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UtfUnknown.Core;
using UtfUnknown.Core.Probers;

namespace UtfUnknown
{
    /// <summary>
    /// Default implementation of charset detection interface. 
    /// The detector can be fed by a System.IO.Stream:
    /// </summary>                
    public class CharsetDetector
    {
#if NETCOREAPP
        /// <summary>
        /// Adds the encodings of the EncodingProvider object to the common language runtime
        /// </summary>
        static CharsetDetector()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
#endif 
        
        internal InputState InputState;

        /// <summary>
        /// Start of the file
        /// </summary>
        private bool _start;

        /// <summary>
        /// De byte array has data?
        /// </summary>
        private bool _gotData;

        /// <summary>
        /// Most of the time true of <see cref="_detectionDetail"/> is set. TODO not always
        /// </summary>
        private bool _done;

        /// <summary>
        /// Lastchar, but not always filled. TODO remove?
        /// </summary>
        private byte _lastChar;

        /// <summary>
        /// "list" of probers
        /// </summary>
        private IList<CharsetProber> _charsetProbers;

        /// <summary>
        /// TODO unknown
        /// </summary>
        private IList<CharsetProber> _escCharsetProber;

        private IList<CharsetProber> CharsetProbers
        {
            get
            {
                switch (InputState)
                {
                    case InputState.EscASCII:
                        return _escCharsetProber;
                    case InputState.Highbyte:
                        return _charsetProbers;
                    default:
                        // pure ascii
                        return new List<CharsetProber>();
                }
            }
        }

        /// <summary>
        /// Detected charset. Most of the time <see cref="_done"/> is true
        /// </summary>
        private DetectionDetail _detectionDetail;

        private const float MinimumThreshold = 0.20f;

        private CharsetDetector()
        {
            _start = true;
            InputState = InputState.PureASCII;
            _lastChar = 0x00;
        }

        /// <summary>
        /// Detect the character encoding form this byte array.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static DetectionResult DetectFromBytes(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            var detector = new CharsetDetector();
            detector.Feed(bytes, 0, bytes.Length);
            return detector.DataEnd();
        }

#if !NETSTANDARD1_0

        /// <summary>
        /// Detect the character encoding by reading the stream.
        /// 
        /// Note: stream position is not reset before and after.
        /// </summary>
        /// <param name="stream">The steam. </param>
        public static DetectionResult DetectFromStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return DetectFromStream(stream, null);
        }

        /// <summary>
        /// Detect the character encoding by reading the stream.
        /// 
        /// Note: stream position is not reset before and after.
        /// </summary>
        /// <param name="stream">The steam. </param>
        /// <param name="maxBytesToRead">max bytes to read from <paramref name="stream"/>. If <c>null</c>, then no max</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxBytesToRead"/> 0 or lower.</exception>
        public static DetectionResult DetectFromStream(Stream stream, long? maxBytesToRead)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (maxBytesToRead <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxBytesToRead));
            }

            var detector = new CharsetDetector();

            ReadStream(stream, maxBytesToRead, detector);
            return detector.DataEnd();
        }

        private static void ReadStream(Stream stream, long? maxBytes, CharsetDetector detector)
        {
            const int bufferSize = 1024;
            byte[] buff = new byte[bufferSize];
            int read;
            long readTotal = 0;

            var toRead = CalcToRead(maxBytes, readTotal, bufferSize);

            while ((read = stream.Read(buff, 0, toRead)) > 0)
            {
                detector.Feed(buff, 0, read);

                if (maxBytes != null)
                {
                    readTotal += read;
                    if (readTotal >= maxBytes)
                    {
                        return;
                    }

                    toRead = CalcToRead(maxBytes, readTotal, bufferSize);
                }

                if (detector._done)
                {
                    return;
                }
            }
        }

        private static int CalcToRead(long? maxBytes, long readTotal, int bufferSize)
        {
            if (readTotal + bufferSize > maxBytes)
            {
                var calcToRead = (int)maxBytes - (int)readTotal;
                return calcToRead;
            }

            return bufferSize;
        }

        /// <summary>
        /// Detect the character encoding of this file.
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns></returns>
        public static DetectionResult DetectFromFile(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            using (FileStream fs = File.OpenRead(filePath))
            {
                return DetectFromStream(fs);
            }
        }
        /// <summary>
        /// Detect the character encoding of this file.
        /// </summary>
        /// <param name="file">The file</param>
        /// <returns></returns>
        public static DetectionResult DetectFromFile(FileInfo file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            using (FileStream fs = file.OpenRead())
            {
                return DetectFromStream(fs);
            }
        }

#endif // !NETSTANDARD1_0

        protected virtual void Feed(byte[] buf, int offset, int len)
        {
            if (_done)
            {
                return;
            }

            if (len > 0)
                _gotData = true;

            // If the data starts with BOM, we know it is UTF
            if (_start)
            {
                _start = false;
                _done = IsStartsWithBom(buf, len);
                if (_done)
                    return;
            }

            FindInputState(buf, len);
            foreach (var prober in CharsetProbers)
            {
                _done = RunProber(buf, offset, len, prober);
                if (_done)
                    return;
            }
        }

        private bool IsStartsWithBom(byte[] buf, int len)
        {
            var bomSet = FindCharSetByBom(buf, len);
            if (bomSet != null)
            {
                _detectionDetail = new DetectionDetail(bomSet, 1.0f);
                return true;
            }
            return false;
        }

        private bool RunProber(byte[] buf, int offset, int len, CharsetProber charsetProber)
        {
            var probingState = charsetProber.HandleData(buf, offset, len);
            if (probingState == ProbingState.FoundIt)
            {
                _detectionDetail = new DetectionDetail(charsetProber);
                return true;
            }
            return false;
        }

        private void FindInputState(byte[] buf, int len)
        {
            for (int i = 0; i < len; i++)
            {
                // other than 0xa0, if every other character is ascii, the page is ascii
                if ((buf[i] & 0x80) != 0 && buf[i] != 0xA0)
                {
                    // we got a non-ascii byte (high-byte)
                    if (InputState != InputState.Highbyte)
                    {
                        InputState = InputState.Highbyte;

                        // kill EscCharsetProber if it is active
                        _escCharsetProber = null;
                        _charsetProbers = _charsetProbers ?? GetNewProbers();
                    }
                }
                else
                {
                    if (InputState == InputState.PureASCII &&
                        (buf[i] == 0x1B || (buf[i] == 0x7B && _lastChar == 0x7E)))
                    {
                        // found escape character or HZ "~{"
                        InputState = InputState.EscASCII;
                        _escCharsetProber = _escCharsetProber ?? GetNewProbers();
                    }
                    _lastChar = buf[i];
                }
            }
        }

        private static string FindCharSetByBom(byte[] buf, int len)
        {
            if (len < 2)
                return null;

            var buf0 = buf[0];
            var buf1 = buf[1];

            if (buf0 == 0xFE && buf1 == 0xFF)
            {
                // FE FF 00 00  UCS-4, unusual octet order BOM (3412)
                return len > 3
                        && buf[2] == 0x00 && buf[3] == 0x00
                    ? CodepageName.X_ISO_10646_UCS_4_3412
                    : CodepageName.UTF16_BE;
            }

            if (buf0 == 0xFF && buf1 == 0xFE)
            {
                return len > 3
                       && buf[2] == 0x00 && buf[3] == 0x00
                    ? CodepageName.UTF32_LE
                    : CodepageName.UTF16_LE;
            }

            if (buf0 == 0x00 && buf1 == 0x00)
            {
                if (len > 3)
                {
                    if (buf[2] == 0xFE && buf[3] == 0xFF)
                        return CodepageName.UTF32_BE;

                    // 00 00 FF FE  UCS-4, unusual octet order BOM (2143)
                    if (buf[2] == 0xFF && buf[3] == 0xFE)
                        return CodepageName.X_ISO_10646_UCS_4_2143;
                }
            }

            if (len < 3)
                return null;

            if (buf0 == 0xEF && buf1 == 0xBB && buf[2] == 0xBF)
                return CodepageName.UTF8;
            
            if (len < 4)
                return null;
            
            // Detect utf-7 with bom (see table in https://en.wikipedia.org/wiki/Byte_order_mark)
            if (buf0 == 0x2B && buf1 == 0x2F && buf[2] == 0x76)
                if (buf[3] == 0x38 || buf[3] == 0x39 || buf[3] == 0x2B || buf[3] == 0x2F)
                    return CodepageName.UTF7;
            
            // Detect GB18030 with bom (see table in https://en.wikipedia.org/wiki/Byte_order_mark)
            // TODO: If you remove this check, GB18030Prober will still be defined as GB18030 -- It's feature or bug?
            if (buf0 == 0x84 && buf1 == 0x31 && buf[2] == 0x95 && buf[3] == 0x33)
                return CodepageName.GB18030;
            
            return null;
        }

        /// <summary>
        /// Notify detector that no further data is available. 
        /// </summary>
        private DetectionResult DataEnd()
        {
            if (!_gotData)
            {
                // we haven't got any data yet, return immediately 
                // caller program sometimes call DataEnd before anything has 
                // been sent to detector
                return new DetectionResult();
            }

            if (_detectionDetail != null)
            {
                _done = true;

                // conf 1.0 is from v1.0 (todo wrong?)
                _detectionDetail.Confidence = 1.0f;
                return new DetectionResult(_detectionDetail);
            }

            if (InputState == InputState.Highbyte)
            {
                var detectionResults = _charsetProbers
                    .Select(prober => new DetectionDetail(prober))
                    .Where(result => result.Confidence > MinimumThreshold)
                    .OrderByDescending(result => result.Confidence)
                    .ToList();

                return new DetectionResult(detectionResults);

                //TODO why done isn't true?
            }
            else if (InputState == InputState.PureASCII)
            {
                //TODO why done isn't true?
                return new DetectionResult(new DetectionDetail(CodepageName.ASCII, 1.0f));
            }

            return new DetectionResult();
        }

        internal IList<CharsetProber> GetNewProbers()
        {
            switch (InputState)
            {
                case InputState.EscASCII:
                    return new List<CharsetProber>() { new EscCharsetProber() };

                case InputState.Highbyte:
                    return new List<CharsetProber>()
                    {
                        new MBCSGroupProber(),
                        new SBCSGroupProber(),
                        new Latin1Prober(),
                    };

                default:
                    // pure ascii
                    return new List<CharsetProber>();
            }
        }
    }
}

