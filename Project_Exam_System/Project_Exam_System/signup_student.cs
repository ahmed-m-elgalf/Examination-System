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

namespace Project_Exam_System
{
    public partial class signup_student : Form
    {
        public signup_student()
        {
            InitializeComponent();
           
        }

        private void signup_student_Load(object sender, EventArgs e)
        {

        }
        string name;
        int x = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog = Exam-System; Integrated Security = True" );
            SqlCommand saving_regis_data = new SqlCommand("saving_regis_data'"  + f_name.Text + "','" + m_name.Text + "','" + l_name.Text +"','" +city_.Text + "','" + name + "','" +  std_signup_username.Text + "','" + std_signup_password.Text + "'", conn);
            conn.Open();
            saving_regis_data.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("your login email is: '" + std_signup_username.Text + "' , and your password is: '" + std_signup_password.Text + "'!");

            this.Hide();
            login l = new login();
            l.Show();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            name = "male";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            name = "female";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                std_signup_password.PasswordChar = '\0';
                checkBox1.Text = "Hide password";
            }
            else
            {
                std_signup_password.PasswordChar = '*';
                checkBox1.Text = "Show password";
            }
        }
    }
}
