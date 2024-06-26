﻿using SuperSimpleTcp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TCPClient
{
    public partial class Form1 : Form
    {
        private FontDialog fd = new FontDialog();
        public Form1()
        {
            InitializeComponent();
        }
        SimpleTcpClient client;
        Hashtable emotions;
        List<char> going_to_send;
        List<string> History;
        List<string> History_sever;
        string yourip;
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

                        //add sent message to our flowlayoutpanel
                        //System.Windows.Forms.RichTextBox rtb = new System.Windows.Forms.RichTextBox();
                        //rtb.Text = "You: " + new string(going_to_send.ToArray());
                        //rtb.Width = 350;
                        ////int c = rtb.Text.Length;
                        ////c = (c / 55)+1;
                        ////rtb.MaxLength = 10;
                        ////rtb.MaximumSize = new Size(550, 0);
                        ////rtb.AutoSize = true;
                        //rtb.Font = this.txtMessage.Font;
                        ////float height = this.txtMessage.Font.GetHeight();
                        ////int high = (int)Math.Round(height)+5;
                        ////rtb.Height = high;
                        //using (Graphics g = CreateGraphics())
                        //{
                        //    rtb.Height = (int)g.MeasureString(rtb.Text,
                        //        rtb.Font, rtb.Width).Height*2;
                        //}
                        //rtb.BorderStyle = BorderStyle.None;
                        //rtb.ReadOnly = true;
                        //rtb.TabStop = false;

                        //rtb.Click += (z,ee) => HideCaret(rtb.Handle);
                        //float height = rtb.Font.GetHeight();
                        //int high = (int)Math.Round(height);
                        //rtb = AddEmotions(high, rtb);
                        //System.Windows.Forms.Panel pn = new Panel();
                        //pn.BorderStyle = BorderStyle.FixedSingle;
                        //pn.Size = new Size(550, lb.Size.Height);
                        //pn.Controls.Add(lb);
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
            try
            {
                //try connect to our sever
                client = new SimpleTcpClient(txtIP.Text);
                client.Events.Connected += Events_Connected;
                client.Events.DataReceived += Events_DataReceived;
                client.Events.Disconnected += Events_Disconnected;
                client.Connect();
                btnSend.Enabled = true;
                btnConnect.Enabled = false;
                button2.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            btnSend.Enabled = true;
            going_to_send = new List<char>();
            History = new List<string>();
            History_sever = new List<string>();
            CreateEmotions();
            Add_pic_emotion();
        }

        private void Events_Disconnected(object sender, ConnectionEventArgs e)
        {
            //this.Invoke((MethodInvoker)delegate
            {
                //System.Windows.Forms.Label lb = new System.Windows.Forms.Label();
                //lb.Text = "Sever disconnected. ";
                //lb.MaximumSize = new Size(550, 0);
                //lb.AutoSize = true;
                //lb.BorderStyle = BorderStyle.FixedSingle;
                //this.flowLayoutPanel1.HorizontalScroll.Maximum = 0;
                //this.flowLayoutPanel1.VerticalScroll.Maximum = 0;
                //this.flowLayoutPanel1.AutoScroll = false;
                //this.flowLayoutPanel1.VerticalScroll.Visible = false;
                //this.flowLayoutPanel1.HorizontalScroll.Visible = false;
                //this.flowLayoutPanel1.AutoScroll = true;
                //this.flowLayoutPanel1.Controls.Add(lb);
                //this.flowLayoutPanel1.AutoScrollPosition = new Point(0, flowLayoutPanel1.VerticalScroll.Maximum);
                MessageBox.Show("Disconnect");
                
                Reset_history();
                //btnSend.Enabled = !true;
                //btnConnect.Enabled = !false;
            }
            //});
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
                //Location = new Point(100, 100),

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
                    //MessageBox.Show(memoryStream.Capacity.ToString());
                    Image.FromStream(memoryStream);
                    //string path = DateTime.Now.ToString("yyyyMMddTHHmmss")+".gif";
                    string path = DateTime.UtcNow.ToString("yyyyMMddTHHmmssfff") + ".gif";
                    FileStream fs = new FileStream(path, FileMode.Create);
                    fs.Write(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
                    //memoryStream.SetLength(0);
                    fs.Close();
                    Console.WriteLine("Client:"+ receivedataimg);
                    Console.WriteLine("Client:"+ memoryStream.Capacity);
                    Console.WriteLine("Client:" + (memoryStream.Capacity - receivedataimg));

                    if (receivedataimg == memoryStream.Capacity)
                    {
                        if (ismemories)
                        {
                            PictureBox p = insertmemoriespicture(path);
                            this.memories_flowlayoutpanel.Controls.Add(p);
                            //File.Delete("outfile.gif");
                            //this.memoryStream.Close();
                            //this.memoryStream.Dispose();
                            //this.memoryStream = new MemoryStream(0);
                        }
                        else
                        {
                            //string path = "outfile.gif";
                            //FileStream fs = new FileStream(path, FileMode.Create);
                            //fs.Write(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
                            ////memoryStream.SetLength(0);
                            //fs.Close();
                            PictureBox p = insertpicture(path);
                            this.flowLayoutPanel1.Controls.Add(p);
                            //File.Delete("outfile.gif");
                            //this.memoryStream.Close();
                            //this.memoryStream.Dispose();
                            //this.memoryStream = new MemoryStream(0);
                            //memoryStream.SetLength(0);
                            //this.memoryStream = Clear(memoryStream);

                            //save_docx(memoryStream);
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
                        //memoryStream.C
                        //memoryStream.SetLength(0);
                        //this.memoryStream = Clear(this.memoryStream);
                        //Thread.Sleep(30);
                        //MessageBox.Show(this, "You've got new file");
                        //memoryStream.Close();
                        //memoryStream.Dispose();
                        //memoryStream = new MemoryStream(0);
                    }
                    else if (receive.Contains("therearefilesofsevermemories"))
                    {
                        receivedataimg = int.Parse(Regex.Match(receive, @"\d+").Value) + memoryStream.Capacity;
                        ismemories = true;
                        Console.WriteLine(receivedataimg);
                        memoryStream.SetLength(0);
                        //memoryStream.C
                        //memoryStream.SetLength(0);
                        //this.memoryStream = Clear(this.memoryStream);
                        //Thread.Sleep(30);
                        //MessageBox.Show(this, "You've got new file");
                        //memoryStream.Close();
                        //memoryStream.Dispose();
                        //memoryStream = new MemoryStream(0);
                    }
                    else if (receive.Contains("thisisahistoryoftheseveryouareonlineandyouripis"))
                    {
                        Console.WriteLine(receive);
                        receive = receive.Replace("thisisahistoryoftheseveryouareonlineandyouripis", "");
                        int indip = receive.IndexOf("/+/+/+");
                        yourip = receive.Substring(0, indip);
                        YourIPis(receive.Substring(0, indip));
                        //yourIPlabel.Text = "Your IP is : "+ receive.Substring(0, indip);
                        receive = receive.Remove(0, indip);
                        Console.WriteLine("ip: "+receive);
                        receive = receive.Replace("/+/+/+", "\n");
                        string[] receive_history = receive.Split('\n');
                        foreach(string str in receive_history)
                        {
                            History.Add(str);
                        }
                        Console.WriteLine(receive);
                        memoryStream.SetLength(0);
                        //memoryStream.C
                        //memoryStream.SetLength(0);
                        //this.memoryStream = Clear(this.memoryStream);
                        //Thread.Sleep(30);
                        //MessageBox.Show(this, "You've got new file");
                        //memoryStream.Close();
                        //memoryStream.Dispose();
                        //memoryStream = new MemoryStream(0);
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
                        //System.Windows.Forms.RichTextBox rtb = new System.Windows.Forms.RichTextBox();
                        //rtb.Text = $"{name}: " + txt;
                        //rtb.Width = 350;
                        ////int c = rtb.Text.Length;
                        ////c = (c / 55)+1;
                        ////rtb.MaxLength = 10;
                        ////rtb.MaximumSize = new Size(550, 0);
                        ////rtb.AutoSize = true;
                        ////float height = this.txtMessage.Font.GetHeight();
                        ////int high = (int)Math.Round(height)+5;
                        ////rtb.Height = high;
                        //rtb.ReadOnly = true;
                        //rtb.Click += (z, ee) => HideCaret(rtb.Handle);
                        var cvt = new FontConverter();
                        Font f = cvt.ConvertFromString(font) as Font;
                        //rtb.Font = f;
                        //using (Graphics g = CreateGraphics())
                        //{
                        //    rtb.Height = (int)g.MeasureString(rtb.Text,
                        //        rtb.Font, rtb.Width).Height * 2;
                        //}
                        //float height = rtb.Font.GetHeight();
                        //int high = (int)Math.Round(height);
                        //AddEmotions(high,rtb);
                        //rtb.BorderStyle = BorderStyle.None;
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
                //txtInfo.Text += $"Sever connected.{Environment.NewLine}";
                System.Windows.Forms.Label lb = new System.Windows.Forms.Label();
                lb.Text = $"Sever {e.IpPort} connected.";
                lb.MaximumSize = new Size(550, 0);
                lb.AutoSize = true;
                lb.BorderStyle = BorderStyle.FixedSingle;
                //System.Windows.Forms.Panel pn = new Panel();
                //pn.BorderStyle = BorderStyle.FixedSingle;
                //pn.Size = new Size(550, lb.Size.Height);
                //pn.Controls.Add(lb);
                this.flowLayoutPanel1.HorizontalScroll.Maximum = 0;
                this.flowLayoutPanel1.VerticalScroll.Maximum = 0;
                this.flowLayoutPanel1.AutoScroll = false;
                this.flowLayoutPanel1.VerticalScroll.Visible = false;
                this.flowLayoutPanel1.HorizontalScroll.Visible = false;
                this.flowLayoutPanel1.AutoScroll = true;
                this.flowLayoutPanel1.Controls.Add(lb);
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

        //public string getFrames(Image originalImg)
        //{
        //    int numberOfFrames = originalImg.GetFrameCount(FrameDimension.Time);
        //    Image[] frames = new Image[numberOfFrames];
        //    int x = 0;
        //    int y = 0;
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        for (int i = 0; i < numberOfFrames; i++)
        //        {
        //            originalImg.SelectActiveFrame(FrameDimension.Time, i);
        //            frames[i] = ((Image)originalImg.Clone());
        //            //frames[i].Save("hinh" + i + ".jpg", ImageFormat.Jpeg);
        //            //Image image = Image.FromFile(@"c:\image.bmp");
        //            // Save image to stream.
        //            frames[i].Save(stream, ImageFormat.Gif);
        //        }
        //    }
        //    //Image img = Image.FromStream(memoryStream);
        //    string path = "outfile.gif";
        //    FileStream fs = new FileStream(path, FileMode.Create);
        //    fs.Write(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        //    fs.Close();
        //    return path;
        //}

        private void Sendfile(string filePath)
        {
            string ext = Path.GetExtension(filePath);
            byte[] img_to_send = null;
            if (ext == ".gif")
            {
                //string path = "outfile.gif";
                //getFrames(Image.FromFile(filePath));//.Save(path,ImageFormat.Gif);
                //lst_img.Save(path, ImageFormat.Gif);
                Bitmap img = new Bitmap(filePath);
                //Bitmap image = new Bitmap(img, img.Width/2, img.Height/2);
                //Graphics g = Graphics.FromImage(image);

                ////Creates a new Bitmap as the size of the window
                //Bitmap bmp = new Bitmap(this.Width, this.Height);

                ////Creates a new graphics to handle the image that is coming from the stream
                //Graphics g = Graphics.FromImage((Image)bmp);
                //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                ////Resizes the image from the stream to fit our windows
                //g.DrawImage(Image.FromFile(filePath), 0, 0, this.Width, this.Height);

                //this.Image = (Image)bmp;

                img_to_send = ImageToBytes(img, ImageFormat.Gif);
            }
            else
            {
                //resize image
                Image original = Image.FromFile(filePath);
                //MessageBox.Show(original.Width.ToString() + "  " + original.Height.ToString());
                double h = 200.0 / original.Height;
                double w = 200.0 / original.Width;
                double scale = Math.Max(h, w);
                int new_h = (int)Math.Round(original.Height * scale);
                int new_w = (int)Math.Round(original.Width * scale);
                //MessageBox.Show(new_h.ToString() + "  " + h);
                //MessageBox.Show(new_w.ToString() + "  "+ w);
                Image resized = ResizeImage(original, new Size(new_w, new_h));

                ImageConverter imgCon = new ImageConverter();
                img_to_send = (byte[])imgCon.ConvertTo(resized, typeof(byte[]));
            }


            //FileStream fileStream = new FileStream(filePath + "_resized.JPG", FileMode.Create); //I use file stream instead of Memory stream here
            //resized.Save(fileStream, ImageFormat.Jpeg);
            //fileStream.Close(); //close after use
            //FileStream fs = new FileStream(filePath + "_resized.JPG", FileMode.Open); ;
            //FileStream fs = new FileStream(filePath , FileMode.Open);

            //FileStream Fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            //long totalBytes = Fs.Length;

            //string senddata = "iushcxlchiasdchjaslfcdajhiodcadshjca" + totalBytes.ToString();
            //MessageBox.Show(totalBytes.ToString());
            //byte[] byteArray = Encoding.UTF8.GetBytes(senddata);
            //MemoryStream stream = new MemoryStream(byteArray);
            //MemoryStream img_stream = new MemoryStream(img_to_send);


            //convert image to memorystream
            
            long totalBytes = img_to_send.Length;
            string senddata = "iushcxlchiasdchjaslfcdajhiodcadshjca" + totalBytes.ToString();
            byte[] byteArray = Encoding.UTF8.GetBytes(senddata);
            MemoryStream stream = new MemoryStream(byteArray);
            MemoryStream img_stream = new MemoryStream(img_to_send);
            //MessageBox.Show(stream.Length.ToString());
            //MessageBox.Show(img_stream.Length.ToString());



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
            //fs.Close();
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
                //this.txtMessage.Text = "";
                this.txtMessage.Font = fd.Font;
                //this.txtMessage.Text = going_to_send;
                //float height = txtMessage.Font.GetHeight();
                //int high = (int)Math.Round(height);
                //AddEmotions(high);
            }
            /*
            var cvt = new FontConverter();
            string s = cvt.ConvertToString(this.txtMessage.Font);
            Font f = cvt.ConvertFromString(s) as Font; ;
            label3.Font = f;
            */
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
            //going_to_send = txtMessage.Text;
            //Console.WriteLine(going_to_send);
            //float height = txtMessage.Font.GetHeight();
            //int high = (int)Math.Round(height);
            txtMessage.MinimumSize = new Size(0,20);
            using (Graphics g = CreateGraphics())
            {
                txtMessage.Height = (int)g.MeasureString(txtMessage.Text,
                    txtMessage.Font, txtMessage.Width).Height ;
            }
            //AddEmotions(high,this.txtMessage);

        }

        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("__________");
            
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
            //Console.WriteLine(going_to_send.Count);
            //Console.WriteLine(txtMessage.SelectionStart);
            //Console.WriteLine(going_to_send.ToString());
            Console.WriteLine(txtMessage.Text.Length);
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
                    Size = new Size(50,50),
                    Margin = new Padding(4,4,4,4),
                    Image = resizeImage(Image.FromFile(d+"\\"+file.Name), new Size(50, 50)),
                };
                this.icon_panel.Controls.Add(p);
                p.Click += (s, ee) => Add_emo_pic_manual(d+"\\"+file.Name);
            }
        }
        private void Add_emo_pic_manual(string emo)
        {
            //txtMessage.Text += emo;
            //going_to_send.AddRange(emo);
            //float height = txtMessage.Font.GetHeight();
            //int high = (int)Math.Round(height);
            //txtMessage.Text = new string(going_to_send.ToArray());
            //AddEmotions(high, this.txtMessage);
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

        private RichTextBox Create_rtb(string s, Font f, string send_pp)
        {
            System.Windows.Forms.RichTextBox rtb = new System.Windows.Forms.RichTextBox();
            rtb.Text = $"{send_pp}: " + s;
            rtb.Width = 350;
            //int c = rtb.Text.Length;
            //c = (c / 55)+1;
            //rtb.MaxLength = 10;
            //rtb.MaximumSize = new Size(550, 0);
            //rtb.AutoSize = true;
            //float height = this.txtMessage.Font.GetHeight();
            //int high = (int)Math.Round(height)+5;
            //rtb.Height = high;
            rtb.ReadOnly = true;
            rtb.Click += (z, ee) => HideCaret(rtb.Handle);
            var cvt = new FontConverter();
            //Font f = cvt.ConvertFromString(font) as Font;
            rtb.Font = f;
            using (Graphics g = CreateGraphics())
            {
                rtb.Height = (int)g.MeasureString(rtb.Text,
                    rtb.Font, rtb.Width).Height+10;
            }
            float height = rtb.Font.GetHeight();
            int high = (int)Math.Round(height);
            //AddEmotions(high, rtb);
            rtb.BorderStyle = BorderStyle.None;
            return rtb;
        }

        private void CreateSeverbtn_Click(object sender, EventArgs e)
        {
            try
            {

                Process firstProc = new Process();
                firstProc.StartInfo.FileName = "Sever.exe - Shortcut";
                firstProc.EnableRaisingEvents = true;

                firstProc.Start();

                //firstProc.WaitForExit();

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred!!!: " + ex.Message);
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string message = "Do you want to leave this chat?";
            string title = "Leave chat";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string mess = "Do you want to save IP of chat for return later?";
                string t = "Save IP";
                MessageBoxButtons btn = MessageBoxButtons.YesNo;
                DialogResult r = MessageBox.Show(mess, t, btn, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    bool ex = false;
                    foreach (var item in IP_lstbox.Items)
                    {
                        if (IP_lstbox.GetItemText(item) == txtIP.Text)
                        {
                            ex = true;
                            
                        }
                    }
                    if(!ex)
                    {
                        History_sever.Add(txtIP.Text);
                        IP_lstbox.Items.Add(txtIP.Text);
                    }
                    button2.Enabled = false;
                }
                client.Disconnect();
                btnSend.Enabled = !true;
                btnConnect.Enabled = !false;
                history_panel.Visible = false;
                this.History_lstbox.Items.Clear();
                this.flowLayoutPanel1.Controls.Clear();
                this.yourIPlabel.Text = "";
                memories_flowlayoutpanel.Controls.Clear();
            }
            else
            {

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
                if(h.Contains(search_history.Text))
                {
                    History_lstbox.Items.Add(h);
                }
            }
        }

        private void YourIPis(string s)
        {
            yourIPlabel.Text = "Your IP is : " + s;
        }

        private void IP_lstbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //public void loadform(object Form)
        //{
        //    if (Application.OpenForms["Form2"] != null)
        //    {
        //        Form f = Form as Form;
        //        f.TopLevel = false;
        //        //f.Dock = DockStyle.Fill;
        //        f.Show();
        //    }
        //}

        private void Minigame_btn_Click(object sender, EventArgs e)
        {
            Game f2 = new Game();
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
                    button2_Click(sender, e);
                }
            }
            txtIP.Text = IP_lstbox.SelectedItem.ToString();
            btnConnect_Click(sender, e);
        }

        private void IP_lstbox_Click(object sender, EventArgs e)
        {
            txtIP.Text = IP_lstbox.SelectedItem.ToString();
        }
    }
}
