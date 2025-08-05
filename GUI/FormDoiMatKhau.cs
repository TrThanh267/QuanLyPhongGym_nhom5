using QuanLyPhongGym.GUI;
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

namespace QuanLyPhongGym_nhom5.GUI
{
    public partial class FormDoiMatKhau : Form
    {
        public FormDoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string tenTaiKhoan = txtTenTaiKhoan.Text.Trim();
            string mkMoi = txtMatKhauMoi.Text.Trim();
            string mkNhapLai = txtNhapLaiMK.Text.Trim();

            if (string.IsNullOrEmpty(tenTaiKhoan) || string.IsNullOrEmpty(mkMoi) || string.IsNullOrEmpty(mkNhapLai))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (mkMoi != mkNhapLai)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận không khớp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var db = new QlGymContext())
            {
                var tk = db.TaiKhoans.FirstOrDefault(t => t.TenTaiKhoan == tenTaiKhoan);
                if (tk != null)
                {
                    tk.MatKhau = mkMoi;
                    db.SaveChanges();

                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tài khoản.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
            
        

        private void FormDoiMatKhau_Load(object sender, EventArgs e)
        {
        }

        private void guna2CircleButtonTroVe_Click(object sender, EventArgs e)
        {
            FormdangNhap formDangNhap = new FormdangNhap();
            formDangNhap.Show();
            this.Hide();
        }

        private void txtTenTaiKhoan_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
