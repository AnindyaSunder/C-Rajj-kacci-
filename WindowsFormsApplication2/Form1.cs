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
    public partial class Form1 : Form
    {

        SqlConnection sql = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\data\Billing_system.mdf;Integrated Security=True;Connect Timeout=30");
        public Form1()
        {
            InitializeComponent();
            show_allitems();
            update_allitems();
        }

 //========================================admin_page login=================================================================================// 

        private void submit1_Click(object sender, EventArgs e)
        {
            int c=0;
            sql.Close();
            sql.Open();
            SqlCommand cmd = new SqlCommand("select * from log where name='" + textBox9.Text + "' and pass='" + textBox11.Text + "'", sql);
            SqlDataReader rd = cmd.ExecuteReader();

            
            while (rd.Read())
            {
                c++;
            
            }

            if (c > 0)
            {

                MessageBox.Show("Login Successfull...");
                textBox11.Clear();
                textBox9.Clear();

                textBox11.Enabled=false;
                textBox9.Enabled = false;
                submit1.Enabled = false;

                label23.Enabled = true;
                label24.Enabled = true;
                panel6.Enabled = true;
                l_out.Visible = true;
                

            }
            else
            {

                MessageBox.Show("Error...");
            }
            sql.Close();
        }
 //======================================== admin_page add_item ==================================================================================//

        private void add_Click(object sender, EventArgs e)
        {
            try
            {
                if (richTextBox1.Text == "" || richTextBox2.Text == "" || richTextBox3.Text == "")
                {
                    MessageBox.Show("Empty input found...!!");
                }

                else
                {
                    sql.Close();
                    sql.Open();
                    SqlCommand cmd = new SqlCommand("insert into allitems(id,iname,price)values('" + richTextBox1.Text + "','" + richTextBox2.Text + "','" + richTextBox3.Text + "')", sql);
                    int a = cmd.ExecuteNonQuery();

                    if (a > 0)
                    {
                        MessageBox.Show("Add successfully...");
                        richTextBox1.Clear();
                        richTextBox2.Clear();
                        richTextBox3.Clear();
                        show_allitems();
                        update_allitems();

                    }
                    else
                    {
                        MessageBox.Show("Error...");
                    }
                    sql.Close();
                
                }
            
            }


            catch
            {
                MessageBox.Show("Something wrong....!");
            }
        }
 //========================================1st_page show_allitems============================================================================//
        void show_allitems()
        {

            sql.Close();
            sql.Open();
            SqlDataAdapter sdp = new SqlDataAdapter("select * from allitems", sql);
            dataGridView1.Rows.Clear();
            DataTable dt = new DataTable();
            sdp.Fill(dt);

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();

                dataGridView1.Rows[n].Cells[0].Value = false;
                dataGridView1.Rows[n].Cells[1].Value = item[0].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[2].ToString();
               
            
            
            }



            sql.Close();
        
        
        
        }
 //========================================admin_page show_allitems=========================================================================//
        void update_allitems()
        {

            sql.Close();
            sql.Open();
            SqlDataAdapter sdp = new SqlDataAdapter("select * from allitems", sql);
            dataGridView3.Rows.Clear();
            DataTable dt = new DataTable();
            sdp.Fill(dt);

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView3.Rows.Add();

                dataGridView3.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView3.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView3.Rows[n].Cells[2].Value = item[2].ToString();



            }



            sql.Close();



        }
 //========================================1st_page Select_allitems=======================================================================//

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if ((bool)dataGridView1.SelectedRows[0].Cells[0].Value == false)
            {
                dataGridView1.SelectedRows[0].Cells[0].Value = true;

            }
            else
            {
                dataGridView1.SelectedRows[0].Cells[0].Value = false;
            }
        }
 //========================================1st_page copy_allitems===========================================================================//


        private void copyitem_Click(object sender, EventArgs e)
        {
              int nn=0;
             int i=0;
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if ((bool)item.Cells[0].Value == true)
                {
                    int n = dataGridView2.Rows.Add();

                    dataGridView2.Rows[n].Cells[0].Value = item.Cells[2].Value.ToString();
                    dataGridView2.Rows[n].Cells[1].Value = item.Cells[3].Value.ToString();
                    
               }
         
            }
          
           nn=Convert.ToInt32(dataGridView1.Rows.Count);
            for(i=0;i<=nn-1;i++)
            {
            dataGridView1.Rows[i].Cells[0].Value = false;
            }

            textBox2.Clear();

        }
 //========================================1st_page multiply columns========================================================================//

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            foreach(DataGridViewRow item in dataGridView2.Rows)
            {

                item.Cells[dataGridView2.Columns[3].Index].Value = (Convert.ToDouble(item.Cells[dataGridView2.Columns[1].Index].Value) * Convert.ToDouble(item.Cells[dataGridView2.Columns[2].Index].Value));
            }
        }
