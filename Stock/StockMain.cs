using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock
{
    public partial class StockMain : Form
    {
        private int childFormNumber = 0;

        public StockMain()
        {
            InitializeComponent();
        }

        private void StockMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products pro = new Products();
         
            pro.Show();
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stock sto = new Stock();
           
            sto.Show();
        }

        private void StockMain_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stock f = new Stock(); 
            
            f.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Products f = new Products();

            f.Show();

        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StockMain main = new StockMain();
            main.Show();
        }

        private void productsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ProductsList prorep = new ProductsList();

            prorep.Show();
        }

        private void stockToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            StockList storep = new StockList();

            storep.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StockList storep = new StockList();

            storep.Show();
        }
    }
}
