using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyPhongGym_nhom5.DAL;
using QuanLyPhongGym_nhom5.Models;

namespace QuanLyPhongGym_nhom5.GUI
{
    public partial class UserControlDichVu : UserControl
    {
        QlGymContext _context = new QlGymContext();
        QuanLyDichVu_DAL _quanLyDichVuDAL;
        QuanLyGoiTap_DAL _QuanLyGoiTap_DAL;
        public UserControlDichVu()
        {
            InitializeComponent();
            _quanLyDichVuDAL = new QuanLyDichVu_DAL(_context);
            _QuanLyGoiTap_DAL = new QuanLyGoiTap_DAL(_context);
            LoadTable();
        }
        public void LoadTable()
        {

            dataGridViewQLDV.DataSource = _quanLyDichVuDAL.GetAllDichVu();
            dataGridViewQLDV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridGoiTap.DataSource = _QuanLyGoiTap_DAL.GetAllGoiTap();
            dataGridGoiTap.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void ButtonThemDV_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn thêm dịch vụ này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.No)
                return;

            DichVu dichVu = new DichVu
            {
                TenDv = textBoxTenDV.Text,
                Gia = decimal.TryParse(textBoxDonGiaDV.Text.Trim(), out decimal gia) ? gia : (decimal?)null,
                SoBuoiDk = int.TryParse(textBoxBuoiDkDV.Text.Trim(), out int soBuoi) ? soBuoi : (int?)null
            };

