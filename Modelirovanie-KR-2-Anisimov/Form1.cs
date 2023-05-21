using System;
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

        public bool IsFinished { get; set; }

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
            for (int i = 0; i < dataGrid_A.ColumnCount; i++)
            {
                dataGrid_A[i, 0].Value = 0;
                dataGrid_A.UpdateCellValue(i, 0);
                dataGrid_B[i, 0].Value = 0;
                dataGrid_B.UpdateCellValue(i, 0);
            }

            for(int i = 0; i < dataGridView_A_Result.ColumnCount; i++)
            {
                dataGridView_A_Result[i, 0].Value = 0;
                dataGridView_A_Result.UpdateCellValue(i, 0);
                dataGridView_B_Result[i, 0].Value = 0;
                dataGridView_B_Result.UpdateCellValue(i, 0);
            }

            for(int i = 0; i < dataGridView_C_Result.ColumnCount; i++)
            {
                dataGridView_C_Result[i, 0].Value = 0;
                dataGridView_C_Result.UpdateCellValue(i, 0);
            }

            for(int i = 0; i < dataGridView_Count_Result.ColumnCount; i++)
            {
                dataGridView_Count_Result[i, 0].Value = 0;
                dataGridView_Count_Result.UpdateCellValue(i, 0);
            }
        }

        public void ResetOAInfo()
        {
            for (int j = dataGridView_D_1.ColumnCount - 1; j > 0; j--)
            {
                dataGridView_D_1[j, 0].Value = 0;
                dataGridView_D_1.UpdateCellValue(j, 0);
            }

            for (int j = dataGridView_A.ColumnCount - 1;j > 0; j--)
            {
                dataGridView_A[j, 0].Value = 0;
                dataGridView_A.UpdateCellValue(j, 0);
            }

            for (int j = dataGridView_Y.ColumnCount - 1; j > 0; j--)
            {
                dataGridView_Y[j, 0].Value = 0;
                dataGridView_Y.UpdateCellValue(j, 0);
            }

            for (int j = dataGridView_D_2.ColumnCount - 1; j > 0;j--)
            {
                dataGridView_D_2[j, 0].Value = 0;
                dataGridView_D_2.UpdateCellValue(j, 0);
            }

            for (int j = dataGridView_X.ColumnCount - 1; j > 0; j--)
            {
                dataGridView_X[j, 0].Value = 0;
                dataGridView_X.UpdateCellValue(j, 0);
            }
        }

        public void FinishModel()
        {
            MessageBox.Show("Моделирование закончено!");
            button_Start.Enabled = true;
            button_Tact.Enabled = false;
            UncheckAllButtonInMP();
            ResetCellsValues();
            IsFinished = true;
        }

        public void UpdateResultGrids(ushort A, ushort B, uint C, byte Count)
        {
            var result = Convert.ToString(A, 2).PadLeft(16, '0');
            for (int i = 0, a = 0; i < dataGridView_A_Result.ColumnCount; i++, a++)
            {
                dataGridView_A_Result.Rows[0].Cells[i].Value = result[a];
                dataGridView_A_Result.UpdateCellValue(i, 0);
            }

            result = Convert.ToString(B, 2).PadLeft(16, '0');
            for (int i = 0, b = 0; i < dataGridView_B_Result.ColumnCount; i++, b++)
            {
                dataGridView_B_Result.Rows[0].Cells[i].Value = result[b];
                dataGridView_B_Result.UpdateCellValue(i, 0);
            }

            result = Convert.ToString(C, 2).PadLeft(32, '0');
            for (int i = 0, c = 0; i < dataGridView_C_Result.ColumnCount; i++, c++)
            {
                dataGridView_C_Result.Rows[0].Cells[i].Value = result[c];
                dataGridView_C_Result.UpdateCellValue(i, 0);
            }

            result = Convert.ToString(Count, 2).PadLeft(4, '0');
            for (int i = 0, count = 0; i < dataGridView_Count_Result.ColumnCount; i++, count++)
            {
                dataGridView_Count_Result.Rows[0].Cells[i].Value = result[count];
                dataGridView_Count_Result.UpdateCellValue(i, 0);
            }
        }


        public void UpdateStateGrid(bool[] D)
        {
            for(int i = 0, j = dataGridView_D_1.ColumnCount - 1; i < D.Length && j > 0; i++, j--)
            {
                dataGridView_D_1[j, 0].Value = Convert.ToInt32(D[i]);
                dataGridView_D_1.UpdateCellValue(j, 0);
            }
        }
        
        public void UpdateInfoForOA(bool[] A, bool[] Y, bool[] D, bool[] X)
        {
            for(int i = 0, j = dataGridView_A.ColumnCount - 1; i < A.Length && j > 0; i++, j--)
            {
                dataGridView_A[j, 0].Value = Convert.ToInt32(A[i]);
                dataGridView_A.UpdateCellValue(j, 0);
            }

            for (int i = 0, j = dataGridView_Y.ColumnCount - 1; i < Y.Length && j > 0; i++, j--)
            {
                dataGridView_Y[j, 0].Value = Convert.ToInt32(Y[i]);
                dataGridView_Y.UpdateCellValue(j, 0);
            }

            for (int i = 0, j = dataGridView_D_2.ColumnCount - 1; i < D.Length && j > 0; i++, j--)
            {
                dataGridView_D_2[j, 0].Value = Convert.ToInt32(D[i]);
                dataGridView_D_2.UpdateCellValue(j, 0);
            }

            for (int i = 0, j = dataGridView_X.ColumnCount - 1; i < X.Length && j > 0; i++, j--)
            {
                dataGridView_X[j, 0].Value = Convert.ToInt32(X[i]);
                dataGridView_X.UpdateCellValue(j, 0);
            }
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
            for (int i = 0; i < dataGrid_A.ColumnCount; i++)
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
                _device.Tact();
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
            if (grid[e.ColumnIndex, e.RowIndex].Value == null) grid[e.ColumnIndex, e.RowIndex].Value = "0";
            grid[e.ColumnIndex, e.RowIndex].Value = grid[e.ColumnIndex, e.RowIndex].Value.ToString() == "0" ? "1" : "0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button_Tact.Enabled = false;
            button_Start.Enabled = true;
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            button_Start.Enabled = false;
            button_auto.Enabled = false;
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
            button_Tact.Enabled = true;
        }

        private void button_auto_Click(object sender, EventArgs e)
        {
            button_Start.Enabled = false;
            button_Tact.Enabled = false;
            if (_type)
            {

                // здесь будет происходит моделирование на уровне операционного устройства
                _device= new OperationalDevice(this);
                _device.AutoModeOA();
            }
            else
            {
                // здесь будет происходит моделирование на уровне микропрограммы
                _mp = new MicroProgrammOA(this);
                _mp.AutoModeMP();
            }
        }

        private void button_clear_A_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGrid_A.ColumnCount - 1; i++)
            {
                dataGrid_A[i, 0].Value = 0;
                dataGrid_A.UpdateCellValue(i, 0);
            }
        }

        private void button_clear_B_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGrid_B.ColumnCount - 1; i++)
            {
                dataGrid_B[i, 0].Value = 0;
                dataGrid_B.UpdateCellValue(i, 0);
            }
        }

        private void button_Clear_click(object sender, EventArgs e)
        {
            _device = new OperationalDevice(this);
            _mp = new MicroProgrammOA(this);
            button_Start.Enabled = true;
            button_Tact.Enabled = false;
            ResetCellsValues();
            ResetOAInfo();
            UncheckAllButtonInMP();
        }
    }
}
