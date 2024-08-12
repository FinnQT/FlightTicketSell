using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public partial class QuanlyUse : Form
    {
        public QuanlyUse()
        {
            InitializeComponent();
            LoadDulieu();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private string connectionString = "Data Source=DESKTOP-UOP6PLS\\MSSQLSERVER01;Initial Catalog=QLMB;Integrated Security=True";

        void LoadDulieu()
        {


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();
                string query = "SELECT ROW_NUMBER() OVER (ORDER BY MaChuyenBay) AS STT,\r\n    MaChuyenBay AS 'Chuyến Bay',\r\n    COUNT(*) AS 'Số Vé',\r\n    SUM(GiaTien) AS 'Doanh Thu',\r\n    (SUM(GiaTien) / (SELECT SUM(GiaTien) FROM VECHUYENBAY)) * 100 AS 'Tỉ Lệ'\r\nFROM VECHUYENBAY\r\nGROUP BY MaChuyenBay;";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView_DTVE.DataSource = dataTable;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();
                string query = "SELECT ROW_NUMBER() OVER (ORDER BY MaChuyenBay) AS STT,\r\n    MaChuyenBay AS 'Chuyến Bay',\r\n    COUNT(*) AS 'Số Vé',\r\n    SUM(GiaTien) AS 'Doanh Thu',\r\n    (SUM(GiaTien) / (SELECT SUM(GiaTien) FROM PHIEUDATCHO)) * 100 AS 'Tỉ Lệ'\r\nFROM PHIEUDATCHO\r\nGROUP BY MaChuyenBay;";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView_DTPhieuDat.DataSource = dataTable;
            }


        }


        private void button_DTTHANG_Click_1(object sender, EventArgs e)
        {
            string thang = textBox_thang.Text;

            double a = 0, b = 0;

            string query = "SELECT\r\n    ROW_NUMBER() OVER (ORDER BY cb.MaChuyenBay) AS STT,\r\n    MONTH(cb.NgayGio) AS 'Tháng',\r\n    cb.MaChuyenBay AS 'Chuyến Bay',\r\n    COUNT(v.MaVe) AS 'Số Vé',\r\n    SUM(v.GiaTien) AS 'Doanh Thu',\r\n    (SUM(v.GiaTien) / (SELECT SUM(GiaTien) FROM VeChuyenBay WHERE MONTH(NgayGio) = '" + thang + "')) * 100 AS 'Tỉ Lệ'\r\nFROM ChuyenBay cb\r\nLEFT JOIN VeChuyenBay v ON cb.MaChuyenBay = v.MaChuyenBay\r\nWHERE MONTH(cb.NgayGio) = '" + thang + "'\r\nGROUP BY MONTH(cb.NgayGio), cb.MaChuyenBay";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    double i = 0;
                    i = (double)reader[4];
                    a = a + i;

                }
                reader.Close();
            }



            string query1 = "SELECT\r\n    ROW_NUMBER() OVER (ORDER BY cb.MaChuyenBay) AS STT,\r\n    MONTH(cb.NgayGio) AS 'Tháng',\r\n    cb.MaChuyenBay AS 'Chuyến Bay',\r\n    COUNT(p.MaPhieuDat) AS 'Số Vé',\r\n    SUM(p.GiaTien) AS 'Doanh Thu',\r\n    (SUM(p.GiaTien) / (SELECT SUM(GiaTien) FROM PHIEUDATCHO WHERE MONTH(NgayGio) = '" + thang + "')) * 100 AS 'Tỉ Lệ'\r\nFROM ChuyenBay cb\r\nLEFT JOIN PHIEUDATCHO p ON cb.MaChuyenBay = p.MaChuyenBay\r\nWHERE MONTH(cb.NgayGio) = '" + thang + "'\r\nGROUP BY MONTH(cb.NgayGio), cb.MaChuyenBay";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query1, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    double i = 0;
                    i = (double)reader[4];
                    b = b + i;

                }
                reader.Close();
            }

            double tong = a + b;

            textBox_TONGDOANTHU.Text = tong.ToString();
        }

        private void button_DTNAM_Click_1(object sender, EventArgs e)
        {
            double a = 0, b = 0;
            string query = "select sum(giatien) from VECHUYENBAY";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    a = (double)reader[0];

                }
                reader.Close();
            }

            string query1 = "select sum(giatien) from PHIEUDATCHO";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query1, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    b = (double)reader[0];

                }
                reader.Close();
            }


            double tong = a + b;
            textBox_TONGDOANTHU.Text = tong.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Document document = new Document();

            // Mở hộp thoại Lưu tệp để người dùng chọn vị trí và tên tệp PDF
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files|*.pdf";
            saveFileDialog.Title = "Save as PDF";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Tạo một đối tượng PdfWriter để ghi dữ liệu vào tệp PDF
                using (PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create)))
                {
                    document.Open();



                    // Tạo bảng dữ liệu từ DataGridView
                    PdfPTable table = new PdfPTable(dataGridView_DTVE.Columns.Count);

                    // Thiết lập độ rộng cột của bảng
                    float[] columnWidths = new float[dataGridView_DTVE.Columns.Count];
                    for (int i = 0; i < dataGridView_DTVE.Columns.Count; i++)
                    {
                        columnWidths[i] = 1f; // Đặt độ rộng cột là 1f (tùy chỉnh theo nhu cầu)
                    }
                    table.SetWidths(columnWidths);

                    // Thêm tiêu đề cột vào bảng
                    for (int i = 0; i < dataGridView_DTVE.Columns.Count; i++)
                    {
                        table.AddCell(dataGridView_DTVE.Columns[i].HeaderText);
                    }

                    // Thêm dữ liệu từ DataGridView vào bảng
                    for (int i = 0; i < dataGridView_DTVE.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView_DTVE.Columns.Count; j++)
                        {
                            if (dataGridView_DTVE.Rows[i].Cells[j].Value != null)
                            {
                                table.AddCell(dataGridView_DTVE.Rows[i].Cells[j].Value.ToString());
                            }
                            else
                            {
                                table.AddCell(string.Empty);
                            }
                        }
                    }



                    PdfPTable table1 = new PdfPTable(dataGridView_DTPhieuDat.Columns.Count);

                    // Thiết lập độ rộng cột của bảng
                    float[] columnWidths1 = new float[dataGridView_DTPhieuDat.Columns.Count];
                    for (int i = 0; i < dataGridView_DTPhieuDat.Columns.Count; i++)
                    {
                        columnWidths[i] = 1f; // Đặt độ rộng cột là 1f (tùy chỉnh theo nhu cầu)
                    }
                    table1.SetWidths(columnWidths);

                    // Thêm tiêu đề cột vào bảng
                    for (int i = 0; i < dataGridView_DTPhieuDat.Columns.Count; i++)
                    {
                        table1.AddCell(dataGridView_DTPhieuDat.Columns[i].HeaderText);
                    }

                    // Thêm dữ liệu từ DataGridView vào bảng
                    for (int i = 0; i < dataGridView_DTPhieuDat.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView_DTPhieuDat.Columns.Count; j++)
                        {
                            if (dataGridView_DTPhieuDat.Rows[i].Cells[j].Value != null)
                            {
                                table1.AddCell(dataGridView_DTPhieuDat.Rows[i].Cells[j].Value.ToString());
                            }
                            else
                            {
                                table1.AddCell(string.Empty);
                            }
                        }
                    }

                    Paragraph paragraph = new Paragraph();
                    Paragraph paragraph1 = new Paragraph();
                    Paragraph paragraph2 = new Paragraph();

                    paragraph1.Add("DOANH THU");
                    paragraph2.Add("TONG TIEN :" + textBox_TONGDOANTHU.Text);
                    paragraph.Add(Environment.NewLine); // Xuống dòng
                    paragraph.Add(Environment.NewLine); // Xuống dòng
                    paragraph.Add(Environment.NewLine); // Xuống dòng
                    paragraph.Add(Environment.NewLine); // Xuống dòng


                    // Thêm đoạn văn bản vào tài liệu PDF


                    // Thêm bảng vào tài liệu PDF
                    document.Add(paragraph1);
                    document.Add(paragraph);
                    document.Add(table);
                    document.Add(paragraph);
                    document.Add(table1);
                    document.Add(paragraph);
                    document.Add(paragraph2);

                    document.Close();
                }

                MessageBox.Show("Export to PDF completed!");
            }
        }
    }
}
