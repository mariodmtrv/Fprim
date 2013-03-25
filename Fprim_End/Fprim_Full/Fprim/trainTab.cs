using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Fprim
{ 
    public partial class MainForm : System.Windows.Forms.Form
    {
        DateTime t = new DateTime();

        string[] questans = new string[128];
        int[] param = new int[] {1,2,3,2,1,3,2,2,1,2,3,1,2,1,3,2,2};
        bool[] usedq = new bool[128];
        int j = 0,presq=0,FullResult=0,curResult=0,qNum=0,questRead=0;
   
        private void readQuest()
        {
           StreamReader p = new StreamReader(@"Theory/moreinfo.txt", Encoding.Unicode);
         //  StreamReader p = new StreamReader(@".../.../Theory/moreinfo.txt", Encoding.Unicode);
            while ((questans[j] = p.ReadLine()) != null)
            {
                j++;
            }
            j /= 4;
        }
        private void startTrainButton_Click(object sender, EventArgs e)
        {
            emptyAll();
            trainInfo form = new trainInfo();
            form.ShowDialog();
            if (questRead == 0) 
            { readQuest(); questRead = 1; }
            
            stopButton.Visible = true;
            quizPanel.Show();
            loadQuestion();
            elapsedText.Visible = true;
            qLabel.Visible = true;
            resPanel.Visible = true;
            t = DateTime.Now;
            resultTimer.Start();
            elapsedTimer.Start();
            FullResult = 0;

           
        }
        private void resultTimer_Tick(object sender, EventArgs e)
        {
            if(curResult>48) curResult = (curResult * 92) / 100;
        }
        private void elapsedTimer_Tick(object sender, EventArgs e)
        {
            elapsedText.Text = (DateTime.Now-t).ToString().Substring(3,5);
        }
        private void ans_Click(object sender, EventArgs e)
        {Random luck=new Random();
        resultTimer.Stop();
        if (int.Parse(((Button)sender).Tag.ToString()) == param[presq])
        {
            resPanel.BackColor = Color.Lime;
            FullResult += curResult;
        }
        else 
        {
            resPanel.BackColor = Color.Red;
        }
        FullResult += luck.Next() % 5;
        loadQuestion();
        }
        private void loadQuestion()
        {
            Random r = new Random();
            int p = r.Next() % j;
            // find free question
            while (usedq[p] == true) { p = r.Next() % j; }
            presq = p;
            presq %=j;
            qNum++;
            if (qNum <= 10)
            {
                usedq[presq] = true;
                questionTextBox.Text = questans[4 * presq];
                ans1.Text = questans[4 * presq + 1];
                ans2.Text = questans[4 * presq + 2];
                ans3.Text = questans[4 * presq + 3];
                curResult = 100;
                qLabel.Text = qNum.ToString() + "/10";
                resultTimer.Start();
            }
            else 
            {
                string s = "Поздрравления! Вашият резултат е " + FullResult.ToString() + " точки.";
                elapsedTimer.Stop();
                MessageBox.Show(s,"Краен резултат",MessageBoxButtons.OK);
                emptyAll();
            }
        }
        private void emptyAll()
        {
            for (int i = 0; i <= j * 4;i++ ) usedq[i] = false;
            quizPanel.Hide();
            elapsedText.Text = "Начало!";
            elapsedText.Visible = false;
            qLabel.Text = "Тест";
            qLabel.Visible = false;
            presq = 0; FullResult = 0; curResult = 0; qNum = 0;
            resPanel.BackColor = Color.Transparent;
            stopButton.Visible = false;
        }
        private void stopButton_Click(object sender, EventArgs e)
        {
            emptyAll();

        }
    }
}