//========================================1st_page adding columns value====================================================================//

        private void totaltk_Click(object sender, EventArgs e)
        {    
            int i=0,a=Convert.ToInt32(dataGridView2.Rows.Count);
            textBox3.Text = "0";
            vat.Text = "0";
            discount.Text = "0";
            for (i = 0; i < a ; i++)
            { 
            textBox3.Text=Convert.ToString(Convert.ToInt32(textBox3.Text)+Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value.ToString()));
            t_payable.Text = textBox3.Text;
            }
        }
//========================================1st_page searching===============================================================================//

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            sql.Close();
            sql.Open();
            SqlDataAdapter sdp = new SqlDataAdapter("Select * from allitems where id like'%" + textBox2.Text + "%' or iname like'%" + textBox2.Text + "%'", sql);
            dataGridView1.Rows.Clear();
            DataTable dt = new DataTable();
            sdp.Fill(dt);

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();

                dataGridView1.Rows[n].Cells[0].Value = false;
                dataGridView1.Rows[n].Cells[1].Value = item[0].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[2].ToString();

            
            }

            sql.Close();
        }
 //========================================admin_page searching===============================================================================//

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            sql.Close();
            sql.Open();
            SqlDataAdapter sdp = new SqlDataAdapter("Select * from allitems where id like'%" + textBox12.Text + "%' or iname like'%" + textBox12.Text + "%'", sql);
            dataGridView3.Rows.Clear();
            DataTable dt = new DataTable();
            sdp.Fill(dt);

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView3.Rows.Add();
                dataGridView3.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView3.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView3.Rows[n].Cells[2].Value = item[2].ToString();


            }

            sql.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
  //========================================admin_page Editing_Item===============================================================================//

        private void dataGridView3_Click(object sender, EventArgs e)
        {
            string iid = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
            string iitem = dataGridView3.SelectedRows[0].Cells[1].Value.ToString();
            string iprice = dataGridView3.SelectedRows[0].Cells[2].Value.ToString();

            richTextBox1.Text = iid;
            richTextBox2.Text = iitem;
            richTextBox3.Text = iprice;
        }
 //========================================admin_page Update_Item===============================================================================//

        private void item_Update_Click(object sender, EventArgs e)
        {
           try
            {
                if (richTextBox1.Text == "" || richTextBox2.Text == "" || richTextBox3.Text == "")
                {
                    MessageBox.Show("Empty input found...!!");
                }
                else
                {


                    sql.Close();
                    sql.Open();

                    SqlCommand cmd = new SqlCommand("update allitems set id='" + richTextBox1.Text + "',iname='" + richTextBox2.Text + "',price='" + richTextBox3.Text + "' where id='" + dataGridView3.SelectedRows[0].Cells[0].Value.ToString() + "'", sql);
                    int a = cmd.ExecuteNonQuery();

                    if (a > 0)
                    {
                        DialogResult r = MessageBox.Show("Update Successfully...");
                        if (r == DialogResult.OK)
                        {
                            richTextBox1.Clear();
                            richTextBox2.Clear();
                            richTextBox3.Clear();
                            show_allitems();
                            update_allitems();

                        }

                    }

                    sql.Close();
                }
            }
           


          catch
            {
                MessageBox.Show("Item not matched...!!");
            }


        }
 //========================================admin_page Delete_Item===============================================================================//

        private void delete_Click(object sender, EventArgs e)
        {
          try
            {
                if (richTextBox1.Text == "" || richTextBox2.Text == "" || richTextBox3.Text == "")
                {
                    MessageBox.Show("Empty input found...!!");
                }
                else
                {

                    sql.Close();
                    sql.Open();

                    SqlCommand cmd = new SqlCommand("delete from allitems where id='" + richTextBox1.Text + "' and iname='" + richTextBox2.Text + "'", sql);
                    int a = cmd.ExecuteNonQuery();

                    if (a > 0)
                    {

                        MessageBox.Show("Delete successfully....!");

                        richTextBox1.Clear();
                        richTextBox2.Clear();
                        richTextBox3.Clear();
                        show_allitems();
                        update_allitems();


                    }

                    sql.Close();
                
                }

            }


     catch
           {

               MessageBox.Show("Item not matched...!!");
           }

        }
