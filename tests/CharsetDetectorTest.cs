// CharsetDetectorTest.cs created with MonoDevelop
//
// Author:
//   Rudi Pettazzi <rudi.pettazzi@gmail.com>
//

#region

using System.IO;
using System.Text;
using UtfUnknown.Core;
using NUnit.Framework;

#endregion

namespace UtfUnknown.Tests
{
    public class CharsetDetectorTest
    {
        [Test]
        public void TestAscii()
        {
            const string text = "The Documentation of the libraries is not complete " +
                             "and your contributions would be greatly appreciated " +
                             "the documentation you want to contribute to and " +
                             "click on the [Edit] link to start writing";
            var stream = AsciiToStream(text);
            using (stream)
            {
                var result = CharsetDetector.DetectFromStream(stream);
                Assert.AreEqual(CodepageName.ASCII, result.Detected.EncodingName);
                Assert.AreEqual(1.0f, result.Detected.Confidence);
                Assert.IsFalse(result.Detected.HasBOM);
            }
        }

        [Test]
        public void TestAscii_with_HZ_sequence()
        {
            const string text = "virtual ~{{NETCLASS_NAME}}();";
            var stream = AsciiToStream(text);
            using (stream)
            {
                var result = CharsetDetector.DetectFromStream(stream);
                Assert.AreEqual(CodepageName.ASCII, result.Detected.EncodingName);
                Assert.AreEqual(1.0f, result.Detected.Confidence);
            }
        }

        private static MemoryStream AsciiToStream(string s)
        {
            return new MemoryStream(Encoding.ASCII.GetBytes(s));
        }

        [Test]
        [TestCase(1024, 1024)]
        [TestCase(2048, 2048)]
        [TestCase(20, 20)]
        [TestCase(20, 30, 10)]
        [TestCase(null, 10000)]
        [TestCase(1000000, 10000)]
        [TestCase(null, 10000, 10)]
        public void DetectFromStreamMaxBytes(int? maxBytes, int expectedPosition, int start = 0)
        {
            // Arrange
            var text = new string('a', 10000);
            var stream = AsciiToStream(text);
            stream.Position = start;

            // Act
            CharsetDetector.DetectFromStream(stream, maxBytes);

            // Assert
            Assert.AreEqual(expectedPosition, stream.Position);
        }

        [Test]
        [TestCase(0, 10, CodepageName.ASCII)]
        [TestCase(0, 100, CodepageName.UTF8)]
        [TestCase(10, 100, CodepageName.UTF8)]
        public void DetectFromByteArray(int offset, int len, string detectedCodepage)
        {
            // Arrange
            string s = "UTF-Unknown은 파일, 스트림, 그 외 바이트 배열의 캐릭터 셋을 탐지하는 라이브러리입니다." + 
                "대한민국 (大韓民國, Republic of Korea)";
            byte[] bytes = Encoding.UTF8.GetBytes(s);

            // Act
            var result = CharsetDetector.DetectFromBytes(bytes, offset, len);

            // Assert
            Assert.AreEqual(detectedCodepage, result.Detected.EncodingName);
            Assert.AreEqual(1.0f, result.Detected.Confidence);
            Assert.IsFalse(result.Detected.HasBOM);
        }

        [Test]
        [TestCase(new byte[] { 0x2B, 0x2F, 0x76, 0x38 })]
        [TestCase(new byte[] { 0x2B, 0x2F, 0x76, 0x39 })]
        [TestCase(new byte[] { 0x2B, 0x2F, 0x76, 0x2B })]
        [TestCase(new byte[] { 0x2B, 0x2F, 0x76, 0x2F })]
        [TestCase(new byte[] { 0x2B, 0x2F, 0x76, 0x38, 0x2D })]
        public void TestCaseBomUtf7(byte[] bufferBytes)
        {
            var result = CharsetDetector.DetectFromBytes(bufferBytes)
                .Detected;
            Assert.AreEqual(CodepageName.UTF7, result.EncodingName);
            Assert.AreEqual(1.0f, result.Confidence);
            Assert.IsTrue(result.HasBOM);
        }

        [Test]
        public void TestBomGb18030()
        {
            var bufferBytes = new byte[] { 0x84, 0x31, 0x95, 0x33 };
            var result = CharsetDetector.DetectFromBytes(bufferBytes)
                .Detected;
            Assert.AreEqual(CodepageName.GB18030, result.EncodingName);
            Assert.AreEqual(1.0f, result.Confidence);
            Assert.IsTrue(result.HasBOM);
        }

        [Test]
        public void TestUTF8_1()
        {
            string s = "ウィキペディアはオープンコンテントの百科事典です。基本方針に賛同し" +
                       "ていただけるなら、誰でも記事を編集したり新しく作成したりできます。" +
                       "ガイドブックを読んでから、サンドボックスで練習してみましょう。質問は" +
                       "利用案内でどうぞ。";
            byte[] buf = Encoding.UTF8.GetBytes(s);
            var result = CharsetDetector.DetectFromBytes(buf);
            Assert.AreEqual(CodepageName.UTF8, result.Detected.EncodingName);
            Assert.AreEqual(1.0f, result.Detected.Confidence);
            Assert.IsFalse(result.Detected.HasBOM);
        }

        [Test]
        public void TestBomUtf8()
        {
            byte[] buf = { 0xEF, 0xBB, 0xBF, 0x68, 0x65, 0x6C, 0x6C, 0x6F, 0x21 };
            var result = CharsetDetector.DetectFromBytes(buf);
            Assert.AreEqual(CodepageName.UTF8, result.Detected.EncodingName);
            Assert.AreEqual(1.0f, result.Detected.Confidence);
            Assert.IsTrue(result.Detected.HasBOM);
        }

