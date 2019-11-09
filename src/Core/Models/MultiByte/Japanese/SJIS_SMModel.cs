using UtfUnknown.Core.Models;

namespace UtfUnknown.Core.Models.MultiByte.Japanese
{
    public class SJIS_SMModel : StateMachineModel
    {
        private readonly static int[] SJIS_cls = {
            //BitPacket.Pack4bits(0,1,1,1,1,1,1,1),  // 00 - 07 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 00 - 07 
            BitPackage.Pack4bits(1,1,1,1,1,1,0,0),  // 08 - 0f 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 10 - 17 
            BitPackage.Pack4bits(1,1,1,0,1,1,1,1),  // 18 - 1f 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 20 - 27 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 28 - 2f 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 30 - 37 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 38 - 3f 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 40 - 47 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 48 - 4f 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 50 - 57 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 58 - 5f 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 60 - 67 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 68 - 6f 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // 70 - 77 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,1),  // 78 - 7f 
            BitPackage.Pack4bits(3,3,3,3,3,3,3,3),  // 80 - 87 
            BitPackage.Pack4bits(3,3,3,3,3,3,3,3),  // 88 - 8f 
            BitPackage.Pack4bits(3,3,3,3,3,3,3,3),  // 90 - 97 
            BitPackage.Pack4bits(3,3,3,3,3,3,3,3),  // 98 - 9f 
            //0xa0 is illegal in sjis encoding, but some pages does 
            //contain such byte. We need to be more error forgiven.
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // a0 - a7     
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // a8 - af 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // b0 - b7 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // b8 - bf 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // c0 - c7 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // c8 - cf 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // d0 - d7 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // d8 - df 
            BitPackage.Pack4bits(3,3,3,3,3,3,3,3),  // e0 - e7 
            BitPackage.Pack4bits(3,3,3,3,3,4,4,4),  // e8 - ef 
            BitPackage.Pack4bits(4,4,4,4,4,4,4,4),  // f0 - f7 
            BitPackage.Pack4bits(4,4,4,4,4,0,0,0)   // f8 - ff 
        };

        private readonly static int[] SJIS_st = {
            BitPackage.Pack4bits(ERROR,START,START,    3,ERROR,ERROR,ERROR,ERROR),//00-07 
            BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,ITSME,ITSME,ITSME,ITSME),//08-0f 
            BitPackage.Pack4bits(ITSME,ITSME,ERROR,ERROR,START,START,START,START) //10-17        
        };

        private readonly static int[] SJISCharLenTable = { 0, 1, 1, 2, 0, 0 };
        
        public SJIS_SMModel() : base(
            new BitPackage(BitPackage.INDEX_SHIFT_4BITS, 
                BitPackage.SHIFT_MASK_4BITS, 
                BitPackage.BIT_SHIFT_4BITS,
                BitPackage.UNIT_MASK_4BITS, SJIS_cls),
            6,
            new BitPackage(BitPackage.INDEX_SHIFT_4BITS, 
                BitPackage.SHIFT_MASK_4BITS, 
                BitPackage.BIT_SHIFT_4BITS,
                BitPackage.UNIT_MASK_4BITS, SJIS_st),
            SJISCharLenTable, CodepageName.SHIFT_JIS)
        {

        }
    }
}