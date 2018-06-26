using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] strs = {"One","Two", "Hello", "World", "Four", "Five"};
            var result = from s in strs 
                         where s.Length < 5     
                         select s;
            foreach (var s in result)
            {
                listBox1.Items.Add(s);
            }

        }
    }
}
