namespace UtfUnknown.Core.Analyzers.Japanese
{
    public class EUCJPDistributionAnalyser : SJISDistributionAnalyser
    {
        /// <summary>
        /// first  byte range: 0xa0 -- 0xfe
        ///  second byte range: 0xa1 -- 0xfe
        /// no validation needed here. State machine has done that
        /// </summary>
        public override int GetOrder(byte[] buf, int offset) 
        { 
            if (buf[offset] >= 0xA0)  
                return 94 * (buf[offset] - 0xA1) + buf[offset+1] - 0xA1;
            else
                return -1;
        }
    }
}