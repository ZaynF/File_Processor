using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace FinalProject_File
{
    public partial class Write_Window : Form
    {
        public Write_Window()
        {
            InitializeComponent();
        }

        String path;

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("目的路徑不能為空");
            }
            else
            {
                FileInfo file = new FileInfo(@textBox1.Text);
                path = textBox1.Text;
                StreamWriter sw = file.CreateText();
                sw.WriteLine(textBox2.Text);
                sw.Flush();
                sw.Close();
                MessageBox.Show("存檔成功", "Warning");
                //MessageBox.Show("創建成功", "Warning");
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            //textBox2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}
