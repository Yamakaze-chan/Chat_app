using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPSever
{
    public partial class Invite_msg : UserControl
    {
        public event EventHandler Playgame;
        string show_label;
        public Invite_msg()
        {
            InitializeComponent();
        }

        public string Show_label
        {
            get { return show_label; }
            set
            {
                show_label = value;
                label1.Text = show_label;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if(Playgame != null)
            {
                Playgame.Invoke(sender, e);
            }
        }
    }
}
