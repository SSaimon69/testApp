using System;
using System.Data;
using System.Data.SqlClient;

namespace testApp
{
    public class Data
    {
        SqlConnection connection;
        DataSet allData = new DataSet();
        string conStr = Properties.Settings.Default.conStr;

        public DataSet getDR { get => allData; }

        public Data()
        {
            updateDataSet();
        }

        public void updateDataSet()
        {
            connection = new SqlConnection(conStr);

            connection.Open();

            string com = "SELECT * FROM Invoice";
            SqlDataAdapter adapter = new SqlDataAdapter(com, connection);
            allData = new DataSet();
            adapter.Fill(allData);

            connection.Close();
        }

        //Возвращает сумму по всем записям
        public int getSum()
        {
            int sum = 0;

            foreach(DataRow a in allData.Tables[0].Rows)
            {
                sum += Convert.ToInt32(a.ItemArray[3]);
            }

            return sum;
            
        }

        //Добавление записи
        public static bool addRec(string cname,int sum, DateTime date)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.conStr);

            SqlCommand comm = new SqlCommand("INSERT INTO Invoice(date,cname,amount) Values(@value1,@value2,@value3)",connection);
            comm.Parameters.AddWithValue("@value1", date.Date);
            comm.Parameters.AddWithValue("@value2", cname);
            comm.Parameters.AddWithValue("@value3", sum);

            try
            {
                connection.Open();
                comm.ExecuteNonQuery();
                connection.Close();
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        //Удаление записи
        public static bool delRec(int id)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.conStr);

            SqlCommand comm = new SqlCommand("DELETE FROM Invoice WHERE id =" + id, connection);

            try
            {
            connection.Open();
            comm.ExecuteNonQuery();
            connection.Close();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        //Обновление записи
        public static bool editRec(int id, string cname, int sum, DateTime date)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.conStr);

            SqlCommand comm = new SqlCommand("UPDATE Invoice SET date = @value1, cname = @value2, amount = @value3 WHERE id = @value4 ", connection);
            comm.Parameters.AddWithValue("@value1", date.Date);
            comm.Parameters.AddWithValue("@value2", cname);
            comm.Parameters.AddWithValue("@value3", sum);
            comm.Parameters.AddWithValue("@value4", id);

            try
            {
                connection.Open();
                comm.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        //Возвращает все записи подходящие под шаблон
        public DataSet getSearchRes(string pattern)
        {
            DataSet result = allData.Clone();

            foreach(DataRow row in allData.Tables[0].Rows)
            {
                if (row[2].ToString().StartsWith(pattern)) result.Tables[0].ImportRow(row);
            }
            return result;
        }
    }
}
