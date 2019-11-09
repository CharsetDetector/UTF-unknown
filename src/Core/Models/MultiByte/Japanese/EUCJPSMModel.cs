using UtfUnknown.Core.Models;

namespace UtfUnknown.Core.Models.MultiByte.Japanese
{
    public class EUCJPSMModel : StateMachineModel
    {
        private readonly static int[] EUCJP_cls = {
            //BitPacket.Pack4bits(5,4,4,4,4,4,4,4),  // 00 - 07 
            BitPackage.Pack4bits(4,4,4,4,4,4,4,4),  // 00 - 07 
            BitPackage.Pack4bits(4,4,4,4,4,4,5,5),  // 08 - 0f 
            BitPackage.Pack4bits(4,4,4,4,4,4,4,4),  // 10 - 17 
            BitPackage.Pack4bits(4,4,4,5,4,4,4,4),  // 18 - 1f 
            BitPackage.Pack4bits(4,4,4,4,4,4,4,4),  // 20 - 27 
            BitPackage.Pack4bits(4,4,4,4,4,4,4,4),  // 28 - 2f 
            BitPackage.Pack4bits(4,4,4,4,4,4,4,4),  // 30 - 37 
            BitPackage.Pack4bits(4,4,4,4,4,4,4,4),  // 38 - 3f 
            BitPackage.Pack4bits(4,4,4,4,4,4,4,4),  // 40 - 47 
            BitPackage.Pack4bits(4,4,4,4,4,4,4,4),  // 48 - 4f 
            BitPackage.Pack4bits(4,4,4,4,4,4,4,4),  // 50 - 57 
            BitPackage.Pack4bits(4,4,4,4,4,4,4,4),  // 58 - 5f 
            BitPackage.Pack4bits(4,4,4,4,4,4,4,4),  // 60 - 67 
            BitPackage.Pack4bits(4,4,4,4,4,4,4,4),  // 68 - 6f 
            BitPackage.Pack4bits(4,4,4,4,4,4,4,4),  // 70 - 77 
            BitPackage.Pack4bits(4,4,4,4,4,4,4,4),  // 78 - 7f 
            BitPackage.Pack4bits(5,5,5,5,5,5,5,5),  // 80 - 87 
            BitPackage.Pack4bits(5,5,5,5,5,5,1,3),  // 88 - 8f 
            BitPackage.Pack4bits(5,5,5,5,5,5,5,5),  // 90 - 97 
            BitPackage.Pack4bits(5,5,5,5,5,5,5,5),  // 98 - 9f 
            BitPackage.Pack4bits(5,2,2,2,2,2,2,2),  // a0 - a7 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // a8 - af 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // b0 - b7 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // b8 - bf 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // c0 - c7 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // c8 - cf 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // d0 - d7 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // d8 - df 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // e0 - e7 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // e8 - ef 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // f0 - f7 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,5)   // f8 - ff 
        };

        private readonly static int[] EUCJP_st = {
            BitPackage.Pack4bits(    3,    4,    3,    5,START,ERROR,ERROR,ERROR),//00-07 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,ITSME,ITSME,ITSME,ITSME),//08-0f 
            BitPackage.Pack4bits(ITSME,ITSME,START,ERROR,START,ERROR,ERROR,ERROR),//10-17 
            BitPackage.Pack4bits(ERROR,ERROR,START,ERROR,ERROR,ERROR,    3,ERROR),//18-1f 
            BitPackage.Pack4bits(    3,ERROR,ERROR,ERROR,START,START,START,START) //20-27 
        };

        private readonly static int[] EUCJPCharLenTable = { 2, 2, 2, 3, 1, 0 };
        
        public EUCJPSMModel() : base(
            new BitPackage(BitPackage.INDEX_SHIFT_4BITS, 
                BitPackage.SHIFT_MASK_4BITS, 
                BitPackage.BIT_SHIFT_4BITS,
                BitPackage.UNIT_MASK_4BITS, EUCJP_cls),
            6,
            new BitPackage(BitPackage.INDEX_SHIFT_4BITS, 
                BitPackage.SHIFT_MASK_4BITS, 
                BitPackage.BIT_SHIFT_4BITS,
                BitPackage.UNIT_MASK_4BITS, EUCJP_st),
            EUCJPCharLenTable, CodepageName.EUC_JP)
        {

        }
    }
}