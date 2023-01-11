using Guna.UI2.WinForms;
using SuperSimpleTcp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace TCPClient
{
    public partial class Client : Form
    {
        private FontDialog fd = new FontDialog();
        public Client()
        {
            InitializeComponent();
        }
        SimpleTcpClient client;
        Hashtable emotions;
        List<char> going_to_send;
        List<string> History;
        List<string> History_sever;
        string yourip;
        string txtIP;
        [DllImport("user32.dll")]

        static extern bool HideCaret(IntPtr hWnd);

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                if (client.IsConnected)
                {
                    if (!string.IsNullOrEmpty(txtMessage.Text))
                    {
                        //send message
                        var cvt = new FontConverter();
                        string s = cvt.ConvertToString(this.txtMessage.Font);
                        string sendmsg = s + "++++++" + new string(going_to_send.ToArray());
                        client.Send(sendmsg);

                        this.flowLayoutPanel1.HorizontalScroll.Maximum = 0;
                        this.flowLayoutPanel1.VerticalScroll.Maximum = 0;
                        this.flowLayoutPanel1.AutoScroll = false;
                        this.flowLayoutPanel1.VerticalScroll.Visible = false;
                        this.flowLayoutPanel1.HorizontalScroll.Visible = false;
                        this.flowLayoutPanel1.AutoScroll = true;
                        this.flowLayoutPanel1.Controls.Add(Create_rtb(new string(going_to_send.ToArray()), this.txtMessage.Font, "You"));
                        this.flowLayoutPanel1.AutoScrollPosition = new Point(0, flowLayoutPanel1.VerticalScroll.Maximum);
                        History.Add(yourip + " : " + txtMessage.Text);
                        this.History_lstbox.Items.Add(yourip + " : " + txtMessage.Text);
                        txtMessage.Text = "";
                        Console.WriteLine("u");

                        going_to_send.Clear();
                    }
                    else
                    {
                        //string must not null
                        MessageBox.Show("string must not null");
                    }
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect1.Text == "Connect")
            {
                try
                {
                    txtIP = IP1.Text + ":" + Port1.Text;
                    label5.Text = "Server is connected";
                    label7.Text = "Active";
                    //try connect to our sever
                    client = new SimpleTcpClient(txtIP);
                    client.Events.Connected += Events_Connected;
                    client.Events.DataReceived += Events_DataReceived;
                    client.Events.Disconnected += Events_Disconnected;
                    client.Connect();
                    btnConnect1.Text = "Disconnect";
                    Send_btn.Enabled = true;
                    txtMessage.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string message = "Do you want to leave this chat?";
                string title = "Leave chat";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    btnConnect1.Text = "Connect";
                    string mess = "Do you want to save IP of chat for return later?";
                    string t = "Save IP";
                    MessageBoxButtons btn = MessageBoxButtons.YesNo;
                    DialogResult r = MessageBox.Show(mess, t, btn, MessageBoxIcon.Question);
                    if (r == DialogResult.Yes)
                    {
                        bool ex = false;
                        foreach (var item in IP_lstbox.Items)
                        {
                            if (IP_lstbox.GetItemText(item) == txtIP)
                            {
                                ex = true;

                            }
                        }
                        if (!ex)
                        {
                            History_sever.Add(txtIP);
                            IP_lstbox.Items.Add(txtIP);
                        }
                    }
                    client.Disconnect();
                    Send_btn.Enabled = !true;

                    history_panel.Visible = false;
                    this.History_lstbox.Items.Clear();
                    this.flowLayoutPanel1.Controls.Clear();
                    this.yourIPlabel.Text = "";
                    memories_flowlayoutpanel.Controls.Clear();
                    label5.Text = "Server is disconnected";
                    label7.Text = "Idle";
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Send_btn.Enabled = true;
            going_to_send = new List<char>();
            History = new List<string>();
            History_sever = new List<string>();
            CreateEmotions();
            Add_pic_emotion();
        }

        private void Events_Disconnected(object sender, ConnectionEventArgs e)
        {

            {

                MessageBox.Show("Disconnect");

                Reset_history();

            }

        }

        private PictureBox insertpicture(MemoryStream fs)
        {
            System.Windows.Forms.PictureBox picture = new System.Windows.Forms.PictureBox()
            {
                Name = "pictureBox",
                Size = new Size(200, 200),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(100, 100),

            };
            picture.Image = Image.FromStream(fs);
            return picture;
        }

        private PictureBox insertpicture(string s)
        {
            System.Windows.Forms.PictureBox picture = new System.Windows.Forms.PictureBox()
            {
                Name = "pictureBox",
                Size = new Size(200, 200),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(100, 100),

            };
            picture.Image = Image.FromFile(s);
            return picture;
        }

        private PictureBox insertmemoriespicture(string s)
        {
            System.Windows.Forms.PictureBox picture = new System.Windows.Forms.PictureBox()
            {
                Name = "pictureBox",
                Size = new Size(100, 100),
                SizeMode = PictureBoxSizeMode.Zoom,

            };
            picture.Image = Image.FromFile(s);
            return picture;
        }

        private void save_docx(MemoryStream ms)
        {
            using (FileStream file = new FileStream("file.docx", FileMode.Create, System.IO.FileAccess.Write))
            {
                byte[] bytes = new byte[ms.Length];
                ms.Read(bytes, 0, (int)ms.Length);
                file.Write(bytes, 0, bytes.Length);
                ms.Close();
            }
        }

        public MemoryStream Clear(MemoryStream source)
        {
            byte[] buffer = source.GetBuffer();
            Array.Clear(buffer, 0, buffer.Length);
            source.Position = 0;
            source.SetLength(0);
            return source;
        }

        MemoryStream memoryStream = new MemoryStream(0);
        int receivedataimg;
        bool ismemories = false;
        private void Events_DataReceived(object sender, SuperSimpleTcp.DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                Thread.Sleep(10);
                memoryStream.Capacity = memoryStream.Capacity + e.Data.Count;
                memoryStream.Write(e.Data.Array, 0, e.Data.Count);
                try
                {
                    Image.FromStream(memoryStream);
                    string path = DateTime.UtcNow.ToString("yyyyMMddTHHmmssfff") + ".gif";
                    FileStream fs = new FileStream(path, FileMode.Create);
                    fs.Write(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
                    fs.Close();
                    Console.WriteLine("Client:" + receivedataimg);
                    Console.WriteLine("Client:" + memoryStream.Capacity);
                    Console.WriteLine("Client:" + (memoryStream.Capacity - receivedataimg));

                    if (receivedataimg == memoryStream.Capacity)
                    {
                        if (ismemories)
                        {
                            PictureBox p = insertmemoriespicture(path);
                            this.memories_flowlayoutpanel.Controls.Add(p);
                        }
                        else
                        {
                            PictureBox p = insertpicture(path);
                            this.flowLayoutPanel1.Controls.Add(p);
                        }
                        this.memoryStream.Close();
                        this.memoryStream.Dispose();
                        this.memoryStream = new MemoryStream(0);
                        ismemories = false;
                    }
                }
                catch (Exception ex)
                {

                    string receive = Encoding.UTF8.GetString(memoryStream.ToArray());
                    Console.WriteLine(receive);
                    //check if file is a picture
                    if (receive.Contains("iushcxlchiasdchjaslfcdajhiodcadshjca"))
                    {
                        receivedataimg = int.Parse(Regex.Match(receive, @"\d+").Value) + memoryStream.Capacity;
                        Console.WriteLine(receivedataimg);
                        memoryStream.SetLength(0);
                    }
                    else if (receive.Contains("therearefilesofsevermemories"))
                    {
                        receivedataimg = int.Parse(Regex.Match(receive, @"\d+").Value) + memoryStream.Capacity;
                        ismemories = true;
                        Console.WriteLine(receivedataimg);
                        memoryStream.SetLength(0);
                    }
                    else if (receive.Contains("thisisahistoryoftheseveryouareonlineandyouripis"))
                    {
                        Console.WriteLine(receive);
                        receive = receive.Replace("thisisahistoryoftheseveryouareonlineandyouripis", "");
                        int indip = receive.IndexOf("/+/+/+");
                        yourip = receive.Substring(0, indip);
                        YourIPis(receive.Substring(0, indip));
                        receive = receive.Remove(0, indip);
                        Console.WriteLine("ip: " + receive);
                        receive = receive.Replace("/+/+/+", "\n");
                        string[] receive_history = receive.Split('\n');
                        foreach (string str in receive_history)
                        {
                            History.Add(str);
                        }
                        Console.WriteLine(receive);
                        memoryStream.SetLength(0);
                    }
                    else
                    {
                        //get string from sender
                        string str = Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count);
                        Console.WriteLine(str);
                        //Add string to flowlayoutpanel
                        int index1 = str.IndexOf("//////");
                        string name = str.Substring(0, index1);
                        int index_font = str.IndexOf("++++++");
                        string font = str.Substring(index1 + 6, index_font - index1 - 6);

                        string txt = str.Substring(index_font + 6, str.Length - index_font - 6);
                        History.Add($"{name} : {txt}");
                        History_lstbox.Items.Add($"{name} : {txt}");
                        var cvt = new FontConverter();
                        Font f = cvt.ConvertFromString(font) as Font;
                        this.flowLayoutPanel1.HorizontalScroll.Maximum = 0;
                        this.flowLayoutPanel1.VerticalScroll.Maximum = 0;
                        this.flowLayoutPanel1.AutoScroll = false;
                        this.flowLayoutPanel1.VerticalScroll.Visible = false;
                        this.flowLayoutPanel1.HorizontalScroll.Visible = false;
                        this.flowLayoutPanel1.AutoScroll = true;
                        this.flowLayoutPanel1.Controls.Add(Create_rtb(txt, f, name));
                        this.flowLayoutPanel1.AutoScrollPosition = new Point(0, flowLayoutPanel1.VerticalScroll.Maximum);
                    }
                }
            });
        }

        private void Events_Connected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                System.Windows.Forms.Label lb = new System.Windows.Forms.Label();
                lb.Text = $"Sever {e.IpPort} connected.";
                lb.MaximumSize = new Size(550, 0);
                lb.AutoSize = true;
                lb.BorderStyle = BorderStyle.FixedSingle;
                this.flowLayoutPanel1.HorizontalScroll.Maximum = 0;
                this.flowLayoutPanel1.VerticalScroll.Maximum = 0;
                this.flowLayoutPanel1.AutoScroll = false;
                this.flowLayoutPanel1.VerticalScroll.Visible = false;
                this.flowLayoutPanel1.HorizontalScroll.Visible = false;
                this.flowLayoutPanel1.AutoScroll = true;
                //this.flowLayoutPanel1.Controls.Add(lb);
                this.flowLayoutPanel1.AutoScrollPosition = new Point(0, flowLayoutPanel1.VerticalScroll.Maximum);
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Choose file to send
            string sSelectedFile;
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = true;

            if (choofdlog.ShowDialog() == DialogResult.OK)
                sSelectedFile = choofdlog.FileName;
            else
                sSelectedFile = string.Empty;
            if (sSelectedFile != string.Empty && client != null)
            {
                Sendfile(sSelectedFile);
            }
        }

        private byte[] ImageToBytes(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }

        private void Sendfile(string filePath)
        {
            string ext = Path.GetExtension(filePath);
            byte[] img_to_send = null;
            if (ext == ".gif")
            {
                Bitmap img = new Bitmap(filePath);
                img_to_send = ImageToBytes(img, ImageFormat.Gif);
            }
            else
            {
                //resize image
                Image original = Image.FromFile(filePath);
                double h = 200.0 / original.Height;
                double w = 200.0 / original.Width;
                double scale = Math.Max(h, w);
                int new_h = (int)Math.Round(original.Height * scale);
                int new_w = (int)Math.Round(original.Width * scale);
                Image resized = ResizeImage(original, new Size(new_w, new_h));

                ImageConverter imgCon = new ImageConverter();
                img_to_send = (byte[])imgCon.ConvertTo(resized, typeof(byte[]));
            }


            //convert image to memorystream

            long totalBytes = img_to_send.Length;
            string senddata = "iushcxlchiasdchjaslfcdajhiodcadshjca" + totalBytes.ToString();
            byte[] byteArray = Encoding.UTF8.GetBytes(senddata);
            MemoryStream stream = new MemoryStream(byteArray);
            MemoryStream img_stream = new MemoryStream(img_to_send);



            client.Send(stream.Length, stream);
            Thread.Sleep(500);
            client.Send(img_stream.Length, img_stream);
            Thread.Sleep(1000);

            System.Windows.Forms.PictureBox picture = new System.Windows.Forms.PictureBox()
            {
                Name = "pictureBox",
                Size = new Size(200, 200),

                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(100, 100),

            };
            picture.Image = (Bitmap)((new ImageConverter()).ConvertFrom(img_to_send)); ;
            this.flowLayoutPanel1.Controls.Add(picture);

            this.memories_flowlayoutpanel.Controls.Add(insertmemoriespicture(filePath));

        }

        public static Image ResizeImage(Image image, Size size, bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            Image newImage = new System.Drawing.Bitmap(image, newWidth, newHeight); // I specify the new image from the original together with the new width and height
            using (Graphics graphicsHandle = Graphics.FromImage(image))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(newImage, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        private void changetextbtn_Click(object sender, EventArgs e)
        {
            fd.MaxSize = 30;
            fd.MinSize = 10;
            fd.ShowColor = true;
            fd.ShowApply = true;
            fd.ShowEffects = false;
            fd.ShowHelp = true;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                this.txtMessage.Font = fd.Font;
            }
        }


        private void CreateEmotions()
        {
            emotions = new Hashtable();
            emotions.Add(" :) ", "Emotion_icon\\3561859_emoji_emoticon_glued_mute_silent_icon.png");
            emotions.Add(" =.= ", "Emotion_icon\\3561838_emoji_emoticon_emoticons_expression_face_icon.png");
            emotions.Add(" :O ", "Emotion_icon\\3561860_emoji_emoticon_emotion_shocked_wonder_icon.png");
            emotions.Add(" :( ", "Emotion_icon\\3561845_emoticon_expression_prejudice_icon.png");
            emotions.Add(" ;-; ", "Emotion_icon\\3561855_cry_emoticon_emotion_face_sad_icon.png");
            emotions.Add(" :v ", "Emotion_icon\\3561854_emoji_emoticon_expression_feeling_grumble_icon.png");
            emotions.Add(" >:< ", "Emotion_icon\\3561847_emoji_emoticon_emoticons_expression_mad_icon.png");
        }

        public Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            //txtMessage.MinimumSize = new Size(0, 20);
            //using (Graphics g = CreateGraphics())
            //{
            //    txtMessage.Height = (int)g.MeasureString(txtMessage.Text,
            //        txtMessage.Font, txtMessage.Width).Height;
            //}
        }

        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("__________");
            if (client != null)
            {
                if (client.IsConnected)
                {
                    bool flag = true;
                    int index = txtMessage.SelectionStart;
                    string temp = string.Empty;
                    if (txtMessage.Text != null)
                    {
                        if (e.KeyChar == (char)Keys.Back)
                        {
                            if (index > -1)
                            {
                                if (index == 0)
                                {
                                    going_to_send.Clear();
                                }
                                else
                                {
                                    going_to_send.RemoveRange(index, 1);
                                }
                            }
                        }
                        else if (e.KeyChar == (char)Keys.Enter)
                        {
                            btnSend_Click(sender, e);
                            //History.Add(txtMessage.Text);
                        }
                        else
                        {
                            going_to_send.Insert(index, e.KeyChar);
                        }
                    }


                    going_to_send.ForEach(i => Console.WriteLine(i));
                    Console.WriteLine(txtMessage.Text.Length);
                }
            }
        }

        private void Add_pic_emotion()
        {
            icon_panel.Controls.Clear();
            this.icon_panel.HorizontalScroll.Maximum = 0;
            this.icon_panel.VerticalScroll.Maximum = 0;
            this.icon_panel.AutoScroll = false;
            this.icon_panel.VerticalScroll.Visible = false;
            this.icon_panel.HorizontalScroll.Visible = false;
            this.icon_panel.AutoScroll = true;
            DirectoryInfo d = new DirectoryInfo("Emotion_icon");

            foreach (var file in d.GetFiles("*.png"))
            {
                PictureBox p = new PictureBox
                {
                    Size = new Size(50, 50),
                    Margin = new Padding(4, 4, 4, 4),
                    Image = resizeImage(Image.FromFile(d + "\\" + file.Name), new Size(50, 50)),
                };
                this.icon_panel.Controls.Add(p);
                p.Click += (s, ee) => Add_emo_pic_manual(d + "\\" + file.Name);
            }
        }
        private void Add_emo_pic_manual(string emo)
        {
            if (client != null && client.IsConnected)
            {
                Sendfile(emo);
            }
        }

        private void icon_btn_Click(object sender, EventArgs e)
        {
            icon_panel.Visible = true;
            Add_pic_emotion();
        }

        private Guna2GradientPanel Create_rtb(string s, Font f, string send_pp)
        {
            Label rtb = new Label();
            rtb.Text = $"{send_pp}: " + s;
            rtb.Location = new Point(15, 15);
            rtb.Width = 350;
            rtb.BackColor = Color.Transparent;
            rtb.ForeColor = Color.White;
            rtb.TextAlign = ContentAlignment.MiddleLeft;
            rtb.Click += (z, ee) => HideCaret(rtb.Handle);
            var cvt = new FontConverter();
            rtb.Font = f;
            using (Graphics g = CreateGraphics())
            {
                rtb.Height = (int)g.MeasureString(rtb.Text,
                    rtb.Font, rtb.Width).Height + 10;
            }
            float height = rtb.Font.GetHeight();
            int high = (int)Math.Round(height);
            Guna2GradientPanel p = new Guna2GradientPanel();
            if (send_pp == "You")
            {
                p.FillColor = Color.FromArgb(247, 37, 133);
                p.FillColor2 = Color.FromArgb(114, 9, 183);
            }
            else
            {
                p.FillColor = Color.FromArgb(0, 119, 182);
                p.FillColor2 = Color.FromArgb(72, 202, 228);
            }
            p.MaximumSize = new Size(770, 0);

            p.GradientMode = LinearGradientMode.Horizontal;
            p.BorderRadius = 20;

            p.Size = new Size(rtb.Width + 30, rtb.Height + 30);
            p.Controls.Add(rtb);
            return p;
        }

        private void CreateSeverbtn_Click(object sender, EventArgs e)
        {
            try
            {

                Process firstProc = new Process();
                firstProc.StartInfo.FileName = "Sever.exe - Shortcut";
                firstProc.EnableRaisingEvents = true;

                firstProc.Start();

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred!!!: " + ex.Message);
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                if (client.IsConnected)
                {
                    history_panel.Visible = !history_panel.Visible;
                    if (history_panel.Visible)
                    {
                        foreach (var h in History)
                        {
                            this.History_lstbox.Items.Add(h);
                        }
                    }
                    else
                    {
                        this.History_lstbox.Items.Clear();
                    }
                }
                else
                {
                    history_panel.Visible = false;
                }
            }
        }

        private void Reset_history()
        {
            //this.yourIPlabel.Text = "";
            //this.flowLayoutPanel1.Controls.Clear();
            History.Clear();
        }

        private void search_history_TextChanged(object sender, EventArgs e)
        {
            this.History_lstbox.Items.Clear();
            foreach (var h in History)
            {
                if (h.Contains(search_history.Text))
                {
                    History_lstbox.Items.Add(h);
                }
            }
        }

        private void YourIPis(string s)
        {
            yourIPlabel.Text = "Your IP is : " + s;
        }

        private void Minigame_btn_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void GIF_btn_Click(object sender, EventArgs e)
        {
            icon_panel.Visible = true;
            icon_panel.Controls.Clear();
            this.icon_panel.HorizontalScroll.Maximum = 0;
            this.icon_panel.VerticalScroll.Maximum = 1;
            this.icon_panel.AutoScroll = false;
            this.icon_panel.VerticalScroll.Visible = false;
            this.icon_panel.HorizontalScroll.Visible = false;
            this.icon_panel.AutoScroll = true;
            DirectoryInfo d = new DirectoryInfo("GIF_icon");

            foreach (var file in d.GetFiles("*.gif"))
            {
                PictureBox p = new PictureBox
                {
                    Size = new Size(50, 50),
                    Margin = new Padding(4, 4, 4, 4),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = resizeImage(Image.FromFile(d + "\\" + file.Name), new Size(50, 50)),

                };
                this.icon_panel.Controls.Add(p);
                p.Click += (s, ee) => Add_emo_pic_manual(d + "\\" + file.Name);
            }
        }

        private void IP_lstbox_DoubleClick(object sender, EventArgs e)
        {
            if (client != null)
            {
                if (client.IsConnected)
                {
                    btnConnect_Click(sender, e);
                }
            }
            txtIP = IP_lstbox.SelectedItem.ToString();
            btnConnect_Click(sender, e);
        }

        private void IP_lstbox_Click(object sender, EventArgs e)
        {
            txtIP = IP_lstbox.SelectedItem.ToString();
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
