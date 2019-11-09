using UtfUnknown.Core.Models;

namespace UtfUnknown.Core.Models.MultiByte
{
    public class UCS2BE_SMModel : StateMachineModel
    {
        private readonly static int[] UCS2BE_cls = {
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // 00 - 07 
            BitPackage.Pack4bits(0,0,1,0,0,2,0,0),  // 08 - 0f 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // 10 - 17 
            BitPackage.Pack4bits(0,0,0,3,0,0,0,0),  // 18 - 1f 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // 20 - 27 
            BitPackage.Pack4bits(0,3,3,3,3,3,0,0),  // 28 - 2f 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // 30 - 37 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // 38 - 3f 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // 40 - 47 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // 48 - 4f 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // 50 - 57 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // 58 - 5f 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // 60 - 67 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // 68 - 6f 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // 70 - 77 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // 78 - 7f 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // 80 - 87 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // 88 - 8f 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // 90 - 97 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // 98 - 9f 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // a0 - a7 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // a8 - af 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // b0 - b7 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // b8 - bf 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // c0 - c7 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // c8 - cf 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // d0 - d7 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // d8 - df 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // e0 - e7 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // e8 - ef 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // f0 - f7 
            BitPackage.Pack4bits(0,0,0,0,0,0,4,5)   // f8 - ff 
        };

        private readonly static int[] UCS2BE_st = {
            BitPackage.Pack4bits(    5,    7,    7,ERROR,    4,    3,ERROR,ERROR),//00-07 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,ITSME,ITSME,ITSME,ITSME),//08-0f 
            BitPackage.Pack4bits(ITSME,ITSME,    6,    6,    6,    6,ERROR,ERROR),//10-17 
            BitPackage.Pack4bits(    6,    6,    6,    6,    6,ITSME,    6,    6),//18-1f 
            BitPackage.Pack4bits(    6,    6,    6,    6,    5,    7,    7,ERROR),//20-27 
            BitPackage.Pack4bits(    5,    8,    6,    6,ERROR,    6,    6,    6),//28-2f 
            BitPackage.Pack4bits(    6,    6,    6,    6,ERROR,ERROR,START,START) //30-37 
        };

        private readonly static int[] UCS2BECharLenTable = { 2, 2, 2, 0, 2, 2 };
        
        public UCS2BE_SMModel() : base(
            new BitPackage(BitPackage.INDEX_SHIFT_4BITS, 
                BitPackage.SHIFT_MASK_4BITS, 
                BitPackage.BIT_SHIFT_4BITS,
                BitPackage.UNIT_MASK_4BITS, UCS2BE_cls),
            6,
            new BitPackage(BitPackage.INDEX_SHIFT_4BITS, 
                BitPackage.SHIFT_MASK_4BITS, 
                BitPackage.BIT_SHIFT_4BITS,
                BitPackage.UNIT_MASK_4BITS, UCS2BE_st),
            UCS2BECharLenTable, CodepageName.UTF16_BE)
        {

        }
    }
}