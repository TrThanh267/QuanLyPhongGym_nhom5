using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhongGym.GUI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            load();
        }
        public void load()
        {
            userControlQuanLyNhanVien1.Visible = false;
            userControlQuanLyKhachHang1.Visible = false;
            userControlQuanLyHoaDon1.Visible = false;
            userControlPhongTap1.Visible = false;
            userControlQuanLyTaiKhoan1.Visible = false;
            userControlDichVu1.Visible = false;
        }
        private void guna2ButtonQLNV_Click(object sender, EventArgs e)
        {
            userControlQuanLyNhanVien1.Visible = true;
            userControlQuanLyNhanVien1.BringToFront();
            userControlQuanLyKhachHang1.Visible = false;
            userControlQuanLyHoaDon1.Visible = false;
            userControlPhongTap1.Visible = false;
            userControlQuanLyTaiKhoan1.Visible = false;
            userControlDichVu1.Visible = false;
            labelhienthi.Visible = false;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            buttonTaiKhoan.PerformClick();
        }

        private void guna2CircleButtonX_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void userControlQuanLyNhanVien1_Load(object sender, EventArgs e)
        {

        }

        private void ButtonQLKhachHang_Click(object sender, EventArgs e)
        {
            userControlQuanLyKhachHang1.Visible = true;
            userControlQuanLyKhachHang1.BringToFront();
            userControlQuanLyNhanVien1.Visible = false;
            userControlQuanLyHoaDon1.Visible = false;
            userControlPhongTap1.Visible = false;
            userControlQuanLyTaiKhoan1.Visible = false;
            userControlDichVu1.Visible = false;
            labelhienthi.Visible = false;
        }

        private void guna2ButtonHD_Click(object sender, EventArgs e)
        {
            userControlQuanLyHoaDon1.Visible = true;
            userControlQuanLyHoaDon1.BringToFront();
            userControlQuanLyKhachHang1.Visible = false;
            userControlQuanLyNhanVien1.Visible = false;
            userControlPhongTap1.Visible = false;
            userControlQuanLyTaiKhoan1.Visible = false;
            userControlDichVu1.Visible = false;
            labelhienthi.Visible = false;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            userControlPhongTap1.Visible = true;
            userControlPhongTap1.BringToFront();
            userControlQuanLyHoaDon1.Visible = false;
            userControlQuanLyKhachHang1.Visible = false;
            userControlQuanLyNhanVien1.Visible = false;
            userControlQuanLyTaiKhoan1.Visible = false;
            userControlDichVu1.Visible = false;
            labelhienthi.Visible = false;
        }

        private void buttonTaiKhoan_Click(object sender, EventArgs e)
        {
            userControlQuanLyTaiKhoan1.Visible = true;
            userControlQuanLyTaiKhoan1.BringToFront();
            userControlQuanLyHoaDon1.Visible = false;
            userControlQuanLyKhachHang1.Visible = false;
            userControlQuanLyNhanVien1.Visible = false;
            userControlPhongTap1.Visible = false;
            userControlDichVu1.Visible = false;
            labelhienthi.Visible = false;
        }

        private void guna2ButtonDV_Click(object sender, EventArgs e)
        {
            userControlDichVu1.Visible = true;
            userControlDichVu1.BringToFront();
            userControlQuanLyTaiKhoan1.Visible = false;
            userControlQuanLyHoaDon1.Visible = false;
            userControlQuanLyKhachHang1.Visible = false;
            userControlQuanLyNhanVien1.Visible = false;
            userControlPhongTap1.Visible = false;
            labelhienthi.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
