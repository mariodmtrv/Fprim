using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Diagnostics;
namespace Fprim
{    
    public partial class MainForm : Form
    {
       
        int br = 0;
        string string3 = "";
       
        private void examples_Click(object sender, EventArgs e)
        { 
            eqPanel.Visible = true;
            examplesMenuStrip.Show(examples, new System.Drawing.Point(0, examples.Height));
        }
        private void rangeButton_Click(object sender, EventArgs e)
        {
            br++;
            br%= 2;
           if (br == 1)
            {
               rangeButton.Image = new Bitmap(@"Images/back.bmp");
              // rangeButton.Image = new Bitmap(@".../.../Images/back.bmp");
                sizePanel.Show();  }
            else 
           { 
               sizePanel.Hide(); 
             rangeButton.Image = new Bitmap(@"Images/show.bmp"); 
            // rangeButton.Image = new Bitmap(@".../.../Images/show.bmp"); 
           }
      
        }
        private void theoryButton_Click(object sender, EventArgs e)
        {
            Theory f = new Theory();
            f.ShowDialog();
        }
        private void stepButton_Click(object sender, EventArgs e)
        {
            OperateButton.Enabled = true;
            panel3.BackColor = Color.Red;
            panel2.BackColor = Color.Lime;
            OperateButton.Text = "Намери";
            rangeButton.Visible = false;
            solutionTextBox.PaintBack();
            solutionTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            solutionTextBox.Enabled = true;
            navRoll.Visible = false;
        }
        private void navRoll_MouseClick(object sender, MouseEventArgs e)
        {
           // navRoll.Cursor.
        }
                private void graphButton_Click(object sender, EventArgs e)
        {
            OperateButton.Enabled = true;
             panel2.BackColor = Color.Red;
             panel3.BackColor = Color.Lime;
            OperateButton.Text = "Начертай";
            rangeButton.Visible = true;
            solutionTextBox.ScrollBars=RichTextBoxScrollBars.None;
            solutionTextBox.Enabled = false;
            navRoll.Visible = true;
        }
        private void stepButton_MouseEnter(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.PapayaWhip;
        }

