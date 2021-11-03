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

        public static MySqlConnection GetConnection(string datasource, string username,
            string password, string database)
        {
            string sql = "datasource=" + datasource +
                         ";username=" + username +
                         ";password=" + password +
                         ";database=" + database;
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

        public static void DataToListView(MySqlCommand commandSql, ListView listViewForm,
            int totalColumns, bool removeFirstColum = true)
        {
            MySqlDataReader readerSql = commandSql.ExecuteReader();

            listViewForm.Items.Clear();

            while (readerSql.Read())
            {
                string[] row = new string[totalColumns];

                for (int n = 1; n <= totalColumns; n++)
                {
                    if (removeFirstColum)
                    {
                        row[n - 1] = readerSql.GetString(n);
                    }
                    else
                    {
                        row[n] = readerSql.GetString(n);
                    }
                }

                ListViewItem lv_row = new ListViewItem(row);

                listViewForm.Items.Add(lv_row);
            }
        }
    }
}
