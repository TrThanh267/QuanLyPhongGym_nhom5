using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.DAL;
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
    public partial class UserControlQuanLyNhanVien : UserControl
    {
        QlGymContext _db = new QlGymContext();
        QuanLyNhanVien_DAL _NhanVienDal;
        public UserControlQuanLyNhanVien()
        {
            InitializeComponent();
            _NhanVienDal = new QuanLyNhanVien_DAL(_db);
            LoadData();
            VaiTro();
        }
        public void LoadData() 
        {
            var listNhanVien = _NhanVienDal.GetNhanVienWithVaiTro();
            dataGridViewNV.DataSource = listNhanVien;
            dataGridViewNV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        public void VaiTro()
        {
            comboBoxVaiTro.DataSource = _db.TaiKhoans.ToList();
            comboBoxVaiTro.DisplayMember = "MaVaitro"; // Hiển thị tên vai trò
            comboBoxVaiTro.ValueMember = "TenTaiKhoan"; // Lấy giá trị từ tên tài khoản
        }

        private void guna2ButtonThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxTenNhanVien.Text) || string.IsNullOrEmpty(textBoxSDT.Text) || string.IsNullOrEmpty(textBoxDiaChi.Text) || string.IsNullOrEmpty(textBoxEmail.Text) || string.IsNullOrEmpty(textBoxSDT.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                    return;
                }
                else
                {
                    NhanVien nhanVien = new NhanVien
                    {
                        TenNhanVien = textBoxTenNhanVien.Text,
                        TenTaiKhoan = comboBoxVaiTro.SelectedValue.ToString(),
                        DiaChi = textBoxDiaChi.Text,
                        Email = textBoxEmail.Text,
                        NgayVaoLam = DateOnly.FromDateTime(dateTimePickerNgayVaoLam.Value),
                        Sdt = textBoxSDT.Text,
                        Luong = decimal.TryParse(textBoxLuong.Text, out decimal luong) ? luong : (decimal?)null
                    };
                    if (_NhanVienDal.them(nhanVien))
                    {
                        MessageBox.Show("Thêm nhân viên thành công!");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Thêm nhân viên thất bại!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void guna2ButtonXoa_Click(object sender, EventArgs e)
        {
            try
            {
                NhanVien nhanVien = new NhanVien
                {
                    MaNv = int.Parse(dataGridViewNV.SelectedRows[0].Cells[0].Value.ToString()),
                    TenTaiKhoan = comboBoxVaiTro.SelectedValue.ToString(),
                    DiaChi = textBoxDiaChi.Text,
                    Email = textBoxEmail.Text,
                    NgayVaoLam = DateOnly.FromDateTime(dateTimePickerNgayVaoLam.Value),
                    Sdt = textBoxSDT.Text,
                    Luong = decimal.TryParse(textBoxLuong.Text, out decimal luong) ? luong : (decimal?)null
                };
                if (_NhanVienDal.xoa(nhanVien))
                {
                    MessageBox.Show("Xóa nhân viên thành công!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Xóa nhân viên thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

        }

        private void guna2ButtonCapNhap_Click(object sender, EventArgs e)
        {
            try
            {
                
                NhanVien nhanVien = new NhanVien
                {
                    MaNv = int.Parse(textBoxMaNV.Text),
                    TenNhanVien = textBoxTenNhanVien.Text,
                    TenTaiKhoan = comboBoxVaiTro.SelectedValue.ToString(),
                    DiaChi = textBoxDiaChi.Text,
                    Email = textBoxEmail.Text,
                    NgayVaoLam = DateOnly.FromDateTime(dateTimePickerNgayVaoLam.Value),
                    Sdt = textBoxSDT.Text,
                    Luong = decimal.TryParse(textBoxLuong.Text, out decimal luong) ? luong : (decimal?)null
                }; ;
                if (_NhanVienDal.sua(nhanVien))
                {
                    MessageBox.Show("Cập nhật nhân viên thành công!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Cập nhật nhân viên thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void guna2ButtonTk_Click(object sender, EventArgs e)
        {
            string searchText = textBoxTK.Text.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                var ketQua = _NhanVienDal.timkiem(searchText);
                dataGridViewNV.DataSource = _NhanVienDal.timkiem(searchText).Cast<object>().ToList();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên hoặc mã nhân viên để tìm kiếm!");
                LoadData();
            }
        }

        private void dataGridViewNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridViewNV.Rows[e.RowIndex];
                textBoxMaNV.Text = row.Cells["MaNv"].Value?.ToString();
                textBoxTenNhanVien.Text = row.Cells["TenNhanVien"].Value?.ToString();
                textBoxSDT.Text = row.Cells["Sdt"].Value?.ToString();
                textBoxEmail.Text = row.Cells["Email"].Value?.ToString();
                textBoxDiaChi.Text = row.Cells["DiaChi"].Value?.ToString();
                textBoxLuong.Text = row.Cells["Luong"].Value?.ToString();

                if (DateTime.TryParse(row.Cells["NgayVaoLam"].Value?.ToString(), out DateTime ngay))
                {
                    dateTimePickerNgayVaoLam.Value = ngay;
                }

                comboBoxVaiTro.Text = row.Cells["TenVaiTro"].Value?.ToString();
            }
        }

        private void dataGridViewNV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }
    }
}
