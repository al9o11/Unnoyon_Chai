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
    public partial class SignUp : Form
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

        public SignUp()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
            
        }
        
        private void Pic_Click(object sender, EventArgs e)
        {
            pictureBox2.Enabled = true;
            pictureBox2.Visible = true;
            pictureBox2.BringToFront();
            this.Opacity = 20;
            pictureBox2.Image = pictureBox1.Image;
            
        }

        private void p2_click(object sender, EventArgs e)
        {
            pictureBox2.Enabled=false;
            pictureBox2.SendToBack();
            this.Opacity = 100;
            pictureBox2.Visible = false; 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text !=""&&textBox2.Text != ""&& textBox3.Text != ""&& textBox4.Text != ""&&textBox5.Text != "")
            {
                if (textBox4.Text == textBox5.Text)
                {
                    SqlConnection conn = new SqlConnection(cs);
                    string qr = "insert into User_info values (@username,@email,@nid,@user_type,@password,@picture)";
                    SqlCommand cmd = new SqlCommand(qr, conn);
                    cmd.Parameters.AddWithValue("@username", textBox1.Text);
                    cmd.Parameters.AddWithValue("@email", textBox2.Text);
                    cmd.Parameters.AddWithValue("@nid", textBox3.Text);
                    cmd.Parameters.AddWithValue("@user_type", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@password", textBox4.Text);
                    cmd.Parameters.AddWithValue("@picture", SavePhoto());

                    conn.Open();
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        this.Close();
                        Home home = new Home();
                        home.Show();
                    }
                    else
                    {
                        label8.Visible = true;
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        pictureBox1.Image = null;
                    }
                }
                else
                {
                    label9.Visible = true;
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
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPG FILE (*.JPG) | *.jpg";
            ofd.Title = "Select Image";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);   
            }
            //ofd.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image=null;
        }
    }
}
