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
    public partial class test_score : Form
    {
        private const CommandType storedProcedure = CommandType.StoredProcedure;

        public test_score()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection conn2 = new SqlConnection("Data Source=.;Initial Catalog=exam-system;Integrated Security=True");
            conn2.Open();
            SqlCommand exam_score = new SqlCommand("exam_final_score", conn2);
            exam_score.CommandType = storedProcedure;
           // exam_score.Parameters.Add("@exam_id", SqlDbType.Int).Value = 1258;
            exam_score.Parameters.Add("@st_mark", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            exam_score.ExecuteScalar();


            int final = Convert.ToInt32(exam_score.Parameters["@st_mark"].Value);


            MessageBox.Show("your score" + final.ToString());

        }
    }
}
