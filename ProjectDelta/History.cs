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
using System.Configuration;
using System.Data.SqlClient;
namespace ProjectDelta
{
    public partial class History : Form
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
        static string name;
        public History(string un)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            name = un;  
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Citizen citizen = new Citizen(name);
            citizen.Show();
        }

        private void History_Load(object sender, EventArgs e)
        {
            BindGridView();
        }
        void BindGridView()
        {
            SqlConnection conn = new SqlConnection(cs);
            string qr = "select * from complain";
            SqlDataAdapter sda = new SqlDataAdapter(qr, conn);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView1.Columns[2];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.RowTemplate.Height =80;
        }
    }
}
