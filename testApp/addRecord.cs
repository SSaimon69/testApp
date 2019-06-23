using System;
using System.Windows.Forms;

namespace testApp
{
    public partial class addRecord : Form
    {
        Form1 natForm;

        public addRecord(Form1 natForm)
        {
            InitializeComponent();
            this.natForm = natForm;
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            if (tbCName.Text != "" && int.TryParse(tbSum.Text, out int sum))
            {
                if (Data.addRec(tbCName.Text, sum, dateTimePicker1.Value))
                {
                    MessageBox.Show("Запись успешно добаленна");
                    natForm.updateGrid();
                    this.Close();
                }
            }
            else MessageBox.Show("Проверьте заполненность полей");
        }
    }
}