//========================================admin_page new_clear_Item===============================================================================//
        private void new_clear_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox2.Clear();
            richTextBox3.Clear();
            textBox12.Clear();
        }


 //========================================1st_page vat_Calculate===============================================================================//

        private void vat_TextChanged(object sender, EventArgs e)
        {

           
            try
            {


                    textBox4.Text = Convert.ToString(Convert.ToDouble(textBox3.Text) * Convert.ToDouble(vat.Text) / 100);
                    t_payable.Text = Convert.ToString(Convert.ToDouble(textBox3.Text) + (Convert.ToDouble(textBox3.Text) * (Convert.ToDouble(vat.Text) / 100)));
                

            }
            catch
            {

                
            }

        }
 //========================================1st_page Discount_Calculate===============================================================================//

        private void discount_TextChanged(object sender, EventArgs e)
        {
            
            try
            {


                textBox5.Text = Convert.ToString(Convert.ToDouble(textBox3.Text) * Convert.ToDouble(discount.Text) / 100);
                t_payable.Text = Convert.ToString(Convert.ToDouble(textBox3.Text) + (Convert.ToDouble(textBox3.Text) * (Convert.ToDouble(vat.Text) / 100)) - (Convert.ToDouble(textBox3.Text) * Convert.ToDouble(discount.Text) / 100));


            }
            catch
            {
               // MessageBox.Show("Dis missing..");

            }
        }
        //========================================1st_page Cash_Back===============================================================================//

        private void cash_TextChanged(object sender, EventArgs e)
        {
            try
            {

              change.Text = Convert.ToString(Convert.ToDouble(cash.Text) - Convert.ToDouble(t_payable.Text));

            }
            catch
            {
               // MessageBox.Show("missing...");
            }

        }
  //========================================1st_page New_Bill===============================================================================//

        private void clear_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox7.Clear();
            vat.Clear();
            discount.Clear();
            cash.Clear();
            change.Clear();
            t_payable.Clear();
            dataGridView2.Rows.Clear();
        }
 //========================================admin_page change_window & log_out & log_in===============================================================================//
        private void label23_Click(object sender, EventArgs e)
        {
           // this.Hide();
            ch_pass ps = new ch_pass();
            ps.Show();
        }
        //========================================admin_page log_out===============================================================================//
        private void l_out_Click(object sender, EventArgs e)
        {
            textBox11.Enabled = true;
            textBox9.Enabled = true;
            submit1.Enabled = true;

            label23.Enabled = false;
            label24.Enabled = false;
            l_out.Visible = false;
            panel6.Enabled = false;
            
        }
 //========================================1st_page customer_id auto generate===============================================================================//
        private void Form1_Load(object sender, EventArgs e)
        {
            sql.Close();
            sql.Open();

            SqlDataAdapter sdp = new SqlDataAdapter("select isnull(max(cast(cid as int)),0)+1 from bill", sql);
            DataTable dt = new DataTable();
            sdp.Fill(dt);

            textBox1.Text = dt.Rows[0][0].ToString();

            sql.Close();
        }
        //========================================1st_page Bill_Add===============================================================================//
        private void bill_Click(object sender, EventArgs e)
        {
            try
            {
                if(t_payable.Text==textBox7.Text)
                {
                sql.Close();
                sql.Open();
                SqlCommand cmd = new SqlCommand("insert into bill(cid,amount,vat,dis,paid)values('" + textBox1.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','"+textBox5.Text+"','"+textBox7.Text+"')", sql);
                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    MessageBox.Show(" Bill add successfully...");
                    sql.Close();
                    sql.Open();

                    SqlDataAdapter sdp = new SqlDataAdapter("select isnull(max(cast(cid as int)),0)+1 from bill", sql);
                    DataTable dt = new DataTable();
                    sdp.Fill(dt);

                    textBox1.Text = dt.Rows[0][0].ToString();

                }
                else
                {
                    MessageBox.Show("Error...");
                }

                }
                else
                {
                
                MessageBox.Show("Pay amount not matched...!");
                textBox7.Clear();
                }

                sql.Close();

            }


            catch
            {
                MessageBox.Show("Something wrong....!");
            }



        }
        //========================================1st_page Print===============================================================================//
        Bitmap bmp;
        private void prnt_Click(object sender, EventArgs e)
        {
            int Height = dataGridView2.Height;
            dataGridView2.Height = dataGridView2.RowCount * dataGridView2.RowTemplate.Height * 2;
            bmp = new Bitmap(dataGridView2.Width, dataGridView2.Height );
            dataGridView2.DrawToBitmap(bmp, new Rectangle(0, 0, dataGridView2.Width, dataGridView2.Height));
            dataGridView2.Height = Height;
            printPreviewDialog1.ShowDialog();


        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0);
        }
        
        


    }
}
