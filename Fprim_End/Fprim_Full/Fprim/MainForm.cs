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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
      
        
        private void MainForm_Load(object sender, EventArgs e)
        {
                  
            allTabControl.SelectedTab = eduTabPage;
           
        }

      private void MainForm_Shown(object sender, EventArgs e)
        {
            string s;
           // StreamReader p = new StreamReader(@".../.../Theory/addins.txt", Encoding.Unicode);
           StreamReader p = new StreamReader(@"Theory/addins.txt", Encoding.Unicode);
            s = p.ReadLine();
                Didyou f = new Didyou();
                f.checkShow.Visible = true;
               f.ShowDialog();
            
            while ((s=p.ReadLine())!= null)
            {  
                switch(s[0])
                {
                    case '1': {anyToolStripMenuItem.DropDownItems.Add(s.Substring(2)); break; }
                    case '2': { trigToolStripMenuItem.DropDownItems.Add(s.Substring(2)); break; }
                    case '3': { divToolStripMenuItem.DropDownItems.Add(s.Substring(2)); break; }
                }
            }
            p.Close();
        }
        private void allTabControl_Click(object sender, EventArgs e)
        {
            if (allTabControl.SelectedTab == eduTabPage) { eqPanel.Visible = true; solutionTextBox.Visible = true; }
            if(!(allTabControl.SelectedTab == eduTabPage)) { eqPanel.Visible = false; solutionTextBox.Visible = false; }
            if (!(allTabControl.SelectedTab == trainTabPage)) { emptyAll(); }
        }

        private void newExToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newExample f = new newExample();
            f.ShowDialog();
        }
    }
}
