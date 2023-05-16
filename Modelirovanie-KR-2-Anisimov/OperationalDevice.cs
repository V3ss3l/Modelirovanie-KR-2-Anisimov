using System;

namespace Modelirovanie_KR_2_Anisimov
{
    class OperationalDevice
    {
        private bool[] _states; // A1, A2, A3 ...
        private int _currentState;
        private int _prevState;
        private bool[] _conditions; // X1, X2, X3, ...
        private bool[] _operations; // Y1, Y2, Y3, ...
        private bool _PP;
        private Form1 _form;
        private Model _model;

        public OperationalDevice(Form1 form)
        {
            _states = new bool[11];
            _currentState = 0;
            _prevState = _currentState;
            _conditions = new bool[7];
            _operations = new bool[12];
            _form = form;
            _model = new Model(_form.ConvertDataToNumbers());
        }

        public void Tact()
        {
            StateMemory();
            ConditionMemory();
            var buff = Decoder();
        }

        public void StateMemory()
        {
            //int stateIndex = BinToDec(stateCode.ToString());
            _states[_prevState] = false;
            _states[_currentState] = true;
            _prevState = _currentState;
        }

        public void ConditionMemory()
        {

        }

        public int Decoder()
        {
            return BinToDec(_currentState.ToString());
        }

        private int BinToDec(string bin)
        {
            int dec = 0;
            for (int i = 0, j = bin.Length - 1; i < bin.Length; i++, j--)
            {
                dec += bin[i] == '1' ? (int)Math.Pow(2, j) : 0;
            }
            return dec;
        }
    }
}
