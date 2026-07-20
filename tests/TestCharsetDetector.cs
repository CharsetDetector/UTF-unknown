using System;

namespace UtfUnknown.Tests;

/// <summary>
/// Exposes the protected members of CharsetDetector for testing.
/// </summary>
public class TestCharsetDetector : CharsetDetector
{
    public new void Feed(ReadOnlySpan<byte> buf) => base.Feed(buf);
    public new DetectionResult DataEnd() => base.DataEnd();
}