        [Test]
        public void Test2byteArrayBomUTF16_BE()
        {
            byte[] buf = { 0xFE, 0xFF, };

            var result = CharsetDetector.DetectFromBytes(buf);
            Assert.AreEqual(CodepageName.UTF16_BE, result.Detected.EncodingName);
            Assert.AreEqual(1.0f, result.Detected.Confidence);
            Assert.IsTrue(result.Detected.HasBOM);
        }

        [Test]
        public void TestBomUTF16_BE()
        {
            byte[] buf = { 0xFE, 0xFF, 0x00, 0x68, 0x00, 0x65 };

            var result = CharsetDetector.DetectFromBytes(buf);
            Assert.AreEqual(CodepageName.UTF16_BE, result.Detected.EncodingName);
            Assert.AreEqual(1.0f, result.Detected.Confidence);
            Assert.IsTrue(result.Detected.HasBOM);
        }

        [Test]
        public void TestBomX_ISO_10646_UCS_4_3412()
        {

            byte[] buf = { 0xFE, 0xFF, 0x00, 0x00, 0x65 };

            var result = CharsetDetector.DetectFromBytes(buf);
            Assert.AreEqual(CodepageName.X_ISO_10646_UCS_4_3412, result.Detected.EncodingName);
            Assert.AreEqual(1.0f, result.Detected.Confidence);
            Assert.IsTrue(result.Detected.HasBOM);
        }

        [Test]
        public void TestBomX_ISO_10646_UCS_4_2143()
        {

            byte[] buf = { 0x00, 0x00, 0xFF, 0xFE, 0x00, 0x65 };

            var result = CharsetDetector.DetectFromBytes(buf);
            Assert.AreEqual(CodepageName.X_ISO_10646_UCS_4_2143, result.Detected.EncodingName);
            Assert.AreEqual(1.0f, result.Detected.Confidence);
            Assert.IsTrue(result.Detected.HasBOM);
        }

        [Test]
        public void Test2byteArrayBomUTF16_LE()
        {
            byte[] buf = { 0xFF, 0xFE, };
            var result = CharsetDetector.DetectFromBytes(buf);
            Assert.AreEqual(CodepageName.UTF16_LE, result.Detected.EncodingName);
            Assert.AreEqual(1.0f, result.Detected.Confidence);
            Assert.IsTrue(result.Detected.HasBOM);
        }

        [Test]
        public void TestBomUTF16_LE()
        {
            byte[] buf = { 0xFF, 0xFE, 0x68, 0x00, 0x65, 0x00 };
            var result = CharsetDetector.DetectFromBytes(buf);
            Assert.AreEqual(CodepageName.UTF16_LE, result.Detected.EncodingName);
            Assert.AreEqual(1.0f, result.Detected.Confidence);
            Assert.IsTrue(result.Detected.HasBOM);
        }

        [Test]
        public void TestBomUTF32_BE()
        {
            byte[] buf = { 0x00, 0x00, 0xFE, 0xFF, 0x00, 0x00, 0x00, 0x68 };
            var result = CharsetDetector.DetectFromBytes(buf);
            Assert.AreEqual(CodepageName.UTF32_BE, result.Detected.EncodingName);
            Assert.AreEqual(1.0f, result.Detected.Confidence);
            Assert.IsTrue(result.Detected.HasBOM);
        }

        [Test]
        public void TestBomUTF32_LE()
        {
            byte[] buf = { 0xFF, 0xFE, 0x00, 0x00, 0x68, 0x00, 0x00, 0x00 };
            var result = CharsetDetector.DetectFromBytes(buf);
            Assert.AreEqual(CodepageName.UTF32_LE, result.Detected.EncodingName);
            Assert.AreEqual(1.0f, result.Detected.Confidence);
            Assert.IsTrue(result.Detected.HasBOM);
        }

        [Test]
        public void TestIssue3()
        {
            byte[] buf = Encoding.UTF8.GetBytes("3");
            var result = CharsetDetector.DetectFromBytes(buf);
            Assert.AreEqual(CodepageName.ASCII, result.Detected.EncodingName);
            Assert.AreEqual(1.0f, result.Detected.Confidence);
            Assert.IsFalse(result.Detected.HasBOM);
        }

        [Test]
        public void TestOutOfRange()
        {
            byte[] buf = Encoding.UTF8.GetBytes("3");
            var result = CharsetDetector.DetectFromBytes(buf);
            Assert.AreEqual(CodepageName.ASCII, result.Detected.EncodingName);
            Assert.AreEqual(1.0f, result.Detected.Confidence);
            Assert.IsFalse(result.Detected.HasBOM);
        }

        [Test]
        public void TestOutOfRange2()
        {
            byte[] buf = Encoding.UTF8.GetBytes("1234567890");
            var result = CharsetDetector.DetectFromBytes(buf);
            Assert.AreEqual(CodepageName.ASCII, result.Detected.EncodingName);
            Assert.AreEqual(1.0f, result.Detected.Confidence);
            Assert.IsFalse(result.Detected.HasBOM);
        }

        [Test]
        public void TestSingleChar()
        {
            byte[] buf = Encoding.UTF8.GetBytes("3");
            var result = CharsetDetector.DetectFromBytes(buf);
            Assert.AreEqual(CodepageName.ASCII, result.Detected.EncodingName);
            Assert.AreEqual(1, result.Detected.Confidence);
            Assert.IsFalse(result.Detected.HasBOM);
        }
    }
}