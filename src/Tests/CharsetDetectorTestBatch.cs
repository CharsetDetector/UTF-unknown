// CharsetDetectorTestBatch.cs created with MonoDevelop
//
// Author:
//    Rudi Pettazzi <rudi.pettazzi@gmail.com>
//

using System.IO;
using Xunit;

namespace Ude.Tests
{

    public class CharsetDetectorTestBatch
    {
        // Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location)
        const string DATA_ROOT = "../../Data";



        [Fact]
        public void TestLatin1()
        {
            Process(Charsets.WIN1252, "latin1");
        }

        [Fact]
        public void TestCJK()
        {
            Process(Charsets.GB18030, "gb18030");
            Process(Charsets.BIG5, "big5");
            Process(Charsets.SHIFT_JIS, "shiftjis");
            Process(Charsets.EUCJP, "eucjp");
            Process(Charsets.EUCKR, "euckr");
            Process(Charsets.EUCTW, "euctw");
            Process(Charsets.ISO2022_JP, "iso2022jp");
            Process(Charsets.ISO2022_KR, "iso2022kr");
        }

        [Fact]
        public void TestHebrew()
        {
            Process(Charsets.WIN1255, "windows1255");
        }

        [Fact]
        public void TestGreek()
        {
            Process(Charsets.ISO_8859_7, "iso88597");
            //Process(Charsets.WIN1253, "windows1253");
        }

        [Fact]
        public void TestCyrillic()
        {
            Process(Charsets.WIN1251, "windows1251");
            Process(Charsets.KOI8R, "koi8r");
            Process(Charsets.IBM855, "ibm855");
            Process(Charsets.IBM866, "ibm866");
            Process(Charsets.MAC_CYRILLIC, "maccyrillic");
        }



        [Fact]
        public void TestUTF8()
        {
            Process(Charsets.UTF8, "utf8");
        }

        private static void Process(string charset, string dirname)
        {
            string path = Path.Combine(DATA_ROOT, dirname);
            if (!Directory.Exists(path))
                return;

            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            {

                var detector = new CharsetDetector();

                var result = detector.GetFromFile(file);
                var detected = result.Detected;
                Assert.True(charset == detected.Charset, string.Format("Charset detection failed for {0}. Expected: {1}, detected: {2} ({3}% confidence)", file, charset, detected.Charset, detected.Confidence * 100));

            }
        }
    }
}

