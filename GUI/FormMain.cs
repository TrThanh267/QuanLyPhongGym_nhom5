using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyPhongGym.GUI;
using QuanLyPhongGym_nhom5;
using QuanLyPhongGym_nhom5.GUI;

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
            labelhienthi.Visible = true;
            userControlQuanLyNhanVien1.Visible = false;
            userControlQuanLyKhachHang1.Visible = false;
            userControlQuanLyHoaDon1.Visible = false;
            userControlPhongTap1.Visible = false;
            userControlQuanLyTaiKhoan1.Visible = false;
            userControlDichVu1.Visible = false;
        }
        private void guna2ButtonQLNV_Click(object sender, EventArgs e)
        {
            var quyen = NguoiDung.nguoidunghientai.MaVaiTroNavigation.TenVaiTro;
            if (quyen == "QuanLy" )
            {
                userControlQuanLyNhanVien1.Visible = true;
                userControlQuanLyNhanVien1.BringToFront();
                userControlQuanLyKhachHang1.Visible = false;
                userControlQuanLyHoaDon1.Visible = false;
                userControlPhongTap1.Visible = false;
                userControlQuanLyTaiKhoan1.Visible = false;
                userControlDichVu1.Visible = false;
                buttonmoving.Left = guna2ButtonQLNV.Left + 0;
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            
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
            var quyen = NguoiDung.nguoidunghientai.MaVaiTroNavigation.TenVaiTro;
            {   if (quyen == "QuanLy" || quyen == "ThuNgan")
                {
                    userControlQuanLyKhachHang1.Visible = true;
                    userControlQuanLyKhachHang1.BringToFront();
                    userControlQuanLyNhanVien1.Visible = false;
                    userControlQuanLyHoaDon1.Visible = false;
                    userControlPhongTap1.Visible = false;
                    userControlQuanLyTaiKhoan1.Visible = false;
                    userControlDichVu1.Visible = false;
                    buttonmoving.Left = ButtonQLKhachHang.Left + 0;
                }
                else 
                { 
                    MessageBox.Show("Bạn không có quyền truy cập vào chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }

        private void guna2ButtonHD_Click(object sender, EventArgs e)
        {
            var quyen = NguoiDung.nguoidunghientai.MaVaiTroNavigation.TenVaiTro;
            if (quyen == "QuanLy" || quyen == "ThuNgan")
            {
                userControlQuanLyHoaDon1.Visible = true;
                userControlQuanLyHoaDon1.BringToFront();
                userControlQuanLyKhachHang1.Visible = false;
                userControlQuanLyNhanVien1.Visible = false;
                userControlPhongTap1.Visible = false;
                userControlQuanLyTaiKhoan1.Visible = false;
                userControlDichVu1.Visible = false;
                buttonmoving.Left = guna2ButtonHD.Left + 0;
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            var quyen = NguoiDung.nguoidunghientai.MaVaiTroNavigation.TenVaiTro;
            if (quyen == "QuanLy" || quyen == "HLV")
            {
                userControlPhongTap1.Visible = true;
                userControlPhongTap1.Visible = true;
                userControlPhongTap1.BringToFront();
                userControlQuanLyHoaDon1.Visible = false;
                userControlQuanLyKhachHang1.Visible = false;
                userControlQuanLyNhanVien1.Visible = false;
                userControlQuanLyTaiKhoan1.Visible = false;
                userControlDichVu1.Visible = false;
                buttonmoving.Left = guna2Button5.Left + 0;
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonTaiKhoan_Click(object sender, EventArgs e)
        {
            var quyen = NguoiDung.nguoidunghientai.MaVaiTroNavigation.TenVaiTro;
            if (quyen == "QuanLy")
            {
                userControlQuanLyTaiKhoan1.Visible = true;
                userControlQuanLyTaiKhoan1.BringToFront();
                userControlQuanLyHoaDon1.Visible = false;
                userControlQuanLyKhachHang1.Visible = false;
                userControlQuanLyNhanVien1.Visible = false;
                userControlPhongTap1.Visible = false;
                userControlDichVu1.Visible = false;
                buttonmoving.Left = buttonTaiKhoan.Left + 0;
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void guna2ButtonDV_Click(object sender, EventArgs e)
        {
            var quyen = NguoiDung.nguoidunghientai.MaVaiTroNavigation.TenVaiTro;
            if (quyen == "QuanLy")
            {
                userControlDichVu1.Visible = true;
                userControlQuanLyTaiKhoan1.Visible = false;
                userControlQuanLyHoaDon1.Visible = false;
                userControlQuanLyKhachHang1.Visible = false;
                userControlQuanLyNhanVien1.Visible = false;
                userControlPhongTap1.Visible = false;
                userControlDichVu1.BringToFront();
                buttonmoving.Left = guna2ButtonDV.Left + 0;
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
