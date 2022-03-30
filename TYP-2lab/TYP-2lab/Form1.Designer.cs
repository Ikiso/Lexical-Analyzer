using System.ComponentModel;
using System.Windows.Forms;

namespace TYP_2lab
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelTableSerevalWorld = new System.Windows.Forms.Label();
            this.listBoxForServerWord = new System.Windows.Forms.ListBox();
            this.listBoxRazdelitelei = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxIndificate = new System.Windows.Forms.ListBox();
            this.listBoxDigit = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelTextResult = new System.Windows.Forms.Label();
            this.labelCode = new System.Windows.Forms.Label();
            this.labelResultBox = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemVibor = new System.Windows.Forms.ToolStripMenuItem();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PerformAnAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CleareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxCode = new System.Windows.Forms.TextBox();
            this.textBoxCodeResult = new System.Windows.Forms.TextBox();
            this.textBoxReusltMessage = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTableSerevalWorld
            // 
            this.labelTableSerevalWorld.AutoSize = true;
            this.labelTableSerevalWorld.Location = new System.Drawing.Point(12, 28);
            this.labelTableSerevalWorld.Name = "labelTableSerevalWorld";
            this.labelTableSerevalWorld.Size = new System.Drawing.Size(165, 15);
            this.labelTableSerevalWorld.TabIndex = 0;
            this.labelTableSerevalWorld.Text = "Таблица служеюных слов(1)";
            // 
            // listBoxForServerWord
            // 
            this.listBoxForServerWord.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listBoxForServerWord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxForServerWord.FormattingEnabled = true;
            this.listBoxForServerWord.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listBoxForServerWord.ItemHeight = 15;
            this.listBoxForServerWord.Location = new System.Drawing.Point(12, 46);
            this.listBoxForServerWord.Name = "listBoxForServerWord";
            this.listBoxForServerWord.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listBoxForServerWord.Size = new System.Drawing.Size(216, 135);
            this.listBoxForServerWord.TabIndex = 1;
            // 
            // listBoxRazdelitelei
            // 
            this.listBoxRazdelitelei.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listBoxRazdelitelei.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxRazdelitelei.FormattingEnabled = true;
            this.listBoxRazdelitelei.ItemHeight = 15;
            this.listBoxRazdelitelei.Location = new System.Drawing.Point(234, 46);
            this.listBoxRazdelitelei.Name = "listBoxRazdelitelei";
            this.listBoxRazdelitelei.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listBoxRazdelitelei.Size = new System.Drawing.Size(216, 135);
            this.listBoxRazdelitelei.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(234, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Таблица разделителей(2)";
            // 
            // listBoxIndificate
            // 
            this.listBoxIndificate.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listBoxIndificate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxIndificate.FormattingEnabled = true;
            this.listBoxIndificate.ItemHeight = 15;
            this.listBoxIndificate.Location = new System.Drawing.Point(234, 231);
            this.listBoxIndificate.Name = "listBoxIndificate";
            this.listBoxIndificate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listBoxIndificate.Size = new System.Drawing.Size(216, 150);
            this.listBoxIndificate.TabIndex = 4;
            // 
            // listBoxDigit
            // 
            this.listBoxDigit.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listBoxDigit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxDigit.FormattingEnabled = true;
            this.listBoxDigit.ItemHeight = 15;
            this.listBoxDigit.Location = new System.Drawing.Point(12, 231);
            this.listBoxDigit.Name = "listBoxDigit";
            this.listBoxDigit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listBoxDigit.Size = new System.Drawing.Size(216, 150);
            this.listBoxDigit.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Таблица чисел(3)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(234, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Таблица индификаторов(4)";
            // 
            // labelTextResult
            // 
            this.labelTextResult.AutoSize = true;
            this.labelTextResult.Location = new System.Drawing.Point(12, 408);
            this.labelTextResult.Name = "labelTextResult";
            this.labelTextResult.Size = new System.Drawing.Size(107, 15);
            this.labelTextResult.TabIndex = 11;
            this.labelTextResult.Text = "Результат анализа";
            // 
            // labelCode
            // 
            this.labelCode.AutoSize = true;
            this.labelCode.Location = new System.Drawing.Point(456, 28);
            this.labelCode.Name = "labelCode";
            this.labelCode.Size = new System.Drawing.Size(96, 15);
            this.labelCode.TabIndex = 12;
            this.labelCode.Text = "Код программы";
            // 
            // labelResultBox
            // 
            this.labelResultBox.AutoSize = true;
            this.labelResultBox.Location = new System.Drawing.Point(678, 28);
            this.labelResultBox.Name = "labelResultBox";
            this.labelResultBox.Size = new System.Drawing.Size(107, 15);
            this.labelResultBox.TabIndex = 13;
            this.labelResultBox.Text = "Результат анализа";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemVibor});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(897, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItemVibor
            // 
            this.toolStripMenuItemVibor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.PerformAnAnalysisToolStripMenuItem,
            this.CleareToolStripMenuItem});
            this.toolStripMenuItemVibor.Name = "toolStripMenuItemVibor";
            this.toolStripMenuItemVibor.Size = new System.Drawing.Size(70, 20);
            this.toolStripMenuItemVibor.Text = "Действие";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.SaveToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.FileToolStripMenuItem.Text = "Файл";
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.OpenToolStripMenuItem.Text = "Открыть";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.SaveToolStripMenuItem.Text = "Записать";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // PerformAnAnalysisToolStripMenuItem
            // 
            this.PerformAnAnalysisToolStripMenuItem.Name = "PerformAnAnalysisToolStripMenuItem";
            this.PerformAnAnalysisToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.PerformAnAnalysisToolStripMenuItem.Text = "Выполнить анализ";
            this.PerformAnAnalysisToolStripMenuItem.Click += new System.EventHandler(this.PerformAnAnalysisToolStripMenuItem_Click);
            // 
            // CleareToolStripMenuItem
            // 
            this.CleareToolStripMenuItem.Name = "CleareToolStripMenuItem";
            this.CleareToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.CleareToolStripMenuItem.Text = "Очистить";
            this.CleareToolStripMenuItem.Click += new System.EventHandler(this.CleareToolStripMenuItem_Click);
            // 
            // textBoxCode
            // 
            this.textBoxCode.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBoxCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCode.Location = new System.Drawing.Point(456, 46);
            this.textBoxCode.Multiline = true;
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxCode.Size = new System.Drawing.Size(216, 354);
            this.textBoxCode.TabIndex = 15;
            this.textBoxCode.TextChanged += new System.EventHandler(this.textBoxCode_TextChanged);
            // 
            // textBoxCodeResult
            // 
            this.textBoxCodeResult.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBoxCodeResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCodeResult.Location = new System.Drawing.Point(678, 46);
            this.textBoxCodeResult.Multiline = true;
            this.textBoxCodeResult.Name = "textBoxCodeResult";
            this.textBoxCodeResult.ReadOnly = true;
            this.textBoxCodeResult.Size = new System.Drawing.Size(216, 354);
            this.textBoxCodeResult.TabIndex = 16;
            // 
            // textBoxReusltMessage
            // 
            this.textBoxReusltMessage.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBoxReusltMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxReusltMessage.Location = new System.Drawing.Point(13, 427);
            this.textBoxReusltMessage.Multiline = true;
            this.textBoxReusltMessage.Name = "textBoxReusltMessage";
            this.textBoxReusltMessage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxReusltMessage.Size = new System.Drawing.Size(881, 52);
            this.textBoxReusltMessage.TabIndex = 17;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(897, 484);
            this.Controls.Add(this.textBoxReusltMessage);
            this.Controls.Add(this.textBoxCodeResult);
            this.Controls.Add(this.textBoxCode);
            this.Controls.Add(this.labelResultBox);
            this.Controls.Add(this.labelCode);
            this.Controls.Add(this.labelTextResult);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxDigit);
            this.Controls.Add(this.listBoxIndificate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxRazdelitelei);
            this.Controls.Add(this.listBoxForServerWord);
            this.Controls.Add(this.labelTableSerevalWorld);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Лексический Анализатор";
            this.TransparencyKey = System.Drawing.SystemColors.ControlLightLight;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelTableSerevalWorld;
        internal ListBox listBoxForServerWord;
        internal ListBox listBoxRazdelitelei;
        private Label label1;
        internal ListBox listBoxIndificate;
        internal ListBox listBoxDigit;
        private Label label2;
        private Label label3;
        private Label labelTextResult;
        private Label labelCode;
        private Label labelResultBox;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItemVibor;
        private ToolStripMenuItem FileToolStripMenuItem;
        private ToolStripMenuItem PerformAnAnalysisToolStripMenuItem;
        internal TextBox textBoxCode;
        private TextBox textBoxCodeResult;
        internal TextBox textBoxReusltMessage;
        internal OpenFileDialog openFileDialog1;
        private ToolStripMenuItem OpenToolStripMenuItem;
        private ToolStripMenuItem SaveToolStripMenuItem;
        internal SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem CleareToolStripMenuItem;
    }
}

