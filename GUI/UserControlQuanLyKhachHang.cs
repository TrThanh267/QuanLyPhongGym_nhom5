using QuanLyPhongGym_nhom5.BLL;
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
        Khachhang_BLL _BLL;
        QlGymContext _context = new QlGymContext();
        public UserControlQuanLyKhachHang()
        {
            InitializeComponent();
            _BLL = new Khachhang_BLL(_context);
            loadData();
        }
        public void loadData()
        {
            var khachHangs = _BLL.GetKhachHangWithGoiTap();
            dataGridViewKH.DataSource = khachHangs;
            dataGridViewKH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            comboBoxtaikhoan.DataSource = _context.TaiKhoans.ToList().Where(tk => tk.MaVaiTro == 1).ToList();
            comboBoxtaikhoan.DisplayMember = "TenTaiKhoan"; // Hiển thị tên tài khoản
            comboBoxtaikhoan.ValueMember = "TenTaiKhoan"; // Lấy giá trị từ tên tài khoản
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
                    TrangThai = checkBoxHD.Checked ? true : false,
                    TenTaiKhoan = comboBoxtaikhoan.SelectedValue.ToString()
                };
                _BLL.AddKhachHang(khachHang);
                var tenTaiKhoan = comboBoxtaikhoan.SelectedValue.ToString();
                if(_BLL.KiemTraTaiKhoanDaSuDung(tenTaiKhoan))
                {
                    MessageBox.Show("Tài khoản đã được sử dụng, vui lòng chọn tài khoản khác!");
                    loadData();
                    return;
                    
                }
                string email = textBoxEmail.Text.Trim();
                if (!email.EndsWith("@gmail.com") || !email.Contains("@") || email.StartsWith("@"))
                {
                    MessageBox.Show("Email phải có định dạng hợp lệ và kết thúc bằng @gmail.com!");
                    return;
                }
                loadData();
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
                TenTaiKhoan = comboBoxtaikhoan.SelectedValue.ToString(),
                TrangThai = checkBoxHD.Checked ? true : false
            };
            if (_BLL.DeleteKhachHang(khachHang.MaKh))
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
                var tenTaiKhoan = comboBoxtaikhoan.SelectedValue.ToString().Trim();
                if (_BLL.KiemTraTaiKhoanDaSuDung(tenTaiKhoan))
                {
                    MessageBox.Show("Tài khoản đã được sử dụng, vui lòng chọn tài khoản khác!");
                    return;
                }
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
                    TrangThai = checkBoxHD.Checked ? true : false,
                    TenTaiKhoan = comboBoxtaikhoan.SelectedValue.ToString()
                };
                if (_BLL.UpdateKhachHang(khachHang))
                {
                    loadData();
                    MessageBox.Show("Sửa khách hàng thành công!");
                    
                }
                else
                {
                    MessageBox.Show("Sửa khách hàng thất bại!");
                }
                if(_BLL.KiemTraTaiKhoanDaSuDungChoKhac(tenTaiKhoan, khachHang.MaKh))
                {
                    MessageBox.Show("Tài khoản đã được sử dụng bởi khách hàng khác, vui lòng chọn tài khoản khác!");
                    return;
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
                var ketQua = _BLL.TimKiem(searchText);
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
                if( selectedRow?.Cells["TenTaiKhoan"]?.Value != null)
                {
                    comboBoxtaikhoan.SelectedValue = selectedRow.Cells["TenTaiKhoan"].Value.ToString();
                }
                else
                {
                    comboBoxtaikhoan.SelectedIndex = -1; // Không có giá trị nào được chọn
                }

            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xem chi tiết!");
            }
        }
    }
}
