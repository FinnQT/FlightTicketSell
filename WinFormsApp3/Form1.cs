using System.Data;
using System.Data.SqlClient;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_dangnhap_Click(object sender, EventArgs e)
        {
            string userName = textBox_tendangnhap.Text;
            string password = textBox_matkhau.Text;

            string tentk = "", matkhau = "";
            string chucnang = "";
            String connectionString = "Data Source=DESKTOP-UOP6PLS\\MSSQLSERVER01;Initial Catalog=QLMB;Integrated Security=True";
            string query = "select* from TAIKHOAN where TenDangNhap='" + userName + "' and MatKhau='" + password + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    tentk = (string)reader[0];
                    matkhau = (string)reader[1];
                    chucnang = (string)reader[2];
                }
                reader.Close();
            }

            if (tentk == userName && matkhau == password)
            {

                if (chucnang == "nhanvien")
                {
                    fQuanly fQuanly = new fQuanly();
                    this.Hide();
                    fQuanly.Show();
                }
                else
                {
                    QuanlyUse quanly = new QuanlyUse();
                    this.Hide();
                    quanly.Show();

                }
            }

        }





        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}