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
    public partial class Edit_pr : Form
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
        string cs = ConfigurationManager.ConnectionStrings["uc"].ConnectionString;
        static string name;
        public Edit_pr(string un)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            name = un;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(cs);
            string qr = "UPDATE User_info SET Email =@email, password =@pass, picture=@picture where username=@name";
            SqlCommand cmd = new SqlCommand(qr, conn);
            cmd.Parameters.AddWithValue("@email", textBox2.Text);
            cmd.Parameters.AddWithValue("@pass", textBox3.Text);
            cmd.Parameters.AddWithValue("@picture", SavePhoto());
            cmd.Parameters.AddWithValue("@name", name);

            conn.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                if (comboBox1.Text == "Citizen")
                {
                    this.Hide();
                    Citizen citizen = new Citizen(name);
                    citizen.Show();
                }
                else if (comboBox1.Text == "Admin")
                {
                    this.Hide();
                    Admin admin = new Admin(name);
                    admin.Show();
                }
                else if (comboBox1.Text == "Official")
                {
                    this.Hide();
                    Official official = new Official(name);
                    official.Show();
                }
            }
            else
            {
                label10.Visible = true;
            }
        }
        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox2.Image.Save(ms, pictureBox2.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.Show();
        }

        private void Edit_pr_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPG FILE (*.JPG) | *.jpg";
            ofd.Title = "Select Image";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = new Bitmap(ofd.FileName);
            }
        }
    }
}
