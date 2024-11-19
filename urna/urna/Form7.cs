using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace urna
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form6 formecand = new Form6();
            formecand.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form8 formelog = new Form8();
            formelog.Show();
            this.Close();
        }
    }
}
