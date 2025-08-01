using QuanLyPhongGym_nhom5.DAL;
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
    public partial class UserControlQuanLyKhachHang : UserControl
    {
        QuanLyKhachHang_DAL _DAL;
        QlGymContext _context = new QlGymContext();
        public UserControlQuanLyKhachHang()
        {
            InitializeComponent();
            _DAL = new QuanLyKhachHang_DAL(_context);
            loadData();
        }
        public void loadData()
        {
            var khachHangs = _DAL.GetKhachHangWithGoiTap();
            dataGridViewKH.DataSource = khachHangs;
            dataGridViewKH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void guna2ButtonThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxTenKH.Text) || string.IsNullOrEmpty(textBoxSDT.Text) || string.IsNullOrEmpty(textBoxDiaChi.Text) || string.IsNullOrEmpty(textBoxEmail.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                return;
            }
            else
            {
                KhachHang khachHang = new KhachHang
                {
                    HoTen = textBoxTenKH.Text,
                    DiaChi = textBoxDiaChi.Text,
                    Email = textBoxEmail.Text,
                    Sdt = textBoxSDT.Text,
                    NgayDangKy = DateOnly.FromDateTime(dateTimePickerDangKi.Value),
                    NgayHetHan = DateOnly.FromDateTime(dateTimePickerHetHan.Value),

                    GioiTinh = radioButtonNam.Checked ? "Nam" : "Nữ",
                };
                _DAL.ThemKhachHang(khachHang);
                loadData();
                MessageBox.Show("Thêm khách hàng thành công!");
            }
        }

        private void una2ButtonXoa_Click(object sender, EventArgs e)
        {
            KhachHang khachHang = new KhachHang()
            {
                MaKh = int.Parse(dataGridViewKH.SelectedRows[0].Cells[0].Value.ToString()),
                HoTen = textBoxTenKH.Text,
                DiaChi = textBoxDiaChi.Text,
                Email = textBoxEmail.Text,
                Sdt = textBoxSDT.Text,
                GioiTinh = radioButtonNam.Checked ? "Nam" : "Nữ",
                NgayDangKy = DateOnly.FromDateTime(dateTimePickerDangKi.Value),
                NgayHetHan = DateOnly.FromDateTime(dateTimePickerHetHan.Value),

            };
            if (_DAL.XoaKhachHang(khachHang))
            {
                loadData();
                MessageBox.Show("Xóa khách hàng thành công!");
            }
            else
            {
                MessageBox.Show("Xóa khách hàng thất bại!");
            }
        }

        private void una2ButtonSua_Click(object sender, EventArgs e)
        {
            if (dataGridViewKH.SelectedRows.Count > 0)
            {
                KhachHang khachHang = new KhachHang()
                {
                    MaKh = int.Parse(dataGridViewKH.SelectedRows[0].Cells[0].Value.ToString()),
                    HoTen = textBoxTenKH.Text,
                    DiaChi = textBoxDiaChi.Text,
                    Email = textBoxEmail.Text,
                    Sdt = textBoxSDT.Text,
                    GioiTinh = radioButtonNam.Checked ? "Nam" : "Nữ",
                    NgayDangKy = DateOnly.FromDateTime(dateTimePickerDangKi.Value),
                    NgayHetHan = DateOnly.FromDateTime(dateTimePickerHetHan.Value),
                    TrangThai = checkBoxHD.Checked ? true : false
                };
                if (_DAL.SuaKhachHang(khachHang))
                {
                    loadData();
                    MessageBox.Show("Sửa khách hàng thành công!");
                }
                else
                {
                    MessageBox.Show("Sửa khách hàng thất bại!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để sửa.");
            }
        }

        private void guna2ButtonTk_Click(object sender, EventArgs e)
        {
            string searchText = textBoxTK.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Vui lòng nhập thông tin tìm kiếm!");
                return;
            }
            else
            {
                var ketQua = _DAL.timkiemKH(searchText);
                if (ketQua.Any())
                {
                    dataGridViewKH.DataSource = ketQua;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng nào phù hợp với từ khóa tìm kiếm!");
                }
            }
        }

        private void dataGridViewKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedRow = dataGridViewKH.Rows[e.RowIndex];
            if (selectedRow != null)
            {
                textBoxTenKH.Text = selectedRow.Cells["HoTen"].Value.ToString();
                textBoxDiaChi.Text = selectedRow.Cells["DiaChi"].Value.ToString();
                textBoxEmail.Text = selectedRow.Cells["Email"].Value.ToString();
                textBoxSDT.Text = selectedRow.Cells["Sdt"].Value.ToString();
                dateTimePickerDangKi.Value = DateTime.Parse(selectedRow.Cells["NgayDangKy"].Value.ToString());
                dateTimePickerHetHan.Value = DateTime.Parse(selectedRow.Cells["NgayHetHan"].Value.ToString());
                if (selectedRow?.Cells["TrangThai"]?.Value != null)
                {
                    checkBoxHD.Checked = Convert.ToBoolean(selectedRow.Cells["TrangThai"].Value);
                }
                else
                {
                    checkBoxHD.Checked = false;
                }

                
                if (selectedRow?.Cells["GioiTinh"]?.Value!=null)
                {
                    radioButtonNam.Checked = selectedRow.Cells["GioiTinh"].Value.ToString() == "Nam";
                }
                else
                {
                    radioButtonNu.Checked = selectedRow.Cells["GioiTinh"].Value.ToString() == "Nữ";
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xem chi tiết!");
            }
        }
    }
}
