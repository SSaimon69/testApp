using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace testApp
{
    public partial class editRecord : Form
    {
        int id;
        DateTime date;
        string cname;
        int sum;
        Form1 natForm;

        public editRecord(DataGridViewRow row, Form1 natForm)
        {
            InitializeComponent();
            id = Convert.ToInt32(row.Cells[0].Value);
            date = Convert.ToDateTime(row.Cells[1].Value);
            cname = Convert.ToString(row.Cells[2].Value);
            sum = Convert.ToInt32(row.Cells[3].Value);
            this.natForm = natForm;

            tbCName.Text = cname;
            tbSum.Text = sum.ToString();
            dateTimePicker1.Value = date;
        }

        private void btnEditRecord_Click(object sender, EventArgs e)
        {
            //Проверяем поля
            if (tbCName.Text != "" && int.TryParse(tbSum.Text, out int sum))
            {
                //Записываем новые значения
                cname = tbCName.Text;
                sum = int.Parse(tbSum.Text);
                date = dateTimePicker1.Value;

                //Обновляем и проверяем на успешность
                if (Data.editRec(id, cname, sum, date))
                {
                    MessageBox.Show("Запись успешно изменена");
                    natForm.updateGrid();
                    this.Close();
                }
            }
            else MessageBox.Show("Проверьте заполненность полей");
        }
    }
}