            if (_quanLyDichVuDAL.themdichvu(dichVu))
            {
                MessageBox.Show("Thêm dịch vụ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTable();
            }
            else
            {
                MessageBox.Show("Lỗi khi thêm dịch vụ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonXoaDV_Click(object sender, EventArgs e)
        {
            if (dataGridViewQLDV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            var confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa dịch vụ này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmResult == DialogResult.No)
                return;
            var dichVu = (DichVu)dataGridViewQLDV.SelectedRows[0].DataBoundItem;
            int maDv = dichVu.MaDv;
            var cellValue = dataGridViewQLDV.SelectedRows[0].Cells["MaDv"].Value;
            MessageBox.Show($"Giá trị MaDv lấy được: {cellValue}");
            if (_quanLyDichVuDAL.DeleteDichVu(maDv))
            {
                LoadTable();
                MessageBox.Show("Xóa dịch vụ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lỗi khi xóa dịch vụ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonSuaDV_Click(object sender, EventArgs e)
        {
            if (dataGridViewQLDV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn cập nhật dịch vụ này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.No)
                return;

            DichVu dichVu = new DichVu
            {
                MaDv = Convert.ToInt32(dataGridViewQLDV.SelectedRows[0].Cells["MaDv"].Value),
                TenDv = textBoxTenDV.Text.Trim(),
                Gia = decimal.TryParse(textBoxDonGiaDV.Text.Trim(), out decimal gia) ? gia : (decimal?)null,
                SoBuoiDk = int.TryParse(textBoxBuoiDkDV.Text.Trim(), out int soBuoi) ? soBuoi : (int?)null
            };

            if (_quanLyDichVuDAL.capnhapDV(dichVu))
            {
                MessageBox.Show("Cập nhật dịch vụ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTable();
            }
            else
            {
                MessageBox.Show("Lỗi khi cập nhật dịch vụ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void buttonTKDV_Click(object sender, EventArgs e)
        {
            string searchSV = textBoxTimKiemDV.Text.Trim();
            if (string.IsNullOrEmpty(searchSV))
            {
                MessageBox.Show("Vui lòng nhập tên dịch vụ để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                var dichVus = _quanLyDichVuDAL.SearchDichVu(searchSV);
                dataGridViewQLDV.DataSource = dichVus;
            }
        }


        private void ButtonThemGT_Click(object sender, EventArgs e)
        {

            GoiTap goiTap = new GoiTap
            {
                TenGoiTap = textBoxTenGT.Text.Trim(),
                Gia = decimal.TryParse(textBoxDonGiaGT.Text.Trim(), out decimal gia) ? gia : (decimal?)null,
                ThoiHan = int.TryParse(textBoxThoiHanGT.Text.Trim(), out int soBuoi) ? soBuoi : (int?)null
            };
            if (_QuanLyGoiTap_DAL.ThemGoiTap(goiTap))
            {
                MessageBox.Show("Thêm gói tập thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTable();
            }
            else
            {
                MessageBox.Show("Lỗi khi thêm gói tập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ButtonXoaGT_Click(object sender, EventArgs e)
        {
            if (dataGridGoiTap.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn gói tập để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                GoiTap goiTap = new GoiTap
                {
                    MaGoiTap = Convert.ToInt32(dataGridGoiTap.SelectedRows[0].Cells["MaGoiTap"].Value),
                    TenGoiTap = textBoxTenGT.Text.Trim(),
                    Gia = decimal.TryParse(textBoxDonGiaGT.Text.Trim(), out decimal gia) ? gia : (decimal?)null,
                    ThoiHan = int.TryParse(textBoxThoiHanGT.Text.Trim(), out int soBuoi) ? soBuoi : (int?)null
                };
                if (_QuanLyGoiTap_DAL.XoaGoiTap(goiTap))
                {
                    MessageBox.Show("Xóa gói tập thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTable();
                }
                else
                {
                    MessageBox.Show("Lỗi khi xóa gói tập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonSuaGT_Click(object sender, EventArgs e)
        {
            if (dataGridGoiTap.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn gói tập để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                GoiTap goiTap = new GoiTap
                {
                    MaGoiTap = Convert.ToInt32(dataGridGoiTap.SelectedRows[0].Cells["MaGoiTap"].Value),
                    TenGoiTap = textBoxTenGT.Text.Trim(),
                    Gia = decimal.TryParse(textBoxDonGiaGT.Text.Trim(), out decimal gia) ? gia : (decimal?)null,
                    ThoiHan = int.TryParse(textBoxThoiHanGT.Text.Trim(), out int soBuoi) ? soBuoi : (int?)null
                };
                if (_QuanLyGoiTap_DAL.CapNhatGoiTap(goiTap))
                {
                    MessageBox.Show("Cập nhật gói tập thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTable();
                }
                else
                {
                    MessageBox.Show("Lỗi khi cập nhật gói tập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonTkGT_Click(object sender, EventArgs e)
        {
            string searchGT = textBoxTimKiemGT.Text.Trim();
            if (string.IsNullOrEmpty(searchGT))
            {
                MessageBox.Show("Vui lòng nhập tên gói tập để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                var goiTaps = _QuanLyGoiTap_DAL.TimKiemGoiTap(searchGT);
                dataGridGoiTap.DataSource = goiTaps;
            }
        }


        private void dataGridViewQLDV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridViewQLDV.Rows.Count)
            {
                var row = dataGridViewQLDV.Rows[e.RowIndex];
                textBoxTenDV.Text = row.Cells["TenDv"].Value?.ToString() ?? string.Empty;
                textBoxDonGiaDV.Text = row.Cells["Gia"].Value?.ToString() ?? string.Empty;
                textBoxBuoiDkDV.Text = row.Cells["SoBuoiDk"].Value?.ToString() ?? string.Empty;
            }
        }

        private void dataGridGoiTap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridGoiTap.Rows.Count)
            {
                var row = dataGridGoiTap.Rows[e.RowIndex];
                textBoxTenGT.Text = row.Cells["TenGoiTap"].Value?.ToString() ?? string.Empty;
                textBoxDonGiaGT.Text = row.Cells["Gia"].Value?.ToString() ?? string.Empty;
                textBoxThoiHanGT.Text = row.Cells["ThoiHan"].Value?.ToString() ?? string.Empty;
            }
        }
    }
}
