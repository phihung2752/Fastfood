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
    public partial class frmDMMonAn : Form
    {
        DataConnection dc = new DataConnection();
        SqlCommand cmd;
        SqlDataAdapter adapt;
        public frmDMMonAn()
        {
            InitializeComponent();
        }
        private void DisplayData()
        {
            SqlConnection conn = dc.GetConnection();
            conn.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from MonAn", conn);
            dgvMonAn.AllowUserToAddRows = false;
            dgvMonAn.EditMode = DataGridViewEditMode.EditProgrammatically;
            adapt.Fill(dt);
            dgvMonAn.DataSource = dt;
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
            txtMaMon.Enabled = true;
            txtMaMon.Focus();
            txtGia.Enabled = true;          
        }
       
        void loadcombobox()
        {
            dc = new DataConnection();
            SqlConnection con = dc.GetConnection();
            con.Open();
            cmd = new SqlCommand("SELECT * FROM LoaiMon", con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Dispose();
            comboBox1.DisplayMember = "MaLoai";
            cbbLoaiMon.DisplayMember = "LoaiMon";
            comboBox1.DataSource = dt;
            cbbLoaiMon.DataSource = dt;

        }
        private void frmDMMonAn_Load(object sender, EventArgs e)
        {           
            txtMaMon.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            comboBox1.Enabled = false;
            DisplayData();
            loadcombobox();
            comboBox1.SelectedIndex = -1;
            ResetValues();
        }
        private void ResetValues()
        {
            txtMaMon.Text = "";
            txtTenMon.Text = "";
            comboBox1.Text = "";
            txtGia.Text = "";
            txtGia.Enabled = true;
            txtGhiChu.Text = "";
            cbbLoaiMon.Text = "";
        }

        private void dgvMonAn_Click(object sender, EventArgs e)
        {
            
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaMon.Focus();
                return;
            }
            txtMaMon.Text = dgvMonAn.CurrentRow.Cells["MaMon"].Value.ToString();
            txtTenMon.Text = dgvMonAn.CurrentRow.Cells["TenMon"].Value.ToString();
            comboBox1.Text = dgvMonAn.CurrentRow.Cells["MaLoai"].Value.ToString();
            txtGia.Text = dgvMonAn.CurrentRow.Cells["Gia"].Value.ToString();
            txtGhiChu.Text = dgvMonAn.CurrentRow.Cells["GhiChu"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SqlConnection conn = dc.GetConnection();
            if (txtMaMon.Text != "" && txtTenMon.Text != "" && txtGia.Text != "" && txtGhiChu.Text != "" && comboBox1.Text != "")
            {
                cmd = new SqlCommand("insert into MonAn(MaMon, TenMon, MaLoai, Gia, GhiChu, LoaiMon) values(@mamon,@tenmon,@maloai,@gia,@ghichu,@loaimon)", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@mamon", txtMaMon.Text);
                cmd.Parameters.AddWithValue("@tenmon", txtTenMon.Text);
                cmd.Parameters.AddWithValue("@maloai", comboBox1.Text);
                cmd.Parameters.AddWithValue("@gia", txtGia.Text);
                cmd.Parameters.AddWithValue("@ghichu", txtGhiChu.Text);
                cmd.Parameters.AddWithValue("@loaimon", cbbLoaiMon.Text);
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
                txtMaMon.Enabled = false;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            SqlConnection conn = dc.GetConnection();
            if (txtMaMon.Text != "")
            {
                cmd = new SqlCommand("update MonAn set MaMon=@mamon, TenMon=@tenmon, MaLoai=@maloai, LoaiMon=@loaimon, Gia=@gia, GhiChu=@ghichu where MaMon=@mamon", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@mamon", txtMaMon.Text);
                cmd.Parameters.AddWithValue("@tenmon", txtTenMon.Text);
                cmd.Parameters.AddWithValue("@maloai", comboBox1.Text);
                cmd.Parameters.AddWithValue("@gia", txtGia.Text);
                cmd.Parameters.AddWithValue("@ghichu", txtGhiChu.Text);
                cmd.Parameters.AddWithValue("@loaimon", cbbLoaiMon.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công");
                conn.Close();
                DisplayData();
                ResetValues();
                btnBoQua.Enabled = false;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã món để cập nhật");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            SqlConnection conn = dc.GetConnection();
            if (txtMaMon.Text != "")
            {
                cmd = new SqlCommand("delete MonAn where MaMon=@mamon", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@mamon", txtMaMon.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Xóa món ăn thành công");
                DisplayData();
                ResetValues();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã món ăn để xóa");
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaMon.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
