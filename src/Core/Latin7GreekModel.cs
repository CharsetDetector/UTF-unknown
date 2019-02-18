namespace UtfUnknown.Core
{
    public class Latin7GreekModel : GreekModel
    {
        /****************************************************************
        255: Control characters that usually does not exist in any text
        254: Carriage/Return
        253: symbol (punctuation) that does not belong to word
        252: 0 - 9
        *****************************************************************/
        //Character Mapping Table:
        private readonly static byte[] LATIN7_CHAR_TO_ORDER_MAP = {
          CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,RET,CTR,CTR,RET,CTR,CTR, /* 0X */
          CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR, /* 1X */
          SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM, /* 2X */
          NUM,NUM,NUM,NUM,NUM,NUM,NUM,NUM,NUM,NUM,SYM,SYM,SYM,SYM,SYM,SYM, /* 3X */
          SYM, 32, 46, 41, 40, 30, 52, 48, 42, 33, 56, 49, 39, 44, 36, 34, /* 4X */
           47, 59, 35, 38, 37, 43, 54, 50, 58, 53, 57,SYM,SYM,SYM,SYM,SYM, /* 5X */
          SYM, 32, 46, 41, 40, 30, 52, 48, 42, 33, 56, 49, 39, 44, 36, 34, /* 6X */
           47, 59, 35, 38, 37, 43, 54, 50, 58, 53, 57,SYM,SYM,SYM,SYM,CTR, /* 7X */
          CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR, /* 8X */
          CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR,CTR, /* 9X */
          SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,SYM,ILL,SYM, /* AX */
          SYM,SYM,SYM,SYM,SYM,SYM, 17,SYM, 19, 22, 15,SYM, 16,SYM, 24, 28, /* BX */
           55,  0, 25, 18, 20,  5, 29, 10, 26,  3,  8, 14, 13,  4, 31,  1, /* CX */
           11,  6,ILL,  7,  2, 12, 27, 23, 45, 21, 51, 60, 17, 19, 22, 15, /* DX */
           61,  0, 25, 18, 20,  5, 29, 10, 26,  3,  8, 14, 13,  4, 31,  1, /* EX */
           11,  6,  9,  7,  2, 12, 27, 23, 45, 21, 51, 60, 16, 24, 28,ILL, /* FX */
        };
        /*X0  X1  X2  X3  X4  X5  X6  X7  X8  X9  XA  XB  XC  XD  XE  XF */

        public Latin7GreekModel() : base(LATIN7_CHAR_TO_ORDER_MAP, "ISO-8859-7")
        {
        }
    }
}