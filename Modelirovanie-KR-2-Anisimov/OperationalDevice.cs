using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public OperationalDevice() {
            _states = new bool[11];
            _currentState = 0;
            _prevState = _currentState;
            _conditions = new bool[7];
            _operations = new bool[12];
        }

       public void StateMemory(byte stateCode)
        {

            _currentState = stateCode; 
            //int stateIndex = BinToDec(stateCode.ToString());
            _states[_prevState] = false;
            _states[_currentState] = true;
            _prevState = _currentState;
        }

        public void ConditionMemory(ushort conditionCode) { 

        }

        /*private int BinToDec(string bin)
        {
            int dec = 0;
            for(int i = 0, j = bin.Length-1; i < bin.Length; i++, j--) {
                dec += bin[i] == '1' ? (int)Math.Pow(2, j) : 0;
            }
            return dec;
        }*/
    }
}
