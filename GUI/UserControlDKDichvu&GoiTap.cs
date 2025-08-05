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
        }
        public void loadData()
        {
            var listDangKyGoiTap = _QuanLyDangKyGoiTap_DAL.LayTatCa();
            dataGridViewDKGT.DataSource = listDangKyGoiTap;
            dataGridViewDKGT.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            comboBoxMaKHGT.DataSource = _db.KhachHangs.ToList();
            comboBoxMaKHGT.DisplayMember = "TenKh";
            comboBoxMaKHGT.ValueMember = "MaKh";

            comboBoxMaGT.DataSource = _db.GoiTaps.ToList();
            comboBoxMaGT.DisplayMember = "TenGoiTap";
            comboBoxMaGT.ValueMember = "MaGoiTap";

            var listDangKyDichVu = _dangKyDichVu_DAL.GetAll();
            dataGridViewDKDV.DataSource = listDangKyDichVu;
            dataGridViewDKDV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            comboBoxMaKHDV.DataSource = _db.KhachHangs.ToList();
            comboBoxMaKHDV.DisplayMember = "TenKh";
            comboBoxMaKHDV.ValueMember = "MaKh";

            comboBoxMaDV.DataSource = _db.DichVus.ToList();
            comboBoxMaDV.DisplayMember = "TenDv";
            comboBoxMaDV.ValueMember = "MaDv";
        }

        private void guna2ButtonMuaGoi_Click(object sender, EventArgs e)
        {
            if (comboBoxMaKHGT.SelectedValue == null || comboBoxMaGT.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng và gói tập!");
                return;
            }
            else
            {
                DangKyGoiTap dangKyGoiTap = new DangKyGoiTap
                {
                    MaKh = (int)comboBoxMaKHGT.SelectedValue,
                    MaGoiTap = (int)comboBoxMaGT.SelectedValue,
                    NgayBatDau = DateOnly.FromDateTime(dateTimePickerBDGT.Value),
                    NgayKetThuc = DateOnly.FromDateTime(dateTimePickerKTGT.Value),
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
                DangKyGoiTap dangKyGoiTap = new DangKyGoiTap
                {

                    MaKh = (int)comboBoxMaKHGT.SelectedValue,
                    MaGoiTap = (int)comboBoxMaGT.SelectedValue,
                    NgayBatDau = DateOnly.FromDateTime(dateTimePickerBDGT.Value),
                    NgayKetThuc = DateOnly.FromDateTime(dateTimePickerKTGT.Value),
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
            if (comboBoxMaKHDV.SelectedValue == null || comboBoxMaDV.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng và dịch vụ!");
                return;
            }
            else
            {
                DangKyDichVu dangKyDichVu = new DangKyDichVu
                {
                    MaKh = (int)comboBoxMaKHDV.SelectedValue,
                    MaDv = (int)comboBoxMaDV.SelectedValue,
                    NgayBatDau = DateOnly.FromDateTime(dateTimePickerBDDV.Value),
                    NgayKetThuc = DateOnly.FromDateTime(dateTimePickerKTDV.Value),
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
                DangKyDichVu dangKyDichVu = new DangKyDichVu
                {
                    MaKh = (int)comboBoxMaKHDV.SelectedValue,
                    MaDv = (int)comboBoxMaDV.SelectedValue,
                    NgayBatDau = DateOnly.FromDateTime(dateTimePickerBDDV.Value),
                    NgayKetThuc = DateOnly.FromDateTime(dateTimePickerKTDV.Value),
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
            if (dataGridViewDKDV.SelectedRows.Count > 0)
            {
                var row = dataGridViewDKDV.SelectedRows[e.RowIndex];
                comboBoxMaKHDV.SelectedValue = row.Cells["MaKh"].Value;
                comboBoxMaDV.SelectedValue = row.Cells["MaDv"].Value;
                dateTimePickerBDDV.Value = DateTime.Parse(row.Cells["NgayBatDau"].Value.ToString());
                dateTimePickerKTDV.Value = DateTime.Parse(row.Cells["NgayKetThuc"].Value.ToString());
            }
        }

        private void dataGridViewDKGT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridViewDKGT.SelectedRows.Count > 0)
            {
                var row = dataGridViewDKGT.SelectedRows[e.RowIndex];
                comboBoxMaKHGT.SelectedValue = row.Cells["MaKh"].Value;
                comboBoxMaGT.SelectedValue = row.Cells["MaGoiTap"].Value;
                dateTimePickerBDGT.Value = DateTime.Parse(row.Cells["NgayBatDau"].Value.ToString());
                dateTimePickerKTGT.Value = DateTime.Parse(row.Cells["NgayKetThuc"].Value.ToString());
            }
        }
    }
}
