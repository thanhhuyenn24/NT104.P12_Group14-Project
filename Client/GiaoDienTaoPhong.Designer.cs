namespace Client
{
    partial class GiaoDienTaoPhong
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.comboBoxPlayers = new System.Windows.Forms.ComboBox();
            this.comboBoxDrawTime = new System.Windows.Forms.ComboBox();
            this.comboBoxRounds = new System.Windows.Forms.ComboBox();
            this.comboBoxWordCount = new System.Windows.Forms.ComboBox();
            this.Status = new System.Windows.Forms.RichTextBox();
            this.btnLeave = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(104, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Players";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(104, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Drawtime";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Rounds";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(104, 251);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Word Count";
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(215, 320);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(175, 51);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start!";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // comboBoxPlayers
            // 
            this.comboBoxPlayers.Enabled = false;
            this.comboBoxPlayers.FormattingEnabled = true;
            this.comboBoxPlayers.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.comboBoxPlayers.Location = new System.Drawing.Point(269, 54);
            this.comboBoxPlayers.Name = "comboBoxPlayers";
            this.comboBoxPlayers.Size = new System.Drawing.Size(121, 24);
            this.comboBoxPlayers.TabIndex = 5;
            // 
            // comboBoxDrawTime
            // 
            this.comboBoxDrawTime.Enabled = false;
            this.comboBoxDrawTime.FormattingEnabled = true;
            this.comboBoxDrawTime.Items.AddRange(new object[] {
            "50",
            "60",
            "70",
            "80",
            "90",
            "100"});
            this.comboBoxDrawTime.Location = new System.Drawing.Point(269, 112);
            this.comboBoxDrawTime.Name = "comboBoxDrawTime";
            this.comboBoxDrawTime.Size = new System.Drawing.Size(121, 24);
            this.comboBoxDrawTime.TabIndex = 6;
            // 
            // comboBoxRounds
            // 
            this.comboBoxRounds.Enabled = false;
            this.comboBoxRounds.FormattingEnabled = true;
            this.comboBoxRounds.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5"});
            this.comboBoxRounds.Location = new System.Drawing.Point(269, 177);
            this.comboBoxRounds.Name = "comboBoxRounds";
            this.comboBoxRounds.Size = new System.Drawing.Size(121, 24);
            this.comboBoxRounds.TabIndex = 7;
            // 
            // comboBoxWordCount
            // 
            this.comboBoxWordCount.Enabled = false;
            this.comboBoxWordCount.FormattingEnabled = true;
            this.comboBoxWordCount.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.comboBoxWordCount.Location = new System.Drawing.Point(269, 243);
            this.comboBoxWordCount.Name = "comboBoxWordCount";
            this.comboBoxWordCount.Size = new System.Drawing.Size(121, 24);
            this.comboBoxWordCount.TabIndex = 8;
            // 
            // Status
            // 
            this.Status.Location = new System.Drawing.Point(440, 54);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(265, 317);
            this.Status.TabIndex = 9;
            this.Status.Text = "";
            // 
            // btnLeave
            // 
            this.btnLeave.Location = new System.Drawing.Point(675, 391);
            this.btnLeave.Name = "btnLeave";
            this.btnLeave.Size = new System.Drawing.Size(92, 34);
            this.btnLeave.TabIndex = 10;
            this.btnLeave.Text = "Leave";
            this.btnLeave.UseVisualStyleBackColor = true;
            this.btnLeave.Click += new System.EventHandler(this.btnLeave_Click);
            // 
            // btnOK
            // 
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(107, 320);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(102, 51);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click_1);
            // 
            // GiaoDienTaoPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnLeave);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.comboBoxWordCount);
            this.Controls.Add(this.comboBoxRounds);
            this.Controls.Add(this.comboBoxDrawTime);
            this.Controls.Add(this.comboBoxPlayers);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "GiaoDienTaoPhong";
            this.Text = "LOBBY";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnStart;
        public System.Windows.Forms.ComboBox comboBoxPlayers;
        public System.Windows.Forms.ComboBox comboBoxDrawTime;
        public System.Windows.Forms.ComboBox comboBoxRounds;
        public System.Windows.Forms.ComboBox comboBoxWordCount;
        private System.Windows.Forms.RichTextBox Status;
        private System.Windows.Forms.Button btnLeave;
        private System.Windows.Forms.Button btnOK;
    }
}