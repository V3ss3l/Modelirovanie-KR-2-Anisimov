using System;

namespace Modelirovanie_KR_2_Anisimov
{
    class Model
    {
        public ushort _A;
        public ushort _B;
        public uint _C;
        public byte _Count;
        public bool PP;
        public Action[] microOperations { get; set; }

        public Model(ushort[] arr)
        {
            _A = arr[0];
            _B = arr[1];
            _C = 0;
            _Count = 0;
            PP = false;
            microOperations = new Action[]
            {
                () => { _C = (uint) _A & 0x7FFF; }, // y1
                () => { _A = (ushort) (_B & 0x7FFF); }, // y2
                () => {
                    var buff = (ushort) ~(_A & 0x7FFF);
                    buff += 0xc000;
                    _C = _C + buff + 1;
                }, // y3
                () =>  { PP = true; }, // y4
                () => { _C +=  (uint) _A & 0x7FFF; }, // y5
                () => { _C = _C << 1; }, // y6
                () => { _Count = 0; }, //y7
                () => { _B = 0; }, // y8
                () => {
                    ushort number = (ushort)((_C << 15 >> 31) == 1 ? 0 : 1);
                    _B = (ushort)((_B << 1) + number);

                }, // y9
                () => { _Count = (byte)(_Count - 1);
                /*(byte)(_Count == 0 ? 15 :*/ }, // y10
                () => { _C = _B; }, // y11
                () => { _C += 2; }, // y12
                () => { _C = _C | 0x10000; }, // y13

            };
        }

        public bool X1() { return ((ushort)(_A << 1 >> 1) & 0x7FFF) == 0; }

        public bool X2() { return (_C & 0xFFFFFFFF) == 0; }

        public bool X3() { return (_C << 15 >> 31) == 1; }

        public bool X4() { return _Count == 0; }

        public bool X5() { return ((ushort)(_B << 15 >> 15)) == 1; }

        public bool X6() { return ((_A >> 15) ^ (_B >> 15)) == 1; }

    }
}
