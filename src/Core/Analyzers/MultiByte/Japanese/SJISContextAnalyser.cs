using System;

namespace UtfUnknown.Core.Analyzers.Japanese;

public class SJISContextAnalyser : JapaneseContextAnalyser
{
    private const byte HIRAGANA_FIRST_BYTE = 0x82;

    protected override int GetOrder(ReadOnlySpan<byte> buf, out int charLen)
    {
        //find out current char's byte length
        if (buf[0] >= 0x81 && buf[0] <= 0x9F
            || buf[0] >= 0xe0 && buf[0] <= 0xFC)
            charLen = 2;
        else
            charLen = 1;

        // return its order if it is hiragana
        if (buf[0] == HIRAGANA_FIRST_BYTE) {
            byte low = buf[1];
            if (low >= 0x9F && low <= 0xF1)
                return low - 0x9F;
        }
        return -1;
    }

    protected override int GetOrder(ReadOnlySpan<byte> buf)
    {
        // We are only interested in Hiragana
        if (buf[0] == HIRAGANA_FIRST_BYTE) {
            byte low = buf[1];
            if (low >= 0x9F && low <= 0xF1)
                return low - 0x9F;
        }
        return -1;
    }
}
