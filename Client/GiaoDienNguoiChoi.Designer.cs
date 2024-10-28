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
            this.Character = new System.Windows.Forms.ListBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_color)).BeginInit();
            this.SuspendLayout();
            // 
            // Character
            // 
            this.Character.FormattingEnabled = true;
            this.Character.ItemHeight = 16;
            this.Character.Location = new System.Drawing.Point(12, 59);
            this.Character.Name = "Character";
            this.Character.Size = new System.Drawing.Size(120, 324);
            this.Character.TabIndex = 0;
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
            this.label2.Click += new System.EventHandler(this.label2_Click);
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
            this.Chat.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "UserName";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // pic
            // 
            this.pic.BackColor = System.Drawing.Color.White;
            this.pic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            // GiaoDienNguoiChoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 462);
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
            this.Controls.Add(this.Character);
            this.Name = "GiaoDienNguoiChoi";
            this.Text = "GiaoDienNguoiChoi";
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_color)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Character;
        private System.Windows.Forms.Label labelTimer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_pen;
        private System.Windows.Forms.Button btn_eraser;
        private System.Windows.Forms.Button btn_undo;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.TextBox tbx_send;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.ListBox Chat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_color;
        private System.Windows.Forms.PictureBox pic_color;
        private System.Windows.Forms.PictureBox pic;
    }
}