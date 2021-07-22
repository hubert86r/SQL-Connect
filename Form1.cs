using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SQL_Connect
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        SqlConnection sqlCon = new SqlConnection(@"SERVER=(local)\bazaradwag2017;User ID=sa;Password=Radwag99;Initial Catalog=SQL_Connect;Connect Timeout = 30000;");
        int productId = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                if (btnSave.Text == "Save")
                {
                    SqlCommand sqlCmd = new SqlCommand("Add_Edit_Product", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@id", 0);
                    sqlCmd.Parameters.AddWithValue("@mode", "Add");
                    sqlCmd.Parameters.AddWithValue("@code", txtCode.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@description", txtDescription.Text.Trim());
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Saved successfully");
                }
                else
                {
                    SqlCommand sqlCmd = new SqlCommand("Add_Edit_Product", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@id", productId);
                    sqlCmd.Parameters.AddWithValue("@mode", "Edit");
                    sqlCmd.Parameters.AddWithValue("@code", txtCode.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@description", txtDescription.Text.Trim());
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Update successfully");
                }
                Reset();
                FillDataGridView();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Error message");
            }
            finally
            {
                sqlCon.Close();
            }
        }
        private void FillDataGridView()
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("View_Search_Product", sqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("@productName", txtSearch.Text.Trim());
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dgvProducts.DataSource = dtbl;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error message");
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error message");
            }
        }

        private void dgvProducts_DoubleClick(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow.Index != -1)
            {
                productId = Convert.ToInt32(dgvProducts.CurrentRow.Cells[0].Value.ToString());
                txtName.Text = dgvProducts.CurrentRow.Cells[2].Value.ToString();
                txtCode.Text = dgvProducts.CurrentRow.Cells[1].Value.ToString();
                txtDescription.Text = dgvProducts.CurrentRow.Cells[3].Value.ToString();
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
            }
        }
        void Reset()
        {
            txtCode.Text = txtName.Text = txtDescription.Text = txtSearch.Text = "";
            btnSave.Text = "Save";
            productId = 0;
            btnDelete.Enabled = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Reset();
            FillDataGridView();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                
                    SqlCommand sqlCmd = new SqlCommand("Delete_Product", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@id", productId);
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Delete successfully");
                
                Reset();
                FillDataGridView();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error message");
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
