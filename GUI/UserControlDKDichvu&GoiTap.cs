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
    public partial class UserControlDKDichvu_GoiTap : UserControl
    {
        QlGymContext _db = new QlGymContext();
        QuanLyDangKyGT _QuanLyDangKyGoiTap_DAL;
        DangKyDichVu_DAL _dangKyDichVu_DAL;
        public UserControlDKDichvu_GoiTap()
        {
            InitializeComponent();
            _QuanLyDangKyGoiTap_DAL = new QuanLyDangKyGT(_db);
            _dangKyDichVu_DAL = new DangKyDichVu_DAL(_db);
            loadData();
            if (NguoiDung.nguoidunghientai.TenTaiKhoan == null)
            {
                MessageBox.Show("Bạn chưa đăng nhập!");
                return;
            }
        }
        public void loadData()
        {
            var listDangKyGoiTap = _QuanLyDangKyGoiTap_DAL.LayTatCa();
            dataGridViewDKGT.DataSource = listDangKyGoiTap;
            dataGridViewDKGT.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            var maKH = _dangKyDichVu_DAL.LayMaKHTheoTenDangNhap(NguoiDung.nguoidunghientai.TenTaiKhoan);
            if (maKH.HasValue)
            {
                textBoxKH.Text = maKH.Value.ToString();
            }
            else
            {
                textBoxKH.Text = "Tai khoan chua duoc thay";
            }
            dateTimePickerBDGT.Value = DateTime.Now;
            dateTimePickerBDDV.Value = DateTime.Now;

            comboBoxMaGT.DataSource = _db.GoiTaps.ToList();
            comboBoxMaGT.DisplayMember = "TenGoiTap";
            comboBoxMaGT.ValueMember = "MaGoiTap";

            var listDangKyDichVu = _dangKyDichVu_DAL.GetAll();
            dataGridViewDKDV.DataSource = listDangKyDichVu;
            dataGridViewDKDV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            comboBoxMaDV.DataSource = _db.DichVus.ToList();
            comboBoxMaDV.DisplayMember = "TenDv";
            comboBoxMaDV.ValueMember = "MaDv";
        }

        private void guna2ButtonMuaGoi_Click(object sender, EventArgs e)
        {
            if (comboBoxMaGT.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng và gói tập!");
                return;
            }
            else
            {
                DateTime batDau = dateTimePickerBDGT.Value;
                DateTime ketThuc = batDau;

                int goiTap = (int)comboBoxMaGT.SelectedValue;
                switch (goiTap)
                {
                    case 2:
                        ketThuc = batDau.AddDays(30);
                        break;
                    case 3:
                        ketThuc = batDau.AddDays(65);
                        break;
                    case 4:
                        ketThuc = batDau.AddDays(100);
                        break;
                    case 5:
                        ketThuc = batDau.AddDays(145);
                        break;
                }
                var confirm = MessageBox.Show("Bạn có chắc muốn đăng ký gói tập này không?", "Xác nhận đăng ký", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.No)
                    return;

                DangKyGoiTap dangKyGoiTap = new DangKyGoiTap
                {
                    MaKh = int.Parse(textBoxKH.Text),
                    MaGoiTap = (int)comboBoxMaGT.SelectedValue,
                    NgayBatDau = DateOnly.FromDateTime(batDau),
                    NgayKetThuc = DateOnly.FromDateTime(ketThuc),
                    TrangThai = ketThuc > DateTime.Now ? true : false
                };
                if (_QuanLyDangKyGoiTap_DAL.Them(dangKyGoiTap))
                {
                    MessageBox.Show("Đăng ký gói tập thành công!");
                    loadData();
                }
                else
                {
                    MessageBox.Show("Đăng ký gói tập thất bại!");
                }
            }
        }

        private void guna2ButtonHuyGoi_Click(object sender, EventArgs e)
        {
            if (dataGridViewDKGT.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đăng ký gói tập để hủy!");
                return;
            }
            else
            {
                var confirm = MessageBox.Show("Bạn có chắc muốn hủy đăng ký gói tập này không?", "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.No)
                    return;
                DangKyGoiTap dangKyGoiTap = new DangKyGoiTap
                {

                    MaKh = int.Parse(textBoxKH.Text),
                    MaGoiTap = (int)comboBoxMaGT.SelectedValue,
                    NgayBatDau = DateOnly.FromDateTime(dateTimePickerBDGT.Value),
                    NgayKetThuc = DateOnly.FromDateTime(dateTimePickerKTGT.Value),
                    TrangThai = dataGridViewDKGT.Columns["TrangThai"].ValueType == typeof(bool) ? (bool)dataGridViewDKGT.SelectedRows[0].Cells["TrangThai"].Value : false
                };
                if (_QuanLyDangKyGoiTap_DAL.Xoa(dangKyGoiTap.MaGoiTap, dangKyGoiTap.MaKh))
                {
                    MessageBox.Show("Hủy đăng ký gói tập thành công!");
                    loadData();
                }
                else
                {
                    MessageBox.Show("Hủy đăng ký gói tập thất bại!");
                }
            }
        }

        private void guna2ButtonMuaDv_Click(object sender, EventArgs e)
        {
            if (comboBoxMaDV.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng và dịch vụ!");
                return;
            }
            else
            {
                if (dateTimePickerKTDV.Value < dateTimePickerBDDV.Value)
                {
                    MessageBox.Show("Ngày kết thúc không thể nhỏ hơn ngày bắt đầu!");
                    return;
                }
                else if (dateTimePickerKTDV.Value == dateTimePickerBDDV.Value)
                {
                    MessageBox.Show("Ngày kết thúc không thể bằng ngày bắt đầu!");
                    return;
                }
                DateTime BatDau = dateTimePickerBDDV.Value;
                DateTime KetThuc = dateTimePickerKTDV.Value;

                int SoNgay = (KetThuc - BatDau).Days;

                var confirm = MessageBox.Show("Bạn có chắc muốn đăng ký dịch vụ này không?", "Xác nhận đăng ký", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.No)
                    return;
                DangKyDichVu dangKyDichVu = new DangKyDichVu
                {
                    MaKh = int.Parse(textBoxKH.Text),
                    MaDv = (int)comboBoxMaDV.SelectedValue,
                    NgayBatDau = DateOnly.FromDateTime(BatDau),
                    NgayKetThuc = DateOnly.FromDateTime(KetThuc),
                    TrangThai = KetThuc > DateTime.Now ? true : false
                };
                if (_dangKyDichVu_DAL.Them(dangKyDichVu))
                {
                    MessageBox.Show("Đăng ký dịch vụ thành công!");
                    loadData();
                }
                else
                {
                    MessageBox.Show("Đăng ký dịch vụ thất bại!");
                }
            }
        }

        private void guna2ButtonHuyDV_Click(object sender, EventArgs e)
        {
            if (dataGridViewDKDV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đăng ký dịch vụ để hủy!");
                return;
            }
            else
            {
                var confirm = MessageBox.Show("Bạn có chắc muốn hủy đăng ký dịch vụ này không?", "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.No)
                    return;
                DangKyDichVu dangKyDichVu = new DangKyDichVu
                {
                    MaKh = int.Parse(textBoxKH.Text),
                    MaDv = (int)comboBoxMaDV.SelectedValue,
                    NgayBatDau = DateOnly.FromDateTime(dateTimePickerBDDV.Value),
                    NgayKetThuc = DateOnly.FromDateTime(dateTimePickerKTDV.Value),
                    TrangThai = dataGridViewDKDV.Columns["TrangThai"].ValueType == typeof(bool) ? (bool)dataGridViewDKDV.SelectedRows[0].Cells["TrangThai"].Value : false
                };
                if (_dangKyDichVu_DAL.Xoa(dangKyDichVu.MaDv, dangKyDichVu.MaKh))
                {
                    MessageBox.Show("Hủy đăng ký dịch vụ thành công!");
                    loadData();
                }
                else
                {
                    MessageBox.Show("Hủy đăng ký dịch vụ thất bại!");
                }
            }
        }

        private void dataGridViewDKDV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridViewDKDV.Rows[e.RowIndex];
                comboBoxMaDV.SelectedValue = row.Cells["MaDv"].Value;
                dateTimePickerBDDV.Value = DateTime.Parse(row.Cells["NgayBatDau"].Value.ToString());
                dateTimePickerKTDV.Value = DateTime.Parse(row.Cells["NgayKetThuc"].Value.ToString());
            }
        }

        private void dataGridViewDKGT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridViewDKGT.Rows[e.RowIndex];
                comboBoxMaGT.SelectedValue = row.Cells["MaGoiTap"].Value;
                dateTimePickerBDGT.Value = DateTime.Parse(row.Cells["NgayBatDau"].Value.ToString());
                dateTimePickerKTGT.Value = DateTime.Parse(row.Cells["NgayKetThuc"].Value.ToString());
            }
        }

        private void comboBoxMaGT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMaGT.SelectedValue != null && int.TryParse(comboBoxMaGT.SelectedValue.ToString(), out int GoiTap))
            {
                DateTime BatDau = DateTime.Today;
                DateTime KetThuc = BatDau;

                switch (GoiTap)
                {
                    case 2:
                        KetThuc = BatDau.AddDays(30);
                        break;
                    case 3:
                        KetThuc = BatDau.AddDays(65);
                        break;
                    case 4:
                        KetThuc = BatDau.AddDays(100);
                        break;
                    case 5:
                        KetThuc = BatDau.AddDays(145);
                        break;
                }

                dateTimePickerBDGT.Value = BatDau;
                dateTimePickerKTGT.Value = KetThuc;
            }
        }

        private void comboBoxMaDV_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            DateTime BatDau = dateTimePickerBDDV.Value;
            DateTime KetThuc = dateTimePickerKTDV.Value;

            int SoNgay = (BatDau - KetThuc).Days;


        }
    }
}
