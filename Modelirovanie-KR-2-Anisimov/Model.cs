using System;

namespace Modelirovanie_KR_2_Anisimov
{
    class Model
    {
        public ushort A;
        public ushort B;
        public uint C;
        public byte Count;
        public bool PP;
        public Action[] microOperations { get; set; }

        public Model(ushort[] arr)
        {
            A = arr[0];
            B = arr[1];
            C = 0;
            Count = 0;
            PP = false;
            microOperations = new Action[]
            {
                () => { C = (uint) A & 0x7FFF; }, // y1
                () => { A = (ushort) (B & 0x7FFF); }, // y2
                () => {
                    var buff = (ushort) ~(A & 0x7FFF);
                    buff = (ushort) (buff | 0xc000);
                    buff += 1;
                    C += buff;
                }, // y3
                () =>  { PP = true; }, // y4
                () => { C +=  (uint) A & 0x7FFF; }, // y5
                () => { C = C << 1; }, // y6
                () => { Count = 0; }, //y7
                () => { B = 0; }, // y8
                () => {
                    ushort number = (ushort)((C << 15 >> 31) == 1 ? 0 : 1);
                    B = (ushort)((B << 1) + number);

                }, // y9
                () => { Count = (byte)(Count == 0 ? 15 : Count - 1); }, // y10
                () => { C = B; }, // y11
                () => { C += 2; }, // y12
                () => { C = C | 0x10000; }, // y13

            };
        }

        public bool X1() { return (ushort)(A & 0x7FFF) == 0; }

        public bool X2() { return (C & 0xFFFFFFFF) == 0; }

        public bool X3() { return (C & 0x10000) != 0; }

        public bool X4() { return Count == 0; }

        public bool X5() { return (ushort) (B & 1) == 1; }

        public bool X6() { return ((A >> 15) ^ (B >> 15)) == 1; }

    }
}
