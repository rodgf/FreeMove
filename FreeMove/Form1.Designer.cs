﻿namespace FreeMove {
  partial class Form1 {
    /// <summary>
    /// Variabile di progettazione necessaria.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Pulire le risorse in uso.
    /// </summary>
    /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Codice generato da Progettazione Windows Form

    /// <summary>
    /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
    /// il contenuto del metodo con l'editor di codice.
    /// </summary>
    private void InitializeComponent() {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.label1 = new System.Windows.Forms.Label();
      this.textBox_From = new System.Windows.Forms.TextBox();
      this.button_BrowseFrom = new System.Windows.Forms.Button();
      this.button_BrowseTo = new System.Windows.Forms.Button();
      this.textBox_To = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.button_Move = new System.Windows.Forms.Button();
      this.checkBox1 = new System.Windows.Forms.CheckBox();
      this.button_Close = new System.Windows.Forms.Button();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.gitHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.opFile = new System.Windows.Forms.RadioButton();
      this.opFolder = new System.Windows.Forms.RadioButton();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.chShellInt = new System.Windows.Forms.CheckBox();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(15, 63);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(63, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Move From:";
      // 
      // textBox_From
      // 
      this.textBox_From.Location = new System.Drawing.Point(85, 63);
      this.textBox_From.Name = "textBox_From";
      this.textBox_From.Size = new System.Drawing.Size(383, 20);
      this.textBox_From.TabIndex = 1;
      // 
      // button_BrowseFrom
      // 
      this.button_BrowseFrom.Location = new System.Drawing.Point(474, 61);
      this.button_BrowseFrom.Name = "button_BrowseFrom";
      this.button_BrowseFrom.Size = new System.Drawing.Size(75, 23);
      this.button_BrowseFrom.TabIndex = 2;
      this.button_BrowseFrom.Text = "Browse...";
      this.button_BrowseFrom.UseVisualStyleBackColor = true;
      this.button_BrowseFrom.Click += new System.EventHandler(this.Button_BrowseFrom_Click);
      // 
      // button_BrowseTo
      // 
      this.button_BrowseTo.Location = new System.Drawing.Point(474, 92);
      this.button_BrowseTo.Name = "button_BrowseTo";
      this.button_BrowseTo.Size = new System.Drawing.Size(75, 23);
      this.button_BrowseTo.TabIndex = 4;
      this.button_BrowseTo.Text = "Browse...";
      this.button_BrowseTo.UseVisualStyleBackColor = true;
      this.button_BrowseTo.Click += new System.EventHandler(this.Button_BrowseTo_Click);
      // 
      // textBox_To
      // 
      this.textBox_To.Location = new System.Drawing.Point(85, 93);
      this.textBox_To.Name = "textBox_To";
      this.textBox_To.Size = new System.Drawing.Size(383, 20);
      this.textBox_To.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(15, 93);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(23, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "To:";
      // 
      // folderBrowserDialog1
      // 
      this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
      // 
      // button_Move
      // 
      this.button_Move.Location = new System.Drawing.Point(471, 156);
      this.button_Move.Name = "button_Move";
      this.button_Move.Size = new System.Drawing.Size(75, 23);
      this.button_Move.TabIndex = 6;
      this.button_Move.Text = "Move";
      this.button_Move.UseVisualStyleBackColor = true;
      this.button_Move.Click += new System.EventHandler(this.Button_Move_Click);
      // 
      // checkBox1
      // 
      this.checkBox1.AutoSize = true;
      this.checkBox1.Checked = true;
      this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBox1.Location = new System.Drawing.Point(18, 119);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new System.Drawing.Size(154, 17);
      this.checkBox1.TabIndex = 5;
      this.checkBox1.Text = "Set original folder to hidden";
      this.checkBox1.UseVisualStyleBackColor = true;
      // 
      // button_Close
      // 
      this.button_Close.Location = new System.Drawing.Point(12, 156);
      this.button_Close.Name = "button_Close";
      this.button_Close.Size = new System.Drawing.Size(75, 23);
      this.button_Close.TabIndex = 7;
      this.button_Close.Text = "Close";
      this.button_Close.UseVisualStyleBackColor = true;
      this.button_Close.Click += new System.EventHandler(this.Button_Close_Click);
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.menuStrip1.Size = new System.Drawing.Size(566, 24);
      this.menuStrip1.TabIndex = 8;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // infoToolStripMenuItem
      // 
      this.infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gitHubToolStripMenuItem});
      this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
      this.infoToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.infoToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
      this.infoToolStripMenuItem.Text = "Info";
      // 
      // gitHubToolStripMenuItem
      // 
      this.gitHubToolStripMenuItem.Name = "gitHubToolStripMenuItem";
      this.gitHubToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
      this.gitHubToolStripMenuItem.Text = "GitHub";
      this.gitHubToolStripMenuItem.Click += new System.EventHandler(this.GitHubToolStripMenuItem_Click);
      // 
      // opFile
      // 
      this.opFile.AutoSize = true;
      this.opFile.Location = new System.Drawing.Point(18, 33);
      this.opFile.Name = "opFile";
      this.opFile.Size = new System.Drawing.Size(41, 17);
      this.opFile.TabIndex = 9;
      this.opFile.Text = "File";
      this.opFile.UseVisualStyleBackColor = true;
      // 
      // opFolder
      // 
      this.opFolder.AutoSize = true;
      this.opFolder.Checked = true;
      this.opFolder.Location = new System.Drawing.Point(85, 33);
      this.opFolder.Name = "opFolder";
      this.opFolder.Size = new System.Drawing.Size(54, 17);
      this.opFolder.TabIndex = 10;
      this.opFolder.TabStop = true;
      this.opFolder.Text = "Folder";
      this.opFolder.UseVisualStyleBackColor = true;
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      // 
      // chShellInt
      // 
      this.chShellInt.AutoSize = true;
      this.chShellInt.Location = new System.Drawing.Point(447, 33);
      this.chShellInt.Name = "chShellInt";
      this.chShellInt.Size = new System.Drawing.Size(102, 17);
      this.chShellInt.TabIndex = 11;
      this.chShellInt.Text = "Shell Integration";
      this.chShellInt.UseVisualStyleBackColor = true;
      this.chShellInt.CheckedChanged += new System.EventHandler(this.chShellInt_CheckedChanged);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(566, 192);
      this.Controls.Add(this.chShellInt);
      this.Controls.Add(this.opFolder);
      this.Controls.Add(this.opFile);
      this.Controls.Add(this.button_Close);
      this.Controls.Add(this.checkBox1);
      this.Controls.Add(this.button_Move);
      this.Controls.Add(this.button_BrowseTo);
      this.Controls.Add(this.textBox_To);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.button_BrowseFrom);
      this.Controls.Add(this.textBox_From);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.menuStrip1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip1;
      this.MaximizeBox = false;
      this.Name = "Form1";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Free Move";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBox_From;
    private System.Windows.Forms.Button button_BrowseFrom;
    private System.Windows.Forms.Button button_BrowseTo;
    private System.Windows.Forms.TextBox textBox_To;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.Button button_Move;
    private System.Windows.Forms.CheckBox checkBox1;
    private System.Windows.Forms.Button button_Close;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem gitHubToolStripMenuItem;
    private System.Windows.Forms.RadioButton opFile;
    private System.Windows.Forms.RadioButton opFolder;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.CheckBox chShellInt;
  }
}

