using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fastfood
{
    public partial class MainUI : Form
    {
        public MainUI()
        {
            InitializeComponent();
            customizeDesing();
        }
        private void customizeDesing()
        {
            panelQuanLySubMenu.Visible = true;
            panelHoaDonSubMenu.Visible = true;
        }
        //private void hideSubMenu()
        //{
        //    if (panelQuanLySubMenu.Visible == true)
        //        panelQuanLySubMenu.Visible = false;
        //    if (panelHoaDonSubMenu.Visible == true)
        //        panelHoaDonSubMenu.Visible = false;
        //}
        //private void showSubMenu(Panel subMenu)
        //{
        //    //if (subMenu.Visible == false)
        //    //{
        //    //    hideSubMenu();
        //        subMenu.Visible = true;
        //    //}
        //    //else
        //    //    subMenu.Visible = false;
        //}

        //private void btnQuanLyChung_Click(object sender, EventArgs e)
        //{

        //}

        private void btnLoaiMon_Click(object sender, EventArgs e)
        {
            openChildForm(new frmDMLoaiMon());
            //hideSubMenu();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnMonAn_Click(object sender, EventArgs e)
        {
            openChildForm(new frmDMMonAn());

        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            openChildForm(new frmDMKhachHang());
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            openChildForm(new frmDMNhanVien());
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            openChildForm(new frmHoaDon());
        }

        private void btnTimKiemHD_Click(object sender, EventArgs e)
        {
            openChildForm(new frmTimKiemHD());
        }

        private void btnQuanLyChung_Click(object sender, EventArgs e)
        {
            customizeDesing();
        }

        private void btnQuanLyHD_Click(object sender, EventArgs e)
        {
            customizeDesing();
        }
    }
}
