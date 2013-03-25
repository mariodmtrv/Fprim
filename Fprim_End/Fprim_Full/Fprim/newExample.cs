using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Fprim
{
    public partial class newExample : Form
    {
        public newExample()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            string s;
            //StreamWriter p = new StreamWriter(@".../.../Theory/addins.txt", true,Encoding.Unicode);
             StreamWriter p = new StreamWriter(@"Theory/addins.txt", true,Encoding.Unicode);
            s = comboBox1.SelectedIndex.ToString();
            if (s != "0")
                p.WriteLine('\n' + s +" "+ textBox1.Text);
            p.Close();
        }
    }
}
