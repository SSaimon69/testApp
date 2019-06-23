using System;
using System.Windows.Forms;

namespace testApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Data inv;

        //Обработчик кнопки открытия базы
        private void btnOpenDefDB_Click(object sender, EventArgs e)
        {
            try
            {
                inv = new Data();
            }
            catch(Exception b)
            {
                MessageBox.Show("Произошла ошибка при подключении к базе данных: " + b.Message);
                return;
            }

            dataGridView1.DataSource = inv.getDR.Tables[0];
            lbSum.Text = inv.getSum().ToString();
        }

        //Обновление таблицы (также обновляется и набор из БД)
        public void updateGrid()
        {
            //Очищвем грид
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();

            inv.updateDataSet();

            dataGridView1.DataSource = inv.getDR.Tables[0];
            lbSum.Text = inv.getSum().ToString();
        }

        //Обработчик кнопки добавления записи
        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            addRecord addForm = new addRecord(this);
            addForm.ShowDialog();
        }

        //Обработчик кнопки удаления записи
        private void btnDeleteRecord_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) MessageBox.Show("Вы не выбрали строку");
            else
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                if (DialogResult.OK == MessageBox.Show("Вы действительно хотите удалить запись с id = " + row.Cells[0].Value.ToString() + " ?", "Предупреждение", MessageBoxButtons.OKCancel))
                {
                    Data.delRec(Convert.ToInt32(row.Cells[0].Value));
                    updateGrid();
                }
            }
        }

        //Обработчик кнопки обновления записи
        private void btnEditRecord_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) MessageBox.Show("Вы не выбрали строку");
            else
            {
                editRecord addForm = new editRecord(dataGridView1.SelectedRows[0], this);
                addForm.ShowDialog();
            }
        }

        //Обработчик получения фокуса поля поиска, для стирания плейсхолдера
        private void tbSearch_Enter(object sender, EventArgs e)
        {
            tbSearch.Text = null;
            tbSearch.ForeColor = System.Drawing.Color.Black;
        }

        //Обработчик потери фокуса поля поиска, для добавления плейсхолдера
        private void tbSearch_Leave(object sender, EventArgs e)
        {
            if (tbSearch.Text == "")
            {
                tbSearch.Text = "Поиск по ФИО";
                tbSearch.ForeColor = System.Drawing.Color.Gray;
            }
        }

        //Обработчик изменения в поле поиска для мгновенного изменения таблицы
        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();

            if (tbSearch.Text == "" || tbSearch.Text == "Поиск по ФИО") dataGridView1.DataSource = inv.getDR.Tables[0];
            else dataGridView1.DataSource = inv.getSearchRes(tbSearch.Text).Tables[0];
        }
    }
}
