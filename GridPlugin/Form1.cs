using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridPlugin
{
    public partial class Form1 : Form
    {
        public int Rows=1;
        public int Columns = 1;
        public int startX=1;
        public int startY=1;
        public bool resetOnly =false ;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) => this.Close();

        private void button1_Click(object sender, EventArgs e)
        {
            this.Rows = (int)numericUpDown4.Value;
            this.Columns = (int)numericUpDown1.Value;
            this.startX = (int)numericUpDown2.Value;
            this.startY = (int)numericUpDown3.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.resetOnly = true;
            this.Close();
        }
    }
}
