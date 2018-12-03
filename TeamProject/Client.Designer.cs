namespace TeamProject
{
    partial class Client
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
            this.picBox = new System.Windows.Forms.PictureBox();
            this.txtChat = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtTTS = new System.Windows.Forms.TextBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.deleteB = new System.Windows.Forms.Button();
            this.whiteB = new System.Windows.Forms.Button();
            this.yellowB = new System.Windows.Forms.Button();
            this.greenB = new System.Windows.Forms.Button();
            this.blueB = new System.Windows.Forms.Button();
            this.redB = new System.Windows.Forms.Button();
            this.blackB = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // picBox
            // 
            this.picBox.BackColor = System.Drawing.SystemColors.Window;
            this.picBox.Location = new System.Drawing.Point(12, 12);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(600, 556);
            this.picBox.TabIndex = 18;
            this.picBox.TabStop = false;
            this.picBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseDown);
            this.picBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseMove);
            // 
            // txtChat
            // 
            this.txtChat.BackColor = System.Drawing.SystemColors.Window;
            this.txtChat.Location = new System.Drawing.Point(642, 12);
            this.txtChat.Multiline = true;
            this.txtChat.Name = "txtChat";
            this.txtChat.ReadOnly = true;
            this.txtChat.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtChat.Size = new System.Drawing.Size(238, 576);
            this.txtChat.TabIndex = 17;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(823, 599);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(57, 23);
            this.btnSend.TabIndex = 20;
            this.btnSend.Text = "보내기";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.SendData);
            // 
            // txtTTS
            // 
            this.txtTTS.Location = new System.Drawing.Point(642, 601);
            this.txtTTS.Name = "txtTTS";
            this.txtTTS.Size = new System.Drawing.Size(175, 21);
            this.txtTTS.TabIndex = 19;
            this.txtTTS.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTTS_KeyUp);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(426, 603);
            this.trackBar1.Maximum = 20;
            this.trackBar1.Minimum = 2;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(139, 45);
            this.trackBar1.TabIndex = 32;
            this.trackBar1.TickFrequency = 2;
            this.trackBar1.Value = 2;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // deleteB
            // 
            this.deleteB.FlatAppearance.BorderSize = 0;
            this.deleteB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.deleteB.Location = new System.Drawing.Point(365, 583);
            this.deleteB.Name = "deleteB";
            this.deleteB.Size = new System.Drawing.Size(52, 52);
            this.deleteB.TabIndex = 31;
            this.deleteB.Text = "모두 지우기";
            this.deleteB.UseVisualStyleBackColor = true;
            this.deleteB.Click += new System.EventHandler(this.deleteB_Click);
            // 
            // whiteB
            // 
            this.whiteB.BackColor = System.Drawing.Color.White;
            this.whiteB.FlatAppearance.BorderSize = 0;
            this.whiteB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.whiteB.Location = new System.Drawing.Point(308, 583);
            this.whiteB.Name = "whiteB";
            this.whiteB.Size = new System.Drawing.Size(52, 52);
            this.whiteB.TabIndex = 30;
            this.whiteB.Text = "지우개";
            this.whiteB.UseVisualStyleBackColor = false;
            this.whiteB.Click += new System.EventHandler(this.whiteB_Click);
            // 
            // yellowB
            // 
            this.yellowB.BackColor = System.Drawing.Color.Yellow;
            this.yellowB.FlatAppearance.BorderSize = 0;
            this.yellowB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.yellowB.Location = new System.Drawing.Point(251, 584);
            this.yellowB.Name = "yellowB";
            this.yellowB.Size = new System.Drawing.Size(52, 52);
            this.yellowB.TabIndex = 29;
            this.yellowB.UseVisualStyleBackColor = false;
            this.yellowB.Click += new System.EventHandler(this.yellowB_Click);
            // 
            // greenB
            // 
            this.greenB.BackColor = System.Drawing.Color.Green;
            this.greenB.FlatAppearance.BorderSize = 0;
            this.greenB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.greenB.Location = new System.Drawing.Point(194, 584);
            this.greenB.Name = "greenB";
            this.greenB.Size = new System.Drawing.Size(52, 52);
            this.greenB.TabIndex = 24;
            this.greenB.UseVisualStyleBackColor = false;
            this.greenB.Click += new System.EventHandler(this.greenB_Click);
            // 
            // blueB
            // 
            this.blueB.BackColor = System.Drawing.Color.Blue;
            this.blueB.FlatAppearance.BorderSize = 0;
            this.blueB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.blueB.Location = new System.Drawing.Point(137, 583);
            this.blueB.Name = "blueB";
            this.blueB.Size = new System.Drawing.Size(52, 52);
            this.blueB.TabIndex = 27;
            this.blueB.UseVisualStyleBackColor = false;
            this.blueB.Click += new System.EventHandler(this.blueB_Click);
            // 
            // redB
            // 
            this.redB.BackColor = System.Drawing.Color.Red;
            this.redB.FlatAppearance.BorderSize = 0;
            this.redB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.redB.Location = new System.Drawing.Point(80, 584);
            this.redB.Name = "redB";
            this.redB.Size = new System.Drawing.Size(52, 52);
            this.redB.TabIndex = 26;
            this.redB.Tag = "";
            this.redB.UseVisualStyleBackColor = false;
            this.redB.Click += new System.EventHandler(this.redB_Click);
            // 
            // blackB
            // 
            this.blackB.BackColor = System.Drawing.Color.Black;
            this.blackB.FlatAppearance.BorderSize = 0;
            this.blackB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.blackB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.blackB.Location = new System.Drawing.Point(23, 584);
            this.blackB.Name = "blackB";
            this.blackB.Size = new System.Drawing.Size(52, 52);
            this.blackB.TabIndex = 25;
            this.blackB.Tag = "";
            this.blackB.UseVisualStyleBackColor = false;
            this.blackB.Click += new System.EventHandler(this.blackB_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(423, 583);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 33;
            this.label1.Text = "굵기 : ";
            // 
            // timer
            // 
            this.timer.AutoSize = true;
            this.timer.Font = new System.Drawing.Font("굴림", 20F);
            this.timer.Location = new System.Drawing.Point(583, 599);
            this.timer.Name = "timer";
            this.timer.Size = new System.Drawing.Size(44, 27);
            this.timer.TabIndex = 34;
            this.timer.Text = "00";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TeamProject.Properties.Resources.무지배경화면_썸네일;
            this.ClientSize = new System.Drawing.Size(916, 658);
            this.Controls.Add(this.timer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.deleteB);
            this.Controls.Add(this.whiteB);
            this.Controls.Add(this.yellowB);
            this.Controls.Add(this.greenB);
            this.Controls.Add(this.blueB);
            this.Controls.Add(this.redB);
            this.Controls.Add(this.blackB);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtTTS);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.txtChat);
            this.Name = "Client";
            this.Text = "Client";
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.trackBar1_Scroll);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.TextBox txtChat;
        private System.Windows.Forms.Button btnSend;
        public System.Windows.Forms.TextBox txtTTS;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button deleteB;
        private System.Windows.Forms.Button whiteB;
        private System.Windows.Forms.Button yellowB;
        private System.Windows.Forms.Button greenB;
        private System.Windows.Forms.Button blueB;
        private System.Windows.Forms.Button redB;
        private System.Windows.Forms.Button blackB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label timer;
    }
}