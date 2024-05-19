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
    public partial class frmDMLoaiMon : Form
    {
        DataConnection dc = new DataConnection();
        SqlCommand cmd;
        SqlDataAdapter adapt;
        public frmDMLoaiMon()
        {
            InitializeComponent();
        }
        private void frmDMLoaiMon_Load(object sender, EventArgs e)
        {
            txtMaLoai.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            DisplayData();
        }
        private void DisplayData() // Hiển thị dữ liệu
        {
            SqlConnection conn = dc.GetConnection();
            conn.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from LoaiMon", conn);
            dgvLoaiMon.AllowUserToAddRows = false;
            dgvLoaiMon.EditMode = DataGridViewEditMode.EditProgrammatically;
            adapt.Fill(dt);
            dgvLoaiMon.DataSource = dt;
            conn.Close();
        }
        private void ResetValue()
        {
            txtMaLoai.Text = "";
            txtLoaiMon.Text = "";
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValue(); //Xoá trắng các textbox
            txtMaLoai.Enabled = true; //cho phép nhập mới
            txtLoaiMon.Focus();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            SqlConnection conn = dc.GetConnection();
            if (txtMaLoai.Text != "" && txtLoaiMon.Text != "")
            {
                cmd = new SqlCommand("insert into LoaiMon(MaLoai, LoaiMon) values(@maloai,@loaimon)", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@maloai", txtMaLoai.Text);
                cmd.Parameters.AddWithValue("@loaimon", txtLoaiMon.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Thêm vào thành công");
                DisplayData();
                ResetValue();
                btnXoa.Enabled = true;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnBoQua.Enabled = false;
                btnLuu.Enabled = false;
                txtMaLoai.Enabled = false;
            }

        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaLoai.Enabled = false;
        }
        private void txtMaLoai_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtLoaiMon_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            SqlConnection conn = dc.GetConnection();
            if (txtMaLoai.Text != "")
            {
                cmd = new SqlCommand("delete LoaiMon where MaLoai=@maloai", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@maloai", txtMaLoai.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Xóa loại thành công");
                DisplayData();
                ResetValue();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã loại để xóa loại");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            SqlConnection conn = dc.GetConnection();
            if (txtMaLoai.Text != "")
            {
                cmd = new SqlCommand("update LoaiMon set MaLoai=@maloai, LoaiMon=@loaimon where MaLoai=@maloai", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@maloai", txtMaLoai.Text);
                cmd.Parameters.AddWithValue("@loaimon", txtLoaiMon.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công");
                conn.Close();
                DisplayData();
                ResetValue();
                btnBoQua.Enabled = false;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã loại để cập nhật");
            }
        }

        private void dgvLoaiMon_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaLoai.Focus();
                return;
            }
            txtMaLoai.Text = dgvLoaiMon.CurrentRow.Cells["MaLoai"].Value.ToString();
            txtLoaiMon.Text = dgvLoaiMon.CurrentRow.Cells["LoaiMon"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }
    }
}