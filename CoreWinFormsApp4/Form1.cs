using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreWinFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var logingUI = new LoginUI();
            logingUI.ShowDialog();
            if (!logingUI.IsLogged)
            {
                Application.Exit();
            }
        }

        private void btn_testConnection_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = getConnection();
            try
            {
                cnn.Open();
                MessageBox.Show("Conn ok");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally {
                if (cnn != null) {
                    cnn.Close();
                }
            }
        }

        private void btn_getData_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = getConnection();
            sqlConnection.Open();
            var request = "SELECT * FROM aviondeaf";
            SqlCommand sqlCommand = new SqlCommand(request, sqlConnection);            
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            StringBuilder sb = new StringBuilder();
            while (sqlDataReader.Read()) {
                sb.Append($"{sqlDataReader.GetValue(0)} - {sqlDataReader.GetValue(1)}").AppendLine();
            }
            MessageBox.Show(sb.ToString());
        }
        private SqlConnection getConnection() {
            var connString = "Server=PC1224;DataBase=Aviation;Trusted_Connection=True";
            SqlConnection cnn = new SqlConnection(connString);
            return cnn;
        }

        private void btn_getData2_Click(object sender, EventArgs e)
        {
            var sqlConnection = getConnection();
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM aviondeaf", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            string txt = "";
            while (sqlDataReader.Read())
            { 
                txt += $"{sqlDataReader.GetValue(1)}";
            }
            MessageBox.Show(txt);
        }
    }
}
