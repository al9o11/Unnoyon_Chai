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
    public partial class Home : Form
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
        public Home()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = true;
            }
            else
            {
                textBox2.UseSystemPasswordChar=false;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
            this.Hide();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
                
            if(textBox1.Text !=""&&textBox2.Text != "")
            {
                SqlConnection conn = new SqlConnection(cs);
                string qr = "select username , password,user_type from User_info where username=@user and password=@pass and user_type=@type";
                SqlCommand cmd = new SqlCommand(qr, conn);
                cmd.Parameters.AddWithValue("@user", textBox1.Text);
                cmd.Parameters.AddWithValue("@pass", textBox2.Text);
                cmd.Parameters.AddWithValue("@type", comboBox1.Text);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows == true)
                {
                    if (comboBox1.Text == "Citizen")
                    {
                        this.Hide();
                        Citizen citizen = new Citizen(textBox1.Text);
                        citizen.Show();
                    }
                    else if (comboBox1.Text == "Official")
                    {
                        this.Hide();
                        Official official = new Official(textBox1.Text);
                        official.Show();
                    }
                    else if (comboBox1.Text == "Admin")
                    {
                        this.Hide();
                        Admin admin = new Admin(textBox1.Text);
                        admin.Show();
                    }
                }
                else
                {
                    label4.Visible = true;
                }
            }
            else
            {
                label5.Visible = true;  
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
