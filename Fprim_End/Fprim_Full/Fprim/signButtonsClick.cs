using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace Fprim
{
    public partial class MainForm : System.Windows.Forms.Form
    {

        private void addSign(string p)
        { int pos=equationBox.SelectionStart;
            string text = equationBox.Text.Insert(pos, p);
            equationBox.Text = text;
            equationBox.Focus();
            equationBox.SelectionStart = pos + p.Length;

        }
        private void plus_Click(object sender, EventArgs e)
        {
            addSign("+");
        }

        private void minus_Click(object sender, EventArgs e)
        {
            addSign("-");
        }

        private void mult_Click(object sender, EventArgs e)
        {
            addSign("*");
        }

        private void div_Click(object sender, EventArgs e)
        {
            addSign("a/b");
        }

        private void sqrt_Click(object sender, EventArgs e)
        {
            addSign("√()");
        }

        private void root_Click(object sender, EventArgs e)
        {
            addSign("√(b,a)");
        }

        private void pow_Click(object sender, EventArgs e)
        {
            addSign("a^b");
        }

        private void sin_Click(object sender, EventArgs e)
        {
            addSign("sin()");
        }

        private void cos_Click(object sender, EventArgs e)
        {
            addSign("cos()");
        }

        private void tg_Click(object sender, EventArgs e)
        {
            addSign("tg()");
        }

        private void cotg_Click(object sender, EventArgs e)
        {
            addSign("cotg()");
        }

    }
}
