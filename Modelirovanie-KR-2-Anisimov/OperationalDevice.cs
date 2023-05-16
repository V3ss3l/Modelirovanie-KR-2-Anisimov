using System;

namespace Modelirovanie_KR_2_Anisimov
{
    class OperationalDevice
    {
        private bool[] _As; // A1, A2, A3 ...
        private int _currentState;
        private int _prevState;
        private bool[] _Xs; // X1, X2, X3, ...
        private bool[] _Ys; // Y1, Y2, Y3, ...
        private bool[] _Ds; // D0, D1, D2, D3
        private Form1 _form;
        private Model _model;

        public OperationalDevice(Form1 form)
        {
            _As = new bool[11];
            _currentState = 0;
            _prevState = _currentState;
            _Xs = new bool[7];
            _Ys = new bool[13];
            _Ds = new bool[4];
            _form = form;
            _model = new Model(_form.ConvertDataToNumbers());
        }

        public void Tact()
        {
            StateMemory(Decoder());
            ConditionMemory();
        }

        public void StateMemory(int index)
        {
            _As[_prevState] = false;
            _As[_currentState] = true;
            _prevState = _currentState;
        }

        public void ConditionMemory()
        {
            _Xs[1] = _model.X1();
            _Xs[2] = _model.X2();
            _Xs[3] = _model.X3();
            _Xs[4] = _model.X4();
            _Xs[5] = _model.X5();
            _Xs[7] = _model.X6();
        }

        public void СombCircuitD()
        {
            _Ds[0] = _Ds[1] = _Ds[2] = _Ds[3] = false;

            _Ds[0] = (_As[0] && _Xs[0]) || (_As[2] && _Xs[3]) || _As[4] ||
                (_As[8] && !_Xs[4]) || (_As[6] && _Xs[3]) || (_As[8] && _Xs[4]);

            _Ds[1] = (_As[1] && !_Xs[1] && !_Xs[2]) || (_As[2] && _Xs[3]) || _As[5] ||
                (_As[6] && _Xs[3]) || (_As[9] && _Xs[5]);

            _Ds[2] = _As[3] || _As[4] || (_As[8] && !_Xs[4]) ||
                _As[5] || (_As[6] && _Xs[3]);

            _Ds[3] = (_As[6] && !_Xs[3]) || _As[7] || (_As[8] && _Xs[4]) || (_As[9] && _Xs[5]);
        }

        public void CombCircuitY()
        {

        }

        public void CalculateTerms()
        {

        }

        public int Decoder()
        {
            var _current = 0;
            if (_Ds[0])
            {
                _current = 1;
                _Ds[0] = false;
            }
            if (_Ds[1])
            {
                _current += 2;
                _Ds[1] = false;
            }
            if (_Ds[2])
            {
                _current += 4;
                _Ds[2] = false;
            }
            if (_Ds[3])
            {
                _current += 8;
                _Ds[3] = false;
            }

            return _current;
        }

        /*private int BinToDec(string bin)
        {
            int dec = 0;
            for (int i = 0, j = bin.Length - 1; i < bin.Length; i++, j--)
            {
                dec += bin[i] == '1' ? (int)Math.Pow(2, j) : 0;
            }
            return dec;
        }*/
    }
}
