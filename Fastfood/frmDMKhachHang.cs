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
    public partial class frmDMKhachHang : Form
    {
        DataConnection dc = new DataConnection();
        SqlCommand cmd;
        SqlDataAdapter adapt;
      
        public frmDMKhachHang()
        {
            InitializeComponent();
        }

        private void frmDMKhachHang_Load(object sender, EventArgs e)
        {
            txtMaKH.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            DisplayData();
        }
        private void DisplayData() // Hiển thị dữ liệu
        {
            SqlConnection conn = dc.GetConnection();
            conn.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from KhachHang", conn);
            dgvKhachHang.AllowUserToAddRows = false;
            dgvKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically;
            adapt.Fill(dt);
            dgvKhachHang.DataSource = dt;
            conn.Close();
        }

       

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaKH.Enabled = true;
            txtMaKH.Focus();
        }
        private void ResetValues()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtDiaChiKH.Text = "";
            txtDienThoaiKH.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SqlConnection conn = dc.GetConnection();
            if (txtMaKH.Text != "" && txtTenKH.Text != "" && txtDiaChiKH.Text != "" && txtDienThoaiKH.Text != "")
            {
                cmd = new SqlCommand("insert into KhachHang(MaKH, TenKH, DienThoaiKH, DiaChiKH) values(@makh,@tenkh,@dienthoaikh,@diachikh)", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@makh", txtMaKH.Text);
                cmd.Parameters.AddWithValue("@tenkh", txtTenKH.Text);
                cmd.Parameters.AddWithValue("@dienthoaikh", txtDienThoaiKH.Text);
                cmd.Parameters.AddWithValue("@diachikh", txtDiaChiKH.Text);
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
                txtMaKH.Enabled = false;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            SqlConnection conn = dc.GetConnection();
            if (txtMaKH.Text != "")
            {
                cmd = new SqlCommand("update KhachHang set MaKH=@makh, TenKH=@tenkh, DienThoaiKH=@dienthoaikh, DiaChiKH=@diachikh where MaKH=@makh", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@makh", txtMaKH.Text);
                cmd.Parameters.AddWithValue("@tenkh", txtTenKH.Text);
                cmd.Parameters.AddWithValue("@dienthoaikh", txtDienThoaiKH.Text);
                cmd.Parameters.AddWithValue("@diachikh", txtDiaChiKH.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công");
                conn.Close();
                DisplayData();
                ResetValues();
                btnBoQua.Enabled = false;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng để cập nhật");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            SqlConnection conn = dc.GetConnection();
            if (txtMaKH.Text != "")
            {
                cmd = new SqlCommand("delete KhachHang where MaKH=@makh", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@makh", txtMaKH.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Xóa khách hàng thành công");
                DisplayData();
                ResetValues();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng để xóa");
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
            txtMaKH.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvKhachHang_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKH.Focus();
                return;
            }
            txtMaKH.Text = dgvKhachHang.CurrentRow.Cells["MaKH"].Value.ToString();
            txtTenKH.Text = dgvKhachHang.CurrentRow.Cells["TenKH"].Value.ToString();
            txtDienThoaiKH.Text = dgvKhachHang.CurrentRow.Cells["DienThoaiKH"].Value.ToString();
            txtDiaChiKH.Text = dgvKhachHang.CurrentRow.Cells["DiaChiKH"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }
    }
}
