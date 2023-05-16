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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //to check username and password
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Stock;Integrated Security=True");
  
            string commandText = "SELECT * FROM [Stock].[dbo].[Login] WHERE UserName=@user and Password=@pass;";

            using (con)
            {
                SqlCommand command = new SqlCommand(commandText, con);
                command.Parameters.Add("@user", SqlDbType.VarChar);
                command.Parameters["@user"].Value = textBox1.Text;
                command.Parameters.Add("@pass", SqlDbType.VarChar);
                command.Parameters["@user"].Value = textBox2.Text;

                try
                {
                    con.Open();

                    SqlDataAdapter sda = new SqlDataAdapter(command, con);

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {
                        this.Hide();
                        StockMain main = new StockMain();
                        main.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Username and Password!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        button1_Click(sender,e);
                    }
                        Int32 rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine("RowsAffected: {0}", rowsAffected);
                }
                catch (Exception ex)
                {
                        Console.WriteLine(ex.Message);
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
