using System;

namespace UtfUnknown.Core.Analyzers.Japanese;

public class EUCJPContextAnalyser : JapaneseContextAnalyser
{
    private const byte HIRAGANA_FIRST_BYTE = 0xA4;

    protected override int GetOrder(ReadOnlySpan<byte> buf, out int charLen)
    {
        byte high = buf[0];

        //find out current char's byte length
        if (high == 0x8E || high >= 0xA1 && high <= 0xFE)
            charLen = 2;
        else if (high == 0xBF)
            charLen = 3;
        else
            charLen = 1;

        // return its order if it is hiragana
        if (high == HIRAGANA_FIRST_BYTE) {
            byte low = buf[1];
            if (low >= 0xA1 && low <= 0xF3)
                return low - 0xA1;
        }
        return -1;
    }

    protected override int GetOrder(ReadOnlySpan<byte> buf)
    {
        // We are only interested in Hiragana
        if (buf[0] == HIRAGANA_FIRST_BYTE) {
            byte low = buf[1];
            if (low >= 0xA1 && low <= 0xF3)
                return low - 0xA1;
        }
        return -1;
    }
}
