// CharsetDetectorTest.cs created with MonoDevelop
//
// Author:
//   Rudi Pettazzi <rudi.pettazzi@gmail.com>
//

#region

using System.IO;
using System.Text;
using Xunit;

#endregion

namespace Ude.Tests
{
    public class CharsetDetectorTest
    {
        [Fact]
        public void TestASCII()
        {
            var detector = new CharsetDetector();
            string s =
                "The Documentation of the libraries is not complete " +
                "and your contributions would be greatly appreciated " +
                "the documentation you want to contribute to and " +
                "click on the [Edit] link to start writing";
            using (MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(s)))
            {
                var result = detector.GetFromStream(ms);
                Assert.Equal(Charsets.ASCII, result.Detected.Charset);
                Assert.Equal(1.0f, result.Detected.Confidence);
            }
        }

        [Fact]
        public void TestUTF8_1()
        {
            var detector = new CharsetDetector();
            string s = "ウィキペディアはオープンコンテントの百科事典です。基本方針に賛同し" +
                       "ていただけるなら、誰でも記事を編集したり新しく作成したりできます。" +
                       "ガイドブックを読んでから、サンドボックスで練習してみましょう。質問は" +
                       "利用案内でどうぞ。";
            byte[] buf = Encoding.UTF8.GetBytes(s);
            var result = detector.GetFromBytes(buf);
            Assert.Equal(Charsets.UTF8, result.Detected.Charset);
            Assert.Equal(1.0f, result.Detected.Confidence);
        }

        [Fact]
        public void TestBomUTF8()
        {
            var detector = new CharsetDetector();
            byte[] buf = { 0xEF, 0xBB, 0xBF, 0x68, 0x65, 0x6C, 0x6C, 0x6F, 0x21 };
            var result = detector.GetFromBytes(buf);
            Assert.Equal(Charsets.UTF8, result.Detected.Charset);
            Assert.Equal(1.0f, result.Detected.Confidence);
        }

        [Fact]
        public void TestBomUTF16_BE()
        {
            var detector = new CharsetDetector();
            byte[] buf = { 0xFE, 0xFF, 0x00, 0x68, 0x00, 0x65 };
            detector = new CharsetDetector();
            var result = detector.GetFromBytes(buf);
            Assert.Equal(Charsets.UTF16_BE, result.Detected.Charset);
            Assert.Equal(1.0f, result.Detected.Confidence);
        }

        [Fact]
        public void TestBomX_ISO_10646_UCS_4_3412()
        {
            var detector = new CharsetDetector();
            byte[] buf = {0xFE, 0xFF, 0x00, 0x00, 0x65};
            detector = new CharsetDetector();
            detector.Feed(buf, 0, buf.Length);
            detector.DataEnd();
            Assert.Equal("X-ISO-10646-UCS-4-3412", detector.Charset);
            Assert.Equal(1.0f, detector.Confidence);
        }

        [Fact]
        public void TestBomX_ISO_10646_UCS_4_2143()
        {
            var detector = new CharsetDetector();
            byte[] buf = {0x00, 0x00, 0xFF, 0xFE, 0x00, 0x65};
            detector = new CharsetDetector();
            detector.Feed(buf, 0, buf.Length);
            detector.DataEnd();
            Assert.Equal("X-ISO-10646-UCS-4-2143", detector.Charset);
            Assert.Equal(1.0f, detector.Confidence);
        }

        [Fact]
        public void TestBomUTF16_LE()
        {
            var detector = new CharsetDetector();
            byte[] buf = { 0xFF, 0xFE, 0x68, 0x00, 0x65, 0x00 };
            var result = detector.GetFromBytes(buf);
            Assert.Equal(Charsets.UTF16_LE, result.Detected.Charset);
            Assert.Equal(1.0f, result.Detected.Confidence);
        }

