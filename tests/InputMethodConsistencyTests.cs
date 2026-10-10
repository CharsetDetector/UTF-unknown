using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using UtfUnknown.Core;

namespace UtfUnknown.Tests;

public class InputMethodConsistencyTests
{
    [TestCase("array")]
    [TestCase("span")]
    [TestCase("slice")]
    public void LateInvalidByteMatchesStreamEarlyDetection(string inputMethod)
    {
        // Streams already stop probing after a confident prefix. A whole-buffer
        // feed sees the late invalid byte before reaching that decision.
        var prefix = Encoding.UTF8.GetBytes(new string('é', 2048));
        var bytes = new byte[prefix.Length + 1];
        prefix.CopyTo(bytes, 0);
        bytes[bytes.Length - 1] = 0xff;

        using var stream = new MemoryStream(bytes);
        var expected = CharsetDetector.DetectFromStream(stream);
        Assert.That(expected.Detected.EncodingName, Is.EqualTo(CodepageName.UTF8));
        Assert.That(stream.Position, Is.LessThan(bytes.Length));
        AssertSameResult(Detect(bytes, inputMethod), expected);
    }

    [Test]
    public void Utf8WithLateEmojiMatchesStream()
    {
        var bytes = Encoding.UTF8.GetBytes(new string('é', 2048) + "😀");
        using var stream = new MemoryStream(bytes);
        AssertSameResult(CharsetDetector.DetectFromBytes(bytes), CharsetDetector.DetectFromStream(stream));
    }

    [TestCase(1023)]
    [TestCase(1024)]
    [TestCase(1025)]
    public void MultibyteCharacterCrossingChunkBoundaryMatchesStream(int prefixLength)
    {
        var bytes = Encoding.UTF8.GetBytes(new string('a', prefixLength) + "€é日本語");
        using var stream = new MemoryStream(bytes);
        var expected = CharsetDetector.DetectFromStream(stream);
        Assert.That(expected.Detected.EncodingName, Is.EqualTo(CodepageName.UTF8));
        AssertSameResult(CharsetDetector.DetectFromBytes(bytes.AsSpan()), expected);
    }

    [Test]
    public async Task ByteDetectionAlsoMatchesAsyncStream()
    {
        var bytes = Encoding.UTF8.GetBytes(new string('é', 2048) + "😀");
        using var stream = new MemoryStream(bytes);
        AssertSameResult(CharsetDetector.DetectFromBytes(bytes), await CharsetDetector.DetectFromStreamAsync(stream));
    }

    [Test]
    public void SelectedSliceStillDetectsBomAndExcludesSurroundingBytes()
    {
        var bytes = new byte[] { 0xff, 0xef, 0xbb, 0xbf, (byte)'a', 0xff };
        var result = CharsetDetector.DetectFromBytes(bytes, 1, 4);
        Assert.That(result.Detected.EncodingName, Is.EqualTo(CodepageName.UTF8));
        Assert.That(result.Detected.Confidence, Is.EqualTo(1.0f));
        Assert.That(result.Detected.HasBOM, Is.True);
    }

    [Test]
    public void EmptyInputStillHasNoDetectedEncoding()
    {
        Assert.That(CharsetDetector.DetectFromBytes(Array.Empty<byte>()).Detected, Is.Null);
        Assert.That(CharsetDetector.DetectFromBytes(ReadOnlySpan<byte>.Empty).Detected, Is.Null);
    }

    [Test]
    public void NullArrayStillThrows()
    {
        Assert.Throws<ArgumentNullException>(() => CharsetDetector.DetectFromBytes((byte[])null));
    }

    private static DetectionResult Detect(byte[] bytes, string inputMethod)
    {
        if (inputMethod == "span")
            return CharsetDetector.DetectFromBytes(bytes.AsSpan());
        if (inputMethod == "slice")
        {
            var surrounded = new byte[bytes.Length + 2];
            surrounded[0] = surrounded[surrounded.Length - 1] = 0x00;
            bytes.CopyTo(surrounded, 1);
            return CharsetDetector.DetectFromBytes(surrounded, 1, bytes.Length);
        }
        return CharsetDetector.DetectFromBytes(bytes);
    }

    private static void AssertSameResult(DetectionResult actual, DetectionResult expected)
    {
        Assert.That(actual.Detected, Is.Not.Null);
        Assert.That(actual.Detected.EncodingName, Is.EqualTo(expected.Detected.EncodingName));
        Assert.That(actual.Detected.Confidence, Is.EqualTo(expected.Detected.Confidence));
        Assert.That(actual.Detected.HasBOM, Is.EqualTo(expected.Detected.HasBOM));
    }
}
