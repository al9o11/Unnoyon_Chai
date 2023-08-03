using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace ProjectDelta
{
    public partial class Admin : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
      (
          int nLeftRect,     // x-coordinate of upper-left corner
          int nTopRect,      // y-coordinate of upper-left corner
          int nRightRect,    // x-coordinate of lower-right corner
          int nBottomRect,   // y-coordinate of lower-right corner
          int nWidthEllipse, // width of ellipse
          int nHeightEllipse // height of ellipse
      );
        public Admin(string un)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
            label1.Text = un;
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            C_Info c_Info = new C_Info(label1.Text);   
            c_Info.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            O_info o_Info = new O_info(label1.Text);   
            o_Info.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Edit_pr edit_Pr = new Edit_pr(label1.Text);
            edit_Pr.Show();
        }
    }
}
