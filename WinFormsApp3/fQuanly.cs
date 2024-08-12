using Microsoft.VisualBasic.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Application = System.Windows.Forms.Application;

namespace WinFormsApp3
{
    public partial class fQuanly : Form
    {

        BindingSource ListSanBay = new BindingSource();
        BindingSource ListTuyenBay = new BindingSource();
        BindingSource ListHanhKhach = new BindingSource();
        BindingSource ListChuyenBay = new BindingSource();
        BindingSource ListSanBayTrungGian = new BindingSource();
        BindingSource ListDatcho = new BindingSource();

        public fQuanly()
        {
            InitializeComponent();
            QuanLySanBay();
            QuanLyTuyenBay();
            QuanLyHanKhach();
            QuanLyCB();
            LoadSanBay();// load san bay cho tiep nhan lich va dat ve dat cho
            TiepNhanLichChuyenBay();
            Datve_Datcho();
            QuanLySBTG();
            loadQuiDinh();
            HuyPhieuDatCho();



        }
        private string connectionString = "Data Source=DESKTOP-UOP6PLS\\MSSQLSERVER01;Initial Catalog=QLMB;Integrated Security=True";


        private int TGBAY_TOITHIEU;
        private int TGDUNG_TOITHIEU;
        private int TGDUNG_TOIDA;
        private int TGDUNG_CHAMNHATKHIDATVE;
        private int TGCHAMNHATKHIHUYVE;



