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

namespace Project_Exam_System
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;

            panel3.Visible = false;
            panel4.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection Conn = new SqlConnection("Data Source=.;Initial Catalog=exam-system;Integrated Security=True");
            SqlCommand cmd2 = new SqlCommand("login_ins '" + ins_username.Text + "', '" + ins_password.Text + "'", Conn);
            Conn.Open();
            Convert.ToInt32(cmd2.ExecuteNonQuery());
            Conn.Close();
            SqlDataAdapter sda = new SqlDataAdapter(cmd2);
            DataTable dta = new DataTable();
            sda.Fill(dta);
            if (dta.Rows[0][0].ToString() == "1")
            {
                this.Hide();
                home_reports  obj = new home_reports();
                obj.Show();

            }
            else
            {
                MessageBox.Show("Incorrect name or ID !");
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                panel3.Visible = false;
                panel2.Visible = true; 
                panel1.Visible = false;

            
               panel4.Visible = true;

            }
            else if (comboBox1.SelectedIndex == 1)
            {
                panel4.Visible = false;

                panel1.Visible = true;
                panel2.Visible = false; 

              
               panel3.Visible = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                std_password.PasswordChar = '\0';
                checkBox1.Text = "Hide password";
            }
            else
            {
                std_password.PasswordChar = '*';
                checkBox1.Text = "Show password";
            }
        }

        private void std_login_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog= Exam-System;Integrated Security=True");
            SqlCommand cmd1 = new SqlCommand("exec login_user '" + std_username.Text + "','" + std_password.Text + "'", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd1);
            DataTable dta = new DataTable();
            sda.Fill(dta);
            if (dta.Rows[0][0].ToString() == "1")
            {
                SqlCommand cmd2 = new SqlCommand(@"INSERT INTO[dbo].[login_student] ([login_user],[password]) VALUES ('" + std_username.Text + "','" + std_password.Text + "')", conn);
                conn.Open();
                cmd2.ExecuteNonQuery();
                conn.Close();
                this.Hide();
                choose_course cc = new choose_course();
                cc.Show();
            }
            else
            {
                MessageBox.Show("please enter valid usename and valid password !");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            signup_student sst = new signup_student();
            sst.Show();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                ins_password.PasswordChar = '\0';
                checkBox2.Text = "Hide Password";
            }
            else
            {
                ins_password.PasswordChar = '*';
                checkBox2.Text = "Show Password";
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
