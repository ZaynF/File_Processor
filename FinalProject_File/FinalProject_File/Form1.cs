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

namespace FinalProject_File
{
    public partial class InterFace : Form
    {
        public InterFace()
        {
            InitializeComponent();
        }

        String destination;
        String filename;
        String delfile;
        
        private void Search_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                destination = textBox1.Text;
                filename = textBox2.Text;
                Search_Result result = new Search_Result();
                result.Show();
                foreach (string fname in System.IO.Directory.GetFileSystemEntries(destination, filename))
                {
                    //Console.WriteLine(fname);
                    result.checkedListBox1.Items.Add(fname);
                }
            }

            else if(textBox1.Text != "")
            {
                destination = textBox1.Text;
                Search_Result result = new Search_Result();
                result.Show();
                foreach (string fname in System.IO.Directory.GetFileSystemEntries(destination))
                {
                    //Console.WriteLine(fname);
                    result.checkedListBox1.Items.Add(fname);
                }
            }

            else
            {
                destination = "C:\\";
                Search_Result result = new Search_Result();
                result.Show();
                foreach (string fname in System.IO.Directory.GetFileSystemEntries(destination))
                {
                    //Console.WriteLine(fname);
                    result.checkedListBox1.Items.Add(fname);
                }
            }           
        }

        private void InterFace_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void Tell_Click(object sender, EventArgs e)
        {
            Explain_Interface ei = new Explain_Interface();
            ei.Show();
        }
    }
}