        [Fact]
        public void TestBomUTF32_BE()
        {
            var detector = new CharsetDetector();
            byte[] buf = { 0x00, 0x00, 0xFE, 0xFF, 0x00, 0x00, 0x00, 0x68 };
            var result = detector.GetFromBytes(buf);
            Assert.Equal(Charsets.UTF32_BE, result.Detected.Charset);
            Assert.Equal(1.0f, result.Detected.Confidence);
        }

        [Fact]
        public void TestBomUTF32_LE()
        {
            var detector = new CharsetDetector();
            byte[] buf = { 0xFF, 0xFE, 0x00, 0x00, 0x68, 0x00, 0x00, 0x00 };
            var result = detector.GetFromBytes(buf);
            Assert.Equal(Charsets.UTF32_LE, result.Detected.Charset);
            Assert.Equal(1.0f, result.Detected.Confidence);
        }

        [Fact]
        public void TestIssue3()
        {
            var detector = new CharsetDetector();
            byte[] buf = Encoding.UTF8.GetBytes("3");
            var result = detector.GetFromBytes(buf);
            Assert.Equal(Charsets.ASCII, result.Detected.Charset);
            Assert.Equal(1.0f, result.Detected.Confidence);
        }

        [Fact]
        public void TestOutOfRange()
        {
            var detector = new CharsetDetector();
            byte[] buf = Encoding.UTF8.GetBytes("3");
            detector.Feed(buf, 0, 10);
            detector.DataEnd();
            Assert.Equal(Charsets.ASCII, detector.Charset);
            Assert.Equal(1.0f, detector.Confidence);
    }

        [Fact]
        public void TestOutOfRange2()
        {
            var detector = new CharsetDetector();
            byte[] buf = Encoding.UTF8.GetBytes("1234567890");
            detector.Feed(buf, 10, 5);
            detector.DataEnd();
            Assert.Equal(Charsets.ASCII, detector.Charset);
            Assert.Equal(1.0f, detector.Confidence);
}

        [Fact]
        public void TestEmpty()
        {
            var detector = new CharsetDetector();
            byte[] buf = Encoding.UTF8.GetBytes("3");
            detector.Feed(buf, 0, 0);
            detector.DataEnd();
            Assert.Equal(null, detector.Charset);
            Assert.Equal(0, detector.Confidence);
        }

        /// <summary>
        /// 2 times dataend should not change result
        /// </summary>
        [Fact]
        public void TestEmpty2()
        {
            var detector = new CharsetDetector();
            byte[] buf = Encoding.UTF8.GetBytes("3");
            detector.Feed(buf, 0, 0);
            detector.DataEnd();
            detector.DataEnd();
            Assert.Equal(null, detector.Charset);
            Assert.Equal(0, detector.Confidence);
        }

        [Fact]
        public void TestOffset()
        {
            var detector = new CharsetDetector();
            byte[] buf = Encoding.ASCII.GetBytes("test1");

            //no crasg
            detector.Feed(buf, 4, 2);
            detector.DataEnd();
            Assert.Equal(Charsets.ASCII, detector.Charset);
            Assert.Equal(1.0f, detector.Confidence);
        }

        [Fact]
        public void TestFeedSecondEmpty()
        {
            var detector = new CharsetDetector();
            byte[] buf = Encoding.UTF8.GetBytes("3");
            detector.Feed(buf, 0, buf.Length);
            detector.DataEnd();

            //feed empty
            detector.Feed(buf, 0, 0);
            detector.DataEnd();
            Assert.Equal(Charsets.ASCII, detector.Charset);
            Assert.Equal(1.0f, detector.Confidence);
        }

        [Fact]
        public void TestFeedSecondEmpty_bom()
        {
            var detector = new CharsetDetector();
            byte[] buf = {0xFE, 0xFF, 0x00, 0x68, 0x00, 0x65};
            detector = new CharsetDetector();
            detector.Feed(buf, 0, buf.Length);
            detector.DataEnd();
            //feed empty
            detector.Feed(buf, 0, 0);
            detector.DataEnd();
            Assert.Equal(Charsets.UTF16_BE, detector.Charset);
            Assert.Equal(1.0f, detector.Confidence);
        }
    }
}