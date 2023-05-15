using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelirovanie_KR_2_Anisimov
{
    class MicroProgrammOA
    {
        private Form1 _form;
        private byte _state;
        private Model _model;
        MicroProgrammOA(Form1 form) {
            _form = form;
            var arr = _form.ConvertDataToNumbers();
            _model = new Model(arr);
            _state = 0;
        }
        public void MicroProgramm()
        {
            switch (_state)
            {
                case 0:
                {
                    _model.microOperations[0]();
                    _model.microOperations[1]();
                    _state = 1;
                    break;
                }
                /*case 1: 
                { 
                    if(_model.Check_A())
                }
                case 2: { }
                case 3: { }
                case 4: { }
                case 5: { }
                case 6: { }
                case 7: { }
                case 8: { }
                case 9: { }
                case 10: { }*/

            }
        }
    }
}