        private void stepButton_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.Transparent;
        }
        private void OperateButton_Click(object sender, EventArgs e)
        {
            checkValidity();
        }
        private void divToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            equationBox.Text = e.ClickedItem.Text.ToString();
        }

        private void trigToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            equationBox.Text = e.ClickedItem.Text.ToString();
        }

        private void anyToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            equationBox.Text = e.ClickedItem.Text.ToString();
        }

        private void сложниФункцииToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            equationBox.Text = e.ClickedItem.Text.ToString();
        }

        private void checkValidity()
        {
            bool fl = false;
            string s="";
            attentionPanel.Visible = false;
            if (equationBox.Text == "") { attentionMessage("noeq");fl = true; }
            else if (varCombo.Text == "") { attentionMessage("novar"); fl = true; }
            else if (!(varCombo.Text.Length == 1 && varCombo.Text[0] >= 'a' && varCombo.Text[0] <= 'z')) { attentionMessage("wrongvar"); fl = true; }
            else
            // scopes check
            {
                int br1 = 0;
                fl = false;
                s = equationBox.Text;
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == '(') br1++;
                    else if (s[i] == ')') { br1--; if (br1 < 0) { fl = true; break; } }

                }
                if (fl == true) attentionMessage("wrongscopes");
            }
            if (s[0] == '-') s.Insert(0,"0");
           if (fl == false)
           {
               if (OperateButton.Text == "Начертай")
               {
                   minxr = minyr=maxxr=maxyr=0.0;
                   graphAll(depack(s));
               }
               else
               {
                   solutionTextBox.Text = "";
                   string3 = "";
                   string3 += (notate(depack(s), 0) + "\r\n");
                 
                  
               }
           } 
        }

      
        /*
         * Codename : GOLGOTHA
         */
        private string notate(string[]expr,int d)
        {
            solutionTextBox.PaintBack();
            // try
            {
               
                string q = "",q2 = "",s2="",ans="",var=varCombo.Text;
                Stack<string> st = new Stack<string>();
                    /*define the two operands
                     * the last two elements in the stack
                    */
                int j = 1;
                while (!(expr[j] == null)) { j++;}
                j--;
                    for (int i = 1; i <= j - 1; i++)
                    {
                        if (!(isSign(expr[i]) || isTrig(expr[i]))) st.Push(expr[i]);
                        else if (isSign(expr[i]))
                        {
                            q = st.Pop().ToString();
                            q2 = st.Pop().ToString();
                            s2 = q2 + expr[i] + q; 
                           
                            st.Push(s2);
                        }
                        else if (isTrig(expr[i]))
                        {
                            s2 = expr[i] + "(" + st.Pop().ToString() + ")";
                            st.Push(s2);
                        }
                    }
                    // expr[j] is the operator 
                    if (st.Count == 2 && isSign(expr[j]))
                    {
                        q =st.Pop();
                        if (q.Length > 1) q = "(" + q + ")";
                        q2 = st.Pop();
                        if (q2.Length > 1) q2 = "(" + q2 + ")";

                        if (expr[j] == "+" || expr[j] == "-")
                        { ruleMessage(expr[j],d);
                        solutionTextBox.Text += "където f(x)=" + q2 + " и g(x)=" + q + "\r\n";
                            if(expr[j]=="+")
                            {
                                  ans= "(" + notate(depack(q2),d+8) + "+" + notate(depack(q),d+8) + ")";
                             }
                            else 
                            { 
                                ans ="("+ notate(depack(q2),d+8) + "-" + notate(depack(q),d+8)+")";
                            }
                            
                        }
                        else if (expr[j] == "*")
                        {
                            if (isNum(q)) 
                            { 
                                ruleMessage("c*",d); ans="("+q+"*"+notate(depack(q2),d+8)+")"; 
                            }
                            else if (isNum(q2)) 
                            { 
                                ruleMessage("c*",d); ans = "(" + q2 + "*" + notate(depack(q),d+8) + ")"; 
                            }
                            else if (isParam(q))
                            {
                                ruleMessage("par*", d);
                                ans = "(" + q + "*" + notate(depack(q2), d + 8) + ")"; 
                            }
                            else if (isParam(q2))
                            {
                                ruleMessage("par*", d);
                                ans = "(" + q2 + "*" + notate(depack(q), d + 8) + ")"; 
                            }
                            else
                            {
                                ruleMessage("*", d); solutionTextBox.Text += "където f(x)=" + q2 + " и g(x)=" + q + "\r\n";
                                ans = notate(depack(q2), d + 8) + "*" + q + "+" + q2 + "*" + notate(depack(q), d + 8);

                            }
                        }
                        else if (expr[j] == "/")
                        {
                            ruleMessage("/", d); solutionTextBox.Text += "където f(x)=" + q2 + " и g(x)=" + q + "\r\n";
                         ans = "(" + notate(depack(q2), d + 8) + "*" + q + "-" + q2 + "*" + notate(depack(q), d + 8) + ")/(" + q + ")^2"; 
                      
                        }
                        else if (expr[j] == "^")
                        {   // power
                            
                            if(q.Length == 1 && isNum(q))
                            {
                                ruleMessage("^",d);
                                ans = "(" + q + "*" + q2 + "^" + (double.Parse(q) - 1.0).ToString()+")"; 
                            }
                            else
                            {
                                ruleMessage("com", d); solutionTextBox.Text += "където f(x)=" + q2 + " и g(x)=" + q + "\r\n";
                                ans = "(" + q + q2 + "^" + "(" + q + "-1)" + "*" + notate(depack(q2), d + 8) + ")";
                            }

                        }
                        solutionTextBox.Text+=inter(d)+"f'("+q2+expr[j]+q+")";
                    }

                    else if (st.Count == 1 && isTrig(expr[j]))
                    {
                        q = st.Pop();
                        ruleMessage("trig",d);
                        if (expr[j] == "sin")
                        {
                            if (q == var)
                            {
                                ans = "cos(" + var + ")";
                            }
                            else
                            {
                                ans = "cos(" + q + ")*" + notate(depack(q),d+8); 
                            }
                        }
                        else if (expr[j] == "cos")
                        {
                            if (q == var)
                            {
                                ans = "-sin(" + var + ")";
                            }
                            else
                            {
                                ans = "-sin(" + q + ")*" + notate(depack(q),d+8);
                            }
                        }
                        else if (expr[j] == "tg")
                        {
                            if (q == var)
                            {
                                ans = "1/(cos(" + var + ")^2)";
                            }
                            else
                            {
                                ans = "1/(cos(" + q + ")^2)*" + notate(depack(q),d+8);
                            }
                        }
                        else if (expr[j] == "cotg")
                        {
                            if (q == var)
                            {
                                ans = "-1/(sin(" + var + ")^2)";
                            }
                            else
                            {
                                ans = "-1/(sin(" + q + ")^2)*" + notate(depack(q),d+8);
                            }
                        }
                        solutionTextBox.Text += (inter(d)+"f'(" + expr[j] + "(" + q + "))");
                    }
                    else if (st.Count == 0)  // is var,num
                    {
                        if (expr[j] == var) { ruleMessage("x",d); ans = "1"; }
                        else
                        {
                            ruleMessage("c",d);
                            ans = "0";
                        }
                        solutionTextBox.Text +=(inter(d)+ "f'(" + expr[j] + ")");
                    }
                    solutionTextBox.Text+="=" + ans + "\r\n";
                    return ans;
            }

            /*   catch (Exception ex)
               { MessageBox.Show(ex.Message); }
             */
        }
        Hashtable h = new Hashtable() // signpriority
           { { "+", 1 }, { "-", 1 }, { "*", 2 }, { "/", 2 }, { "^", 3 }, { "√", 3 }, { "(", 0 }, { "sin", 4 }, { "cos", 4 }, { "tg", 4 }, { "cotg", 4 } };
        private string[] depack(string s1)
        {
            Stack<string> st = new Stack<string>();
            string[] expr = new string[128];
            string q = "", q2 = "", s = "", s2 = "";
            bool fl = false;
            int j = 0, br1 = 0, r = 0, r2 = 0;

            s = s1;
            // string analysis
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] >= '0' && s[i] <= '9') // num
                {
                    q = "";
                    while (i + 1 < s.Length && ((s[i + 1] >= '0' && s[i + 1] <= '9') || s[i + 1] == ',')) { q += s[i]; i++; }
                    q += s[i];
                    expr[++j] = q;

                }
                else if (s[i] >= 'a' && s[i] <= 'z') // expr, var
                {   // missed *
                    if ((i - 1 >= 0) && (s[i - 1] >= '0' && s[i - 1] <= '9'))
                    {
                        s = s.Insert(i, "*");
                        i--;
                        continue;
                    }

                    q = ""; r = i;
                    while (i + 1 < s.Length && s[i + 1] >= 'a' && s[i + 1] <= 'z')
                    { q += s[i]; i++; }
                    q += s[i];
                    if (q.Length == 1) // var
                    {
                        expr[++j] = q;
                    }
                    else if (isTrig(q))
                    {
                        if (s[i + 1] != '(') attentionMessage("wrongexpr");
                        else st.Push(q);
                    }

                }
                // operate with scopes
                else if (s[i] == '(')
                {
                    // num*() , ()*()
                    if (i >= 1 && (s[i - 1] == ')' || (s[i - 1] >= '0' && s[i - 1] <= '9')))
                    { s = s.Insert(i, "*"); i--; }
                    else
                        st.Push("(");
                }
                else if (s[i] == ')')
                {    // find opening
                    while (!(st.Peek() == "("))
                    {
                        expr[++j] = st.Pop();
                    }
                    st.Pop();
                }
                else if (s[i] == '√')// replace √ with ^
                {
                    br1 = 0;
                    if (s[i + 1] != '(') attentionMessage("wrongexpr");
                    else br1++;
                    i += 2;
                    r = i;
                    fl = false;
                    i--;
                    while (!(br1 == 0))
                    {
                        if (s[++i] == '(') br1++;
                        else if (s[i] == ')') br1--;
                        if (br1 == 1 && s[i] == ',') { r2 = i + 1; fl = true; break; }
                    }
                    // replace
                    if (fl == true)
                    {
                        q = s.Substring(r, r2 - r - 1);
                        while (!(br1 == 0))
                        {
                            if (s[++i] == '(') br1++;
                            else if (s[i] == ')') br1--;
                        }
                        //√(b,3) -> b^(1/3) 
                        if (!(isNum(s.Substring(r2, i - r2)))) attentionMessage("wrongexpr");
                        q2 = (1/double.Parse(s.Substring(r2, i - r2))).ToString();
                      
                    }
                    // is sqrt
                    else if (fl == false)
                    {
                        q = s.Substring(r, i - r);
                        q2 = "0,5";
                    }
                    // actual replace of √ with ^
                    s = s.Remove(r - 2, i - r + 3);
                    s2 = q + "^" + q2;
                    s = s.Insert(r - 2, s2);
                    i = r - 3;
                }


                // math operators 
                else if (isSign(s[i].ToString()))
                {
                    // вадим докато не намерим оператор с по-малък приоритет
                    while (!(st.Count == 0))
                    {
                        if ((int)h[s[i].ToString()] <= (int)h[st.Peek().ToString()]) { expr[++j] = st.Pop(); }
                        else break;
                    }
                    st.Push(s[i].ToString());
                }

            }
            // empty stack at end
            while (!(st.Count == 0))
            {
                expr[++j] = st.Pop();
            }
            return expr;
        }
        private bool isParam(string q)
        {
            if (q.Length == 1 && q[0] >='a' && q[0] <= 'z' && !(q==varCombo.Text)) return true;
            return false;
        }
        private bool isNum(string q)
        {
            
            if(q[0]>='0'&&q[0]<='9')return true;
            else if (q.Length>1 && q[0] == '-' && (q[1] >= '0' && q[1] <= '9')) return true;
               return false;
        }

        private bool isSign(string p)
        {
            if(p.Length==1)
                if (p == "+" || p == "-" || p == "*" || p == "/" || p == "^" ) return true;
            return false;
        }
        private bool isTrig(string p)
        {
            if (p == "sin" || p == "cos" || p == "tg" || p == "cotg") return true;
            return false;
        }
        private void attentionMessage(string p)
        {
            string mes = "";
            switch (p)
            {
                case "novar": { mes = "Изберете променлива"; break; }
                case "noeq": { mes = "Изберете задача"; break; }
                case "wrongvar": { mes = "Променлива е една малка латинска буква"; break; }
                case "wrongscopes": { mes = "Неправилно поставени скоби"; break; }
                case "wrongexpr": { mes = "Проверете коректността на израза"; break; }
                case "graphparam": { mes = "Грешен диапазон на графиката"; break; }
            }
            attentionPanel.Visible = true;
            attentionText.Text = mes;
        }
        private void ruleMessage(string p,int j)
        { string mes = "";
        
        {
            switch (p)
            {
                case "c": { mes = "Производната на константа е 0"; break; }
                case "trig": { mes = "Производната на тригонометрична функция е таблична"; break; }
                case "+": { mes = "Производната на сбор е сума от производните на членовете"; break; }
                case "-": { mes = "Производната на разлика е разликата от производните на членовете"; break; }
                case "*": { mes = "Производната на произведение е f'*g+f*g'"; break; }
                case "/": { mes = "Производната на частно е {f'*g + f*g'}/{g^2}"; break; }
                case "c*": { mes = "Производната на произведение на функция с константа е const*f'"; break; }
                case "par*": { mes = "Производната на произведение на функция с параметър е параметър*f' "; break; }
                case "com": { mes = "Прилагаме правилото за сложна функция g(f(x))'=g'(f(x))*f'(x)"; break; }
                case "x": { mes = "Производната на променливата е 1"; break; } 
            }
            solutionTextBox.Text += (inter(j) + (mes + "\r\n")); ;
        }
        }

        string inter(int j) // adds j spaces
        {string s="";
        for (int i = 1; i <= j; i++)
            s += " ";
        return s;
        }
    }
    public static class RichTextBoxExtensions
    { // draws background for sollution textbox
        public static void PaintBack(this RichTextBox box)
        {

            Graphics gr = box.CreateGraphics();
            Random r1 = new Random();
            gr.FillRectangle(new SolidBrush(Color.White), 0, 0, box.Width, box.Height);
            int x = 0, y = 0, a1 = 13, a2 = 29, h = 800, w = 800;
            h = box.Height; w = box.Width;
            int dens = h * w / 6400;
            for (int i = 1; i <= dens; i++)
            {

                x = r1.Next() % h;
                y = r1.Next() % w;

                Pen myPen = new Pen(System.Drawing.Color.FromArgb((r1.Next() % 25) * 10, (r1.Next() % 25) * 10, (r1.Next() % 25) * 10));
                gr.DrawBezier(myPen, x, y, x - a1 + r1.Next() % a2, y - a1 + r1.Next() % a2, x - a1 + r1.Next() % a2, y - a1 + r1.Next() % a2, x - a1 + r1.Next() % a2, y - a1 + r1.Next() % a2);
            }
        }
    }
}
