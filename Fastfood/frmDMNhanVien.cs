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

namespace Fastfood
{
    public partial class frmDMNhanVien : Form
    {
        DataConnection dc = new DataConnection();
        SqlCommand cmd;
        SqlDataAdapter adapt;
        public frmDMNhanVien()
        {
            InitializeComponent();
        }

        private void frmDMNhanVien_Load(object sender, EventArgs e)
        {
            txtMaNV.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            DisplayData();
        }
        private void DisplayData() // Hiển thị dữ liệu
        {
            SqlConnection conn = dc.GetConnection();
            conn.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from NhanVien", conn);
            dgvNhanVien.AllowUserToAddRows = false;
            dgvNhanVien.EditMode = DataGridViewEditMode.EditProgrammatically;
            adapt.Fill(dt);
            dgvNhanVien.DataSource = dt;
            conn.Close();
        }

        private void dgvNhanVien_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNV.Focus();
                return;
            }
            txtMaNV.Text = dgvNhanVien.CurrentRow.Cells["MaNV"].Value.ToString();
            txtTenNV.Text = dgvNhanVien.CurrentRow.Cells["TenNV"].Value.ToString();
            if (dgvNhanVien.CurrentRow.Cells["GioiTinh"].Value.ToString() == "Nam") chkGioiTinh.Checked = true;
            else chkGioiTinh.Checked = false;
            txtDiaChiNV.Text = dgvNhanVien.CurrentRow.Cells["DiaChiNV"].Value.ToString();
            txtDienThoaiNV.Text = dgvNhanVien.CurrentRow.Cells["DienThoaiNV"].Value.ToString();
            dateTimePicker1.Text = dgvNhanVien.CurrentRow.Cells["NgaySinhNV"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaNV.Enabled = true;
            txtMaNV.Focus();
        }
        private void ResetValues()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            chkGioiTinh.Checked = false;
            txtDiaChiNV.Text = "";
            dateTimePicker1.Text = "";
            txtDienThoaiNV.Text = "";
        }
        string gt;
        private void btnLuu_Click(object sender, EventArgs e)
        {
            
            SqlConnection conn = dc.GetConnection();

            if (chkGioiTinh.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";
            if (txtMaNV.Text != "" && txtTenNV.Text != "" && txtDiaChiNV.Text != "" && txtDienThoaiNV.Text != "")
            {              
                cmd = new SqlCommand("insert into NhanVien(MaNV, TenNV, DiaChiNV, DienThoaiNV, NgaySinhNV, GioiTinh) values(@manv,@tennv,@diachinv,@dienthoainv,@ngaysinhnv,@gioitinh)", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@manv", txtMaNV.Text);
                cmd.Parameters.AddWithValue("@tenNV", txtTenNV.Text);
                cmd.Parameters.AddWithValue("@diachinv", txtDiaChiNV.Text);
                cmd.Parameters.AddWithValue("@dienthoainv", txtDienThoaiNV.Text);
                cmd.Parameters.AddWithValue("@ngaysinhnv", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@gioitinh", gt);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Thêm vào thành công");
                DisplayData();
                ResetValues();
                btnXoa.Enabled = true;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnBoQua.Enabled = false;
                btnLuu.Enabled = false;
                txtMaNV.Enabled = false;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            SqlConnection conn = dc.GetConnection();
            if (txtMaNV.Text != "")
            {
                if (chkGioiTinh.Checked == true)
                    gt = "Nam";
                else
                    gt = "Nữ";
                cmd = new SqlCommand("update NhanVien set MaNV=@manv, TenNV=@TenNV, DiaChiNV=@diachinv, DienThoaiNV=@dienthoainv, NgaySinhNV=@ngaysinhnv, GioiTinh=@gioitinh where MaNV=@manv", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@manv", txtMaNV.Text);
                cmd.Parameters.AddWithValue("@tenNV", txtTenNV.Text);
                cmd.Parameters.AddWithValue("@diachinv", txtDiaChiNV.Text);
                cmd.Parameters.AddWithValue("@dienthoainv", txtDienThoaiNV.Text);
                cmd.Parameters.AddWithValue("@ngaysinhnv", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@gioitinh", gt);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công");
                conn.Close();
                DisplayData();
                ResetValues();
                btnBoQua.Enabled = false;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên để cập nhật");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            SqlConnection conn = dc.GetConnection();
            if (txtMaNV.Text != "")
            {
                cmd = new SqlCommand("delete NhanVien where MaNV=@manv", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@manv", txtMaNV.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Xóa nhân viên thành công");
                DisplayData();
                ResetValues();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên để xóa");
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaNV.Enabled = false;
        }

        private void txtMaNV_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }      
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
