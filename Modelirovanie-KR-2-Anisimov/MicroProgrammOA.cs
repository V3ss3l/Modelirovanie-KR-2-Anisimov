using System.Threading;

namespace Modelirovanie_KR_2_Anisimov
{
    class MicroProgrammOA
    {
        private Form1 _form;
        private byte _state;
        private readonly Model _model;
        public MicroProgrammOA(Form1 form1)
        {
            _form = form1;
            //ushort[] arr = new ushort[] { 1, 4 };
            var arr = _form.ConvertDataToNumbers();
            _model = new Model(arr);
            _state = 0;
        }
        public void MicroProgramm()
        {
            _form.UncheckAllButtonInMP();
            switch (_state)
            {
                case 0:
                    {
                        _form.RadioButtonArr[0].Checked = true;
                        _model.microOperations[0]();
                        _model.microOperations[1]();
                        Thread.Sleep(500);
                        _form.CheckBoxArr[0].Checked = true;
                        _state = 1;
                        break;
                    }
                case 1:
                    {
                        _form.RadioButtonArr[1].Checked = true;
                        if (!_model.X1())
                        {
                            if (!_model.X2())
                            {
                                _model.microOperations[2]();
                                Thread.Sleep(500);
                                _form.CheckBoxArr[1].Checked = true;
                                _state = 2;

                            }
                            else if (_model.X2()) _state = 11;
                        }
                        else
                        {
                            _model.microOperations[3]();
                            Thread.Sleep(500);
                            _form.CheckBoxArr[2].Checked = true;
                            _state = 11;
                        }

                        break;
                    }
                case 2:
                    {
                        _form.RadioButtonArr[2].Checked = true;
                        if (_model.X3())
                        {
                            _model.microOperations[4]();
                            Thread.Sleep(500);
                            _form.CheckBoxArr[3].Checked = true;
                            _state = 3;
                        }
                        else
                        {
                            _model.microOperations[3]();
                            Thread.Sleep(500);
                            _form.CheckBoxArr[2].Checked = true;
                            _state = 11;
                        }
                        break;
                    }
                case 3:
                    {
                        _form.RadioButtonArr[3].Checked = true;
                        _model.microOperations[5]();
                        _model.microOperations[6]();
                        _model.microOperations[7]();
                        Thread.Sleep(500);
                        _form.CheckBoxArr[4].Checked = true;
                        _state = 4;
                        break;
                    }
                case 4:
                    {

                        _form.RadioButtonArr[4].Checked = true;
                        _model.microOperations[2]();
                        Thread.Sleep(500);
                        _form.CheckBoxArr[5].Checked = true;
                        _state = 5;
                        break;
                    }
                case 5:
                    {

                        _form.RadioButtonArr[5].Checked = true;
                        _model.microOperations[8]();
                        Thread.Sleep(500);
                        _form.CheckBoxArr[6].Checked = true;
                        _state = 6;
                        break;
                    }
                case 6:
                    {

                        _form.RadioButtonArr[6].Checked = true;
                        if (_model.X3())
                        {
                            _model.microOperations[4]();
                            Thread.Sleep(500);
                            _form.CheckBoxArr[7].Checked = true;
                            _state = 7;
                        }
                        else
                        {
                            _model.microOperations[5]();
                            _model.microOperations[9]();
                            Thread.Sleep(500);
                            _form.CheckBoxArr[8].Checked = true;
                            _state = 8;
                        }
                        break;
                    }
                case 7:
                    {

                        _form.RadioButtonArr[7].Checked = true;
                        _model.microOperations[5]();
                        _model.microOperations[9]();
                        Thread.Sleep(500);
                        _form.CheckBoxArr[8].Checked = true;
                        _state = 8;
                        break;
                    }
                case 8:
                    {

                        _form.RadioButtonArr[8].Checked = true;
                        if (_model.X4())
                        {
                            _model.microOperations[10]();
                            Thread.Sleep(500);
                            _form.CheckBoxArr[9].Checked = true;
                            _state = 9;
                        }
                        else
                        {
                            _model.microOperations[2]();
                            Thread.Sleep(500);
                            _form.CheckBoxArr[5].Checked = true;
                            _state = 5;
                        }
                        break;
                    }
                case 9:
                    {

                        _form.RadioButtonArr[9].Checked = true;
                        if (_model.X5())
                        {
                            _model.microOperations[11]();
                            Thread.Sleep(500);
                            _form.CheckBoxArr[10].Checked = true;
                            _state = 10;
                        }
                        else
                        {
                            if (_model.X6())
                            {
                                _model.microOperations[12]();
                                Thread.Sleep(500);
                                _form.CheckBoxArr[11].Checked = true;
                                _state = 11;
                            }
                            else _state = 11;
                        }
                        break;
                    }
                case 10:
                    {

                        _form.RadioButtonArr[10].Checked = true;
                        if (_model.X6())
                        {
                            _model.microOperations[12]();
                            Thread.Sleep(500);
                            _form.CheckBoxArr[11].Checked = true;
                            _state = 11;
                        }
                        else _state = 11;
                        break;
                    }
                case 11:
                    {
                        _form.RadioButtonArr[11].Checked = true;
                        _state = 0;
                        _form.IsFinished = true;
                        _form.FinishModel();
                        break;
                    }

            }
            _form.UpdateResultGrids(_model.A, _model.B, _model.C, _model.Count);
        }
    }
}
