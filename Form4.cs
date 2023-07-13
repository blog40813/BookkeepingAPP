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
    public partial class Form4 : Form
    {
        //Form3 categories;
        public static string formname = "Form4";

        //Form3 _records;
        Categories categories = new Categories();
        Records records = new Records();
        string user = "";        

        public Form4(string user,ref Records in_record)
        {
            InitializeComponent();
            comboBox1.Items.Add("全");
            comboBox1.Items.Add("--------支出");
            comboBox1.Items.Add("----食物");
            comboBox1.Items.Add("早餐");
            comboBox1.Items.Add("午餐");
            comboBox1.Items.Add("晚餐");
            comboBox1.Items.Add("點心");
            comboBox1.Items.Add("飲料");
            comboBox1.Items.Add("宵夜");
            comboBox1.Items.Add("其他（食）");
            comboBox1.Items.Add("----交通");
            comboBox1.Items.Add("汽油");
            comboBox1.Items.Add("公車");
            comboBox1.Items.Add("捷運");
            comboBox1.Items.Add("火車");
            comboBox1.Items.Add("其他（交）");
            comboBox1.Items.Add("----娛樂");
            comboBox1.Items.Add("電影");
            comboBox1.Items.Add("衣著");
            comboBox1.Items.Add("旅遊");
            comboBox1.Items.Add("其他（娛）");
            comboBox1.Items.Add("----生活");
            comboBox1.Items.Add("房租");
            comboBox1.Items.Add("電信");
            comboBox1.Items.Add("水電");
            comboBox1.Items.Add("其他（生）");
            comboBox1.Items.Add("--------收入");
            comboBox1.Items.Add("薪資");
            comboBox1.Items.Add("獎金");
            comboBox1.Items.Add("其他（收）");

            this.records = in_record;
            this.user = user;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                //將record中的數據顯示再txtrecord，透過viewintextbox實踐
                textBox1.Text = records.ViewInTextbox();
                //清空listbox
                listBox1.Items.Clear();
                //將record中匹配的記錄按照類添加到listbox_item
                listBox1.Items.AddRange(records.FindInListBox(comboBox1.SelectedItem.ToString(),categories).ToArray());
            }
            else
            {
                //將record中的數據顯示再txtrecord，透過findintextbox實踐
                textBox1.Text = records.ViewInTextbox();
                //textBox1.Text = records.FindInTextBox(comboBox1.SelectedItem.ToString(), categories);
                Console.WriteLine(comboBox1.SelectedItem.ToString());
                //清空listbox
                listBox1.Items.Clear();
                //將record中匹配的記錄按照類添加到listbox_item
                listBox1.Items.AddRange(records.FindInListBox(comboBox1.SelectedItem.ToString(), categories).ToArray());
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.Text = "檢視帳款";
            textBox1.Text = records.ViewInTextbox();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(records.FindInListBox("全", categories).ToArray());           
        }

        private void button_delete_Click(object sender, EventArgs e)
        {            
            button_delete.Enabled = false;

            if (listBox1.SelectedIndex != -1) //檢查是否有選取ListBox項目
            {
                string get = listBox1.SelectedItem.ToString();
                string index = "";
                int n = 0;
                while ('0' <= get[n] && get[n] <= '9')
                {
                    index += get[n];
                    n++;
                }
                records.Delete(int.Parse(index));

                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                textBox1.Text = records.ViewInTextbox();
                //移除所選取的ListBox項目
            }
            else
            {
                MessageBox.Show("請先選取要刪除的項目"); //若未選取任何項目，顯示提示訊息
            }
        }

        private void button_main_Click(object sender, EventArgs e)
        {       
                this.Close();           
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_delete.Enabled = true;
        }
    }
}
