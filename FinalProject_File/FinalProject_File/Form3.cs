using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Spire.Pdf;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Collections;
using System.Windows.Forms;

namespace FinalProject_File
{
    public partial class Combine_Interface : Form
    {
        public Combine_Interface()
        {
            InitializeComponent();
        }

        String original;
        String destination;
        String fname;
        String[] files;
        string Output;
        int pos;

        private void Combine_Interface_Load(object sender, EventArgs e)
        {

        }

        private void Select_Click(object sender, EventArgs e)
        {
            for(int i = 0;i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    listBox1.Items.Add(checkedListBox1.Items[i]);
                }
            }

            for(int i = 0;i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }

            //checkedListBox1.ClearSelected();
        }

        private void Combine_Pdf_Click(object sender, EventArgs e)
        {
           
            files = new String[] { "" };
            destination = listBox1.GetItemText(listBox1.Items[0]);
            pos = destination.LastIndexOf("\\");
            original = destination.Substring(0, pos);

            if(radioButton1.Checked == true)
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    files[files.Length - 1] = listBox1.GetItemText(listBox1.Items[i]);
                    Array.Resize(ref files, files.Length + 1);
                }

                Array.Resize(ref files, files.Length - 1);
               
                if (textBox1.Text != "")
                {
                    Output = original + "\\" + textBox1.Text + ".pdf";
                }

                else
                {
                    Output = original + "\\Output.pdf";
                }

                PdfDocumentBase doc = Spire.Pdf.PdfDocument.MergeFiles(files);
                doc.Save(Output, FileFormat.PDF);
                System.Diagnostics.Process.Start(Output);
            }

            else if(radioButton2.Checked == true && textBox2.Text != "")
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    files[files.Length - 1] = listBox1.GetItemText(listBox1.Items[i]);
                    Array.Resize(ref files, files.Length + 1);
                }

                Array.Resize(ref files, files.Length - 1);

                if (textBox1.Text != "")
                {
                    Output = textBox2.Text + "\\" + textBox1.Text + ".pdf";
                }

                else
                {
                    Output = textBox2.Text + "\\Output.pdf";
                }

                PdfDocumentBase doc = Spire.Pdf.PdfDocument.MergeFiles(files);
                doc.Save(Output, FileFormat.PDF);
                System.Diagnostics.Process.Start(Output);
            }

            else
            {
                MessageBox.Show("Error");
            }
        }

        private void Clear_all_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
