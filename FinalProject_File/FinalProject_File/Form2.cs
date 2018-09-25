using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Collections;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Core;
using Root.Reports;
using System.Windows.Forms;

namespace FinalProject_File
{
    public partial class Search_Result : Form
    {
        public Search_Result()
        {
            InitializeComponent();
        }
        int pos;
        int pos_2;
        int fname_length;
        string file_name;
        string content;

        string destination;
        string original;
        string compress;
        string zip_path;
        string new_path;
        string back_path = "";

        private void Choose_Click(object sender, EventArgs e)
        {

        }

        /********************************複製*********************************************/
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    if(radioButton1.Checked == true)
                    {
                        destination = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                        pos = destination.LastIndexOf("\\");
                        //label1.Text = pos.ToString();
                        file_name = destination.Substring(pos + 1);
                        //label1.Text = file_name;

                        if (file_name.Contains("."))
                        {
                            FileInfo file = new FileInfo(destination);
                            file.CopyTo(destination.Substring(0, pos + 1) + "copy_" + file_name);
                        }

                        else
                        {
                            string copy_path = destination.Substring(0, pos) + "\\" + file_name;
                            Directory.CreateDirectory(copy_path);
                            DirectoryCopy(destination, copy_path, true);
                        }
                    }

                    else if(radioButton2.Checked == true && textBox1.Text != "")
                    {
                        new_path = textBox1.Text;

                        destination = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                        pos = destination.LastIndexOf("\\");
                        //label1.Text = pos.ToString();
                        file_name = destination.Substring(pos + 1);
                        //label1.Text = file_name;

                        if (file_name.Contains("."))
                        {
                            FileInfo file = new FileInfo(destination);
                            file.CopyTo(new_path + "\\copy_" + file_name);
                        }

                        else
                        {
                            string copy_path = new_path + "\\copy_" + file_name;
                            Directory.CreateDirectory(copy_path);
                            DirectoryCopy(destination, copy_path, true);
                        }
                    }

