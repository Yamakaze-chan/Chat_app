namespace TCPClient
{
    partial class Form1
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
            this.btnSend = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.Changtext_btn = new System.Windows.Forms.Button();
            this.Icon_btn = new System.Windows.Forms.Button();
            this.icon_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.CreateSeverbtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.history_panel = new System.Windows.Forms.Panel();
            this.History_lstbox = new System.Windows.Forms.ListBox();
            this.search_history = new System.Windows.Forms.TextBox();
            this.memories_flowlayoutpanel = new System.Windows.Forms.FlowLayoutPanel();
            this.yourIPlabel = new System.Windows.Forms.Label();
            this.IP_lstbox = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Minigame_btn = new System.Windows.Forms.Button();
            this.GIF_btn = new System.Windows.Forms.Button();
            this.history_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(719, 446);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 13;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(638, 446);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 12;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(72, 446);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 11;
            this.label2.Text = "Message";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(90, 12);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(430, 22);
            this.txtIP.TabIndex = 8;
            this.txtIP.Text = "127.0.0.1:9000";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 7;
            this.label1.Text = "Sever: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(638, 489);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Choose file";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(15, 65);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(617, 355);
            this.flowLayoutPanel1.TabIndex = 16;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(139, 446);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(493, 33);
            this.txtMessage.TabIndex = 17;
            this.txtMessage.Text = "";
            this.txtMessage.TextChanged += new System.EventHandler(this.txtMessage_TextChanged);
            this.txtMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMessage_KeyPress);
            // 
            // Changtext_btn
            // 
            this.Changtext_btn.Location = new System.Drawing.Point(15, 489);
            this.Changtext_btn.Name = "Changtext_btn";
            this.Changtext_btn.Size = new System.Drawing.Size(79, 45);
            this.Changtext_btn.TabIndex = 18;
            this.Changtext_btn.Text = "Change Text";
            this.Changtext_btn.UseVisualStyleBackColor = true;
            this.Changtext_btn.Click += new System.EventHandler(this.changetextbtn_Click);
            // 
            // Icon_btn
            // 
            this.Icon_btn.Location = new System.Drawing.Point(763, 489);
            this.Icon_btn.Name = "Icon_btn";
            this.Icon_btn.Size = new System.Drawing.Size(75, 23);
            this.Icon_btn.TabIndex = 21;
            this.Icon_btn.Text = "Icon";
            this.Icon_btn.UseVisualStyleBackColor = true;
            this.Icon_btn.Click += new System.EventHandler(this.icon_btn_Click);
            // 
            // icon_panel
            // 
            this.icon_panel.Location = new System.Drawing.Point(638, 65);
            this.icon_panel.Name = "icon_panel";
            this.icon_panel.Size = new System.Drawing.Size(232, 355);
            this.icon_panel.TabIndex = 24;
            this.icon_panel.Visible = false;
            // 
            // CreateSeverbtn
            // 
            this.CreateSeverbtn.Location = new System.Drawing.Point(638, 533);
            this.CreateSeverbtn.Name = "CreateSeverbtn";
            this.CreateSeverbtn.Size = new System.Drawing.Size(121, 23);
            this.CreateSeverbtn.TabIndex = 26;
            this.CreateSeverbtn.Text = "create Sever";
            this.CreateSeverbtn.UseVisualStyleBackColor = true;
            this.CreateSeverbtn.Click += new System.EventHandler(this.CreateSeverbtn_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(638, 572);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 23);
            this.button2.TabIndex = 27;
            this.button2.Text = "disconnect";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(638, 611);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(200, 23);
            this.button3.TabIndex = 28;
            this.button3.Text = "Search in history chat";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // history_panel
            // 
            this.history_panel.Controls.Add(this.History_lstbox);
            this.history_panel.Controls.Add(this.search_history);
            this.history_panel.Location = new System.Drawing.Point(844, 426);
            this.history_panel.Name = "history_panel";
            this.history_panel.Size = new System.Drawing.Size(261, 285);
            this.history_panel.TabIndex = 29;
            this.history_panel.Visible = false;
            // 
            // History_lstbox
            // 
            this.History_lstbox.FormattingEnabled = true;
            this.History_lstbox.ItemHeight = 16;
            this.History_lstbox.Location = new System.Drawing.Point(0, 20);
            this.History_lstbox.Name = "History_lstbox";
            this.History_lstbox.Size = new System.Drawing.Size(261, 244);
            this.History_lstbox.TabIndex = 1;
            // 
            // search_history
            // 
            this.search_history.Location = new System.Drawing.Point(0, 0);
            this.search_history.Name = "search_history";
            this.search_history.Size = new System.Drawing.Size(261, 22);
            this.search_history.TabIndex = 0;
            this.search_history.TextChanged += new System.EventHandler(this.search_history_TextChanged);
            // 
            // memories_flowlayoutpanel
            // 
            this.memories_flowlayoutpanel.AutoScroll = true;
            this.memories_flowlayoutpanel.Location = new System.Drawing.Point(877, 65);
            this.memories_flowlayoutpanel.Name = "memories_flowlayoutpanel";
            this.memories_flowlayoutpanel.Size = new System.Drawing.Size(240, 355);
            this.memories_flowlayoutpanel.TabIndex = 30;
            // 
            // yourIPlabel
            // 
            this.yourIPlabel.Location = new System.Drawing.Point(15, 38);
            this.yourIPlabel.Name = "yourIPlabel";
            this.yourIPlabel.Size = new System.Drawing.Size(505, 23);
            this.yourIPlabel.TabIndex = 31;
            this.yourIPlabel.Text = "Your IP is :";
            // 
            // IP_lstbox
            // 
            this.IP_lstbox.FormattingEnabled = true;
            this.IP_lstbox.ItemHeight = 16;
            this.IP_lstbox.Items.AddRange(new object[] {
            "a",
            "b",
            "127.0.0.1:9010"});
            this.IP_lstbox.Location = new System.Drawing.Point(526, 6);
            this.IP_lstbox.Name = "IP_lstbox";
            this.IP_lstbox.Size = new System.Drawing.Size(160, 36);
            this.IP_lstbox.TabIndex = 32;
            this.IP_lstbox.Click += new System.EventHandler(this.IP_lstbox_Click);
            this.IP_lstbox.DoubleClick += new System.EventHandler(this.IP_lstbox_DoubleClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TCPSever.Properties.Resources.ezgif_com_gif_maker__18_;
            this.pictureBox1.Location = new System.Drawing.Point(1005, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 25;
            this.pictureBox1.TabStop = false;
            // 
            // Minigame_btn
            // 
            this.Minigame_btn.Location = new System.Drawing.Point(638, 641);
            this.Minigame_btn.Name = "Minigame_btn";
            this.Minigame_btn.Size = new System.Drawing.Size(98, 23);
            this.Minigame_btn.TabIndex = 33;
            this.Minigame_btn.Text = "Minigame";
            this.Minigame_btn.UseVisualStyleBackColor = true;
            this.Minigame_btn.Click += new System.EventHandler(this.Minigame_btn_Click);
            // 
            // GIF_btn
            // 
            this.GIF_btn.Location = new System.Drawing.Point(763, 533);
            this.GIF_btn.Name = "GIF_btn";
            this.GIF_btn.Size = new System.Drawing.Size(75, 23);
            this.GIF_btn.TabIndex = 34;
            this.GIF_btn.Text = "GIF";
            this.GIF_btn.UseVisualStyleBackColor = true;
            this.GIF_btn.Click += new System.EventHandler(this.GIF_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 720);
            this.Controls.Add(this.GIF_btn);
            this.Controls.Add(this.Minigame_btn);
            this.Controls.Add(this.IP_lstbox);
            this.Controls.Add(this.yourIPlabel);
            this.Controls.Add(this.memories_flowlayoutpanel);
            this.Controls.Add(this.history_panel);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.CreateSeverbtn);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.icon_panel);
            this.Controls.Add(this.Icon_btn);
            this.Controls.Add(this.Changtext_btn);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.history_panel.ResumeLayout(false);
            this.history_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RichTextBox txtMessage;
        private System.Windows.Forms.Button Changtext_btn;
        private System.Windows.Forms.Button Icon_btn;
        private System.Windows.Forms.FlowLayoutPanel icon_panel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button CreateSeverbtn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel history_panel;
        private System.Windows.Forms.TextBox search_history;
        private System.Windows.Forms.ListBox History_lstbox;
        private System.Windows.Forms.FlowLayoutPanel memories_flowlayoutpanel;
        private System.Windows.Forms.Label yourIPlabel;
        private System.Windows.Forms.ListBox IP_lstbox;
        private System.Windows.Forms.Button Minigame_btn;
        private System.Windows.Forms.Button GIF_btn;
    }
}

