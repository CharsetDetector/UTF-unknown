namespace UtfUnknown.Core.Models.MultiByte.Korean
{
    public class CP949SMModel : StateMachineModel
    {
        /*
        * 0: Unused
        * 1: 00-40, 5B-60, 7B-7F : Ascii
        * 2: C7-FD
        * 3: C9,FE : User-Defined Area
        * 4: 41-52
        * 5: 53-5A, 61-7A
        * 6: 81-A0
        * 7: A1-AC, B0-C5
        * 8: AD-AF
        * 9: C6
        */

        /* Byte 1
        * Ascii	: 00-7F			: 1 + 4 + 5
        * Case 1: 81-AC, B0-C5	: 6 + 7
        * Case 2: AD-AF			: 8
        * Case 3: C6			: 9
        * Case 4: C7-FE			: 2 (+ 3)
        */

        /* Byte 2
        * Case 1: 41-5A, 61-7A, 81-FE	: 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9
        * Case 2: 41-5A, 61-7A, 81-A0	: 4 + 5 + 6
        * Case 3: 41-52, A1-FE			: 2 + 3 + 4 + 7 + 8 + 9
        * Case 4: A1-FE					: 2 + 3 + 7 + 8 + 9
        */

        private readonly static int[] CP949_cls = {
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 00 - 07 
            BitPackage.Pack4bits(1,1,1,1,1,1,0,0),  // 08 - 0f 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 10 - 17 
            BitPackage.Pack4bits(1,1,1,0,1,1,1,1),  // 18 - 1f 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 20 - 27 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 28 - 2f 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 30 - 37 
            BitPackage.Pack4bits(1,1,1,1,1,1,1,1),  // 38 - 3f 
            BitPackage.Pack4bits(1,4,4,4,4,4,4,4),  // 40 - 47 
            BitPackage.Pack4bits(4,4,4,4,4,4,4,4),  // 48 - 4f 
            BitPackage.Pack4bits(4,4,5,5,5,5,5,5),  // 50 - 57 
            BitPackage.Pack4bits(5,5,5,1,1,1,1,1),  // 58 - 5f 
            BitPackage.Pack4bits(1,5,5,5,5,5,5,5),  // 60 - 67 
            BitPackage.Pack4bits(5,5,5,5,5,5,5,5),  // 68 - 6f 
            BitPackage.Pack4bits(5,5,5,5,5,5,5,5),  // 70 - 77 
            BitPackage.Pack4bits(5,5,5,1,1,1,1,1),  // 78 - 7f 
            BitPackage.Pack4bits(0,6,6,6,6,6,6,6),  // 80 - 87 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,6),  // 88 - 8f 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,6),  // 90 - 97 
            BitPackage.Pack4bits(6,6,6,6,6,6,6,6),  // 98 - 9f 
            BitPackage.Pack4bits(6,7,7,7,7,7,7,7),  // a0 - a7 
            BitPackage.Pack4bits(7,7,7,7,7,8,8,8),  // a8 - af 
            BitPackage.Pack4bits(7,7,7,7,7,7,7,7),  // b0 - b7 
            BitPackage.Pack4bits(7,7,7,7,7,7,7,7),  // b8 - bf 
            BitPackage.Pack4bits(7,7,7,7,7,7,9,2),  // c0 - c7 
            BitPackage.Pack4bits(2,3,2,2,2,2,2,2),  // c8 - cf 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // d0 - d7 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // d8 - df 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // e0 - e7 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // e8 - ef 
            BitPackage.Pack4bits(2,2,2,2,2,2,2,2),  // f0 - f7 
            BitPackage.Pack4bits(2,2,2,2,2,2,3,0)   // f8 - ff 
        };

        /*
            //  0     1     2     3     4     5     6     7     8     9  // Class / Previous State
            // ===================================================================================
            ERROR,START,    6,ERROR,START,START,    3,    3,    4,    5, // START
            ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR, // ERROR
            ITSME,ITSME,ITSME,ITSME,ITSME,ITSME,ITSME,ITSME,ITSME,ITSME, // ITSME
            ERROR,ERROR,START,START,START,START,START,START,START,START, // Case 1
            ERROR,ERROR,ERROR,ERROR,START,START,START,ERROR,ERROR,ERROR, // Case 2
            ERROR,ERROR,START,START,START,ERROR,ERROR,START,START,START, // Case 3
            ERROR,ERROR,START,START,ERROR,ERROR,ERROR,START,START,START, // Case 4
        */

        private readonly static int[] CP949_st = {
			BitPackage.Pack4bits(ERROR,START,    6,ERROR,START,START,    3,    3), // 00 - 07
			BitPackage.Pack4bits(    4,    5,ERROR,ERROR,ERROR,ERROR,ERROR,ERROR), // 08 - 0f
			BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,ITSME,ITSME,ITSME,ITSME), // 10 - 17
			BitPackage.Pack4bits(ITSME,ITSME,ITSME,ITSME,ITSME,ITSME,ERROR,ERROR), // 18 - 1f
			BitPackage.Pack4bits(START,START,START,START,START,START,START,START), // 20 - 27
			BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,START,START,START,ERROR), // 28 - 2f
			BitPackage.Pack4bits(ERROR,ERROR,ERROR,ERROR,START,START,START,ERROR), // 30 - 37
			BitPackage.Pack4bits(ERROR,START,START,START,ERROR,ERROR,START,START), // 38 - 3f
			BitPackage.Pack4bits(ERROR,ERROR,ERROR,START,START,START,    0,    0)  // 40 - 45
        };

        private readonly static int[] CP949CharLenTable = { 0, 1, 2, 0, 1, 1, 2, 2, 0, 2 };
        
        public CP949SMModel() : base(
            new BitPackage(BitPackage.INDEX_SHIFT_4BITS, 
                BitPackage.SHIFT_MASK_4BITS, 
                BitPackage.BIT_SHIFT_4BITS,
                BitPackage.UNIT_MASK_4BITS, CP949_cls),
            10,
            new BitPackage(BitPackage.INDEX_SHIFT_4BITS, 
                BitPackage.SHIFT_MASK_4BITS, 
                BitPackage.BIT_SHIFT_4BITS,
                BitPackage.UNIT_MASK_4BITS, CP949_st),
            CP949CharLenTable, CodepageName.CP949)
        {

        }
    }
}
