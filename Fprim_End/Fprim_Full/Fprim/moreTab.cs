using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fprim
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        private void button6_Click(object sender, EventArgs e)
        {
            AboutProg f = new AboutProg();
            f.ShowDialog();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Didyou f = new Didyou();
            f.ShowDialog();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            derMore f = new derMore();
            f.ShowDialog();
        }
    }
}
