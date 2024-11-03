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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GiaoDienTaoPhong));
            this.btnStart = new System.Windows.Forms.Button();
            this.comboBoxPlayers = new System.Windows.Forms.ComboBox();
            this.comboBoxDrawTime = new System.Windows.Forms.ComboBox();
            this.comboBoxRounds = new System.Windows.Forms.ComboBox();
            this.Status = new System.Windows.Forms.RichTextBox();
            this.btnLeave = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(224, 323);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(175, 51);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "  ";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // comboBoxPlayers
            // 
            this.comboBoxPlayers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.comboBoxPlayers.Enabled = false;
            this.comboBoxPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.comboBoxPlayers.FormattingEnabled = true;
            this.comboBoxPlayers.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5"});
            this.comboBoxPlayers.Location = new System.Drawing.Point(245, 83);
            this.comboBoxPlayers.Name = "comboBoxPlayers";
            this.comboBoxPlayers.Size = new System.Drawing.Size(150, 37);
            this.comboBoxPlayers.TabIndex = 5;
            // 
            // comboBoxDrawTime
            // 
            this.comboBoxDrawTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.comboBoxDrawTime.Enabled = false;
            this.comboBoxDrawTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.comboBoxDrawTime.FormattingEnabled = true;
            this.comboBoxDrawTime.Items.AddRange(new object[] {
            "30",
            "50",
            "60",
            "70",
            "80",
            "90",
            "100"});
            this.comboBoxDrawTime.Location = new System.Drawing.Point(245, 157);
            this.comboBoxDrawTime.Name = "comboBoxDrawTime";
            this.comboBoxDrawTime.Size = new System.Drawing.Size(150, 37);
            this.comboBoxDrawTime.TabIndex = 6;
            // 
            // comboBoxRounds
            // 
            this.comboBoxRounds.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.comboBoxRounds.Enabled = false;
            this.comboBoxRounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.comboBoxRounds.FormattingEnabled = true;
            this.comboBoxRounds.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.comboBoxRounds.Location = new System.Drawing.Point(245, 230);
            this.comboBoxRounds.Name = "comboBoxRounds";
            this.comboBoxRounds.Size = new System.Drawing.Size(150, 37);
            this.comboBoxRounds.TabIndex = 7;
            // 
            // Status
            // 
            this.Status.BackColor = System.Drawing.SystemColors.HighlightText;
            this.Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Status.Location = new System.Drawing.Point(451, 86);
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Size = new System.Drawing.Size(269, 288);
            this.Status.TabIndex = 9;
            this.Status.Text = "";
            // 
            // btnLeave
            // 
            this.btnLeave.Location = new System.Drawing.Point(628, 387);
            this.btnLeave.Name = "btnLeave";
            this.btnLeave.Size = new System.Drawing.Size(125, 34);
            this.btnLeave.TabIndex = 10;
            this.btnLeave.Text = "  ";
            this.btnLeave.UseVisualStyleBackColor = true;
            this.btnLeave.Click += new System.EventHandler(this.btnLeave_Click);
            // 
            // btnOK
            // 
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(73, 323);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(102, 51);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = " ";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click_1);
            // 
            // GiaoDienTaoPhong
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnLeave);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.comboBoxRounds);
            this.Controls.Add(this.comboBoxDrawTime);
            this.Controls.Add(this.comboBoxPlayers);
            this.Controls.Add(this.btnStart);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "GiaoDienTaoPhong";
            this.Text = "LOBBY";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        public System.Windows.Forms.ComboBox comboBoxPlayers;
        public System.Windows.Forms.ComboBox comboBoxDrawTime;
        public System.Windows.Forms.ComboBox comboBoxRounds;
        private System.Windows.Forms.RichTextBox Status;
        private System.Windows.Forms.Button btnLeave;
        private System.Windows.Forms.Button btnOK;
    }
}