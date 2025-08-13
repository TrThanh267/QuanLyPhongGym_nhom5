using Microsoft.EntityFrameworkCore;
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
        HienThiDangKy_DAL HienThiDangKy_DAL;
        public UserControlQuanLyHoaDon()
        {
            InitializeComponent();
            QuanLyHoaDon_DAL = new QuanLyHoaDon_DAL(_db);
            QuanLyChiTietHoaDon_DAL = new QuanLyChiTietHoaDon_DAL(_db);
            HienThiDangKy_DAL = new HienThiDangKy_DAL(_db);
            laodtable();
            loadtableCTHD();
        }
        public void laodtable()
        {
            var listHoaDon = QuanLyHoaDon_DAL.GetHoaDonsWithKhachHang();
            dataGridViewHD.DataSource = listHoaDon;
            dataGridViewHD.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            var dangKyList = HienThiDangKy_DAL.GetDangKy();
            dataGridViewDangKyGTHD.DataSource = dangKyList;

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

            var dangKyList = HienThiDangKy_DAL.GetDangKy();
            dataGridViewDangKyHDCT.DataSource = dangKyList;



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
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn thêm hóa đơn này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.No)
                return;

            HoaDon hoaDon = new HoaDon
            {
                MaKh = (int)comboBoxmaKH.SelectedValue,
                MaNv = (int)comboBoxMaNV.SelectedValue,
                NgayTao = DateOnly.FromDateTime(dateTimePickerNgayTao.Value),
                HinhThucThanhToan = textBoxHinhThucTT.Text,
            };

            if (QuanLyHoaDon_DAL.ThemHoaDon(hoaDon))
            {
                MessageBox.Show("Thêm hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                laodtable();
            }
            else
            {
                MessageBox.Show("Thêm hóa đơn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void una2ButtonXoaHD_Click(object sender, EventArgs e)
        {
            if (dataGridViewHD.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa hóa đơn này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.No)
                return;

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
                MessageBox.Show("Xóa hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                laodtable();
            }
            else
            {
                MessageBox.Show("Xóa hóa đơn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void una2ButtonSuaHD_Click(object sender, EventArgs e)
        {
            if (dataGridViewHD.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrEmpty(textBoxHinhThucTT.Text) || comboBoxmaKH.SelectedItem == null || comboBoxMaNV.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn cập nhật hóa đơn này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.No)
                return;

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
                MessageBox.Show("Sửa hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                laodtable();
            }
            else
            {
                MessageBox.Show("Sửa hóa đơn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridViewHD.Rows[e.RowIndex];

                textBoxHinhThucTT.Text = selectedRow.Cells["HinhThucThanhToan"].Value?.ToString();
                comboBoxmaKH.SelectedValue = selectedRow.Cells["MaKh"].Value;
                comboBoxMaNV.SelectedValue = selectedRow.Cells["MaNv"].Value;

                var cellValue = selectedRow.Cells["NgayTao"].Value;
                DateTime minDate = dateTimePickerNgayTao.MinDate;

                if (cellValue is DateOnly dateOnly)
                {
                    var dt = dateOnly.ToDateTime(TimeOnly.MinValue);
                    if (dt >= minDate && dt <= dateTimePickerNgayTao.MaxDate)
                    {
                        dateTimePickerNgayTao.Value = dt;
                    }
                }
                else if (cellValue is DateTime dateTime)
                {
                    if (dateTime >= minDate && dateTime <= dateTimePickerNgayTao.MaxDate)
                    {
                        dateTimePickerNgayTao.Value = dateTime;
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn hợp lệ.");
            }
        }

        private void guna2ButtonThemCTHD_Click(object sender, EventArgs e)
        {
            if (comboBoxMaHD.SelectedItem == null|| string.IsNullOrEmpty(textBoxSoLuong.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if((int)comboBoxMaGoiTap.SelectedValue == 27 && (int)comboBoxmaDichVu.SelectedValue == 24)
            {
                MessageBox.Show("Chọn Gói tập hoặc dịch vụ đã đăng kí");
                return;
            }

            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn thêm chi tiết hóa đơn này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.No)
                return;

            HoaDonChiTiet hoaDonChiTiet = new HoaDonChiTiet
            {
                MaHd = (int?)comboBoxMaHD.SelectedValue,
                MaGoiTap = (int?)comboBoxMaGoiTap.SelectedValue,
                MaDv = (int?)comboBoxmaDichVu.SelectedValue,
                SoLuong = int.TryParse(textBoxSoLuong.Text, out int soLuong) ? soLuong : (int?)null,
            };

            if (QuanLyChiTietHoaDon_DAL.AddChiTietHoaDon(hoaDonChiTiet))
            {
                MessageBox.Show("Thêm chi tiết hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadtableCTHD();
            }
            else
            {
                MessageBox.Show("Thêm chi tiết hóa đơn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void una2ButtonXoaCTHD_Click(object sender, EventArgs e)
        {
            if (dataGridViewCTHD.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn chi tiết hóa đơn để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa chi tiết hóa đơn này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.No)
                return;

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
                MessageBox.Show("Xóa chi tiết hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadtableCTHD();
            }
            else
            {
                MessageBox.Show("Xóa chi tiết hóa đơn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void una2ButtonSuaCTHD_Click(object sender, EventArgs e)
        {
            if (dataGridViewCTHD.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn chi tiết hóa đơn để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn cập nhật chi tiết hóa đơn này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.No)
                return;

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
                MessageBox.Show("Sửa chi tiết hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadtableCTHD();
            }
            else
            {
                MessageBox.Show("Sửa chi tiết hóa đơn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridViewCTHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex>=0)
            {
                var selectedRow = dataGridViewCTHD.Rows[e.RowIndex];
                comboBoxMaHD.SelectedValue = selectedRow.Cells["MaHd"].Value;
                comboBoxmaDichVu.SelectedValue = selectedRow.Cells["MaDv"].Value;
                comboBoxMaGoiTap.SelectedValue = selectedRow.Cells["MaGoiTap"].Value;
                textBoxSoLuong.Text = selectedRow.Cells["SoLuong"].Value?.ToString() ?? "";
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

        private void dataGridViewDangKyGTHD_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void dataGridViewDangKyHDCT_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            

        }
    }
}
