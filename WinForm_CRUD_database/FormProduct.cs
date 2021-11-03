using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySqlDatabase;
using ListViews;

namespace WinForm_CRUD_database
{
    public partial class FormProducts : Form, IListView
    {
        MySqlConnection connectSql;
        MySqlCommand commandSql;

        public FormProducts()
        {
            InitializeComponent();
            UpdateList();
        }

        public void Clean()
        {
            textBoxProduct.Clear();
            textBoxQuantity.Clear();
            textBoxValue.Clear();
            textBoxProduct.Focus();
        }

        public void Obtain()
        {
            textBoxProduct.Text = listViewProducts.SelectedItems[0].SubItems[0].Text;
            textBoxQuantity.Text = listViewProducts.SelectedItems[0].SubItems[1].Text;
            textBoxValue.Text = listViewProducts.SelectedItems[0].SubItems[2].Text;
        }

        public void UpdateList()
        {
            string querySql = "SELECT * FROM products";
            connectSql = MySqlFunctions.GetConnection();
            commandSql = new MySqlCommand(querySql, connectSql);

            MySqlFunctions.DataToListView(commandSql, listViewProducts, 3);

            connectSql.Close();
        }

        private void PictureBoxSearchClick(object sender, EventArgs e)
        {
            try
            {
                string querySql = "SELECT * FROM products WHERE Product LIKE @search";
                connectSql = MySqlFunctions.GetConnection();
                commandSql = new MySqlCommand(querySql, connectSql);

                commandSql.Parameters.AddWithValue("@search", "%" + textBoxSearch.Text + "%");

                MySqlFunctions.DataToListView(commandSql, listViewProducts, 3);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Search Function: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectSql.Close();
            }
        }

        private void ButtonRegisterClick(object sender, EventArgs e)
        {
            string querySql = "INSERT INTO products VALUES (UUID(), @product, @quant, @val)";

            connectSql = MySqlFunctions.GetConnection();

            commandSql = new MySqlCommand(querySql, connectSql);

            commandSql.Parameters.Add("@product", MySqlDbType.VarChar).Value = textBoxProduct.Text;
            commandSql.Parameters.Add("@quant", MySqlDbType.Int32).Value = textBoxQuantity.Text;
            commandSql.Parameters.Add("@val", MySqlDbType.Double).Value = textBoxValue.Text;

            try
            {
                commandSql.ExecuteNonQuery();

                MessageBox.Show("Added successfully", "Information", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Product not inserted. \n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectSql.Close();
            }

            Clean();
            UpdateList();
        }

        private void ButtonUpdateClick(object sender, EventArgs e)
        {
            string querySql = "UPDATE products SET Product = @product, quantity = @quant, " +
                "value = @val WHERE Product = @product";

            connectSql = MySqlFunctions.GetConnection();

            commandSql = new MySqlCommand(querySql, connectSql);

            commandSql.Parameters.Add("@product", MySqlDbType.VarChar).Value = textBoxProduct.Text;
            commandSql.Parameters.Add("@quant", MySqlDbType.Int32).Value = textBoxQuantity.Text;
            commandSql.Parameters.Add("@val", MySqlDbType.Double).Value = textBoxValue.Text;

            try
            {
                commandSql.ExecuteNonQuery();

                MessageBox.Show("Updated sucessfully", "Information", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Product not Updated. \n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectSql.Close();
            }

            UpdateList();
        }

        private void ButtonDeleteClick(object sender, EventArgs e)
        {
            string querySql = "DELETE FROM products WHERE Product = @product";
            connectSql = MySqlFunctions.GetConnection();

            commandSql = new MySqlCommand(querySql, connectSql);

            commandSql.Parameters.Add("@product", MySqlDbType.VarChar).Value = textBoxProduct.Text;

            try
            {
                commandSql.ExecuteNonQuery();

                MessageBox.Show("Deleted successfully", "Information", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Product not deleted. \n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectSql.Close();
            }

            Clean();
            UpdateList();
        }

        private void ListViewProductsSelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewProducts.SelectedItems.Count != 0)
            {
                Obtain();
            }
        }
    }
}
