namespace FinalProject_File
{
    partial class Explain_Interface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox1.Location = new System.Drawing.Point(28, 31);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(599, 47);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "搜尋 : 可在上面打入指定的路徑並進行搜尋，另外在副檔名輸入 “*.副檔名”，則只顯示副檔名的結果，而若是沒輸入指定路徑，則搜尋結果自動導向C槽";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox2.Location = new System.Drawing.Point(28, 84);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(599, 22);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "轉檔 : 可以將docx、Excel、ppt，jpg轉換成pdf";
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox3.Location = new System.Drawing.Point(28, 121);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(599, 44);
            this.textBox3.TabIndex = 3;
            this.textBox3.Text = "解壓縮 : 可以藉由勾選搜尋結果將大量的zip黨進行解壓縮，另外，可以在旁邊選擇指解壓縮檔案裡的指定副檔名(ex : pdf、docx…)，另外，可決定要在原始路" +
    "徑解壓縮還是至指定路徑解壓縮";
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox4.Location = new System.Drawing.Point(28, 171);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(599, 22);
            this.textBox4.TabIndex = 4;
            this.textBox4.Text = "壓縮 : 藉由勾選搜尋結果，將大量資料壓縮為一個zip";
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox5.Location = new System.Drawing.Point(28, 209);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(599, 40);
            this.textBox5.TabIndex = 5;
            this.textBox5.Text = "加入壓縮 : 針對現有的zip檔，可再把新的檔案寫入壓縮檔，若是勾選多個zip檔，會針對第一個壓縮檔進行寫入(資料夾無法寫入)";
            // 
            // textBox6
            // 
            this.textBox6.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox6.Location = new System.Drawing.Point(28, 255);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(599, 43);
            this.textBox6.TabIndex = 6;
            this.textBox6.Text = "合併 : 在搜尋介面按合併，會開啟一個新的介面，並將當前搜尋結果輸出到新介面上，並藉由選擇來排列所要合併的順序，再將其輸出至指定的路徑(只能合併pdf)";
            // 
            // Explain_Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 344);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "Explain_Interface";
            this.Text = "說明";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
    }
}