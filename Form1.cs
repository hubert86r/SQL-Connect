﻿using System;
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
                SqlCommand sqlCmd = new SqlCommand("ConnectAddOrEdit", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@mode", "Add");
                sqlCmd.Parameters.AddWithValue("@code", lbl);
                sqlCmd.Parameters.AddWithValue("@name", "Add");
                sqlCmd.Parameters.AddWithValue("@description", "Add");
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Error message");
            }
        }

        private void Name_Click(object sender, EventArgs e)
        {

        }
    }
}