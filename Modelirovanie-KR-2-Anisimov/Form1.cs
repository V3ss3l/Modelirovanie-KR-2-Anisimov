using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        public Form1()
        {
            InitializeComponent();
            dataGrid_A = dataGridView1;
            dataGrid_B = dataGridView2;
            for(int i = 0; i < dataGrid_A.ColumnCount - 1; i++)
            {
                dataGrid_A[i, 0].Value = 0;
                dataGrid_B[i, 0].Value = 0;
            }
            /*dataGrid_A.DataSource = numbers_1;
            dataGrid_B.DataSource = numbers_2;*/
            _device = new OperationalDevice(this);
           
        }

        public ushort[] ConvertDataToNumbers()
        {
            string str_1 = "";
            string str_2 = "";
            for(int i = dataGrid_A.ColumnCount - 1; i > 0; i--)
            {
                str_1 += dataGrid_A.Rows[0].Cells[i].Value.ToString();
                str_2 += dataGrid_B.Rows[0].Cells[i].Value.ToString();
            }
            ushort[] arr = new ushort[2];
            arr[0] = Convert.ToUInt16(str_1);
            arr[1] = Convert.ToUInt16(str_2);
            return arr;
        }

        private void button_Tact_Click(object sender, EventArgs e)
        {
            if (_type) {
                
                // здесь будет происходит моделирование на уровне операционного устройства
            }
            else
            {
                // здесь будет происходит моделирование на уровне микропрограммы
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
            grid[e.ColumnIndex, e.RowIndex].Value = grid[e.ColumnIndex, e.RowIndex].Value.ToString() == "0" ? "1" : "0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            var arr = ConvertDataToNumbers();
            var model = new Model(arr);
        }
    }
}
