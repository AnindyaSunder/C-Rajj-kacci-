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

namespace WindowsFormsApplication2
{
    public partial class ch_pass : Form
    {
        SqlConnection sql = new SqlConnection(@"Data Source=ANINDYA\SQLEXPRESS;Initial Catalog=kacci;Integrated Security=True");
        public ch_pass()
        {
            InitializeComponent();
        }

        private void ch_pass_exit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void change_pass_Click(object sender, EventArgs e)
        {
            sql.Close();
            sql.Open();

            SqlDataAdapter sdp = new SqlDataAdapter("select count(*) from log where name='" + textBox1.Text + "' and pass='" + textBox2.Text + "'", sql);
            DataTable dt = new DataTable();
            sdp.Fill(dt);
            errorProvider1.Clear();

            if (dt.Rows[0][0].ToString() == "1")
            {
                if (textBox3.Text == textBox4.Text)
                {
                    SqlCommand cmd = new SqlCommand("update log set pass='" + textBox3.Text + "'where name='" + textBox1.Text + "' and pass='" + textBox2.Text + "'", sql);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Change Password succserefully...!");


                }
                else
                {
                    errorProvider1.SetError(textBox3, "Password not matched....!");
                    errorProvider1.SetError(textBox4, "Password not matched....!");

                }

            }
            else
            {
                errorProvider1.SetError(textBox1, "incorrect User name....!");
                errorProvider1.SetError(textBox2, "incorrect Password....!");
            
            }


            sql.Close();
        }
    }
}
