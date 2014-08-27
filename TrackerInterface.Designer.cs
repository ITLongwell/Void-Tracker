namespace VoidTracker
{
    partial class TrackerInterface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrackerInterface));
            this.TextDisplay = new System.Windows.Forms.RichTextBox();
            this.PathButton = new System.Windows.Forms.Button();
            this.PathTextBox = new System.Windows.Forms.TextBox();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.FileNameEntry = new System.Windows.Forms.TextBox();
            this.FileLabel = new System.Windows.Forms.Label();
            this.StopButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.newButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TotalTimeLabel = new System.Windows.Forms.Label();
            this.Time = new System.Windows.Forms.Label();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.SuspendLayout();
            // 
            // TextDisplay
            // 
            this.TextDisplay.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TextDisplay.Location = new System.Drawing.Point(12, 10);
            this.TextDisplay.Name = "TextDisplay";
            this.TextDisplay.Size = new System.Drawing.Size(360, 112);
            this.TextDisplay.TabIndex = 0;
            this.TextDisplay.Text = "";
            // 
            // PathButton
            // 
            this.PathButton.BackColor = System.Drawing.Color.DarkGray;
            this.PathButton.Location = new System.Drawing.Point(249, 157);
            this.PathButton.Name = "PathButton";
            this.PathButton.Size = new System.Drawing.Size(60, 25);
            this.PathButton.TabIndex = 1;
            this.PathButton.Text = "Set";
            this.PathButton.UseVisualStyleBackColor = false;
            this.PathButton.Click += new System.EventHandler(this.PathButton_Click);
            // 
            // PathTextBox
            // 
            this.PathTextBox.Location = new System.Drawing.Point(56, 159);
            this.PathTextBox.Name = "PathTextBox";
            this.PathTextBox.Size = new System.Drawing.Size(187, 21);
            this.PathTextBox.TabIndex = 2;
            // 
            // FileNameEntry
            // 
            this.FileNameEntry.Location = new System.Drawing.Point(56, 128);
            this.FileNameEntry.Name = "FileNameEntry";
            this.FileNameEntry.Size = new System.Drawing.Size(187, 21);
            this.FileNameEntry.TabIndex = 3;
            // 
            // FileLabel
            // 
            this.FileLabel.AutoSize = true;
            this.FileLabel.BackColor = System.Drawing.Color.DimGray;
            this.FileLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.FileLabel.Location = new System.Drawing.Point(12, 131);
            this.FileLabel.Name = "FileLabel";
            this.FileLabel.Size = new System.Drawing.Size(39, 14);
            this.FileLabel.TabIndex = 4;
            this.FileLabel.Text = "Name:";
            // 
            // StopButton
            // 
            this.StopButton.BackColor = System.Drawing.Color.DarkGray;
            this.StopButton.Location = new System.Drawing.Point(297, 229);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 59);
            this.StopButton.TabIndex = 5;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = false;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.Color.DarkGray;
            this.StartButton.Location = new System.Drawing.Point(15, 229);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 59);
            this.StartButton.TabIndex = 6;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.BackColor = System.Drawing.Color.DarkGray;
            this.loadButton.Location = new System.Drawing.Point(315, 127);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(57, 25);
            this.loadButton.TabIndex = 7;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = false;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // newButton
            // 
            this.newButton.BackColor = System.Drawing.Color.DarkGray;
            this.newButton.Location = new System.Drawing.Point(249, 127);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(60, 25);
            this.newButton.TabIndex = 9;
            this.newButton.Text = "New";
            this.newButton.UseVisualStyleBackColor = false;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DimGray;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(18, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 14);
            this.label1.TabIndex = 10;
            this.label1.Text = "Path:";
            // 
            // TotalTimeLabel
            // 
            this.TotalTimeLabel.AutoSize = true;
            this.TotalTimeLabel.BackColor = System.Drawing.Color.DimGray;
            this.TotalTimeLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TotalTimeLabel.Location = new System.Drawing.Point(248, 196);
            this.TotalTimeLabel.Name = "TotalTimeLabel";
            this.TotalTimeLabel.Size = new System.Drawing.Size(61, 14);
            this.TotalTimeLabel.TabIndex = 11;
            this.TotalTimeLabel.Text = "Total Time:";
            // 
            // Time
            // 
            this.Time.AutoSize = true;
            this.Time.BackColor = System.Drawing.Color.DimGray;
            this.Time.Location = new System.Drawing.Point(315, 196);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(49, 14);
            this.Time.TabIndex = 12;
            this.Time.Text = "00:00:00";
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Location = new System.Drawing.Point(15, 192);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(102, 18);
            this.checkBox.TabIndex = 13;
            this.checkBox.Text = "Active Logging";
            this.checkBox.UseVisualStyleBackColor = true;
            this.checkBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // TrackerInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(385, 300);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.Time);
            this.Controls.Add(this.TotalTimeLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.FileLabel);
            this.Controls.Add(this.FileNameEntry);
            this.Controls.Add(this.PathTextBox);
            this.Controls.Add(this.PathButton);
            this.Controls.Add(this.TextDisplay);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TrackerInterface";
            this.Text = "Void Tracker";
            this.Load += new System.EventHandler(this.TrackerInterface_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox TextDisplay;
        private System.Windows.Forms.Button PathButton;
        private System.Windows.Forms.TextBox PathTextBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.TextBox FileNameEntry;
        private System.Windows.Forms.Label FileLabel;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label TotalTimeLabel;
        private System.Windows.Forms.Label Time;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.HelpProvider helpProvider1;
    }
}

