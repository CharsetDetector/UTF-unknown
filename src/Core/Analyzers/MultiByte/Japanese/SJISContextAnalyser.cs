namespace UtfUnknown.Core.Analyzers.Japanese
{
    public class SJISContextAnalyser : JapaneseContextAnalyser
    {
        private const byte HIRAGANA_FIRST_BYTE = 0x82;

        protected override int GetOrder(byte[] buf, int offset, out int charLen)
        {
            //find out current char's byte length
            if (buf[offset] >= 0x81 && buf[offset] <= 0x9F 
                || buf[offset] >= 0xe0 && buf[offset] <= 0xFC)
                charLen = 2;
            else 
                charLen = 1;

            // return its order if it is hiragana
            if (buf[offset] == HIRAGANA_FIRST_BYTE) {
                byte low = buf[offset+1];
                if (low >= 0x9F && low <= 0xF1)
                    return low - 0x9F;
            }
            return -1;                    
        }

        protected override int GetOrder(byte[] buf, int offset)
        {
            // We are only interested in Hiragana
            if (buf[offset] == HIRAGANA_FIRST_BYTE) {
                byte low = buf[offset+1];
                if (low >= 0x9F && low <= 0xF1)
                    return low - 0x9F;
            }
            return -1;
        }

    }
}