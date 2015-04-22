namespace SpreadsheetGUI
{
    partial class SpreadsheetForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpreadsheetForm));
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.File = new System.Windows.Forms.ToolStripMenuItem();
            this.New = new System.Windows.Forms.ToolStripMenuItem();
            this.Open = new System.Windows.Forms.ToolStripMenuItem();
            this.Save = new System.Windows.Forms.ToolStripMenuItem();
            this.Close = new System.Windows.Forms.ToolStripMenuItem();
            this.Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.Help = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.CellNameLabel = new System.Windows.Forms.Label();
            this.CellNameBox = new System.Windows.Forms.TextBox();
            this.ValueLabel = new System.Windows.Forms.Label();
            this.ValueBox = new System.Windows.Forms.TextBox();
            this.ContentsLabel = new System.Windows.Forms.Label();
            this.InputBox = new System.Windows.Forms.GroupBox();
            this.SetKey = new System.Windows.Forms.Button();
            this.ContentsBox = new System.Windows.Forms.TextBox();
            this.spreadsheetGrid = new SS.SpreadsheetPanel();
            this.SaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.MenuBar.SuspendLayout();
            this.InputBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuBar
            // 
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File,
            this.Edit,
            this.Help});
            this.MenuBar.Location = new System.Drawing.Point(0, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Size = new System.Drawing.Size(612, 24);
            this.MenuBar.TabIndex = 0;
            this.MenuBar.Text = "menuStrip1";
            // 
            // File
            // 
            this.File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.New,
            this.Open,
            this.Save,
            this.Close});
            this.File.Name = "File";
            this.File.Size = new System.Drawing.Size(37, 20);
            this.File.Text = "File";
            // 
            // New
            // 
            this.New.Name = "New";
            this.New.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.New.Size = new System.Drawing.Size(159, 22);
            this.New.Text = "New      ";
            this.New.Click += new System.EventHandler(this.New_Click);
            // 
            // Open
            // 
            this.Open.Name = "Open";
            this.Open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.Open.Size = new System.Drawing.Size(159, 22);
            this.Open.Text = "Open...";
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // Save
            // 
            this.Save.Name = "Save";
            this.Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.Save.Size = new System.Drawing.Size(159, 22);
            this.Save.Text = "Save...";
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Close
            // 
            this.Close.Name = "Close";
            this.Close.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.Close.Size = new System.Drawing.Size(159, 22);
            this.Close.Text = "Close";
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // Edit
            // 
            this.Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Clear});
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(39, 20);
            this.Edit.Text = "Edit";
            // 
            // Clear
            // 
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(101, 22);
            this.Clear.Text = "Clear";
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // Help
            // 
            this.Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewHelp});
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(44, 20);
            this.Help.Text = "Help";
            // 
            // ViewHelp
            // 
            this.ViewHelp.Name = "ViewHelp";
            this.ViewHelp.Size = new System.Drawing.Size(127, 22);
            this.ViewHelp.Text = "View Help";
            this.ViewHelp.Click += new System.EventHandler(this.ViewHelp_Click);
            // 
            // CellNameLabel
            // 
            this.CellNameLabel.AutoSize = true;
            this.CellNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CellNameLabel.Location = new System.Drawing.Point(6, 12);
            this.CellNameLabel.Name = "CellNameLabel";
            this.CellNameLabel.Size = new System.Drawing.Size(47, 24);
            this.CellNameLabel.TabIndex = 1;
            this.CellNameLabel.Text = "Cell:";
            // 
            // CellNameBox
            // 
            this.CellNameBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CellNameBox.Location = new System.Drawing.Point(53, 15);
            this.CellNameBox.Name = "CellNameBox";
            this.CellNameBox.ReadOnly = true;
            this.CellNameBox.Size = new System.Drawing.Size(41, 20);
            this.CellNameBox.TabIndex = 2;
            // 
            // ValueLabel
            // 
            this.ValueLabel.AutoSize = true;
            this.ValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValueLabel.Location = new System.Drawing.Point(100, 12);
            this.ValueLabel.Name = "ValueLabel";
            this.ValueLabel.Size = new System.Drawing.Size(64, 24);
            this.ValueLabel.TabIndex = 3;
            this.ValueLabel.Text = "Value:";
            // 
            // ValueBox
            // 
            this.ValueBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ValueBox.Location = new System.Drawing.Point(165, 15);
            this.ValueBox.Name = "ValueBox";
            this.ValueBox.ReadOnly = true;
            this.ValueBox.Size = new System.Drawing.Size(135, 20);
            this.ValueBox.TabIndex = 4;
            // 
            // ContentsLabel
            // 
            this.ContentsLabel.AutoSize = true;
            this.ContentsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContentsLabel.Location = new System.Drawing.Point(307, 12);
            this.ContentsLabel.Name = "ContentsLabel";
            this.ContentsLabel.Size = new System.Drawing.Size(89, 24);
            this.ContentsLabel.TabIndex = 5;
            this.ContentsLabel.Text = "Contents:";
            // 
            // InputBox
            // 
            this.InputBox.Controls.Add(this.SetKey);
            this.InputBox.Controls.Add(this.ContentsBox);
            this.InputBox.Controls.Add(this.CellNameLabel);
            this.InputBox.Controls.Add(this.ContentsLabel);
            this.InputBox.Controls.Add(this.CellNameBox);
            this.InputBox.Controls.Add(this.ValueBox);
            this.InputBox.Controls.Add(this.ValueLabel);
            this.InputBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.InputBox.Location = new System.Drawing.Point(0, 24);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(612, 47);
            this.InputBox.TabIndex = 8;
            this.InputBox.TabStop = false;
            // 
            // SetKey
            // 
            this.SetKey.Location = new System.Drawing.Point(561, 13);
            this.SetKey.Name = "SetKey";
            this.SetKey.Size = new System.Drawing.Size(45, 23);
            this.SetKey.TabIndex = 7;
            this.SetKey.Text = "Set";
            this.SetKey.UseVisualStyleBackColor = true;
            this.SetKey.Click += new System.EventHandler(this.SetKey_Click);
            // 
            // ContentsBox
            // 
            this.ContentsBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ContentsBox.Location = new System.Drawing.Point(397, 15);
            this.ContentsBox.Name = "ContentsBox";
            this.ContentsBox.Size = new System.Drawing.Size(158, 20);
            this.ContentsBox.TabIndex = 6;
            this.ContentsBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ContentsBox_KeyDown);
            // 
            // spreadsheetGrid
            // 
            this.spreadsheetGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spreadsheetGrid.Location = new System.Drawing.Point(0, 71);
            this.spreadsheetGrid.Name = "spreadsheetGrid";
            this.spreadsheetGrid.Size = new System.Drawing.Size(612, 354);
            this.spreadsheetGrid.TabIndex = 7;
            // 
            // SaveDialog
            // 
            this.SaveDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveDialog_FileOk);
            // 
            // SpreadsheetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 425);
            this.Controls.Add(this.spreadsheetGrid);
            this.Controls.Add(this.InputBox);
            this.Controls.Add(this.MenuBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuBar;
            this.Name = "SpreadsheetForm";
            this.Text = "Spreadsheet";
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.InputBox.ResumeLayout(false);
            this.InputBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem File;
        private System.Windows.Forms.ToolStripMenuItem New;
        private System.Windows.Forms.ToolStripMenuItem Close;
        private System.Windows.Forms.ToolStripMenuItem Open;
        private System.Windows.Forms.ToolStripMenuItem Help;
        private System.Windows.Forms.ToolStripMenuItem Save;
        private System.Windows.Forms.Label CellNameLabel;
        private System.Windows.Forms.TextBox CellNameBox;
        private System.Windows.Forms.Label ValueLabel;
        private System.Windows.Forms.TextBox ValueBox;
        private System.Windows.Forms.Label ContentsLabel;
        private SS.SpreadsheetPanel spreadsheetGrid;
        private System.Windows.Forms.GroupBox InputBox;
        private System.Windows.Forms.TextBox ContentsBox;
        private System.Windows.Forms.ToolStripMenuItem Edit;
        private System.Windows.Forms.ToolStripMenuItem Clear;
        private System.Windows.Forms.ToolStripMenuItem ViewHelp;
        private System.Windows.Forms.Button SetKey;
        private System.Windows.Forms.SaveFileDialog SaveDialog;


    }
}

