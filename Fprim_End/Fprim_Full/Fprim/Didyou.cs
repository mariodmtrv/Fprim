using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Fprim
{
    public partial class Didyou : Form
    {
        string[] st = new string[32];
        int j = 0, q = 10;
        public Didyou()
        { 
            InitializeComponent();
       
     //  StreamReader p = new StreamReader(@".../.../Theory/facts.txt",Encoding.Unicode);
       StreamReader p = new StreamReader(@"Theory/facts.txt", Encoding.Unicode);
        while ((st[j] = p.ReadLine()) != null)
        {
            j++;
        }
        j--;
        richTextBox1.Text = st[q];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (q == j) q =0;
            richTextBox1.Text = st[++q];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (q ==1) q = j;
            richTextBox1.Text = st[--q];
        }
        
     
    }
}
