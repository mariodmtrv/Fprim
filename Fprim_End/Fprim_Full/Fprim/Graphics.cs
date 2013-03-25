using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Fprim
{
    public partial class MainForm : Form
    {
        public double minxr, minyr, maxxr, maxyr;
        private void graphAll(string[] s)
        {
            double pointdist = 0;
            int j = 1;
            try
            { 
                Graphics book = solutionTextBox.CreateGraphics();

                Pen linePen = new Pen(Color.Blue, 1.1F);

                book.FillRectangle(new SolidBrush(Color.White), 0, 0, solutionTextBox.Width, solutionTextBox.Height);

                Pen gridPen = new Pen(Color.LightGray);

                string var = varCombo.Text;

                Stack<string> st = new Stack<string>();

                double q = 0, q2 = 0, result = 0, prevx = 0, prevres = 0;

                bool fl = true, relate = false;
                // change resource for minx...
                double minx = -(double)numericSizeControl.Value+minxr,miny = -(double)numericSizeControl.Value+minyr, maxx = (double)numericSizeControl.Value+maxxr, maxy = (double)numericSizeControl.Value+maxyr;

                double zoomx = solutionTextBox.Width / Math.Abs(maxx - minx), zoomy = solutionTextBox.Height / Math.Abs(maxy - miny);

                // draw grid
                for (int i = 1; i < 10; i++)
                { book.DrawLine(gridPen, 0, (solutionTextBox.Height / 10) * i, solutionTextBox.Width, (solutionTextBox.Height / 10) * i); }

                for (int i = 1; i < 10; i++)
                { book.DrawLine(gridPen, (solutionTextBox.Width / 10) * i, 0, (solutionTextBox.Width / 10) * i, solutionTextBox.Height); }

                // graph exists?
                if (minx >= maxx || miny >= maxy) attentionMessage("graphparam");
                else
                {
                    pointdist = (maxx - minx) / 500;
                    for (double i = minx; i < maxx; i += pointdist)
                    {
                        j = 1;
                        fl = true;
                        result = 0;
                        while (s[j] != null)
                        {
                            if (s[j] == var) st.Push(i.ToString());
                            else if (isNum(s[j])) st.Push(s[j]);
                            else if (isSign(s[j]))
                            {
                                q = double.Parse(st.Pop());
                                q2 = double.Parse(st.Pop());
                                if (s[j] == "/")
                                {
                                    if (Math.Abs(q) < 0.00001) { fl = false; break; }
                                    else st.Push((q2 / q).ToString());
                                }
                                else if (s[j] == "+")
                                {
                                    st.Push((q2 + q).ToString());
                                }
                                else if (s[j] == "-")
                                {
                                    st.Push((q2 - q).ToString());
                                }

                                else if (s[j] == "*")
                                {
                                    st.Push((q2 * q).ToString());
                                }

                                else if (s[j] == "^")
                                {
                                    st.Push((Math.Pow(q2, q)).ToString());
                                }

                            }
                            else if (isTrig(s[j]))
                            {
                                q = double.Parse(st.Pop());
                                if (s[j] == "sin")
                                {
                                    st.Push(Math.Sin(q).ToString());
                                }
                                else if (s[j] == "cos")
                                {
                                    st.Push(Math.Cos(q).ToString());
                                }
                                else if (s[j] == "tg")
                                {
                                    st.Push(Math.Tan(q).ToString());
                                }
                                else if (s[j] == "cotg")
                                {
                                    st.Push((1 / Math.Tan(q)).ToString());
                                }
                            }
                            else attentionMessage("wrongexpr");
                            j++;
                        }
                        if (fl == true) { result = double.Parse(st.Pop()); }
                        else
                        {
                            while (!(st.Count == 0)) { st.Pop(); }
                        }
                        if (result >= miny && result <= maxy)
                        {
                            if (relate == true)
                            {
                                // connect to previous point
                                book.DrawLine(linePen, (float)(Math.Abs(minx - prevx) * zoomx), (float)((maxy - prevres) * zoomy), (float)(Math.Abs(minx - i) * zoomx), (float)((maxy - result) * zoomy));
                                prevx = i; prevres = result;
                            }
                            else
                            {
                                prevx = i; prevres = result; relate = true;

                            }

                        }
                        
                        else 
                        {
                            relate = false;
                            fl = true;
                        }
                    }
                }

            }
            catch (Exception)
            { attentionMessage("wrongexpr"); }
        }
        private void navRoll_Click(object sender, EventArgs e)
        {

            Point p = Cursor.Position;
       //   int centerx = 935,centery=550;

            int centerx = button1.Location.X+ navRoll.Size.Width / 2;
            int centery = button1.Location.Y+ navRoll.Size.Height / 2;
            int clx = p.X, cly = p.Y;
           int rat = 50;
            minxr += (clx - centerx)/rat;
            maxxr += (clx - centerx)/rat;
            minyr += (centery - cly)/rat;
            maxyr += (centery - cly)/rat;
            graphAll(depack(equationBox.Text));
        }
    }
}
