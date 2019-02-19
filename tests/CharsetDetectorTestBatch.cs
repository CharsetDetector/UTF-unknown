// Author:
//    Rudi Pettazzi <rudi.pettazzi@gmail.com>
//    Julian Verdurmen
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using UtfUnknown.Core;
using NUnit.Framework;

namespace UtfUnknown.Tests
{

    public class CharsetDetectorTestBatch
    {
        private static readonly string TESTS_ROOT = GetTestsPath();
        private static readonly string DATA_ROOT = FindRootPath();
        
        private StreamWriter _logWriter;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public CharsetDetectorTestBatch()
        {
            _logWriter = new StreamWriter(Path.Combine(TESTS_ROOT, "test-diag.log"));
        }

        static string FindRootPath()
        {
            //find Data in Test directory
            string path = TESTS_ROOT;

            var fullPath = Path.Combine(path, "Data");

            if (!Directory.Exists(fullPath))
            {
                throw new DirectoryNotFoundException($"Directory Data with test files not found, path: {fullPath}");
            }

            return fullPath;
        }

        private static string GetTestsPath()
        {
            var path = TestContext.CurrentContext.TestDirectory;

            var directoryName = "TESTS";

            var index = path.IndexOf(directoryName, StringComparison.CurrentCultureIgnoreCase);

            path = path.Substring(0, index + directoryName.Length);

            return path;
        }

        [TestCaseSource(nameof(AllTestFiles))]
        public void TestFile(TestCase testCase)
        {
            TestFile(testCase.ExpectedEncoding, testCase.InputFile.FullName);
        }

        public class TestCase
        {
            /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
            public TestCase(FileInfo inputFile, string expectedEncoding)
            {
                ExpectedEncoding = expectedEncoding;
                InputFile = inputFile;
            }

            public FileInfo InputFile { get; set; }
            public string ExpectedEncoding { get; set; }

            public override string ToString()
            {
                return ExpectedEncoding + ": " + InputFile.Name;
            }
        }

        private static List<TestCase> AllTestFiles()
        {
            var testCases = new List<TestCase>();

            var dirInfo = new DirectoryInfo(DATA_ROOT);
            var dirs = dirInfo.GetDirectories();
            foreach (var dir in dirs)
            {
                testCases.AddRange(CreateTestCases(dir));
            }

            return testCases;
        }

        private static List<TestCase> CreateTestCases(DirectoryInfo dirname)
        {
            //encoding is the directory name  - before the optional '(' 
            var expectedEncoding = dirname.Name.Split('(').First().Trim();

            var files = dirname.GetFiles();
            var cases = files.Select(f => new TestCase(f, expectedEncoding)).ToList();
            return cases;
        }


        private void TestFile(string expectedCharset, string file)
        {
            var result = CharsetDetector.DetectFromFile(file);
            var detected = result.Detected;
            
            _logWriter.WriteLine(string.Format("- {0} ({1}) -> {2}", file, expectedCharset, JsonConvert.SerializeObject(result)));

            StringAssert.AreEqualIgnoringCase(expectedCharset, detected.EncodingName,
                $"Charset detection failed for {file}. Expected: {expectedCharset}, detected: {detected.EncodingName} ({detected.Confidence * 100}% confidence)");
            Assert.NotNull(detected.Encoding);
        }
    }
}
