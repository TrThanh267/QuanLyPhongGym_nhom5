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
    public partial class UserControlQuanLyHoaDon : UserControl
    {
        QlGymContext _db = new QlGymContext();
        QuanLyHoaDon_DAL QuanLyHoaDon_DAL;
        QuanLyChiTietHoaDon_DAL QuanLyChiTietHoaDon_DAL;
        public UserControlQuanLyHoaDon()
        {
            InitializeComponent();
            QuanLyHoaDon_DAL = new QuanLyHoaDon_DAL(_db);
            QuanLyChiTietHoaDon_DAL = new QuanLyChiTietHoaDon_DAL(_db);
            laodtable();
            loadtableCTHD();
        }
        public void laodtable()
        {
            var listHoaDon = QuanLyHoaDon_DAL.GetHoaDonsWithKhachHang();
            dataGridViewHD.DataSource = listHoaDon;
            dataGridViewHD.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            comboBoxmaKH.DataSource = _db.KhachHangs.ToList();
            comboBoxmaKH.DisplayMember = "HoTen";
            comboBoxmaKH.ValueMember = "MaKh";

            comboBoxMaNV.DataSource = _db.NhanViens.ToList();
            comboBoxMaNV.DisplayMember = "TenNhanVien";
            comboBoxMaNV.ValueMember = "MaNv";
        }
        public void loadtableCTHD()
        {
            var listChiTietHoaDon = QuanLyChiTietHoaDon_DAL.GetHoaDonChiTiets();
            dataGridViewCTHD.DataSource = listChiTietHoaDon;
            dataGridViewCTHD.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            comboBoxMaHD.DataSource = _db.HoaDons.ToList();
            comboBoxMaHD.ValueMember = "MaHd";
            comboBoxMaHD.DisplayMember = "MaHd";

            comboBoxmaDichVu.DataSource = _db.DichVus.ToList();
            comboBoxmaDichVu.DisplayMember = "TenDv";
            comboBoxmaDichVu.ValueMember = "MaDv";

            comboBoxMaGoiTap.DataSource = _db.GoiTaps.ToList();
            comboBoxMaGoiTap.DisplayMember = "TenGoiTap";
            comboBoxMaGoiTap.ValueMember = "MaGoiTap";
        }

        private void guna2ButtonThemHD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxHinhThucTT.Text) || comboBoxmaKH.SelectedItem == null || comboBoxMaNV.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                return;
            }
            else
            {
                HoaDon hoaDon = new HoaDon
                {
                    MaKh = (int)comboBoxmaKH.SelectedValue,
                    MaNv = (int)comboBoxMaNV.SelectedValue,
                    NgayTao = DateOnly.FromDateTime(dateTimePickerNgayTao.Value),
                    HinhThucThanhToan = textBoxHinhThucTT.Text,
                };
                if (QuanLyHoaDon_DAL.ThemHoaDon(hoaDon))
                {
                    MessageBox.Show("Thêm hóa đơn thành công!");
                    laodtable();
                }
                else
                {
                    MessageBox.Show("Thêm hóa đơn thất bại!");
                }

            }
        }

        private void una2ButtonXoaHD_Click(object sender, EventArgs e)
        {
            if (dataGridViewHD.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để xóa!");
                return;
            }
            else
            {
                HoaDon hoaDon = new HoaDon
                {
                    MaHd = int.Parse(dataGridViewHD.SelectedRows[0].Cells[0].Value.ToString()),
                    MaKh = (int)comboBoxmaKH.SelectedValue,
                    MaNv = (int)comboBoxMaNV.SelectedValue,
                    NgayTao = DateOnly.FromDateTime(dateTimePickerNgayTao.Value),
                    HinhThucThanhToan = textBoxHinhThucTT.Text,
                };
                if (QuanLyHoaDon_DAL.XoaHoaDon(hoaDon))
                {
                    MessageBox.Show("Xóa hóa đơn thành công!");
                    laodtable();
                }
                else
                {
                    MessageBox.Show("Xóa hóa đơn thất bại!");
                }
            }
        }

        private void una2ButtonSuaHD_Click(object sender, EventArgs e)
        {
            if (dataGridViewHD.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để sửa!");
                return;
            }
            else if (string.IsNullOrEmpty(textBoxHinhThucTT.Text) || comboBoxmaKH.SelectedItem == null || comboBoxMaNV.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                return;
            }
            else
            {
                HoaDon hoaDon = new HoaDon
                {
                    MaHd = int.Parse(dataGridViewHD.SelectedRows[0].Cells[0].Value.ToString()),
                    MaKh = (int)comboBoxmaKH.SelectedValue,
                    MaNv = (int)comboBoxMaNV.SelectedValue,
                    NgayTao = DateOnly.FromDateTime(dateTimePickerNgayTao.Value),
                    HinhThucThanhToan = textBoxHinhThucTT.Text,
                };
                if (QuanLyHoaDon_DAL.SuaHoaDon(hoaDon))
                {
                    MessageBox.Show("Sửa hóa đơn thành công!");
                    laodtable();
                }
                else
                {
                    MessageBox.Show("Sửa hóa đơn thất bại!");
                }
            }
        }

        private void guna2ButtonTkHD_Click(object sender, EventArgs e)
        {
            string searchText = textBoxTKHD.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                var hoaDon = QuanLyHoaDon_DAL.GetHoaDonsWithKhachHang();
                dataGridViewHD.DataSource = hoaDon;
            }
        }

        private void dataGridViewHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedRow = dataGridViewHD.SelectedRows[e.RowIndex];
            if (selectedRow != null)
            {
                textBoxHinhThucTT.Text = selectedRow.Cells["HinhThucThanhToan"].Value.ToString();
                comboBoxmaKH.SelectedValue = selectedRow.Cells["MaKh"].Value;
                comboBoxMaNV.SelectedValue = selectedRow.Cells["MaNv"].Value;
                dateTimePickerNgayTao.Value = DateTime.Parse(selectedRow.Cells["NgayTao"].Value.ToString());
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xem chi tiết.");
            }
        }

        private void guna2ButtonThemCTHD_Click(object sender, EventArgs e)
        {
            if ((comboBoxMaHD.SelectedIndex == null) && (comboBoxmaDichVu.SelectedIndex == null) || (comboBoxMaGoiTap.SelectedItem == null) || string.IsNullOrEmpty(textBoxSoLuong.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                return;
            }
            else
            {
                HoaDonChiTiet hoaDonChiTiet = new HoaDonChiTiet
                {
                    MaHd = (int?)comboBoxMaHD.SelectedValue,
                    MaGoiTap = (int?)comboBoxMaGoiTap.SelectedValue,
                    MaDv = (int?)comboBoxmaDichVu.SelectedValue,
                    SoLuong = int.TryParse(textBoxSoLuong.Text, out int soLuong) ? soLuong : (int?)null,
                };
                if (QuanLyChiTietHoaDon_DAL.AddChiTietHoaDon(hoaDonChiTiet))
                {
                    MessageBox.Show("Thêm chi tiết hóa đơn thành công!");
                    loadtableCTHD();
                }
                else
                {
                    MessageBox.Show("Thêm chi tiết hóa đơn thất bại!");
                }
            }
        }

        private void una2ButtonXoaCTHD_Click(object sender, EventArgs e)
        {
            if (dataGridViewCTHD.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn chi tiết hóa đơn để xóa!");
                return;
            }
            else
            {
                HoaDonChiTiet hoaDonChiTiet = new HoaDonChiTiet
                {
                    MaHdct = int.Parse(dataGridViewCTHD.SelectedRows[0].Cells[0].Value.ToString()),
                    MaHd = (int?)comboBoxMaHD.SelectedValue,
                    MaGoiTap = (int?)comboBoxMaGoiTap.SelectedValue,
                    MaDv = (int?)comboBoxmaDichVu.SelectedValue,
                    SoLuong = int.TryParse(textBoxSoLuong.Text, out int soLuong) ? soLuong : (int?)null,
                };
                if (QuanLyChiTietHoaDon_DAL.DeleteChiTietHoaDon(hoaDonChiTiet))
                {
                    MessageBox.Show("Xóa chi tiết hóa đơn thành công!");
                    loadtableCTHD();
                }
                else
                {
                    MessageBox.Show("Xóa chi tiết hóa đơn thất bại!");
                }
            }
        }

        private void una2ButtonSuaCTHD_Click(object sender, EventArgs e)
        {
            if (dataGridViewCTHD.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn chi tiết hóa đơn để sửa!");
            }
            else
            {
                HoaDonChiTiet hoaDonChiTiet = new HoaDonChiTiet
                {
                    MaHdct = int.Parse(dataGridViewCTHD.SelectedRows[0].Cells[0].Value.ToString()),
                    MaHd = (int?)comboBoxMaHD.SelectedValue,
                    MaGoiTap = (int?)comboBoxMaGoiTap.SelectedValue,
                    MaDv = (int?)comboBoxmaDichVu.SelectedValue,
                    SoLuong = int.TryParse(textBoxSoLuong.Text, out int soLuong) ? soLuong : (int?)null,
                };
                if (QuanLyChiTietHoaDon_DAL.UpdateChiTietHoaDon(hoaDonChiTiet))
                {
                    MessageBox.Show("Sửa chi tiết hóa đơn thành công!");
                    loadtableCTHD();
                }
                else
                {
                    MessageBox.Show("Sửa chi tiết hóa đơn thất bại!");
                }
            }
        }

        private void dataGridViewCTHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedRow = dataGridViewCTHD.SelectedRows[e.RowIndex];
            if (selectedRow != null)
            {
                comboBoxMaHD.SelectedValue = selectedRow.Cells["MaHd"].Value;
                comboBoxmaDichVu.SelectedValue = selectedRow.Cells["MaDv"].Value;
                comboBoxMaGoiTap.SelectedValue = selectedRow.Cells["MaGoiTap"].Value;
                textBoxSoLuong.Text = selectedRow.Cells["SoLuong"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một chi tiết hóa đơn để xem.");
            }
        }

        private void guna2ButtonTkCTHD_Click(object sender, EventArgs e)
        {
            string searchText = textBoxTKCTHD.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                var chiTietHoaDon = QuanLyChiTietHoaDon_DAL.GetHoaDonChiTiets();
                dataGridViewCTHD.DataSource = chiTietHoaDon;
            }
            else
            {
                MessageBox.Show("Nhập thông tin cần tiềm kiếm");
            }
        }
    }
}
