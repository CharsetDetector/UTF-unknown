﻿// Author:
//    Rudi Pettazzi <rudi.pettazzi@gmail.com>
//    Julian Verdurmen
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace UtfUnknown.Tests
{
    public class CharsetDetectorTestBatch : IDisposable
    {
        private const string DIRECTORY_NAME = "TESTS";
        private static readonly string TESTS_ROOT = GetTestsPath();
        private static readonly string DATA_ROOT = FindRootPath();

        private StreamWriter _logWriter;
        private bool _disposed;

        public CharsetDetectorTestBatch()
        {
            string frameworkName = GetCurrentFrameworkName();
            Assert.IsNotEmpty(frameworkName, "Framework name should not be empty");
            _logWriter = new StreamWriter(Path.Combine(TESTS_ROOT, $"test-diag-{frameworkName}.log"));
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

            if (path.IndexOf(DIRECTORY_NAME, StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                path = TruncatePath(path);
            }
            else
            {
                // fix for .netcoreapp2.1 -  TestContext.CurrentContext.TestDirectory is bugged in NUnit under .netcoreapp2.1
                path = TestContext.CurrentContext.WorkDirectory;
                path = TruncatePath(path);
            }

            return path;
        }

        private static string TruncatePath(string path)
        {
            var index = path.IndexOf(DIRECTORY_NAME, StringComparison.CurrentCultureIgnoreCase);
            path = path.Substring(0, index + DIRECTORY_NAME.Length);
            return path;
        }

        [TestCaseSource(nameof(AllTestFiles))]
        public void TestFile(TestCase testCase)
        {
            TestFile(testCase.ExpectedEncoding, testCase.InputFile.FullName);
        }

        [TestCaseSource(nameof(AllTestFiles))]
        public Task TestFileAsync(TestCase testCase)
        {
            return TestFileAsync(testCase.ExpectedEncoding, testCase.InputFile.FullName);
        }

        [TestCaseSource(nameof(AllTestFilesUnsupportedEncoding))]
        public void TestFileUnsupportedEncodings(TestCase testCase)
        {
            var result = CharsetDetector.DetectFromFile(testCase.InputFile.FullName);
            var detected = result.Detected;

            _logWriter.WriteLine(string.Concat(
                $"- {testCase.InputFile.FullName} ({testCase.ExpectedEncoding}) -> ",
                $"{JsonConvert.SerializeObject(result, Formatting.Indented, new EncodingJsonConverter())}"));

            StringAssert.AreEqualIgnoringCase(
                testCase.ExpectedEncoding,
                detected.EncodingName,
                string.Concat(
                    $"Charset detection failed for {testCase.InputFile.FullName}. ",
                    $"Expected: {testCase.ExpectedEncoding}. ",
                    $"Detected: {detected.EncodingName} ",
                    $"({detected.Confidence * 100.0f:0.00############}% confidence)."));
        }

        [TestCaseSource(nameof(AllTestFilesUnsupportedEncoding))]
        public async Task TestFileUnsupportedEncodingsAsync(TestCase testCase)
        {
            var result = await CharsetDetector.DetectFromFileAsync(testCase.InputFile.FullName);
            var detected = result.Detected;

            _logWriter.WriteLine(string.Concat(
                $"- {testCase.InputFile.FullName} ({testCase.ExpectedEncoding}) -> ",
                $"{JsonConvert.SerializeObject(result, Formatting.Indented, new EncodingJsonConverter())}"));

            StringAssert.AreEqualIgnoringCase(
                testCase.ExpectedEncoding,
                detected.EncodingName,
                string.Concat(
                    $"Charset detection failed for {testCase.InputFile.FullName}. ",
                    $"Expected: {testCase.ExpectedEncoding}. ",
                    $"Detected: {detected.EncodingName} ",
                    $"({detected.Confidence * 100.0f:0.00############}% confidence)."));
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

        private static IReadOnlyList<TestCase> AllTestFilesUnsupportedEncoding()
        {
            var path = Path.Combine(TESTS_ROOT, "DataUnsupported");
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException($"Directory Data with test files not found, path: {path}");
            }

            var dirs = new DirectoryInfo(path).GetDirectories();
            var testCases = new List<TestCase>();
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

            _logWriter.WriteLine($"- {file} ({expectedCharset}) -> {JsonConvert.SerializeObject(result, Formatting.Indented, new EncodingJsonConverter())}");
            StringAssert.AreEqualIgnoringCase(expectedCharset, detected.EncodingName,
                $"Charset detection failed for {file}. Expected: {expectedCharset}, detected: {detected.EncodingName} ({detected.Confidence * 100.0f:0.00############}% confidence)");
            Assert.NotNull(detected.Encoding);
        }


        private async Task TestFileAsync(string expectedCharset, string file)
        {
            var result = await CharsetDetector.DetectFromFileAsync(file);
            var detected = result.Detected;

            _logWriter.WriteLine($"- {file} ({expectedCharset}) -> {JsonConvert.SerializeObject(result, Formatting.Indented, new EncodingJsonConverter())}");
            StringAssert.AreEqualIgnoringCase(expectedCharset, detected.EncodingName,
                $"Charset detection failed for {file}. Expected: {expectedCharset}, detected: {detected.EncodingName} ({detected.Confidence * 100.0f:0.00############}% confidence)");
            Assert.NotNull(detected.Encoding);

        }

        private string GetCurrentFrameworkName()
        {
#if NETCOREAPP3_0
                    return "dotnetcore3";
#elif NETCOREAPP2_1
            return "dotnetcore2.1";
#elif NET452
                return "net452";
#else
                return "unknown";
#endif
        }


        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing && _logWriter != null)
            {
                _logWriter.Flush();
                _logWriter.Dispose();
                _logWriter = null;
            }

            _disposed = true;
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            Dispose();
        }
    }
}