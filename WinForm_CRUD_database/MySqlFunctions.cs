using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MySqlDatabase
{
    public class MySqlFunctions
    {

        public static MySqlConnection GetConnection()
        {
            string sql = "datasource=127.0.0.1;" +
                         "username=root;" +
                         "password=VaSs2021Fr@$;" +
                         "database=myshop";
            MySqlConnection connectSql = new MySqlConnection(sql);

            try
            {
                connectSql.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("MySQL Connection! \n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return connectSql;
        }

        public static void DataToListView(MySqlCommand commandSql, ListView listViewForm, int totalColumns)
        {
            MySqlDataReader readerSql = commandSql.ExecuteReader();

            listViewForm.Items.Clear();

            while (readerSql.Read())
            {
                string[] row = new string[totalColumns];

                for (int n = 1; n <= totalColumns; n++)
                {
                    row[n - 1] = readerSql.GetString(n);
                }

                ListViewItem lv_row = new ListViewItem(row);

                listViewForm.Items.Add(lv_row);
            }
        }
    }
}
