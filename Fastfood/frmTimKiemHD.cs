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
    public partial class frmTimKiemHD : Form
    {
        DataTable tblHDB; //Hoá đơn bán
        public frmTimKiemHD()
        {
            InitializeComponent();
        }
        private void frmTimKiemHD_Load(object sender, EventArgs e)
        {
            dgvTimKiemHD.Columns["SoLuong"].Visible = false;
            dgvTimKiemHD.Columns["GiamGia"].Visible = false;
            dgvTimKiemHD.Columns["DonGia"].Visible = false;
            dgvTimKiemHD.Columns["TenMon"].Visible = false;
            dgvTimKiemHD.Columns["MaMon"].Visible = false;
            ResetValues();
            dgvTimKiemHD.DataSource = null;
        }
        private void ResetValues()
        {
            foreach (Control Ctl in this.Controls)
                if (Ctl is TextBox)
                    Ctl.Text = "";
            txtMaHD.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaHD.Text == "") && (txtThang.Text == "") && (txtNam.Text == "") &&
               (txtMaNV.Text == "") && (txtMaKH.Text == "") &&
               (txtThanhTien.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //MaHD, MaNV, NgayBan, MaKH, ThanhTien
            sql = "SELECT * FROM ChiTietHD WHERE 1=1";
            if (txtMaHD.Text != "")
                sql = sql + " AND MaHD Like N'%" + txtMaHD.Text + "%'";
            if (txtThang.Text != "")
                sql = sql + " AND MONTH(NgayBan) =" + txtThang.Text;
            if (txtNam.Text != "")
                sql = sql + " AND YEAR(NgayBan) =" + txtNam.Text;
            if (txtMaNV.Text != "")
                sql = sql + " AND MaNV Like N'%" + txtMaNV.Text + "%'";
            if (txtMaKH.Text != "")
                sql = sql + " AND MaKH Like N'%" + txtMaKH.Text + "%'";
            if (txtThanhTien.Text != "")
                sql = sql + " AND ThanhTien <=" + txtThanhTien.Text;
            tblHDB = Functions.GetDataToTable(sql);
            if (tblHDB.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Có " + tblHDB.Rows.Count + " bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvTimKiemHD.DataSource = tblHDB;
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            dgvTimKiemHD.Columns["SoLuong"].Visible = false;
            dgvTimKiemHD.Columns["GiamGia"].Visible = false;
            dgvTimKiemHD.Columns["DonGia"].Visible = false;
            dgvTimKiemHD.Columns["TenMon"].Visible = false;
            dgvTimKiemHD.Columns["MaMon"].Visible = false;
            dgvTimKiemHD.AllowUserToAddRows = false;
            dgvTimKiemHD.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            LoadDataGridView();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
