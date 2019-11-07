using UtfUnknown.Core.Models;

namespace UtfUnknown.Core.Models.MultiByte.Chinese
{
    public class GB18030_SMModel : StateMachineModel
    {
        private readonly static int[] GB18030_cls = {
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 00 - 07 
            BitPackage.Pack4bits(1,1,1,1,1,1,0,0),  // 08 - 0f 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 10 - 17 
            BitPackage.Pack4bits(1,1,1,0,1,1,1,1),  // 18 - 1f 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 20 - 27 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 28 - 2f 
            BitPackage.Pack4bits(3,3,3,3,3,3,3,3),  // 30 - 37 
            BitPackage.Pack4bits(3,3,1,1,1,1,1,1),  // 38 - 3f 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 40 - 47 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 48 - 4f 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 50 - 57 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 58 - 5f 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 60 - 67 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 68 - 6f 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 70 - 77 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,4),  // 78 - 7f 
            BitPackage.Pack4bits(5,6,6,6,6,6,6,6),  // 80 - 87 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,6),  // 88 - 8f 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,6),  // 90 - 97 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,6),  // 98 - 9f 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,6),  // a0 - a7 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,6),  // a8 - af 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,6),  // b0 - b7 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,6),  // b8 - bf 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,6),  // c0 - c7 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,6),  // c8 - cf 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,6),  // d0 - d7 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,6),  // d8 - df 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,6),  // e0 - e7 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,6),  // e8 - ef 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,6),  // f0 - f7 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,0)   // f8 - ff 
        };

        private readonly static int[] GB18030_st = {
            BitPackage.Pack4bits(ERROR,START,START,START,START,START,    3,ERROR),//00-07 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ITSME,ITSME),//08-0f 
            BitPackage.Pack4bits(ITSME,ITSME,ITSME,ITSME,ITSME,ERROR,ERROR,START),//10-17 
            BitPackage.Pack4bits(    4,ERROR,START,START,ERROR,ERROR,ERROR,ERROR),//18-1f 
            BitPackage.Pack4bits(ERROR,ERROR,    5,ERROR,ERROR,ERROR,ITSME,ERROR),//20-27 
            BitPackage.Pack4bits(ERROR,ERROR,START,START,START,START,START,START) //28-2f 
        };

        // To be accurate, the length of class 6 can be either 2 or 4. 
        // But it is not necessary to discriminate between the two since 
        // it is used for frequency analysis only, and we are validating 
        // each code range there as well. So it is safe to set it to be 
        // 2 here. 
        private readonly static int[] GB18030CharLenTable = {0, 1, 1, 1, 1, 1, 2};
        
        public GB18030_SMModel() : base(
            new BitPackage(BitPackage.INDEX_SHIFT_4BITS, 
                BitPackage.SHIFT_MASK_4BITS, 
                BitPackage.BIT_SHIFT_4BITS,
                BitPackage.UNIT_MASK_4BITS, GB18030_cls),
            7,
            new BitPackage(BitPackage.INDEX_SHIFT_4BITS, 
                BitPackage.SHIFT_MASK_4BITS, 
                BitPackage.BIT_SHIFT_4BITS,
                BitPackage.UNIT_MASK_4BITS, GB18030_st),
            GB18030CharLenTable, CodepageName.GB18030)
        {

        }
    }
}