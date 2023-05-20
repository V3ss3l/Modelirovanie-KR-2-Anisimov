using System;

namespace Modelirovanie_KR_2_Anisimov
{
    class OperationalDevice
    {
        private bool[] _As; // A1, A2, A3 ...
        //private int _currentState;
        private int _prevState;
        private bool[] _Xs; // X1, X2, X3, ...
        private bool[] _Ys; // Y1, Y2, Y3, ...
        private bool[] _Ds; // D0, D1, D2, D3
        private bool[] _Ts; // T1, T2, T3, ...
        private Form1 _form;
        private Model _model;

        public OperationalDevice(Form1 form)
        {
            _As = new bool[11];
            _prevState = 0;
            _Xs = new bool[7];
            _Xs[0] = true;
            _Ys = new bool[13];
            _Ds = new bool[4];
            _Ts = new bool[20];
            _form = form;
            _model = new Model(_form.ConvertDataToNumbers());
        }

        public void Tact()
        {
            _form.UpdateStateGrid(_Ds);

            StateMemory(Decoder(_Ds));
            CalculateTerms();
            CombCircuitY();
            СombCircuitD(_Ds);
            OperationalAutomaton();
            ConditionMemory();

            _form.UpdateInfoForOA(_As, _Ys, _Ds, _Xs);
            _form.UpdateResultGrids(_model.A, _model.B, _model.C, _model.Count);
        }

        private void StateMemory(int index)
        {
            _As[_prevState] = false;
            _As[index] = true;
            _prevState = index;
        }

        private void OperationalAutomaton()
        {
            for(int i = 0; i < _Ys.Length; i++)
            {
                if (_Ys[i]) {
                    _model.microOperations[i]();
                }
            }
        }

        // TODO: Проанализровать данный код, так как возможны ошибки при моделировании
        private void ConditionMemory()
        {
            _Xs[1] = _model.X1();
            _Xs[2] = _model.X2();
            _Xs[3] = _model.X3();
            _Xs[4] = _model.X4();
            _Xs[5] = _model.X5();
            _Xs[6] = _model.X6();
        }

        private void СombCircuitD(bool[] D)
        {
            D[0] = D[1] = D[2] = D[3] = false;

            D[0] = _Ts[8] || _Ts[10] || _Ts[12] ||
                _Ts[13] || _Ts[15] || _Ts[18];

            D[1] = _Ts[9] || _Ts[10] || _Ts[14] ||
                _Ts[15] || _Ts[19];

            D[2] = _Ts[11] || _Ts[12] || _Ts[13] ||
                _Ts[14] || _Ts[15];

            D[3] = _Ts[16] || _Ts[17] || _Ts[18] || _Ts[19];
        }

        private void CalculateTerms()
        {
            for (int i = 0; i < _Ts.Length; i++) _Ts[i] = false;

            _Ts[0] = _As[0] && !_Xs[0];
            _Ts[1] = _As[1] && _Xs[1];
            _Ts[2] = _As[1] && !_Xs[1] && _Xs[2];
            _Ts[3] = _As[2] && !_Xs[3];
            _Ts[4] = _As[9] && !_Xs[5] && !_Xs[6];
            _Ts[5] = _As[9] && !_Xs[5] && _Xs[6];
            _Ts[6] = _As[10] && !_Xs[6];
            _Ts[7] = _As[10] && _Xs[6];
            _Ts[8] = _As[0] && _Xs[0];
            _Ts[9] = _As[1] && !_Xs[1] && !_Xs[2];
            _Ts[10] = _As[2] && _Xs[3];
            _Ts[11] = _As[3];
            _Ts[12] = _As[4];
            _Ts[13] = _As[8] && !_Xs[4];
            _Ts[14] = _As[5];
            _Ts[15] = _As[6] && _Xs[3];
            _Ts[16] = _As[6] && !_Xs[3];
            _Ts[17] = _As[7];
            _Ts[18] = _As[8] && _Xs[4];
            _Ts[19] = _As[9] && _Xs[5];
        }

        private void CombCircuitY()
        {
            _Ys[0] = _Ts[8];
            _Ys[1] = _Ts[8];
            _Ys[2] = _Ts[9] || _Ts[12] || _Ts[13];
            _Ys[3] = _Ts[1] || _Ts[3];
            _Ys[4] = _Ts[10] || _Ts[15];
            _Ys[5] = _Ts[11] || _Ts[16] || _Ts[17];
            _Ys[6] = _Ts[11];
            _Ys[7] = _Ts[11];
            _Ys[8] = _Ts[14];
            _Ys[9] = _Ts[16] || _Ts[17];
            _Ys[10] = _Ts[18]; 
            _Ys[11] = _Ts[19];
            _Ys[12] = _Ts[5] || _Ts[7];
        }



        private int Decoder(bool[] D)
        {
            var _current = 0;
            if (D[0])
            {
                _current = 1;
                D[0] = false;
            }
            if (D[1])
            {
                _current += 2;
                D[1] = false;
            }
            if (D[2])
            {
                _current += 4;
                D[2] = false;
            }
            if (D[3])
            {
                _current += 8;
                D[3] = false;
            }

            return _current;
        }

    }
}
