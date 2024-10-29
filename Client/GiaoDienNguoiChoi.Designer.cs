namespace Client
{
    partial class GiaoDienNguoiChoi
    {
        private const int V = 0;

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
            this.labelTimer = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_undo = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.tbx_send = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.Chat = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pic = new System.Windows.Forms.PictureBox();
            this.pic_color = new System.Windows.Forms.PictureBox();
            this.btn_color = new System.Windows.Forms.Button();
            this.btn_eraser = new System.Windows.Forms.Button();
            this.btn_pen = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lb1 = new System.Windows.Forms.Label();
            this.tb1 = new System.Windows.Forms.TextBox();
            this.tb2 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lb2 = new System.Windows.Forms.Label();
            this.tb3 = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lb3 = new System.Windows.Forms.Label();
            this.tb4 = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lb4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lb5 = new System.Windows.Forms.Label();
            this.tb5 = new System.Windows.Forms.TextBox();
            this.tbCmt = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_color)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTimer
            // 
            this.labelTimer.AutoSize = true;
            this.labelTimer.Location = new System.Drawing.Point(148, 21);
            this.labelTimer.Name = "labelTimer";
            this.labelTimer.Size = new System.Drawing.Size(42, 16);
            this.labelTimer.TabIndex = 1;
            this.labelTimer.Text = "Timer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(276, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Round";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(489, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Word to Draw";
            // 
            // btn_undo
            // 
            this.btn_undo.Enabled = false;
            this.btn_undo.Location = new System.Drawing.Point(487, 398);
            this.btn_undo.Name = "btn_undo";
            this.btn_undo.Size = new System.Drawing.Size(75, 23);
            this.btn_undo.TabIndex = 9;
            this.btn_undo.Text = "Undo";
            this.btn_undo.UseVisualStyleBackColor = true;
            this.btn_undo.Click += new System.EventHandler(this.btn_undo_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Enabled = false;
            this.btn_clear.Location = new System.Drawing.Point(487, 427);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(75, 23);
            this.btn_clear.TabIndex = 10;
            this.btn_clear.Text = "Clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // tbx_send
            // 
            this.tbx_send.Location = new System.Drawing.Point(640, 330);
            this.tbx_send.Name = "tbx_send";
            this.tbx_send.Size = new System.Drawing.Size(148, 22);
            this.tbx_send.TabIndex = 11;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(677, 358);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
            this.btn_send.TabIndex = 12;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = true;
            // 
            // Chat
            // 
            this.Chat.FormattingEnabled = true;
            this.Chat.ItemHeight = 16;
            this.Chat.Location = new System.Drawing.Point(640, 59);
            this.Chat.Name = "Chat";
            this.Chat.Size = new System.Drawing.Size(148, 260);
            this.Chat.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "UserName";
            // 
            // pic
            // 
            this.pic.BackColor = System.Drawing.Color.White;
            this.pic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic.Enabled = false;
            this.pic.Location = new System.Drawing.Point(138, 59);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(496, 324);
            this.pic.TabIndex = 19;
            this.pic.TabStop = false;
            this.pic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic_MouseDown);
            this.pic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic_MouseMove);
            this.pic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic_MouseUp);
            // 
            // pic_color
            // 
            this.pic_color.BackColor = System.Drawing.Color.Black;
            this.pic_color.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_color.Location = new System.Drawing.Point(213, 398);
            this.pic_color.Name = "pic_color";
            this.pic_color.Size = new System.Drawing.Size(55, 52);
            this.pic_color.TabIndex = 18;
            this.pic_color.TabStop = false;
            // 
            // btn_color
            // 
            this.btn_color.Enabled = false;
            this.btn_color.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_color.Image = global::Client.Properties.Resources.icons8_paint_24;
            this.btn_color.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_color.Location = new System.Drawing.Point(274, 398);
            this.btn_color.Name = "btn_color";
            this.btn_color.Size = new System.Drawing.Size(60, 52);
            this.btn_color.TabIndex = 17;
            this.btn_color.Text = "Color";
            this.btn_color.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_color.UseVisualStyleBackColor = true;
            this.btn_color.Click += new System.EventHandler(this.btn_color_Click);
            // 
            // btn_eraser
            // 
            this.btn_eraser.Enabled = false;
            this.btn_eraser.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_eraser.Image = global::Client.Properties.Resources.icons8_eraser_tool_24;
            this.btn_eraser.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_eraser.Location = new System.Drawing.Point(406, 398);
            this.btn_eraser.Name = "btn_eraser";
            this.btn_eraser.Size = new System.Drawing.Size(60, 52);
            this.btn_eraser.TabIndex = 8;
            this.btn_eraser.Text = "Eraser";
            this.btn_eraser.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_eraser.UseVisualStyleBackColor = true;
            this.btn_eraser.Click += new System.EventHandler(this.btn_eraser_Click);
            // 
            // btn_pen
            // 
            this.btn_pen.Enabled = false;
            this.btn_pen.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_pen.Image = global::Client.Properties.Resources.icons8_sign_up_24;
            this.btn_pen.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_pen.Location = new System.Drawing.Point(340, 398);
            this.btn_pen.Name = "btn_pen";
            this.btn_pen.Size = new System.Drawing.Size(60, 52);
            this.btn_pen.TabIndex = 7;
            this.btn_pen.Text = "Pen";
            this.btn_pen.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_pen.UseVisualStyleBackColor = true;
            this.btn_pen.Click += new System.EventHandler(this.btn_pen_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lb1);
            this.panel1.Controls.Add(this.tb1);
            this.panel1.Location = new System.Drawing.Point(16, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(116, 59);
            this.panel1.TabIndex = 20;
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb1.Location = new System.Drawing.Point(5, 4);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(53, 20);
            this.lb1.TabIndex = 1;
            this.lb1.Text = "label1";
            // 
            // tb1
            // 
            this.tb1.Location = new System.Drawing.Point(8, 29);
            this.tb1.Name = "tb1";
            this.tb1.Size = new System.Drawing.Size(100, 22);
            this.tb1.TabIndex = 0;
            // 
            // tb2
            // 
            this.tb2.Location = new System.Drawing.Point(8, 29);
            this.tb2.Name = "tb2";
            this.tb2.Size = new System.Drawing.Size(100, 22);
            this.tb2.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lb2);
            this.panel2.Controls.Add(this.tb2);
            this.panel2.Location = new System.Drawing.Point(16, 125);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(116, 59);
            this.panel2.TabIndex = 21;
            this.panel2.Visible = false;
            // 
            // lb2
            // 
            this.lb2.AutoSize = true;
            this.lb2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb2.Location = new System.Drawing.Point(5, 4);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(53, 20);
            this.lb2.TabIndex = 1;
            this.lb2.Text = "label4";
            // 
            // tb3
            // 
            this.tb3.Location = new System.Drawing.Point(8, 29);
            this.tb3.Name = "tb3";
            this.tb3.Size = new System.Drawing.Size(100, 22);
            this.tb3.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lb3);
            this.panel3.Controls.Add(this.tb3);
            this.panel3.Location = new System.Drawing.Point(16, 189);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(116, 59);
            this.panel3.TabIndex = 21;
            this.panel3.Visible = false;
            // 
            // lb3
            // 
            this.lb3.AutoSize = true;
            this.lb3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb3.Location = new System.Drawing.Point(5, 4);
            this.lb3.Name = "lb3";
            this.lb3.Size = new System.Drawing.Size(53, 20);
            this.lb3.TabIndex = 1;
            this.lb3.Text = "label6";
            // 
            // tb4
            // 
            this.tb4.Location = new System.Drawing.Point(8, 29);
            this.tb4.Name = "tb4";
            this.tb4.Size = new System.Drawing.Size(100, 22);
            this.tb4.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lb4);
            this.panel4.Controls.Add(this.tb4);
            this.panel4.Location = new System.Drawing.Point(16, 253);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(116, 59);
            this.panel4.TabIndex = 21;
            this.panel4.Visible = false;
            // 
            // lb4
            // 
            this.lb4.AutoSize = true;
            this.lb4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb4.Location = new System.Drawing.Point(5, 4);
            this.lb4.Name = "lb4";
            this.lb4.Size = new System.Drawing.Size(53, 20);
            this.lb4.TabIndex = 1;
            this.lb4.Text = "label7";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.lb5);
            this.panel5.Controls.Add(this.tb5);
            this.panel5.Location = new System.Drawing.Point(16, 318);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(116, 59);
            this.panel5.TabIndex = 22;
            this.panel5.Visible = false;
            // 
            // lb5
            // 
            this.lb5.AutoSize = true;
            this.lb5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb5.Location = new System.Drawing.Point(5, 4);
            this.lb5.Name = "lb5";
            this.lb5.Size = new System.Drawing.Size(53, 20);
            this.lb5.TabIndex = 1;
            this.lb5.Text = "label8";
            // 
            // tb5
            // 
            this.tb5.Location = new System.Drawing.Point(8, 29);
            this.tb5.Name = "tb5";
            this.tb5.Size = new System.Drawing.Size(100, 22);
            this.tb5.TabIndex = 0;
            // 
            // tbCmt
            // 
            this.tbCmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.tbCmt.Location = new System.Drawing.Point(640, 13);
            this.tbCmt.Name = "tbCmt";
            this.tbCmt.Size = new System.Drawing.Size(148, 28);
            this.tbCmt.TabIndex = 23;
            // 
            // GiaoDienNguoiChoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 462);
            this.Controls.Add(this.tbCmt);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.pic_color);
            this.Controls.Add(this.btn_color);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Chat);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.tbx_send);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_undo);
            this.Controls.Add(this.btn_eraser);
            this.Controls.Add(this.btn_pen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelTimer);
            this.Name = "GiaoDienNguoiChoi";
            this.Text = "Game_TAMSAOTHATBAN";
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_color)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelTimer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_pen;
        public System.Windows.Forms.Button btn_eraser;
        public System.Windows.Forms.Button btn_undo;
        public System.Windows.Forms.Button btn_clear;
        public System.Windows.Forms.TextBox tbx_send;
        public System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.ListBox Chat;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Button btn_color;
        private System.Windows.Forms.PictureBox pic_color;
        public System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.TextBox tb2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lb2;
        private System.Windows.Forms.TextBox tb3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lb3;
        private System.Windows.Forms.TextBox tb4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lb4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lb5;
        private System.Windows.Forms.TextBox tb5;
        private System.Windows.Forms.TextBox tbCmt;
    }
}