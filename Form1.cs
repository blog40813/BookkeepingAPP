using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AccountingApp
{
    public partial class Form1 : Form
    {
        SortedList<string, string> userlist = new SortedList<string, string>();
        string[] input = new string[2];
        int login = 0;
        string lo_user = "";
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Login";
            string get;
            if (!File.Exists("User.txt"))
            {
                StreamWriter sw = new StreamWriter("User.txt");
                sw.Close();
            }
                StreamReader sr = new StreamReader("User.txt");
            while ((get = sr.ReadLine()) != null)
            {
                input = get.Split(' ');
                userlist.Add(input[0], input[1]);
            }
            sr.Close();
            //MessageBox.Show("end");
        }



        private void button1_Click(object sender, EventArgs e)
        {
            //textBox1.Text = "";//先清空textbox1的文字
            bool exist = false;
            string password = "";
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                exist = userlist.TryGetValue(textBox1.Text, out password);
                if (!exist)
                {
                    DialogResult register = MessageBox.Show("是否註冊帳號?", "未找到使用者", MessageBoxButtons.YesNo);
                    if (register == DialogResult.Yes)
                    {                       
                        StreamWriter sw = new StreamWriter("User.txt", true);
                        userlist.Add(textBox1.Text, textBox2.Text);
                        exist = true;
                        login = 1;
                        sw.WriteLine(textBox1.Text + " " + textBox2.Text);
                        sw.Close();
                        lo_user = textBox1.Text;
                    }
                    else
                    {
                        login = 0;
                    }
                }
                else
                {
                    if (password == textBox2.Text)
                    {
                        lo_user = textBox1.Text;
                        login = 1;
                    }
                    else
                    {
                        login = -1;
                        MessageBox.Show("password error!");
                    }
                }




            }
            else
            {
                login = 0;
                if (textBox1.Text == "" && textBox2.Text == "") MessageBox.Show("請輸入帳號與密碼", "輸入錯誤");
                else if (textBox1.Text == "") MessageBox.Show("請輸入帳號", "輸入錯誤");
                else MessageBox.Show("請輸入密碼", "輸入錯誤");

            }

            Form2 f = new Form2(lo_user);  //產生Form2的物件，才可以使用它所提供的Method
            if (login == 1)
            {
                f.ShowDialog(this);//設定Form2為Form1的上層，並開啟Form2視窗。
                textBox1.Text = "";
                textBox2.Text = "";
            }

            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                //若使用者在Form2按下了OK，進入這個判斷式
                //textBox1.Text = "按下了" + f.DialogResult.ToString();
                //textBox1.Text = "";
                textBox2.Text = "";
            }
            else if (f.DialogResult == System.Windows.Forms.DialogResult.Cancel)
            {
                //若使用者在Form2按下了Cancel或者直接點選X關閉視窗，都會進入這個判斷式
                //textBox1.Text = "按下了" + f.DialogResult.ToString();
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                //textBox1.Text = "按下了" + f.DialogResult.ToString();
                textBox1.Text = "";
                textBox2.Text = "";
            }

            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
   
}
