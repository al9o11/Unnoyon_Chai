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
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
namespace ProjectDelta
{
    public partial class Complain : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["uc"].ConnectionString;
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
        public static string name;
        public Complain(string un)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            name= un;
        }

        private void Complain_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(cs);
            string qr = "insert into complain values (@title,@descp,@picture,@username)";
            SqlCommand cmd = new SqlCommand(qr, conn);
            cmd.Parameters.AddWithValue("@title", textBox1.Text);
            cmd.Parameters.AddWithValue("@descp", textBox2.Text);
            cmd.Parameters.AddWithValue("@picture", SavePhoto());
            cmd.Parameters.AddWithValue("username", name);

            conn.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                this.Hide();
                Citizen citizen = new Citizen(name);
                citizen.Show();
            }
            else
            {
                label10.Visible = true;
                textBox1.Text = "";
                textBox2.Text = "";
                pictureBox1.Image = null;
            }
            
        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Citizen citizen = new Citizen(name);
            citizen.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Citizen citizen = new Citizen(name);
            citizen.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPG FILE (*.JPG) | *.jpg";
            ofd.Title = "Select Image";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = new Bitmap(ofd.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
        }
    }
}
