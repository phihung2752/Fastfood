using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using COMExcel = Microsoft.Office.Interop.Excel;


namespace Fastfood
{
    public partial class frmHoaDon : Form
    {
        DataConnection dc = new DataConnection();
        SqlCommand cmd;
        SqlDataAdapter adapt;
        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            btnInHoaDon.Enabled = false;
            btnBoQua.Enabled = false;
            txtMaHD.ReadOnly = true;
            txtDienThoaiKH.Enabled = false;
            txtDiaChiKH.Enabled = false;
            txtThanhTien.ReadOnly = true;
            txtGia.Enabled = false;
            ResetValues();
            loadcomboboxKH();
            cboMaKH.SelectedIndex = -1;
            txtTenKH.SelectedIndex = -1;
            txtDiaChiKH.SelectedIndex = -1;
            txtDienThoaiKH.SelectedIndex = -1;
            cboMaKH.SelectedIndex = -1;
            loadcomboboxNV();
            cboMaNV.SelectedIndex = -1;
            txtTenNV.SelectedIndex = -1;
            loadcomboboxMA();
            cboMaMon.SelectedIndex = -1;
            txtTenMon.SelectedIndex = -1;
            txtGia.SelectedIndex = -1;
            DisplayData();
        }
        void loadcomboboxKH()
        {
            dc = new DataConnection();
            SqlConnection con = dc.GetConnection();
            con.Open();
            cmd = new SqlCommand("SELECT MaKH, TenKH, DienThoaiKH, DiaChiKH FROM KhachHang", con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Dispose();
            cboMaKH.DisplayMember = "MaKH";
            txtTenKH.DisplayMember = "TenKH";
            txtDienThoaiKH.DisplayMember = "DienThoaiKH";
            txtDiaChiKH.DisplayMember = "DiaChiKH";
            cboMaKH.DataSource = dt;
            txtTenKH.DataSource = dt;
            txtDienThoaiKH.DataSource = dt;
            txtDiaChiKH.DataSource = dt;
        }
        void loadcomboboxNV()
        {
            dc = new DataConnection();
            SqlConnection con = dc.GetConnection();
            con.Open();
            cmd = new SqlCommand("SELECT MaNV, TenNV FROM NhanVien", con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Dispose();
            cboMaNV.DisplayMember = "MaNV";
            txtTenNV.DisplayMember = "TenNV";
            cboMaNV.DataSource = dt;
            txtTenNV.DataSource = dt;
        }
        void loadcomboboxMA()
        {
            dc = new DataConnection();
            SqlConnection con = dc.GetConnection();
            con.Open();
            cmd = new SqlCommand("SELECT MaMon, TenMon, Gia FROM MonAn", con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Dispose();
            cboMaMon.DisplayMember = "MaMon";
            txtTenMon.DisplayMember = "TenMon";
            txtGia.DisplayMember = "Gia";
            cboMaMon.DataSource = dt;
            txtTenMon.DataSource = dt;
            txtGia.DataSource = dt;

        }
        private void DisplayData()
        {
            SqlConnection conn = dc.GetConnection();
            conn.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from ChiTietHD", conn);
            adapt.Fill(dt);
            dgvHoaDon.DataSource = dt;
            conn.Close();
            dgvHoaDon.Columns["NgayBan"].Visible = false;
            dgvHoaDon.Columns["MaNV"].Visible = false;
            dgvHoaDon.Columns["MaKH"].Visible = false;
            dgvHoaDon.AllowUserToAddRows = false;
            dgvHoaDon.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void ResetValuesHang()
        {
            cboMaMon.Text = "";
            txtSoLuong.Text = "";
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";
        }       
        private void ResetValues()
        {
            txtMaHD.Text = "";
            cboMaNV.Text = "";
            txtTenNV.Text = "";
            txtTenKH.Text = "";
            txtDiaChiKH.Text = "";
            txtDienThoaiKH.Text = "";
            txtTenMon.Text = "";
            txtGia.Text = "";
            cboMaKH.Text = "";
            cboMaMon.Text = "";
            txtSoLuong.Text = "";
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            SqlConnection conn = dc.GetConnection();
            if (txtMaHD.Text != "")
            {
                cmd = new SqlCommand("delete ChiTietHD where MaHD=@mahd", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@mahd", txtMaHD.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Xóa hóa đơn thành công");
                DisplayData();
                ResetValues();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã món ăn để xóa");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnInHoaDon.Enabled = false;
            btnThem.Enabled = false;
            btnBoQua.Enabled = true;
            ResetValues();
            txtMaHD.Text = Functions.CreateKey("HDB");
            DisplayData();
        }
        public void GetPay()
        {
            double sdpt = 0;
            double thanhtien = 0;
            double sl = Convert.ToDouble(txtSoLuong.Text);
            double dg = Convert.ToDouble(txtGia.Text);
            double pt = Convert.ToDouble(txtGiamGia.Text);
            sdpt += dg * sl / 100 * pt;
            thanhtien += dg * sl - sdpt;
            txtThanhTien.Text = thanhtien.ToString();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            SqlConnection conn = dc.GetConnection();
            if (txtMaHD.Text != "" && txtTenMon.Text != "" && txtGia.Text != "")
            {
                GetPay();
                cmd = new SqlCommand("insert into ChiTietHD(MaHD, MaMon, TenMon, SoLuong, DonGia, GiamGia, ThanhTien, NgayBan, MaNV, MaKH) values(@mahd,@mamon,@tenmon,@soluong,@dongia,@giamgia,@thanhtien,@ngayban,@manv,@makh)", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@mahd", txtMaHD.Text);
                cmd.Parameters.AddWithValue("@mamon", cboMaMon.Text);
                cmd.Parameters.AddWithValue("@tenmon", txtTenMon.Text);
                cmd.Parameters.AddWithValue("@soluong", txtSoLuong.Text);
                cmd.Parameters.AddWithValue("@dongia", txtGia.Text);
                cmd.Parameters.AddWithValue("@giamgia", txtGiamGia.Text);
                cmd.Parameters.AddWithValue("@thanhtien", txtThanhTien.Text);
                cmd.Parameters.AddWithValue("@ngayban", txtNgayBan.Text);
                cmd.Parameters.AddWithValue("@manv", cboMaNV.Text);
                cmd.Parameters.AddWithValue("@makh", cboMaKH.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Thêm vào thành công");
                DisplayData();
                ResetValues();
                btnLuu.Enabled = false;
                btnBoQua.Enabled = false;
                btnXoa.Enabled = true;
                btnThem.Enabled = true;
                //lblBangChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChu(Tongmoi.ToString());
                ResetValuesHang();
                btnXoa.Enabled = true;
                btnThem.Enabled = true;
                btnInHoaDon.Enabled = true;
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dgvHoaDon_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHD.Focus();
                return;
            }
            txtMaHD.Text = dgvHoaDon.CurrentRow.Cells["MaHD"].Value.ToString();
            txtNgayBan.Text = dgvHoaDon.CurrentRow.Cells["NgayBan"].Value.ToString();
            //cboMaNV.Text = dgvHoaDon.CurrentRow.Cells["MaNV"].Value.ToString();
            //txtTenNV.Text = dgvHoaDon.CurrentRow.Cells["TenNV"].Value.ToString();
            //cboMaKH.Text = dgvHoaDon.CurrentRow.Cells["MaKH"].Value.ToString();
            //txtTenKH.Text = dgvHoaDon.CurrentRow.Cells["TenKH"].Value.ToString();
            //txtDiaChiKH.Text = dgvHoaDon.CurrentRow.Cells["DiaChiKH"].Value.ToString();
            //txtDienThoaiKH.Text = dgvHoaDon.CurrentRow.Cells["DienThoaiKH"].Value.ToString();
            cboMaMon.Text = dgvHoaDon.CurrentRow.Cells["MaMon"].Value.ToString();
            txtTenMon.Text = dgvHoaDon.CurrentRow.Cells["TenMon"].Value.ToString();
            txtGia.Text = dgvHoaDon.CurrentRow.Cells["DonGia"].Value.ToString();
            txtSoLuong.Text = dgvHoaDon.CurrentRow.Cells["SoLuong"].Value.ToString();
            txtGiamGia.Text = dgvHoaDon.CurrentRow.Cells["GiamGia"].Value.ToString();
            txtThanhTien.Text = dgvHoaDon.CurrentRow.Cells["ThanhTien"].Value.ToString();
            btnXoa.Enabled = true;
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            ResetValuesHang();
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            // Tạo ứng dụng Excel
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();

            // Tạo WorkBook mới
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);

            // Tạo Sheet mới
            Microsoft.Office.Interop.Excel._Worksheet worksheet = workbook.Sheets.Add();
            worksheet.Name = "Exported from Data gridview";

            // Xem Sheet Excel
            app.Visible = true;

            // Tạo tiêu đề cột
            for (int i = 1; i <= dgvHoaDon.Columns.Count; i++)
            {
                worksheet.Cells[1, i] = dgvHoaDon.Columns[i - 1].HeaderText;
            }

            // Lấy dữ liệu từng dòng
            for (int i = 0; i < dgvHoaDon.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dgvHoaDon.Columns.Count; j++)
                {
                    if (dgvHoaDon.Rows[i].Cells[j].Value != null)
                    {
                        worksheet.Cells[i + 2, j + 1] = dgvHoaDon.Rows[i].Cells[j].Value.ToString();
                    }
                }
            }

            // Lưu file .Xlsx
            workbook.SaveAs("C:\\Users\\PhiHung\\Downloads", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            // Thoát khỏi Excel
            app.Quit();
        }
    }
}
