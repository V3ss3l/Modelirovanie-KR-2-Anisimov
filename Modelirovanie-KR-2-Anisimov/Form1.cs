using System;
using System.Threading;
using System.Windows.Forms;

namespace Modelirovanie_KR_2_Anisimov
{
    public partial class Form1 : Form
    {
        private bool _type = false;
        private DataGridView dataGrid_A;
        private DataGridView dataGrid_B;
        private OperationalDevice _device;
        private MicroProgrammOA _mp;
        public CheckBox[] CheckBoxArr { get; set; }
        public RadioButton[] RadioButtonArr { get; set; }

        public Boolean IsFinished { get; set; }

        public Form1()
        {
            InitializeComponent();
            CheckBoxArr = new CheckBox[]
            {
                checkBox_1, checkBox_2, checkBox_3, checkBox_4, checkBox_5, checkBox_6,
                checkBox_7, checkBox_8, checkBox_9, checkBox_10, checkBox_11, checkBox_12
            };
            RadioButtonArr = new RadioButton[]
            {
                radioButton_A0, radioButton_A1, radioButton_A2, radioButton_A3, radioButton_A4, radioButton_A5,
                radioButton_A6, radioButton_A7, radioButton_A8, radioButton_A9, radioButton_A10, radioButton_A0_1
            };
            dataGrid_A = dataGridView1;
            dataGrid_B = dataGridView2;
            ResetCellsValues();
            IsFinished = false;
        }

        public void ResetCellsValues()
        {
            for (int i = 0; i < dataGrid_A.ColumnCount - 1; i++)
            {
                dataGrid_A[i, 0].Value = 0;
                dataGrid_A.UpdateCellValue(i, 0);
                dataGrid_B[i, 0].Value = 0;
                dataGrid_B.UpdateCellValue(i, 0);
            }
        }

        public void FinishModel()
        {
            MessageBox.Show("Моделирование закончено!");
            button_Start.Enabled = true;
            button_Tact.Enabled = false;
            UncheckAllButtonInMP();
            ResetCellsValues();
        }

        public void UncheckAllButtonInMP()
        {
            foreach (CheckBox checkBox in CheckBoxArr)
            {
                checkBox.Checked = false;
            }
            foreach (RadioButton radioButton in RadioButtonArr)
            {
                radioButton.Checked = false;
            }
        }

        public ushort[] ConvertDataToNumbers()
        {
            string str_1 = "";
            string str_2 = "";
            for (int i = 0; i < dataGrid_A.ColumnCount - 1; i++)
            {
                str_1 += dataGrid_A[i, 0].Value.ToString();
                str_2 += dataGrid_B[i, 0].Value.ToString();
            }
            ushort[] arr = new ushort[2];
            arr[0] = Convert.ToUInt16(str_1, 2);
            arr[1] = Convert.ToUInt16(str_2, 2);
            return arr;
        }

        private void button_Tact_Click(object sender, EventArgs e)
        {
            if (_type)
            {

                // здесь будет происходит моделирование на уровне операционного устройства
            }
            else
            {
                // здесь будет происходит моделирование на уровне микропрограммы
                _mp.MicroProgramm();
            }
        }

        private void comboBox_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Type.SelectedIndex == 0) _type = false;
            else _type = true;
        }

        private void updateValueOfCell(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            //if (e.ColumnIndex == 15) grid[e.ColumnIndex, e.RowIndex].Value = "0";
            if (grid[e.ColumnIndex, e.RowIndex].Value == null) grid[e.ColumnIndex, e.RowIndex].Value = "0";
            grid[e.ColumnIndex, e.RowIndex].Value = grid[e.ColumnIndex, e.RowIndex].Value.ToString() == "0" ? "1" : "0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.button_Tact.Enabled = false;
            this.button_Start.Enabled = true;
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            this.button_Start.Enabled = false;
            if (_type)
            {
                // здесь будет происходит моделирование на уровне операционного устройства
                _device = new OperationalDevice(this);
                _device.Tact();
            }
            else
            {
                // здесь будет происходит моделирование на уровне микропрограммы
                _mp = new MicroProgrammOA(this);
                _mp.MicroProgramm();
            }
            this.button_Tact.Enabled = true;
        }

        private void button_auto_Click(object sender, EventArgs e)
        {
            this.button_Start.Enabled = false;
            this.button_Tact.Enabled = false;
            if (_type)
            {

                // здесь будет происходит моделирование на уровне операционного устройства
            }
            else
            {
                // здесь будет происходит моделирование на уровне микропрограммы
                _mp = new MicroProgrammOA(this);
                while (!IsFinished)
                {
                    Thread.Sleep(1000);
                    _mp.MicroProgramm();
                }
                IsFinished = false;
            }
        }
    }
}