        void loadCB()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();
                string query = "SELECT * FROM CHUYENBAY";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView_CB.DataSource = dataTable;
            }

        }

        void loadChiTietChuyenBay_SanBaytrunggian()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();
                string query = "select CHUYENBAY.MaChuyenBay,SANBAYTRUNGGIAN.MASANBAYTRUNGGIAN, SANBAYTRUNGGIAN.TENSANBAY, SANBAYTRUNGGIAN.THOIGIANDUNG,CHUYENBAY_SANBAYTRUNGGIAN.STT ,SANBAYTRUNGGIAN.GHICHU from CHUYENBAY, CHUYENBAY_SANBAYTRUNGGIAN, SANBAYTRUNGGIAN \r\nwhere\r\nCHUYENBAY.MaChuyenBay=CHUYENBAY_SANBAYTRUNGGIAN.MaChuyenBay and\r\nCHUYENBAY_SANBAYTRUNGGIAN.MASANBAYTRUNGGIAN=SANBAYTRUNGGIAN.MASANBAYTRUNGGIAN";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView_chitietCB_SBTG.DataSource = dataTable;
            }

        }
        void loadbangSANBAYTRUNGGIAN()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();
                string query = "SELECT * FROM SANBAYTRUNGGIAN";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView_sanbaytrunggian.DataSource = dataTable;
            }

        }

        //load automachuyenbay
        void LoadAutoMaCB()
        {
            textBox_machuyenbay.Text = "";
            string chuoimax = "";
            int i = 0;
            string query = "select Count(machuyenbay) from CHUYENBAY";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    i = (int)reader[0];

                }
                reader.Close();
            }

            if (i == 0)
            {
                textBox_machuyenbay.Text = "CB00" + 1;
            }
            else
            {
                string query1 = "select MAX(RIGHT(machuyenbay, 3)) from CHUYENBAY";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query1, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        chuoimax = (string)reader[0];

                    }
                    reader.Close();
                }
                string numberstring = new string(chuoimax.Where(char.IsDigit).ToArray());
                int j = int.Parse(numberstring);
                if (j >= 0 && j < 9)
                {
                    j = j + 1;
                    textBox_machuyenbay.Text = "CB00" + j;
                }
                else if (j >= 9)
                {
                    j = j + 1;
                    textBox_machuyenbay.Text = "CB0" + j;
                }
            }
        }



        //load auto madongia
        string LoadAutoMaDonGia()
        {
            string madongia = "";
            string chuoimax = "";
            int i = 0;
            string query = "select count(MaDonGia) from dongia";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    i = (int)reader[0];

                }
                reader.Close();
            }
            if (i == 0)
            {
                madongia = "DG00" + 1;
            }
            else
            {
                string query1 = "select MAX(RIGHT(MaDonGia, 3)) from dongia";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query1, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        chuoimax = (string)reader[0];

                    }
                    reader.Close();
                }
                string numberstring = new string(chuoimax.Where(char.IsDigit).ToArray());
                int j = int.Parse(numberstring);
                if (j >= 0 && j < 9)
                {
                    j = j + 1;
                    madongia = "DG00" + j;
                }
                else if (j >= 9)
                {
                    j = j + 1;
                    madongia = "DG0" + j;
                }
            }

            return madongia;
        }

        string LoadAutoMaTTVE()
        {
            string maTTVE = "";
            string chuoimax = "";
            int i = 0;
            string query = "select count(MaTinhTrangVe) from TINHTRANGVE";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    i = (int)reader[0];

                }
                reader.Close();
            }
            if (i == 0)
            {
                maTTVE = "TT00" + 1;
            }
            else
            {
                string query1 = "select MAX(RIGHT(MaTinhTrangVe, 3)) from TINHTRANGVE";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query1, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        chuoimax = (string)reader[0];

                    }
                    reader.Close();
                }
                string numberstring = new string(chuoimax.Where(char.IsDigit).ToArray());
                int j = int.Parse(numberstring);
                if (j >= 0 && j < 9)
                {
                    j = j + 1;
                    maTTVE = "TT00" + j;
                }
                else if (j >= 9)
                {
                    j = j + 1;
                    maTTVE = "TT0" + j;
                }
            }

            return maTTVE;
        }


        void LoadMaTuyenbay()
        {
            comboBox_matuyenbay.Items.Clear();

            string query = "select * from tuyenbay";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox_matuyenbay.Items.Add(reader[0]);
                }
                reader.Close();
            }

        }

        // load matuyenbay auto
        string LoadAutoMatuyenbay()
        {
            string maTUYENBAY = "";
            string chuoimax = "";
            int i = 0;
            string query = "SELECT Count(MaTuyenBay) FROM TUYENBAY";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    i = (int)reader[0];

                }
                reader.Close();
            }


            if (i == 0)
            {
                maTUYENBAY = "TB00" + 1;
            }
            else
            {
                string query1 = "select MAX(RIGHT(MaTuyenBay, 3)) from TUYENBAY";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query1, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        chuoimax = (string)reader[0];

                    }
                    reader.Close();
                }
                string numberstring = new string(chuoimax.Where(char.IsDigit).ToArray());
                int j = int.Parse(numberstring);
                if (j >= 0 && j < 9)
                {
                    j = j + 1;
                    maTUYENBAY = "TB00" + j;
                }
                else if (j >= 9)
                {
                    j = j + 1;
                    maTUYENBAY = "TB0" + j;
                }
            }

            return maTUYENBAY;

        }







        void LoadSanBay()
        {
            comboBox_sanbaydi.Items.Clear();
            comboBox_sanbayden.Items.Clear();
            comboBox_sanbaydiDatcho.Items.Clear();
            comboBox_sanbaydenDatcho.Items.Clear();

            string query = "select * from sanbay";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    comboBox_sanbaydi.Items.Add(reader[1]);
                    comboBox_sanbayden.Items.Add(reader[1]);
                    comboBox_sanbaydiDatcho.Items.Add(reader[1]);
                    comboBox_sanbaydenDatcho.Items.Add(reader[1]);

                }
                reader.Close();
            }
        }

        void LoadSanBayTrungGian()
        {

            string query = "select * from SANBAYTRUNGGIAN";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    checkedListBox_sanbaytrunggian.Items.Add(reader[0]);

                }
                reader.Close();
            }
        }




        private void button_tiepnhan_Click(object sender, EventArgs e)
        {

            string machuyenbay = textBox_machuyenbay.Text;
            string tuyenbay = comboBox_matuyenbay.Text;
            DateTime ngaygio = dateTimePicker_ngaytiepnhan.Value;
            DateTime defaultDateTime = new DateTime();

            int soluongghehang1;
            int soluongghehang2;
            int dongia;
            int thoigianbay;





            //kiểm tra kiểu dữ liệu đầu vào
            string errorMessage = "";
            if (string.IsNullOrEmpty(textBox_machuyenbay.Text) || string.IsNullOrEmpty(comboBox_matuyenbay.Text) ||
                ngaygio == defaultDateTime ||
                string.IsNullOrEmpty(textBox_soghe2.Text) || string.IsNullOrEmpty(textBox_soghe1.Text) ||
                string.IsNullOrEmpty(textBox_dongia.Text) || string.IsNullOrEmpty(textBox_thoigianbay.Text)
                )
            {
                errorMessage = "Có thuộc tính không hợp lệ." + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                // Hiển thị thông báo lỗi cho người dùng
                MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!int.TryParse(textBox_soghe2.Text, out soluongghehang2) ||
                    !int.TryParse(textBox_soghe1.Text, out soluongghehang1) |
                !int.TryParse(textBox_dongia.Text, out dongia) ||
                !int.TryParse(textBox_thoigianbay.Text, out thoigianbay)
                )
                {

                    errorMessage = "Kiểu dữ liệu không đúng." + Environment.NewLine;
                }

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    // Hiển thị thông báo lỗi cho người dùng
                    MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    soluongghehang1 = int.Parse(textBox_soghe1.Text);
                    soluongghehang2 = int.Parse(textBox_soghe2.Text);
                    dongia = int.Parse(textBox_dongia.Text);
                    thoigianbay = int.Parse(textBox_thoigianbay.Text);
                    if (thoigianbay > TGBAY_TOITHIEU)
                    {

                        // insert chuyenbay
                        string query_chuyenbay = "insert into CHUYENBAY values (@value1,@value2,@value3,@value4,@value5,@value6)";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(query_chuyenbay, connection);
                            command.Parameters.AddWithValue("@value1", machuyenbay);
                            command.Parameters.AddWithValue("@value2", tuyenbay);
                            command.Parameters.AddWithValue("@value3", ngaygio);
                            command.Parameters.AddWithValue("@value4", thoigianbay);
                            command.Parameters.AddWithValue("@value5", soluongghehang1);
                            command.Parameters.AddWithValue("@value6", soluongghehang2);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Insert CHUYEN BAY successful!");
                            }
                        }

                        //insert chuyenbay_sanbaytrunggian
                        int stt = 1;
                        foreach (var checkedItem in checkedListBox_sanbaytrunggian.CheckedItems)
                        {

                            string item = checkedItem.ToString();
                            string query_insertSBTG = "insert into CHUYENBAY_SANBAYTRUNGGIAN values (@value14,@value15,@value16)";
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                SqlCommand command = new SqlCommand(query_insertSBTG, connection);
                                command.Parameters.AddWithValue("@value14", machuyenbay);
                                command.Parameters.AddWithValue("@value15", item);
                                command.Parameters.AddWithValue("@value16", stt);
                                int rowsAffected4 = command.ExecuteNonQuery();
                                if (rowsAffected4 > 0)
                                {
                                    MessageBox.Show("Insert SBTG successful!");
                                    stt++;
                                }
                            }
                        }

                        //insert dongia

                        string query_insertDonGia = "insert into DONGIA values (@value11,@value12,@value13)";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(query_insertDonGia, connection);
                            command.Parameters.AddWithValue("@value11", LoadAutoMaDonGia());
                            command.Parameters.AddWithValue("@value12", machuyenbay);
                            command.Parameters.AddWithValue("@value13", dongia);
                            int rowsAffected3 = command.ExecuteNonQuery();
                            if (rowsAffected3 > 0)
                            {
                                MessageBox.Show("Insert DONGIA successful!");
                            }
                        }



                        //insert Tinh Trang Ve

                        string query_insertTinhtrangve1 = "insert into TINHTRANGVE values (@value17,@value18,@value19,@value20,@value21)";
                        string hv1 = "HV1";
                        string hv2 = "HV2";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(query_insertTinhtrangve1, connection);
                            command.Parameters.AddWithValue("@value17", LoadAutoMaTTVE());
                            command.Parameters.AddWithValue("@value18", machuyenbay);
                            command.Parameters.AddWithValue("@value19", hv1);
                            command.Parameters.AddWithValue("@value20", soluongghehang1);
                            command.Parameters.AddWithValue("@value21", 0);
                            int rowsAffected5 = command.ExecuteNonQuery();
                            if (rowsAffected5 > 0)
                            {
                                MessageBox.Show("Insert TTV1 successful!");
                            }
                        }

                        string query_insertTinhtrangve2 = "insert into TINHTRANGVE values (@value22,@value23,@value24,@value25,@value26)";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(query_insertTinhtrangve2, connection);
                            command.Parameters.AddWithValue("@value22", LoadAutoMaTTVE());
                            command.Parameters.AddWithValue("@value23", machuyenbay);
                            command.Parameters.AddWithValue("@value24", hv2);
                            command.Parameters.AddWithValue("@value25", soluongghehang2);
                            command.Parameters.AddWithValue("@value26", 0);
                            int rowsAffected6 = command.ExecuteNonQuery();
                            if (rowsAffected6 > 0)
                            {
                                MessageBox.Show("Insert TTV2 successful!");
                            }
                        }


                    }
                    else { MessageBox.Show("THOI GIAN BAY TOI THIEU KHONG THOA MAN QUY DINH"); }
                }
            }
            LoadAutoMaCB();
            loadCB();
            loadChiTietChuyenBay_SanBaytrunggian();
            loadbangSANBAYTRUNGGIAN();
            loadDSCHUYENBAY();
            ChuyenBay_Load();
            CB_LoadTTV();
        }

        private void comboBox_matuyenbay_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = comboBox_matuyenbay.SelectedItem.ToString();
            if (comboBox_matuyenbay.SelectedIndex != -1)
            {
                string query = "select * from tuyenbay,SANBAY where MaTuyenBay='" + selectedValue + "'and TUYENBAY.SanBayDen=SANBAY.MaSanBay";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        textBox_sanbayden.Text = (string)reader[4];

                    }
                    reader.Close();
                }

                string query1 = "select * from tuyenbay,SANBAY where MaTuyenBay='" + selectedValue + "'and TUYENBAY.SanBayDi=SANBAY.MaSanBay";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query1, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        textBox_sanbaydi.Text = (string)reader[4];

                    }
                    reader.Close();
                }

            }
            else
            {
                textBox_sanbayden.Text = string.Empty;
                textBox_sanbaydi.Text = string.Empty;
            }
        }


        void loadDSCHUYENBAY()
        {


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();
                string query = "select chuyenbay.MACHUYENBAY,NgayGio, ThoiGianBay,MaHangVe,SoGheTrong,SoGheDat from CHUYENBAY,TUYENBAY,TINHTRANGVE\r\nwhere\r\nCHUYENBAY.MaChuyenBay=TINHTRANGVE.MaChuyenBay and\r\nCHUYENBAY.MaTuyenBay=TUYENBAY.MaTuyenBay and\r\nCHUYENBAY.MaChuyenBay=TINHTRANGVE.MaChuyenBay";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView_loadchuyenbaybanve.DataSource = dataTable;
                dataGridView_DSchuyenbayDatCho.DataSource = dataTable;
            }
        }


        private void button_xemDSchuyenbay_Click(object sender, EventArgs e)
        {

            string masanbayden = "";
            string masanbaydi = "";
            string masanbaydicombobox = comboBox_sanbaydi.Text;
            string masanbaydencombobox = comboBox_sanbayden.Text;
            string query = "select MaSanBay from sanbay where TenSanBay='" + masanbaydicombobox + "'";
            string query1 = "select MaSanBay from sanbay where TenSanBay='" + masanbaydencombobox + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    masanbaydi = (string)reader[0];
                }
                reader.Close();
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query1, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    masanbayden = (string)reader[0];
                }
                reader.Close();
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();
                string query_danhsachCB = "select chuyenbay.MACHUYENBAY,NgayGio, ThoiGianBay,MaHangVe,SoGheTrong,SoGheDat from CHUYENBAY,TUYENBAY,TINHTRANGVE\r\nwhere\r\nCHUYENBAY.MaChuyenBay=TINHTRANGVE.MaChuyenBay and\r\nCHUYENBAY.MaTuyenBay=TUYENBAY.MaTuyenBay and\r\nCHUYENBAY.MaChuyenBay=TINHTRANGVE.MaChuyenBay and\r\nSanBayDi='" + masanbaydi + "' and SanBayDen ='" + masanbayden + "'";
                SqlCommand command = new SqlCommand(query_danhsachCB, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView_loadchuyenbaybanve.DataSource = dataTable;
            }



        }

        private void radioButton_vehang1_CheckedChanged(object sender, EventArgs e)
        {


            if (radioButton_vehang1.Checked)
            {

                radioButton_vehang2.Checked = false;
                double dongia = 0; ;
                string machuyenbay = textBox_machuyenbayVe.Text;
                string query1 = "select DonGia from DONGIA where MaChuyenBay='" + machuyenbay + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query1, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        dongia = (double)reader[0];
                    }
                    reader.Close();
                }

                textBox_giave.Text = dongia.ToString();

            }

        }

        private void radioButton_vehang2_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton_vehang2.Checked)
            {
                radioButton_vehang1.Checked = false;
                double dongia = 0; ;
                string machuyenbay = textBox_machuyenbayVe.Text;

                string query1 = "select DonGia from DONGIA where MaChuyenBay='" + machuyenbay + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query1, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        dongia = (double)reader[0];
                    }
                    reader.Close();
                }
                dongia = dongia * 105 / 100;
                textBox_giave.Text = dongia.ToString();
            }

        }


        string LoadAutoMaVE()
        {
            string MaVe = "";
            string chuoimax = "";
            int i = 0;
            string query = "select count(mave) from VECHUYENBAY";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    i = (int)reader[0];

                }
                reader.Close();
            }
            if (i == 0)
            {
                MaVe = "MV00" + 1;
            }
            else
            {
                string query1 = "select MAX(RIGHT(mave, 3)) from VECHUYENBAY";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query1, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        chuoimax = (string)reader[0];

                    }
                    reader.Close();
                }
                string numberstring = new string(chuoimax.Where(char.IsDigit).ToArray());
                int j = int.Parse(numberstring);
                if (j >= 0 && j < 9)
                {
                    j = j + 1;
                    MaVe = "MV00" + j;
                }
                else if (j >= 9)
                {
                    j = j + 1;
                    MaVe = "MV0" + j;
                }
            }


            return MaVe;
        }

        // load auto makhachhang


        string LoadAutoMaHANHKHACH()
        {
            string MAHK = "";
            string chuoimax = "";
            int i = 0;
            string query = "select count(MAHANHKHACH) from HANHKHACH";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    i = (int)reader[0];

                }
                reader.Close();
            }

            if (i == 0)
            {
                MAHK = "KH00" + 1;
            }
            else
            {
                string query1 = "select MAX(RIGHT(MAHANHKHACH, 3)) from HANHKHACH";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query1, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        chuoimax = (string)reader[0];

                    }
                    reader.Close();
                }
                string numberstring = new string(chuoimax.Where(char.IsDigit).ToArray());
                int j = int.Parse(numberstring);
                if (j >= 0 && j < 9)
                {
                    j = j + 1;
                    MAHK = "KH00" + j;
                }
                else if (j >= 9)
                {
                    j = j + 1;
                    MAHK = "KH0" + j;
                }
            }

            return MAHK;
        }

        private void button_muave_Click(object sender, EventArgs e)
        {
            string machuyenbay = textBox_machuyenbayVe.Text;
            string TenHanhKhach = textBox_tenhanhkhach.Text;
            string CMND = textBox_CMND.Text;
            string dienthoai = textBox_dienthoai.Text;
            double giatien = double.Parse(textBox_giave.Text);
            string mahangve = "";

            string errorMessage = "";
            if (string.IsNullOrEmpty(textBox_machuyenbayVe.Text) || string.IsNullOrEmpty(textBox_tenhanhkhach.Text) ||
                string.IsNullOrEmpty(textBox_CMND.Text) || string.IsNullOrEmpty(textBox_dienthoai.Text) ||
                string.IsNullOrEmpty(textBox_giave.Text))
            {
                errorMessage = "Có thuộc tính không hợp lệ." + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                // Hiển thị thông báo lỗi cho người dùng
                MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (radioButton_vehang1.Checked)
                {
                    mahangve = "HV1";
                }
                else { mahangve = "HV2"; }


                // lay so luong ghe dat, so luong ghe trong

                int soghedat = 0;
                int soghetrong = 0;

                string query = "select SoGheDat, SoGheTrong from TINHTRANGVE where machuyenbay='" + machuyenbay + "' and MaHangVe='" + mahangve + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        soghedat = (int)reader[0];
                        soghetrong = (int)reader[1];
                    }
                    reader.Close();
                }



                if (soghetrong >= 1)
                {


                    //insert vào khách hàng
                    string mahk = LoadAutoMaHANHKHACH();
                    string query_insertHANHKHACH = "insert into HANHKHACH values (@value1,@value2,@value3,@value4)";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query_insertHANHKHACH, connection);
                        command.Parameters.AddWithValue("@value1", mahk);
                        command.Parameters.AddWithValue("@value2", TenHanhKhach);
                        command.Parameters.AddWithValue("@value3", CMND);
                        command.Parameters.AddWithValue("@value4", dienthoai);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Insert HANHKHACH successful!");
                        }
                    }

                    //insert dữ liệu vào vé
                    string query_insertVe = "insert into VECHUYENBAY values (@value1,@value2,@value3,@value4,@value5)";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query_insertVe, connection);
                        command.Parameters.AddWithValue("@value1", LoadAutoMaVE());
                        command.Parameters.AddWithValue("@value2", machuyenbay);
                        command.Parameters.AddWithValue("@value3", mahangve);
                        command.Parameters.AddWithValue("@value4", mahk);
                        command.Parameters.AddWithValue("@value5", giatien);
                        int rowsAffected1 = command.ExecuteNonQuery();
                        if (rowsAffected1 > 0)
                        {
                            MessageBox.Show("Insert VE successful!");
                        }
                    }

                    // Giảm số lượng vé trống, tăng số lượng vé đã đặt
                    soghetrong = soghetrong - 1;
                    soghedat = soghedat + 1;

                    //Update Tinh Trang Ve

                    string query_UpdateTTVE = "UPDATE TINHTRANGVE SET SoGheTrong = @GiaTri1, SoGheDat = @GiaTri2 WHERE MaChuyenBay='" + machuyenbay + "' and MaHangVe='" + mahangve + "'";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query_UpdateTTVE, connection);
                        command.Parameters.AddWithValue("@GiaTri1", soghetrong);
                        command.Parameters.AddWithValue("@GiaTri2", soghedat);
                        int rowsAffected2 = command.ExecuteNonQuery();
                        if (rowsAffected2 > 0)
                        {
                            MessageBox.Show("Update TTVe successful!");
                        }
                    }

                }
                else { MessageBox.Show("Chuyến bay hết chỗ!"); }

            }
            loadDSCHUYENBAY();
            HanhKhach_Load();

        }




        //code dat cho

        private void button_xemCBDatcho_Click(object sender, EventArgs e)
        {
            string masanbayden = "";
            string masanbaydi = "";
            string masanbaydicombobox = comboBox_sanbaydiDatcho.Text;
            string masanbaydencombobox = comboBox_sanbaydenDatcho.Text;
            string query = "select MaSanBay from sanbay where TenSanBay='" + masanbaydicombobox + "'";
            string query1 = "select MaSanBay from sanbay where TenSanBay='" + masanbaydencombobox + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    masanbaydi = (string)reader[0];
                }
                reader.Close();
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query1, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    masanbayden = (string)reader[0];
                }
                reader.Close();
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();
                string query_danhsachCB = "select chuyenbay.MACHUYENBAY,NgayGio, ThoiGianBay,MaHangVe,SoGheTrong,SoGheDat from CHUYENBAY,TUYENBAY,TINHTRANGVE\r\nwhere\r\nCHUYENBAY.MaChuyenBay=TINHTRANGVE.MaChuyenBay and\r\nCHUYENBAY.MaTuyenBay=TUYENBAY.MaTuyenBay and\r\nCHUYENBAY.MaChuyenBay=TINHTRANGVE.MaChuyenBay and\r\nSanBayDi='" + masanbaydi + "' and SanBayDen ='" + masanbayden + "'";
                SqlCommand command = new SqlCommand(query_danhsachCB, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView_DSchuyenbayDatCho.DataSource = dataTable;
            }
        }



        private void radioButton_hangve1Datcho_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton_hangve1Datcho.Checked)
            {

                radioButton_hangve2Datcho.Checked = false;
                double dongia = 0; ;
                string machuyenbay = textBox_machuyenbayDatcho.Text;
                string query1 = "select DonGia from DONGIA where MaChuyenBay='" + machuyenbay + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query1, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        dongia = (double)reader[0];
                    }
                    reader.Close();
                }

                textBox_giatienDatcho.Text = dongia.ToString();

            }
        }

        private void radioButton_hangve2Datcho_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton_hangve2Datcho.Checked)
            {
                radioButton_hangve1Datcho.Checked = false;
                double dongia = 0; ;
                string machuyenbay = textBox_machuyenbayDatcho.Text;

                string query1 = "select DonGia from DONGIA where MaChuyenBay='" + machuyenbay + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query1, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        dongia = (double)reader[0];
                    }
                    reader.Close();
                }
                dongia = dongia * 105 / 100;
                textBox_giatienDatcho.Text = dongia.ToString();
            }
        }
        string LoadAutoMaPhieuDatCho()
        {
            string PhieuDat = "";
            string chuoimax = "";
            int i = 0;
            string query = "select count(MaPhieuDat) from PHIEUDATCHO";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    i = (int)reader[0];

                }
                reader.Close();
            }

            if (i == 0)
            {
                PhieuDat = "PD00" + 1;
            }
            else
            {
                string query1 = "select MAX(RIGHT(MaPhieuDat, 3)) from PHIEUDATCHO";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query1, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        chuoimax = (string)reader[0];

                    }
                    reader.Close();
                }
                string numberstring = new string(chuoimax.Where(char.IsDigit).ToArray());
                int j = int.Parse(numberstring);
                if (j >= 0 && j < 9)
                {
                    j = j + 1;
                    PhieuDat = "PD00" + j;
                }
                else if (j >= 9)
                {
                    j = j + 1;
                    PhieuDat = "PD0" + j;
                }
            }

            return PhieuDat;
        }

        private void button_Datcho_Click(object sender, EventArgs e)
        {


            string machuyenbay = textBox_machuyenbayDatcho.Text;
            string TenHanhKhach = textBox_tenhanhkhachDatcho.Text;
            string CMND = textBox_CMNĐatcho.Text;
            string dienthoai = textBox_dienthoaiDatCho.Text;
            double giatien = double.Parse(textBox_giatienDatcho.Text);
            DateTime ngaygioDatcho = dateTimePicker_NgayDatCho.Value;
            string mahangve = "";

            string errorMessage = "";
            if (string.IsNullOrEmpty(textBox_machuyenbayDatcho.Text) || string.IsNullOrEmpty(textBox_tenhanhkhachDatcho.Text) ||
                string.IsNullOrEmpty(textBox_CMNĐatcho.Text) || string.IsNullOrEmpty(textBox_dienthoaiDatCho.Text) ||
                string.IsNullOrEmpty(textBox_giatienDatcho.Text))
            {
                errorMessage = "Có thuộc tính không hợp lệ." + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                // Hiển thị thông báo lỗi cho người dùng
                MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {




                if (radioButton_hangve1Datcho.Checked)
                {
                    mahangve = "HV1";
                }
                else { mahangve = "HV2"; }

                DateTime dt_cb = LoadNgayBayCuaChuyenBay(machuyenbay);
                TimeSpan duration = dt_cb - ngaygioDatcho;
                int totaldays = (int)duration.TotalDays;
                if (totaldays > TGDUNG_CHAMNHATKHIDATVE)
                {
                    // lay so luong ghe dat, so luong ghe trong

                    int soghedat = 0;
                    int soghetrong = 0;

                    string query = "select SoGheDat, SoGheTrong from TINHTRANGVE where machuyenbay='" + machuyenbay + "' and MaHangVe='" + mahangve + "'";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            soghedat = (int)reader[0];
                            soghetrong = (int)reader[1];
                        }
                        reader.Close();
                    }



                    if (soghetrong >= 1)
                    {


                        //insert vào khách hàng
                        string mahk = LoadAutoMaHANHKHACH();
                        string query_insertHANHKHACH = "insert into HANHKHACH values (@value1,@value2,@value3,@value4)";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(query_insertHANHKHACH, connection);
                            command.Parameters.AddWithValue("@value1", mahk);
                            command.Parameters.AddWithValue("@value2", TenHanhKhach);
                            command.Parameters.AddWithValue("@value3", CMND);
                            command.Parameters.AddWithValue("@value4", dienthoai);
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Insert HANHKHACH successful!");
                            }
                        }

                        //insert dữ liệu vào vé
                        string query_insertVe = "insert into PHIEUDATCHO values (@value1,@value2,@value3,@value4,@value5,@value6)";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(query_insertVe, connection);
                            command.Parameters.AddWithValue("@value1", LoadAutoMaPhieuDatCho());
                            command.Parameters.AddWithValue("@value2", machuyenbay);
                            command.Parameters.AddWithValue("@value3", mahk);
                            command.Parameters.AddWithValue("@value4", mahangve);
                            command.Parameters.AddWithValue("@value5", giatien);
                            command.Parameters.AddWithValue("@value6", ngaygioDatcho);
                            int rowsAffected1 = command.ExecuteNonQuery();
                            if (rowsAffected1 > 0)
                            {
                                MessageBox.Show("Insert DATCHO successful!");
                            }
                        }

                        // Giảm số lượng vé trống, tăng số lượng vé đã đặt
                        soghetrong = soghetrong - 1;
                        soghedat = soghedat + 1;

                        //Update Tinh Trang Ve

                        string query_UpdateTTVE = "UPDATE TINHTRANGVE SET SoGheTrong = @GiaTri1, SoGheDat = @GiaTri2 WHERE MaChuyenBay='" + machuyenbay + "' and MaHangVe='" + mahangve + "'";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(query_UpdateTTVE, connection);
                            command.Parameters.AddWithValue("@GiaTri1", soghetrong);
                            command.Parameters.AddWithValue("@GiaTri2", soghedat);
                            int rowsAffected2 = command.ExecuteNonQuery();
                            if (rowsAffected2 > 0)
                            {
                                MessageBox.Show("Update TTVe successful!");
                            }
                        }

                    }
                    else { MessageBox.Show("Chuyến bay hết chỗ!"); }
                }
                else { MessageBox.Show("KHÔNG THÕA MÃN SỐ NGÀY ĐẶT QUI ĐỊNH "); }

            }

            loadDSCHUYENBAY();
            HanhKhach_Load();
            loadPhieudatcho();
        }



        // cac chuc nang


        //Code tiep nhan lich chuyen bay

        void TiepNhanLichChuyenBay()
        {

            LoadAutoMaCB();
            LoadMaTuyenbay();
            LoadSanBayTrungGian();
            loadCB();
            loadChiTietChuyenBay_SanBaytrunggian();
            loadbangSANBAYTRUNGGIAN();
        }
        void Datve_Datcho()
        {
            loadDSCHUYENBAY();
        }












        //code san bay
        void QuanLySanBay()
        {

            dataGridView_sanbay.DataSource = ListSanBay;
            Sanbay_loadsanbay();
            SanBayBinding();



        }

        void Sanbay_loadsanbay()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();
                string query = "SELECT * FROM SANBAY";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                ListSanBay.DataSource = dataTable;
            }

        }

        void SanBayBinding()
        {

            textBox_masanbay.DataBindings.Add(new Binding("Text", dataGridView_sanbay.DataSource, "masanbay", true, DataSourceUpdateMode.Never));
            textBox_tensanbay.DataBindings.Add(new Binding("Text", dataGridView_sanbay.DataSource, "tensanbay", true, DataSourceUpdateMode.Never));

        }



        private void button_themSB_Click(object sender, EventArgs e)

        {
            string masanbay = textBox_masanbay.Text;
            string tensanbay = textBox_tensanbay.Text;
            string query = "insert into SanBay values (@value1,@value2)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@value1", masanbay);
                command.Parameters.AddWithValue("@value2", tensanbay);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Insert SANBAY successful!");
                }
                else
                {
                    MessageBox.Show("ERROR!");
                }
            }
            Sanbay_loadsanbay();
            LoadSanBay();



        }

        private void button_xoaSB_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string masanbay = textBox_masanbay.Text;
                string query = "delete from sanbay where masanbay='" + masanbay + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Delete SANBAY successful!");
                    }
                    else
                    {
                        MessageBox.Show("ERROR!");
                    }
                }

            }
            Sanbay_loadsanbay();
            LoadSanBay();

        }

        private void button_suaSB_Click(object sender, EventArgs e)
        {
            string masanbay = textBox_masanbay.Text;
            string tensanbay = textBox_tensanbay.Text;
            string query = "update sanbay set TenSanBay='" + tensanbay + "' where MaSanBay='" + masanbay + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("UpdateSANBAY successful!");
                }
                else
                {
                    MessageBox.Show("ERROR!");
                }
            }
            Sanbay_loadsanbay();
            LoadSanBay();


        }

        //code tuyen bay

        void QuanLyTuyenBay()
        {
            dataGridView_LoadTuyenBay.DataSource = ListTuyenBay;
            Tuyenbay_loadTuyenBay();
            TuyenBayBinding();

        }
        void Tuyenbay_loadTuyenBay()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();
                string query = "SELECT * FROM TUYENBAY";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                ListTuyenBay.DataSource = dataTable;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();
                string query = "SELECT * FROM SANBAY";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView_loadSanbaycuaTuyenBay.DataSource = dataTable;
            }
        }

        void TuyenBayBinding()
        {

            textBox__matuyenbay.DataBindings.Add(new Binding("Text", dataGridView_LoadTuyenBay.DataSource, "matuyenbay", true, DataSourceUpdateMode.Never));
            textBox_sanbaydiTuyenbay.DataBindings.Add(new Binding("Text", dataGridView_LoadTuyenBay.DataSource, "SanBayDi", true, DataSourceUpdateMode.Never));
            textBox_sanbaydenTuyenbay.DataBindings.Add(new Binding("Text", dataGridView_LoadTuyenBay.DataSource, "SanBayDen", true, DataSourceUpdateMode.Never));

        }

        private void button_themTuyenBay_Click(object sender, EventArgs e)
        {

            string sanbaydi = textBox_sanbaydiTuyenbay.Text;
            string sanbayden = textBox_sanbaydenTuyenbay.Text;
            string query = "insert into TUYENBAY values (@value1,@value2,@value3)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@value1", LoadAutoMatuyenbay());
                command.Parameters.AddWithValue("@value2", sanbaydi);
                command.Parameters.AddWithValue("@value3", sanbayden);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Insert TUYENBAY successful!");
                }
                else
                {
                    MessageBox.Show("ERROR!");
                }
            }
            Tuyenbay_loadTuyenBay();
            LoadMaTuyenbay();
        }

        private void button_XoaTuyenBay_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string matuyenbay = textBox__matuyenbay.Text;
                string query = "delete from TUYENBAY where matuyenbay='" + matuyenbay + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Delete TUYENBAY successful!");
                    }
                    else
                    {
                        MessageBox.Show("ERROR!");
                    }
                }
                Tuyenbay_loadTuyenBay();
                LoadMaTuyenbay();
            }

        }

        private void button_suaTuyenBay_Click(object sender, EventArgs e)
        {
            string matuyenbay = textBox__matuyenbay.Text;
            string sanbaydi = textBox_sanbaydiTuyenbay.Text;
            string sanbayden = textBox_sanbaydenTuyenbay.Text;
            string query = "update TUYENBAY set sanbaydi='" + sanbaydi + "' , sanbayden='" + sanbayden + "' where matuyenbay='" + matuyenbay + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Update TUYENBAY successful!");
                }
                else
                {
                    MessageBox.Show("ERROR!");
                }
            }
            Tuyenbay_loadTuyenBay();
            LoadMaTuyenbay();
        }

        // code hanh khach

        void QuanLyHanKhach()
        {
            dataGridView_loadHanhKhach.DataSource = ListHanhKhach;
            HanhKhach_Load();
            HANHKHACHBinding();

        }

        void HanhKhach_Load()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();
                string query = "SELECT * FROM HANHKHACH";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                ListHanhKhach.DataSource = dataTable;
            }

        }

        void HANHKHACHBinding()
        {

            textBox_infomahanhkhach.DataBindings.Add(new Binding("Text", dataGridView_loadHanhKhach.DataSource, "mahanhkhach", true, DataSourceUpdateMode.Never));
            textBox_infotenhanhkhach.DataBindings.Add(new Binding("Text", dataGridView_loadHanhKhach.DataSource, "tenhanhkhach", true, DataSourceUpdateMode.Never));
            textBox_infoCMND.DataBindings.Add(new Binding("Text", dataGridView_loadHanhKhach.DataSource, "cmnd", true, DataSourceUpdateMode.Never));
            textBox_infodienthoai.DataBindings.Add(new Binding("Text", dataGridView_loadHanhKhach.DataSource, "dienthoai", true, DataSourceUpdateMode.Never));


        }

        private void button_suahanhkhach_Click(object sender, EventArgs e)
        {
            string mahanhkhach = textBox_infomahanhkhach.Text;
            string tenhanhkhach = textBox_infotenhanhkhach.Text;
            string CMND = textBox_infoCMND.Text;
            string dienthoai = textBox_infodienthoai.Text;
            string query = "update hanhkhach set TenHanhKhach='" + tenhanhkhach + "', CMND='" + CMND + "', dienthoai='" + dienthoai + "' where MaHanhKhach	='" + mahanhkhach + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Update HANHKHACH successful!");
                }
                else
                {
                    MessageBox.Show("ERROR!");
                }
            }

            HanhKhach_Load();

        }


        //button xoa hanh khach

        private void button_XoaHanhkhach_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                string mahanhkhach = textBox_infomahanhkhach.Text;
                // xoa khach hang trong ve va dat cho neu co

                string query = "delete from VECHUYENBAY where mahanhkhach='" + mahanhkhach + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    int rowsAffected = command.ExecuteNonQuery();

                }


                string query1 = "delete from PHIEUDATCHO where mahanhkhach='" + mahanhkhach + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query1, connection);
                    int rowsAffected1 = command.ExecuteNonQuery();

                }


                string query2 = "delete from HANHKHACH where mahanhkhach='" + mahanhkhach + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query2, connection);
                    int rowsAffected2 = command.ExecuteNonQuery();
                    if (rowsAffected2 > 0)
                    {
                        MessageBox.Show("Delete HANHKHACH successful!");
                    }
                    else
                    {
                        MessageBox.Show("ERROR!");
                    }
                }

            }

            HanhKhach_Load();
        }

        // code chuyen bay


        void QuanLyCB()
        {
            dataGridView_CB_loadcb.DataSource = ListChuyenBay;
            ChuyenBay_Load();
            CHUYENBAYBinding();
            CB_LoadTTV();


        }


        void ChuyenBay_Load()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();
                string query = "SELECT * FROM CHUYENBAY";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                ListChuyenBay.DataSource = dataTable;
            }

        }

        void CHUYENBAYBinding()
        {
            textBox_CB_machuyenbay.DataBindings.Add(new Binding("Text", dataGridView_CB_loadcb.DataSource, "machuyenbay", true, DataSourceUpdateMode.Never));
            textBox_CB_matuyenbay.DataBindings.Add(new Binding("Text", dataGridView_CB_loadcb.DataSource, "matuyenbay", true, DataSourceUpdateMode.Never));
            dateTimePicker_CB_ngaygio.DataBindings.Add(new Binding("Value", dataGridView_CB_loadcb.DataSource, "ngaygio", true, DataSourceUpdateMode.Never));
            textBox_CB_thoigianbay.DataBindings.Add(new Binding("Text", dataGridView_CB_loadcb.DataSource, "thoigianbay", true, DataSourceUpdateMode.Never));
            textBox_CB_ghehang1.DataBindings.Add(new Binding("Text", dataGridView_CB_loadcb.DataSource, "soluongghehang1", true, DataSourceUpdateMode.Never));
            textBox_CB_ghehang2.DataBindings.Add(new Binding("Text", dataGridView_CB_loadcb.DataSource, "soluongghehang2", true, DataSourceUpdateMode.Never));

        }


        void CB_LoadTTV()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();
                string query = "SELECT * FROM TINHTRANGVE";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView_CB_loadTTV.DataSource = dataTable;
            }

        }

        private void button_CB_xoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                string machuyenbay = textBox_CB_machuyenbay.Text;
                // 


                string query = "delete from TINHTRANGVE where machuyenbay='" + machuyenbay + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    int rowsAffected = command.ExecuteNonQuery();

                }


                string query1 = "delete from VECHUYENBAY where machuyenbay='" + machuyenbay + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query1, connection);
                    int rowsAffected1 = command.ExecuteNonQuery();


                }

                string query2 = "delete from DONGIA where machuyenbay='" + machuyenbay + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query2, connection);
                    int rowsAffected2 = command.ExecuteNonQuery();


                }

                string query3 = "delete from PHIEUDATCHO where machuyenbay='" + machuyenbay + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query3, connection);
                    int rowsAffected3 = command.ExecuteNonQuery();


                }

                string query4 = "delete from CHUYENBAY_SANBAYTRUNGGIAN where machuyenbay='" + machuyenbay + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query4, connection);
                    int rowsAffected4 = command.ExecuteNonQuery();

                }




                string query5 = "delete from CHUYENBAY where machuyenbay='" + machuyenbay + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query5, connection);
                    int rowsAffected5 = command.ExecuteNonQuery();
                    if (rowsAffected5 > 0)
                    {
                        MessageBox.Show("Delete CHUYENBAY successful!");
                    }
                    else
                    {
                        MessageBox.Show("ERROR!");
                    }
                }

            }

            ChuyenBay_Load();
            CB_LoadTTV();
            loadCB();
            loadChiTietChuyenBay_SanBaytrunggian();
            loadPhieudatcho();
            loadDSCHUYENBAY();

        }

        private void button_CB_sua_Click(object sender, EventArgs e)
        {

            string machuyenbay = textBox_CB_machuyenbay.Text;
            string matuyenbay = textBox_CB_matuyenbay.Text;
            DateTime ngaygio = dateTimePicker_CB_ngaygio.Value;
            int thoigianbay = int.Parse(textBox_CB_thoigianbay.Text);
            int ghehang1 = int.Parse(textBox_CB_ghehang1.Text);
            int ghehang2 = int.Parse(textBox_CB_ghehang2.Text);

            if (thoigianbay > TGBAY_TOITHIEU)
            {
                string query = "update CHUYENBAY set MaTuyenBay='" + matuyenbay + "',NgayGio='" + ngaygio + "', ThoiGianBay=" + thoigianbay + ", SoLuongGheHang1=" + ghehang1 + ",SoLuongGheHang2=" + ghehang2 + " where MaChuyenBay='" + machuyenbay + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Update CHUYENBAY successful!");
                    }
                    else
                    {
                        MessageBox.Show("ERROR!");
                    }
                }

                // update ve cua TINHTRANGVE 
                //HV1
                string query1 = "update tinhtrangve set soghetrong=" + ghehang1 + " where machuyenbay='" + machuyenbay + "' and MaHangve='HV1'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query1, connection);
                    int rowsAffected1 = command.ExecuteNonQuery();
                    if (rowsAffected1 > 0)
                    {
                        MessageBox.Show("Update TTV HV1 successful!");
                    }
                    else
                    {
                        MessageBox.Show("ERROR!");
                    }
                }


                //HV2

                string query2 = "update tinhtrangve set soghetrong=" + ghehang2 + " where machuyenbay='" + machuyenbay + "' and MaHangve='HV2'";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query2, connection);
                    int rowsAffected2 = command.ExecuteNonQuery();
                    if (rowsAffected2 > 0)
                    {
                        MessageBox.Show("Update TTV HV2 successful!");
                    }
                    else
                    {
                        MessageBox.Show("ERROR!");
                    }
                }

            }
            else { MessageBox.Show("THOI GIAN BAY TOI THIEU KHONG THOA MAN QUY DINH"); }
            ChuyenBay_Load();
            CB_LoadTTV();
            loadCB();
            loadChiTietChuyenBay_SanBaytrunggian();
            loadPhieudatcho();
            loadDSCHUYENBAY();
        }

        //code san bay trung gian

        void QuanLySBTG()
        {
            dataGridView_SBTG_loadSBTG.DataSource = ListSanBayTrungGian;
            SBTGBinding();
            SBTG_Load();

        }

        void SBTG_Load()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();
                string query = "SELECT * FROM SANBAYTRUNGGIAN";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                ListSanBayTrungGian.DataSource = dataTable;
            }

        }
        void SBTGBinding()
        {
            textBox_SBTG_maSBTG.DataBindings.Add(new Binding("Text", dataGridView_SBTG_loadSBTG.DataSource, "masanbaytrunggian", true, DataSourceUpdateMode.Never));
            textBox_SBTG_tenSBTG.DataBindings.Add(new Binding("Text", dataGridView_SBTG_loadSBTG.DataSource, "tensanbay", true, DataSourceUpdateMode.Never));
            textBox_SBTG_TGDung.DataBindings.Add(new Binding("Text", dataGridView_SBTG_loadSBTG.DataSource, "thoigiandung", true, DataSourceUpdateMode.Never));
            textBox_SBTG_Ghichu.DataBindings.Add(new Binding("Text", dataGridView_SBTG_loadSBTG.DataSource, "ghichu", true, DataSourceUpdateMode.Never));

        }


        private void button_SBTG_them_Click(object sender, EventArgs e)
        {
            string masanbayTG = textBox_SBTG_maSBTG.Text;
            string tensanbayTG = textBox_SBTG_tenSBTG.Text;
            int thoigiandung = int.Parse(textBox_SBTG_TGDung.Text);
            string ghichu = textBox_SBTG_Ghichu.Text;
            if (thoigiandung > TGDUNG_TOITHIEU && thoigiandung < TGDUNG_TOIDA)
            {

                string query = "insert into SANBAYTRUNGGIAN values (@value1,@value2,@value3,@value4)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@value1", masanbayTG);
                    command.Parameters.AddWithValue("@value2", tensanbayTG);
                    command.Parameters.AddWithValue("@value3", thoigiandung);
                    command.Parameters.AddWithValue("@value4", ghichu);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Insert SANBAYTG successful!");
                    }
                    else
                    {
                        MessageBox.Show("ERROR!");
                    }
                }
            }
            else { MessageBox.Show("THỜI GIAN DỪNG KHÔNG THÕA MÃN QUY ĐỊNH!"); }
            SBTG_Load();
        }

        private void button_SBTG_xoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string masanbayTG = textBox_SBTG_maSBTG.Text;
                string query = "delete from SANBAYTRUNGGIAN where masanbaytrunggian='" + masanbayTG + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Delete SANBAYTG successful!");
                    }
                    else
                    {
                        MessageBox.Show("ERROR!");
                    }
                }
            }
            SBTG_Load();
        }

        private void button_SBTG_sua_Click(object sender, EventArgs e)
        {
            string masanbayTG = textBox_SBTG_maSBTG.Text;
            string tensanbayTG = textBox_SBTG_tenSBTG.Text;
            int thoigiandung = int.Parse(textBox_SBTG_TGDung.Text);
            string ghichu = textBox_SBTG_Ghichu.Text;
            if (thoigiandung > TGDUNG_TOITHIEU && thoigiandung < TGDUNG_TOIDA)
            {
                string query = "update SANBAYTRUNGGIAN set TenSanBay='" + tensanbayTG + "', thoigiandung=" + thoigiandung + ", ghichu='" + ghichu + "' where masanbaytrunggian='" + masanbayTG + "'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Update SANBAYTG successful!");
                    }
                    else
                    {
                        MessageBox.Show("ERROR!");
                    }
                }
            }
            else { MessageBox.Show("THỜI GIAN DỪNG KHÔNG THÕA MÃN QUY ĐỊNH!"); }
            SBTG_Load();
        }


        void loadQuiDinh()
        {

            int a = 0, b = 0, c = 0, d = 0, e = 0;
            string query = "select * from THAMSO";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    a = (int)reader[0];
                    b = (int)reader[1];
                    c = (int)reader[2];
                    d = (int)reader[3];
                    e = (int)reader[4];

                }
                reader.Close();
            }

            textbox_thoigianbaytoithieu.Text = "" + a;
            textBox_tgdungtoithieuSBTG.Text = "" + b;
            textBox_tgdungtoidaSBTG.Text = "" + c;
            textBox_TGchamnhatkhidatve.Text = "" + d;
            textBox_thoigianhuyve.Text = "" + e;


            TGBAY_TOITHIEU = a;
            TGDUNG_TOITHIEU = b;
            TGDUNG_TOIDA = c;
            TGDUNG_CHAMNHATKHIDATVE = d;
            TGCHAMNHATKHIHUYVE = e;

        }





        private void fQuanly_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
                Application.Exit();
            }
        }

        private void button_capnhatQUIDINH_Click(object sender, EventArgs e)
        {
            int a = int.Parse(textbox_thoigianbaytoithieu.Text);
            int b = int.Parse(textBox_tgdungtoithieuSBTG.Text);
            int c = int.Parse(textBox_tgdungtoidaSBTG.Text);
            int d = int.Parse(textBox_TGchamnhatkhidatve.Text);
            int f = int.Parse(textBox_thoigianhuyve.Text);

            string query = "Update THAMSO SET TGBayToiThieu=" + a + ", TGDungToiThieu=" + b + ", TGDungToiDa=" + c + ",TGChamNhatDatVe=" + d + ",TGChamNhatHuyVe=" + f + "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Update THAMSO successful!");
                }
                else
                {
                    MessageBox.Show("ERROR!");
                }
            }
            loadQuiDinh();

        }

        //ham lay ngay so sanh
        DateTime LoadNgayBayCuaChuyenBay(string macb)
        {
            DateTime dt = new DateTime();

            string query = "select NgayGio from CHUYENBAY where machuyenbay='" + macb + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    dt = (DateTime)reader[0];

                }
                reader.Close();
            }
            return dt;
        }



        void HuyPhieuDatCho()
        {

            dataGridView_huyve_loaddatcho.DataSource = ListDatcho;
            Datchobinding();
            loadPhieudatcho();
        }

        void Datchobinding()
        {
            textBox_Huyve_maphieudat.DataBindings.Add(new Binding("Text", dataGridView_huyve_loaddatcho.DataSource, "maphieudat", true, DataSourceUpdateMode.Never));
            textBox_HUYVE_macb.DataBindings.Add(new Binding("Text", dataGridView_huyve_loaddatcho.DataSource, "machuyenbay", true, DataSourceUpdateMode.Never));
            textBox_Huyve_mahangve.DataBindings.Add(new Binding("Text", dataGridView_huyve_loaddatcho.DataSource, "mahanhkhach", true, DataSourceUpdateMode.Never));
            textBox_pdc_mahanhkhach.DataBindings.Add(new Binding("Text", dataGridView_huyve_loaddatcho.DataSource, "mahangve", true, DataSourceUpdateMode.Never));
            textBox_huyve_giatien.DataBindings.Add(new Binding("Text", dataGridView_huyve_loaddatcho.DataSource, "giatien", true, DataSourceUpdateMode.Never));
            dateTimePicker_huyve_ngaydatcho.DataBindings.Add(new Binding("Value", dataGridView_huyve_loaddatcho.DataSource, "ngaydat", true, DataSourceUpdateMode.Never));

        }

        void loadPhieudatcho()
        {
            loadQuiDinh();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();
                string query = "SELECT * FROM PHIEUDATCHO";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                ListDatcho.DataSource = dataTable;
            }

        }

        private void button_huyve_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                string machuyenbay = textBox_HUYVE_macb.Text;
                string mahanhkhach = textBox_pdc_mahanhkhach.Text;



                DateTime dt_cb = LoadNgayBayCuaChuyenBay(machuyenbay);
                TimeSpan duration = dt_cb - DateTime.Now;
                int totaldays = (int)duration.TotalDays;
                if (totaldays > TGCHAMNHATKHIHUYVE)
                {

                    string query5 = "delete from PHIEUDATCHO where machuyenbay='" + machuyenbay + "'";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query5, connection);
                        int rowsAffected5 = command.ExecuteNonQuery();
                        if (rowsAffected5 > 0)
                        {
                            MessageBox.Show("Delete CHUYENBAY successful!");
                        }
                        else
                        {
                            MessageBox.Show("ERROR!");
                        }
                    }




                    string query2 = "delete from HANHKHACH where mahanhkhach='" + mahanhkhach + "'";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query2, connection);
                        int rowsAffected2 = command.ExecuteNonQuery();
                        if (rowsAffected2 > 0)
                        {
                            MessageBox.Show("Delete HANHKHACH successful!");
                        }
                        else
                        {
                            MessageBox.Show("ERROR!");
                        }

                    }
                }
                else { MessageBox.Show("KHÔNG THÕA MÃN QUI ĐỊNH VỀ THỜI GIAN HỦY VÉ"); }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Show();
            Application.Exit();
        }

        private void tabControl_tiepnhan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {
        }

        private void label21_Click(object sender, EventArgs e)
        {
        }

        private void label22_Click(object sender, EventArgs e)
        {
        }

        private void label20_Click(object sender, EventArgs e)
        {
        }

        private void label19_Click(object sender, EventArgs e)
        {
        }

        private void label18_Click(object sender, EventArgs e)
        {
        }
    }
}
