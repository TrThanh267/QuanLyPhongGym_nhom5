using QuanLyPhongGym.DAL;
using QuanLyPhongGym_nhom5;
using QuanLyPhongGym_nhom5.GUI;
using QuanLyPhongGym_nhom5.Models;
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
    public partial class FormdangNhap : Form
    {
        QlGymContext _db = new QlGymContext();
        DangNhapDAL _DAL;
        public FormdangNhap()
        {
            InitializeComponent();
            _DAL = new DangNhapDAL(_db);
            loadForm();
        }
        public void loadForm()
        {
            labelhienthi.Visible = false;
            guna2TextBoxMatKhau.UseSystemPasswordChar = true;
        }


        private void guna2Buttondangnnhap_Click(object sender, EventArgs e)
        {
            string taiKhoan = guna2TextBoxTaiKhoan.Text.Trim();
            string matKhau = guna2TextBoxMatKhau.Text.Trim();
            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var taiKhoans = _DAL.login(taiKhoan, matKhau);
            if (taiKhoans != null)
            {

                NguoiDung.nguoidunghientai = taiKhoans;
                if (taiKhoans.MaVaiTroNavigation.TenVaiTro.Equals("KhachHang", StringComparison.OrdinalIgnoreCase))
                {
                    DangKyDV_GT dangKyDV_GT = new DangKyDV_GT();
                    dangKyDV_GT.Show();
                    this.Hide();
                }
                else
                {
                    if (NguoiDung.nguoidunghientai.MaVaiTroNavigation.TenVaiTro == "QuanLy")
                    {
                        MessageBox.Show("Bạn đã đăng nhập với vai trò là Quản Lý");
                    }
                    else if (NguoiDung.nguoidunghientai.MaVaiTroNavigation.TenVaiTro == "HLV")
                    {
                        MessageBox.Show("Bạn đã đăng nhập với vai trò là Huấn Luyện Viên");
                    }
                    else
                    {
                        MessageBox.Show("Bạn đã đăng nhập với vai trò là Thu Ngân");
                    }
                    FormMain formMain = new FormMain();
                    formMain.Show();
                    this.Hide();
                }
            }
            else
            {
                labelhienthi.Visible = true;
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void ButtonQuenMK_Click(object sender, EventArgs e)
        {
            FormDoiMatKhau formDoiMatKhau = new FormDoiMatKhau();
            this.Hide();
            formDoiMatKhau.ShowDialog();
            this.Show();
        }

        private void checkBoxHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxHienMatKhau.Checked)
            {
                guna2TextBoxMatKhau.UseSystemPasswordChar = false;
            }
            else
            {
                guna2TextBoxMatKhau.UseSystemPasswordChar = true;
            }
        }
    }
}
