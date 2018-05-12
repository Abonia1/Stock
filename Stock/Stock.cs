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

namespace Stock
{
    public partial class Stock : Form
    {
        public Stock()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Stock;Integrated Security=True");
        SqlDataAdapter sda;
        DataTable dt;



        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Stock;Integrated Security=True");
            //validate textbox value
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Enter valid data!!!");
            }
            else
            {

                // Insert Logic
                con.Open();


                var sqlQuery = "";
                if (IfStockExists(con, textBox1.Text))
                {
                    sqlQuery = @"UPDATE [Stock] SET [ProductName] = '" + textBox2.Text + "' ,[Quantity] = '" + textBox3.Text + "',[TransDate] = '" + dateTimePicker1.Text + "' WHERE [ProductCode] = '" + textBox1.Text + "'";
                }
                else
                {
                    sqlQuery = @"INSERT INTO [Stock].[dbo].[Stock] ([ProductCode],[ProductName],[Quantity],[TransDate]) VALUES
                            ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Text + "')";
                }

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("updated!!!!");
                //Reading Data
                LoadData();
                count();
            }
        }
       

        private void Stock_Load(object sender, EventArgs e)
        {

            LoadData();
            count();
           
        }
        public void count()
        {
            //sum products
            textBox5.Text = dataGridView1.Rows.Count.ToString();

            //sum quantity
            int sum = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; ++i)

            {

                sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);

            }

            textBox6.Text = sum.ToString();
        }
        private bool IfStockExists(SqlConnection con, string productCode)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select 1 From [Stock] WHERE [ProductCode]='" + productCode + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public void LoadData()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Stock;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT *
  FROM[dbo].[Stock]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Quantity"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["TransDate"].ToString();

            }
        }
      

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                dateTimePicker1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();

            
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
           
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Stock;Integrated Security=True");
            var sqlQuery = "";
            if (IfStockExists(con, textBox1.Text))
            {
                con.Open();
                sqlQuery = @"DELETE FROM [Stock] WHERE [ProductCode] = '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Record Not Exists...!");
            }
            //Reading Data
            LoadData();
            count();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
           
        }

        void FillDataGridView()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT *
  FROM[dbo].[Stock]", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@Product_Name", textBox4.Text.Trim());
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error Message");
            }
        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
