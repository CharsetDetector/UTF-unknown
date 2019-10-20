using System;
using System.IO;
using UtfUnknown;

namespace ConsoleExample
{
    public class DetectFile
    {
        /// <summary>
        /// Command line example: detects the encoding of the given file.
        /// </summary>
        /// <param name="args">a filename</param>
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: ConsoleExample <filename>");
                return;
            }

            var filename = args[0];
            if (!File.Exists(filename))
            {
                Console.WriteLine($"File not found: {filename}");
                return;
            }

            var result = CharsetDetector.DetectFromFile(filename);
            var message = result.Detected != null
                ? $"Detected encoding {result.Detected.Encoding.WebName} with confidence {result.Detected.Confidence}."
                : $"Detection failed: {filename}";
            Console.WriteLine(message);
        }
    }
}
