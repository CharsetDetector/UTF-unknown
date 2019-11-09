namespace UtfUnknown.Core.Models.MultiByte.Korean
{
    public class Iso_2022_KR_SMModel : StateMachineModel
    {   
        private readonly static int[] ISO2022KR_cls = {
            BitPackage.Pack4bits(2,0,0,0,0,0,0,0), // 00 - 07 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0), // 08 - 0f 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0), // 10 - 17 
            BitPackage.Pack4bits(0,0,0,1,0,0,0,0), // 18 - 1f 
            BitPackage.Pack4bits(0,0,0,0,3,0,0,0), // 20 - 27 
            BitPackage.Pack4bits(0,4,0,0,0,0,0,0), // 28 - 2f 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0), // 30 - 37 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0), // 38 - 3f 
            BitPackage.Pack4bits(0,0,0,5,0,0,0,0), // 40 - 47 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0), // 48 - 4f 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0), // 50 - 57 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0), // 58 - 5f 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0), // 60 - 67 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0), // 68 - 6f 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0), // 70 - 77 
            BitPackage.Pack4bits(0,0,0,0,0,0,0,0), // 78 - 7f 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2), // 80 - 87 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2), // 88 - 8f 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2), // 90 - 97 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2), // 98 - 9f 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2), // a0 - a7 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2), // a8 - af 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2), // b0 - b7 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2), // b8 - bf 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2), // c0 - c7 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2), // c8 - cf 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2), // d0 - d7 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2), // d8 - df 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2), // e0 - e7 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2), // e8 - ef 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2), // f0 - f7 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2)  // f8 - ff 
        };

        private readonly static int[] ISO2022KR_st = {
            BitPackage.Pack4bits(START,    3,ERROR,START,START,START,ERROR,ERROR), //00-07 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,ITSME,ITSME,ITSME,ITSME), //08-0f 
            BitPackage.Pack4bits(ITSME,ITSME,ERROR,ERROR,ERROR,    4,ERROR,ERROR), //10-17 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,    5,ERROR,ERROR,ERROR), //18-1f 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ITSME,START,START,START,START)  //20-27 
        };

        private readonly static int[] ISO2022KRCharLenTable = {0, 0, 0, 0, 0, 0};

        public Iso_2022_KR_SMModel() : base(
            new BitPackage(BitPackage.INDEX_SHIFT_4BITS, 
                BitPackage.SHIFT_MASK_4BITS, 
                BitPackage.BIT_SHIFT_4BITS,
                BitPackage.UNIT_MASK_4BITS, ISO2022KR_cls),
            6,
            new BitPackage(BitPackage.INDEX_SHIFT_4BITS, 
                BitPackage.SHIFT_MASK_4BITS, 
                BitPackage.BIT_SHIFT_4BITS,
                BitPackage.UNIT_MASK_4BITS, ISO2022KR_st),
            ISO2022KRCharLenTable, CodepageName.ISO_2022_KR)
        {

        }
    }
}