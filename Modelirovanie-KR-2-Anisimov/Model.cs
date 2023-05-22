using System;

namespace Modelirovanie_KR_2_Anisimov
{
    class Model
    {
        public ushort A;
        public uint B;
        public uint C;
        public byte Count;
        public bool PP;
        // массив делегатов, содержащий в себе микрооперации
        public Action[] microOperations { get; set; }

        public Model(ushort A, uint B)
        {
            this.A = A;
            this.B = B;
            C = 0;
            Count = 0;
            PP = false;
            microOperations = new Action[]
            {
                () => { C = (uint) A & 0x7FFF; }, // y1
                () => { A = (ushort)((A & 0x8000) + (B & 0x7FFF)); }, // y2
                () => { C = (uint)(C + (((ushort)(~A) | 0x18000) + 1)); }, // y3
                () =>  { PP = true; }, // y4
                () => { C +=  (uint) A & 0x7FFF; }, // y5
                () => { C <<= 1; }, // y6
                () => { Count = 0; }, //y7
                () => { B = B & 0x10000; }, // y8
                () => { B = (B & 0x10000) + ((B << 1) + ((~C >> 16) & 0x1) & 0xFFFF); }, // y9
                () => { Count = (byte)(Count == 0 ? 15 : Count - 1); }, // y10
                () => { C = B & 0xFFFF; }, // y11
                () => { C += 2; }, // y12
                () => { C = C | 0x10000; }, // y13

            };
        }
        #region Logical Conditions 
        public bool X1() { return (ushort)(A & 0x7FFF) == 0; }

        public bool X2() { return (C & 0xFFFFFFFF) == 0; }

        public bool X3() { return (C & 0x10000) != 0; }

        public bool X4() { return Count == 0; }

        public bool X5() { return (ushort) (B & 0x1) != 0; }

        public bool X6() { return ((A >> 15) ^ (B >> 16)) == 1; }
        #endregion
    }
}
