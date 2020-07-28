using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bulls_And_Cows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static int secret,bulls=0,cows=0,attempts=0;
        static  Stopwatch stopWatch = new Stopwatch();

        private void gameInstructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The Computer generates a 4 - digit Random secret number. The digits are all different. Then,the player tries to guess the number and Computer gives the number of matches. If the matching digits are in their right positions, they are 'bulls', if in different positions, they are 'cows'.");
          }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stopWatch.Reset();
            stopWatch.Start();
            Random r = new Random();
            int d1 = r.Next(1, 10), d2 = r.Next(1, 10), d3 = r.Next(1, 10), d4 = r.Next(1, 10);
            while (d1 == d2 || d1 == d3 || d1 == d4 || d2 == d3 || d2 == d4 || d3 == d4)
            {
                if (d1 == d2 || d2 == d3 || d2 == d4)
                {
                    d2 = r.Next(1, 10);
                }
                if (d1 == d3 || d2 == d3 || d3 == d4)
                {
                    d3 = r.Next(1, 10);
                }
                if (d1 == d4 || d2 == d4 || d3 == d4)
                {
                    d4 = r.Next(1, 10);
                }
            }
            secret = d1 * 1000 + d2 * 100 + d3 * 10 + d4 * 1;
            textBox1.Text = secret.ToString();
            textBox2.Enabled = true;
            button2.Enabled = true;
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            Label l1 = new Label();
            Label l2 = new Label();
            Label l3 = new Label();
            l1.Text = "Attempt No. ";
            tableLayoutPanel1.Controls.Add(l1);
            l2.Text = "Guess ";
            tableLayoutPanel1.Controls.Add(l2);
            l3.Text = "Result ";
            tableLayoutPanel1.Controls.Add(l3);
            toolStripLabel1.Text = "";
            toolStripLabel2.Text = "";
            toolStripLabel3.Text = "";
            bulls = 0;
            cows = 0;
            attempts = 0;
            label3.Text = "";
            textBox2.Text = "";
            textBox2.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            toolStripLabel1.Text = "Status : You Lost";
            toolStripLabel2.Text = "Attempts : "+attempts.ToString();
            toolStripLabel3.Text = "Secret no. is : " + secret.ToString();
            textBox2.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            label3.Text = "";
            textBox2.Text = "";
            stopWatch.Reset();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            foreach (char c in textBox2.Text.ToCharArray()) 
            { 
                if (System.Text.RegularExpressions.Regex.IsMatch(c.ToString(), "[^0-9]"))
                {
                    List<char> l = textBox2.Text.ToCharArray().ToList();
                    l.Remove(c);
                    textBox2.Text = new string(l.ToArray());
                    textBox2.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0; 
                }
            }
            if (textBox2.Text.Length == 4)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, "^(?!.*(.).*\\1)\\d{4}"))
                {
                    label3.Text = "";
                    button3.Enabled = true;

                }
                else
                {
                    label3.Text="Enter 4-digit number without repetition";
                    textBox2.Text = "";
                }
            }
            else
                button3.Enabled = false;      
        }

        public void count_bulls(int a,int b)
        {
            if (a == b)
                bulls++;
        }
        public void count_cows(int a,int b,int c,int d)
        {
            if (a == b)
                cows++;
            else if (a == c)
                cows++;
            else if (a == d)
                cows++;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            bulls = 0;
            cows = 0;
            int number = int.Parse(textBox2.Text);
            attempts++;
            Label l1 = new Label();
            Label l2 = new Label();
            Label l3 = new Label();
            if (secret==number)
            {
                stopWatch.Stop();
                bulls = 4;
                cows = 0;
                l1.Text = attempts.ToString();
                l2.Text = number.ToString();
                l3.Text = bulls + " Bulls and " + cows + " Cows";
                tableLayoutPanel1.Controls.Add(l1);
                tableLayoutPanel1.Controls.Add(l2);
                tableLayoutPanel1.Controls.Add(l3);
                toolStripLabel1.Text = "Status : You Won";
                toolStripLabel2.Text = "Attempts : "+attempts.ToString();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}",ts.Hours, ts.Minutes, ts.Seconds);
                toolStripLabel3.Text = "Time taken : "+ elapsedTime;
                textBox2.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;

            }
            else
            {
                int snum1 = (secret / 1000) % 10;
                int snum2=(secret / 100) % 10;
                int snum3= (secret / 10) % 10;
                int snum4= (secret / 1) % 10;
                int num1 = (number / 1000) % 10;
                int num2= (number / 100) % 10;
                int num3= (number / 10) % 10;
                int num4= (number / 1) % 10;
                count_bulls(snum1, num1);
                count_bulls(snum2, num2);
                count_bulls(snum3, num3);
                count_bulls(snum4, num4);
                count_cows(num1, snum2, snum3, snum4);
                count_cows(num2, snum1, snum3, snum4);
                count_cows(num3, snum1, snum2, snum4);
                count_cows(num4, snum1, snum2, snum3);
                l1.Text = attempts.ToString();
                l2.Text = number.ToString();
                l3.Text = bulls + " Bulls and " + cows + " Cows";
                tableLayoutPanel1.Controls.Add(l1);
                tableLayoutPanel1.Controls.Add(l2);
                tableLayoutPanel1.Controls.Add(l3);
            }
            textBox2.Text = "";
            textBox2.Focus();
            button3.Enabled = false;
        }
        
    }
}
