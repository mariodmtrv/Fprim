using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fprim
{
    public partial class Theory : Form
    {
        public Theory()
        {
            InitializeComponent();
        }
        string s = "";
        private void button1_Click(object sender, EventArgs e)
        {
            openFile("def");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFile("rules");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFile("some");
        }

        private void openFile(string p)
        {
            s = @"Theory/";
           // s = @".../.../Theory/";
            s += p;
            s += ".rtf";
            richTextBox1.LoadFile(s, RichTextBoxStreamType.RichText);
        }
    }
    }