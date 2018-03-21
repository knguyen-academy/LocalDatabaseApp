using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using System.Configuration;
using System.Data.SqlClient;


namespace LocalDatabaseApp
{
    public partial class Form1 : Form
    {
        //Create connection using Connectstring
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-2N25UD6;Initial Catalog=TestDB;Integrated Security=True;Pooling=False");

        public Form1()
        {
            InitializeComponent();

            //Open DB connection
            conn.Open();

            //Check DB connection
            if (conn.State == ConnectionState.Open)
                Server_stat_label.Text = "ON";
            else
                Server_stat_label.Text = "DOWN";

        }

        private void Connect_button_Click(object sender, EventArgs e)
        {

            //Create SQL command
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            //Retrive username and password
            cmd.CommandText = "Select count(*) From LoginTab where username = '" + UserName_textBox.Text + "' and password = '" + Password_textBox.Text + "' ";
            DataTable dt = new DataTable();
            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            sa.Fill(dt);

            //If username and password found ~ return 1 count or count==1
            if (dt.Rows[0][0].ToString() == "1")
            {
                Login_label.Text = "Login Sucess";
                Form2 form2 = new Form2();
                form2.Show();
                //conn.Close();
                this.Hide();
                

            }
            else
                Login_label.Text = "Username or Password is not correct";
            //dataGridView1.DataSource = dt;

            conn.Close();
        }

        private void Exit_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
