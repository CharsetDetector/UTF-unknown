namespace UtfUnknown.Core.Models.MultiByte.Chinese
{
    public class EUCTWSMModel : StateMachineModel
    {
        private readonly static int[] EUCTW_cls = {
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 00 - 07 
            BitPackage.Pack4bits(2,2,2,2,2,2,0,0),  // 08 - 0f 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 10 - 17 
            BitPackage.Pack4bits(2,2,2,0,2,2,2,2),  // 18 - 1f 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 20 - 27 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 28 - 2f 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 30 - 37 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 38 - 3f 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 40 - 47 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 48 - 4f 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 50 - 57 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 58 - 5f 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 60 - 67 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 68 - 6f 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 70 - 77 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 78 - 7f 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // 80 - 87 
            BitPackage.Pack4bits(0,0,0,0,0,0,6,0),  // 88 - 8f 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // 90 - 97 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0),  // 98 - 9f 
            BitPackage.Pack4bits(0,3,4,4,4,4,4,4),  // a0 - a7 
            BitPackage.Pack4bits(5,5,1,1,1,1,1,1),  // a8 - af 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // b0 - b7 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // b8 - bf 
            BitPackage.Pack4bits(1,1,3,1,3,3,3,3),  // c0 - c7 
            BitPackage.Pack4bits(3,3,3,3,3,3,3,3),  // c8 - cf 
            BitPackage.Pack4bits(3,3,3,3,3,3,3,3),  // d0 - d7 
            BitPackage.Pack4bits(3,3,3,3,3,3,3,3),  // d8 - df 
            BitPackage.Pack4bits(3,3,3,3,3,3,3,3),  // e0 - e7 
            BitPackage.Pack4bits(3,3,3,3,3,3,3,3),  // e8 - ef 
            BitPackage.Pack4bits(3,3,3,3,3,3,3,3),  // f0 - f7 
            BitPackage.Pack4bits(3,3,3,3,3,3,3,0)   // f8 - ff 
        };

        private readonly static int[] EUCTW_st = {
            BitPackage.Pack4bits(ERROR,ERROR,START,    3,    3,    3,    4,ERROR),//00-07 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ITSME,ITSME),//08-0f 
            BitPackage.Pack4bits(ITSME,ITSME,ITSME,ITSME,ITSME,ERROR,START,ERROR),//10-17 
            BitPackage.Pack4bits(START,START,START,ERROR,ERROR,ERROR,ERROR,ERROR),//18-1f 
            BitPackage.Pack4bits(    5,ERROR,ERROR,ERROR,START,ERROR,START,START),//20-27 
            BitPackage.Pack4bits(START,ERROR,START,START,START,START,START,START) //28-2f 
        };

        private readonly static int[] EUCTWCharLenTable = { 0, 0, 1, 2, 2, 2, 3 };
        
        public EUCTWSMModel() : base(
            new BitPackage(BitPackage.INDEX_SHIFT_4BITS, 
                BitPackage.SHIFT_MASK_4BITS, 
                BitPackage.BIT_SHIFT_4BITS,
                BitPackage.UNIT_MASK_4BITS, EUCTW_cls),
            7,
            new BitPackage(BitPackage.INDEX_SHIFT_4BITS, 
                BitPackage.SHIFT_MASK_4BITS, 
                BitPackage.BIT_SHIFT_4BITS,
                BitPackage.UNIT_MASK_4BITS, EUCTW_st),
            EUCTWCharLenTable, CodepageName.EUC_TW)
        {

        }
    }
}