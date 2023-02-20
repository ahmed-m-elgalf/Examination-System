using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Exam_System
{
    public partial class report_student_grades : Form
    {
        public report_student_grades()
        {
            InitializeComponent();
        }

        private void report_student_grades_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.get_student_grades' table. You can move, or remove it, as needed.
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int st_id = int.Parse(textBox1.Text);
            this.get_student_gradesTableAdapter.Fill(this.DataSet1.get_student_grades,st_id);

            this.reportViewer1.RefreshReport();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            home_reports obj = new home_reports();
            obj.Show();
            this.Hide();
        }
    }
}