                    else
                    {
                        MessageBox.Show("Error");
                    }
                   
                }
            }
        }

        /************************************轉檔*****************************************/

        public Microsoft.Office.Interop.Word.Document wordDocument { get; set; }
        public Microsoft.Office.Interop.PowerPoint.Presentation pptPresentation { get; set; }
        public Microsoft.Office.Interop.Excel.Workbook excelWorkbook { get; set; }
        private void button2_Click(object sender, EventArgs e)
        {
            for(int i = 0;i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    destination = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                    pos = destination.LastIndexOf("\\");
                    pos_2 = destination.LastIndexOf(".");
                    file_name = destination.Substring(0,pos_2);

                    if(destination.Substring(pos_2) == ".docx")
                    {
                        Microsoft.Office.Interop.Word.Application appWord = new Microsoft.Office.Interop.Word.Application();
                        wordDocument = appWord.Documents.Open(destination);
                        wordDocument.ExportAsFixedFormat(file_name + ".pdf", WdExportFormat.wdExportFormatPDF);
                    }

                    else if(destination.Substring(pos_2) == ".ppt")
                    {
                        Microsoft.Office.Interop.PowerPoint.Application appPpt = new Microsoft.Office.Interop.PowerPoint.Application();
                        pptPresentation = appPpt.Presentations.Open(destination, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoFalse);
                        pptPresentation.ExportAsFixedFormat(file_name + ".pdf", PpFixedFormatType.ppFixedFormatTypePDF);
                    }

                    else if(destination.Substring(pos_2) == ".xlsx")
                    {
                        Microsoft.Office.Interop.Excel.Application appExcel = new Microsoft.Office.Interop.Excel.Application();
                        excelWorkbook = appExcel.Workbooks.Open(destination);
                        Microsoft.Office.Interop.Excel.XlFileFormat xlformatpdf = (Microsoft.Office.Interop.Excel.XlFileFormat)57;
                        excelWorkbook.SaveAs(file_name + ".pdf", xlformatpdf);
                        //excelWorkbook.ExportAsFixedFormat(file_name + ".pdf",)
                    }

                    else if(destination.Substring(pos_2) == ".jpg")
                    {
                        Report r = new Report();//初始化
                        r.sTitle = "This is report.net testing file";//標題
                        r.sAuthor = "einboch";//作者

                        Root.Reports.Page p = new Root.Reports.Page(r);//加入新頁
                        RepObj ro = new RepImageMM(destination, 200, 150);//讀取影像檔及設定影像大小
                        p.AddCB_MM(200, ro);//報表物件加入

                        RT.ViewPDF(r, destination.Substring(0,pos_2) + ".pdf");//產生PDF檔 
                    }

                    else
                    {
                        MessageBox.Show("當前檔案無法轉換成pdf格式", "Error");
                    }
                }
            }
        }

        /***********************************解壓縮***************************************/
        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    if (radioButton1.Checked == true)
                    {
                        destination = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                        pos = destination.LastIndexOf("\\");
                        pos_2 = destination.LastIndexOf(".");
                        fname_length = pos_2 - pos - 1;
                        //label1.Text = pos_2.ToString();
                        file_name = destination.Substring(pos + 1, fname_length);
                        label1.Text = file_name;

                        string extract_path = destination.Substring(0, pos_2);
                        Directory.CreateDirectory(extract_path);
                        //ZipFile.CreateFromDirectory(startPath, zipPath);

                        using (ZipArchive archive = ZipFile.OpenRead(destination))
                        {
                            if(comboBox1.SelectedItem == "all" || comboBox1.SelectedItem == null)
                            {
                                ZipFile.ExtractToDirectory(destination, extract_path);
                            }

                            else if(comboBox1.SelectedItem != "all")
                            {
                                foreach (ZipArchiveEntry entry in archive.Entries)
                                {
                                    if (entry.FullName.EndsWith(comboBox1.SelectedItem.ToString(), StringComparison.OrdinalIgnoreCase))
                                    {
                                        entry.ExtractToFile(Path.Combine(extract_path, entry.FullName));
                                    }
                                }
                            }
                        }
                    }

                    else if(radioButton2.Checked == true && textBox1.Text != "")
                    {
                        destination = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                        string extract_path = textBox1.Text;
                        if(Directory.Exists(extract_path) == false)
                        {
                            Directory.CreateDirectory(extract_path);
                        }
                        pos = destination.LastIndexOf("\\");
                        pos_2 = destination.LastIndexOf(".");
                        fname_length = pos_2 - pos - 1;
                        //label1.Text = pos_2.ToString();
                        file_name = destination.Substring(pos + 1, fname_length);

                        extract_path = extract_path +"\\" + file_name;

                        Directory.CreateDirectory(extract_path);

                        using (ZipArchive archive = ZipFile.OpenRead(destination))
                        {
                            if (comboBox1.SelectedItem == "all" || comboBox1.SelectedItem == null)
                            {
                                ZipFile.ExtractToDirectory(destination, extract_path);
                            }

                            else if (comboBox1.SelectedItem != "all")
                            {
                                foreach (ZipArchiveEntry entry in archive.Entries)
                                {
                                    if (entry.FullName.EndsWith(comboBox1.SelectedItem.ToString(), StringComparison.OrdinalIgnoreCase))
                                    {
                                        entry.ExtractToFile(Path.Combine(extract_path, entry.FullName));
                                    }
                                }
                            }
                        }
                    }

                    else
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
        }
        /***************************壓縮******************************************/
        private void button4_Click(object sender, EventArgs e)
        {
            int num = 0;
            
            for(int i = 0;i < checkedListBox1.Items.Count; i++)
            {
                if(checkedListBox1.GetItemChecked(i) == true)
                {
                    if(radioButton1.Checked == true)
                    {
                        destination = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                        pos = destination.LastIndexOf("\\");
                        original = destination.Substring(0, pos);
                        compress = original + "\\copy_" + textBox2.Text;
                        zip_path = original + "\\" + textBox2.Text + ".zip";
                        file_name = destination.Substring(pos);
                        //label1.Text = compress;

                        if (num == 0)
                        {
                            Directory.CreateDirectory(compress);
                            //ZipFile.CreateFromDirectory(compress, zip_path);
                            num++;

                            if (file_name.Contains("."))
                            {
                                FileInfo file = new FileInfo(destination);
                                file.CopyTo(compress + "\\" + file_name);
                            }

                            else
                            {
                                DirectoryCopy(destination, compress, true);
                            }
                        }

                        else
                        {
                            if (file_name.Contains("."))
                            {
                                FileInfo file = new FileInfo(destination);
                                file.CopyTo(compress + "\\" + file_name);
                            }

                            else
                            {
                                //Directory
                                string copy_path = compress + "\\" + file_name;
                                Directory.CreateDirectory(copy_path);
                                DirectoryCopy(destination, copy_path, true);
                            }                                                       
                        }
                    }

                    else if(radioButton2.Checked == true && textBox1.Text != "")
                    {
                        destination = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                        pos = destination.LastIndexOf("\\");
                        original = textBox1.Text;
                        compress = original + "\\" + textBox2.Text;
                        zip_path = compress + ".zip";
                        file_name = destination.Substring(pos);

                        if (num == 0)
                        {
                            Directory.CreateDirectory(compress);
                            //ZipFile.CreateFromDirectory(compress, zip_path);
                            num++;

                            if (file_name.Contains("."))
                            {
                                FileInfo file = new FileInfo(destination);
                                file.CopyTo(compress + "\\" + file_name);
                            }

                            else
                            {
                                DirectoryCopy(destination, compress, true);
                            }
                        }

                        else
                        {
                            if (file_name.Contains("."))
                            {
                                FileInfo file = new FileInfo(destination);
                                file.CopyTo(compress + "\\" + file_name);
                            }

                            else
                            {
                                string copy_path = compress + "\\" + file_name;
                                Directory.CreateDirectory(copy_path);
                                DirectoryCopy(destination, copy_path, true);
                            }
                        }
                    }

                    else
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
            ZipFile.CreateFromDirectory(compress, zip_path);
            //label1.Text = compress;
            Directory.Delete(compress,true);
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
        /******************************加入壓縮****************************************/
        private void button5_Click(object sender, EventArgs e)
        {
            int num = 0;
            for (int i = 0;i < checkedListBox1.Items.Count; i++)
            {                
                if(checkedListBox1.GetItemChecked(i) == true)
                {
                    destination = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                    pos = destination.LastIndexOf("\\");
                    file_name = destination.Substring(pos);

                    if (file_name.EndsWith(".zip") && num < 1)
                    {
                        zip_path = destination;
                        break;
                    }                   
                }
            }

            for(int i = 0;i < checkedListBox1.Items.Count; i++)
            {
                if(checkedListBox1.GetItemChecked(i) == true)
                {
                    destination = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                    pos = destination.LastIndexOf("\\");
                    file_name = destination.Substring(pos);

                    if(destination != zip_path)
                    {
                        using (FileStream zipToOpen = new FileStream(zip_path, FileMode.Open))
                        {
                            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                            {
                                ZipArchiveEntry readmeEntry = archive.CreateEntry(file_name);
                                using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
                                {
                                    writer.WriteLine("Information about this package.");
                                    writer.WriteLine("========================");
                                }
                            }
                        }
                    }                    
                }
            }
        }

        /*************************************刪除***************************************/
        private void delete_Click(object sender, EventArgs e)
        {
            for(int i = 0;i < checkedListBox1.Items.Count; i++)
            {
                if(checkedListBox1.GetItemChecked(i) == true)
                {
                    destination = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                    pos = destination.LastIndexOf("\\");
                    file_name = destination.Substring(pos);

                    if (file_name.Contains("."))
                    {
                        FileInfo file = new FileInfo(destination);
                        file.Delete();
                    }

                    else
                    {
                        Directory.Delete(destination,true);
                    }
                }
            }
        }

        /****************************************更新************************************/
        private void refresh_Click(object sender, EventArgs e)
        {
            destination = checkedListBox1.GetItemText(checkedListBox1.Items[0]);
            pos = destination.LastIndexOf("\\");
            original = destination.Substring(0, pos);

            checkedListBox1.Items.Clear();

            foreach (string fname in System.IO.Directory.GetFileSystemEntries(original))
            {
                checkedListBox1.Items.Add(fname);
            }
        }

        /*************************************移動***************************************/
        private void move_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("新路徑不能為空", "Error");
            }

            else
            {
                for(int i = 0;i < checkedListBox1.Items.Count; i++)
                {
                    if(checkedListBox1.GetItemChecked(i) == true)
                    {
                        destination = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                        pos = destination.LastIndexOf("\\");
                        file_name = destination.Substring(pos);
                        new_path = textBox1.Text;

                        if (file_name.Contains("."))
                        {
                            FileInfo file = new FileInfo(destination);
                            file.MoveTo(new_path + "\\" + file_name);
                        }

                        else
                        {
                            string copy_path = new_path + file_name;
                            Directory.Move(destination, copy_path);
                        }
                    }
                }
            }
        }

        /*******************************************進入*********************************/
        private void Enter_in_Click(object sender, EventArgs e)
        {
            for(int i = 0;i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    destination = checkedListBox1.GetItemText(checkedListBox1.Items[i]) + "\\";
                    pos = checkedListBox1.GetItemText(checkedListBox1.Items[i]).LastIndexOf("\\");
                    back_path = destination.Substring(0,pos) + "\\";
                    checkedListBox1.Items.Clear();

                    foreach (string fname in System.IO.Directory.GetFileSystemEntries(destination))
                    {
                        checkedListBox1.Items.Add(fname);
                    }
                    break;
                }
            }
        }

        /****************************************退回************************************/
        private void Go_Back_Click(object sender, EventArgs e)
        {
            pos = destination.LastIndexOf("\\");
            destination = destination.Substring(0, pos);
            pos_2 = destination.LastIndexOf("\\");
            back_path = destination.Substring(0, pos_2) + "\\";

            checkedListBox1.Items.Clear();

            foreach (string fname in System.IO.Directory.GetFileSystemEntries(back_path))
            {
                checkedListBox1.Items.Add(fname);
            }
        }

        /****************************************合併***********************************/
        private void Combine_Click(object sender, EventArgs e)
        {
            destination = checkedListBox1.GetItemText(checkedListBox1.Items[0]);
            pos = destination.LastIndexOf("\\");
            original = destination.Substring(0, pos);

            Combine_Interface ci = new Combine_Interface();
            ci.Show();
            foreach (string fname in System.IO.Directory.GetFileSystemEntries(original))
            {
                //Console.WriteLine(fname);
                ci.checkedListBox1.Items.Add(fname);
            }
        }

        private void Build_new_Click(object sender, EventArgs e)
        {
            for(int i = 0;i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    if (textBox1.Text == "")
                    {
                        MessageBox.Show("請輸入所要創建資料夾的路徑", "Error");
                    }

                    else
                    {
                        destination = checkedListBox1.GetItemText(checkedListBox1.Items[i]) + "\\" + textBox1.Text;
                        Directory.CreateDirectory(destination);
                    }
                }

                else if(i == checkedListBox1.Items.Count - 1 && !checkedListBox1.GetItemChecked(i))
                {
                    if (textBox1.Text == "")
                    {
                        MessageBox.Show("請輸入所要創建資料夾的路徑", "Error");
                    }

                    else
                    {
                        Directory.CreateDirectory(textBox1.Text);
                    }
                }
            }
            
        }
    }
}
