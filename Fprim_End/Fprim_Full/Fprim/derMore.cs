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
    public partial class derMore : Form
    {
        public derMore()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFile("early");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFile("physics");
        }

        private void button3_Click(object sender, EventArgs e)
        {
              openFile("high");
        }

        private void button4_Click(object sender, EventArgs e)
        {
                     openFile("source");
        }
        private void openFile(string s)
        {
            string loc = @"derMore/";
          // string loc = @".../.../derMore/";
            loc += s;
            loc += ".rtf";
            richTextBox1.LoadFile(loc,RichTextBoxStreamType.RichText);
        }
    }
}
