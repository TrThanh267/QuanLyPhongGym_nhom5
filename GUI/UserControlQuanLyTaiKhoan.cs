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
    public partial class UserControlQuanLyTaiKhoan : UserControl
    {
        QlGymContext _db = new QlGymContext();
        QuanLyTaiKhoan_DAL _QuanLyTaiKhoan_DAL;
        public UserControlQuanLyTaiKhoan()
        {
            InitializeComponent();
            _QuanLyTaiKhoan_DAL = new QuanLyTaiKhoan_DAL(_db);
            loadData();
        }
        public void loadData()
        {
            var listTaiKhoan = _QuanLyTaiKhoan_DAL.GetAllTaiKhoans();
            dataGridTaiKhoan.DataSource = listTaiKhoan;
            dataGridTaiKhoan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            comboBoxVaiTro.DataSource = _db.VaiTros.ToList();
            comboBoxVaiTro.DisplayMember = "TenVaiTro";
            comboBoxVaiTro.ValueMember = "MaVaiTro";
        }

        private void guna2ButtonThemTB_Click(object sender, EventArgs e)
        {
            if (textBoxTenTK.Text == "" || textBoxMK.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            else
            {
                TaiKhoan taiKhoan = new TaiKhoan
                {
                    TenTaiKhoan = textBoxTenTK.Text,
                    MatKhau = textBoxMK.Text,
                    MaVaiTro = (int)comboBoxVaiTro.SelectedValue
                };
                if (_QuanLyTaiKhoan_DAL.AddTaiKhoan(taiKhoan))
                {
                    MessageBox.Show("Thêm tài khoản thành công!");
                    loadData();
                }
                else
                {
                    MessageBox.Show("Thêm tài khoản thất bại!");
                }
            }
        }

        private void una2ButtonXoaTK_Click(object sender, EventArgs e)
        {
            if (dataGridTaiKhoan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản để xóa!");
                return;
            }
            else
            {
                TaiKhoan taiKhoan = new TaiKhoan
                {
                    TenTaiKhoan = textBoxTenTK.Text,
                    MatKhau = textBoxMK.Text,
                    MaVaiTro = (int)comboBoxVaiTro.SelectedValue
                };
                if (_QuanLyTaiKhoan_DAL.DeleteTaiKhoan(taiKhoan))
                {
                    MessageBox.Show("Xóa tài khoản thành công!");
                    loadData();
                }
                else
                {
                    MessageBox.Show("Xóa tài khoản thất bại!");
                }
            }
        }

        private void una2ButtonSuaTK_Click(object sender, EventArgs e)
        {
            if (dataGridTaiKhoan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản để sửa!");
                return;
            }
            else
            {
                TaiKhoan taiKhoan = new TaiKhoan
                {
                    TenTaiKhoan = textBoxTenTK.Text,
                    MatKhau = textBoxMK.Text,
                    MaVaiTro = (int)comboBoxVaiTro.SelectedValue
                };
                if (_QuanLyTaiKhoan_DAL.UpdateTaiKhoan(taiKhoan))
                {
                    MessageBox.Show("Sửa tài khoản thành công!");
                    loadData();
                }
                else
                {
                    MessageBox.Show("Sửa tài khoản thất bại!");
                }
            }
        }

        private void guna2ButtonTkTB_Click(object sender, EventArgs e)
        {
            string searchText = textBoxTKTB.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản để tìm kiếm!");
                return;
            }
            else
            {
                var result = _QuanLyTaiKhoan_DAL.timkiemTK(searchText);
                if (result.Count > 0)
                {
                    dataGridTaiKhoan.DataSource = result;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tài khoản nào với tên: " + searchText);
                    loadData();
                }
            }
        }

        private void dataGridTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && e.RowIndex < dataGridTaiKhoan.Rows.Count)
            {
                var row = dataGridTaiKhoan.Rows[e.RowIndex];
                textBoxTenTK.Text = row.Cells["TenTaiKhoan"].Value.ToString();
                textBoxMK.Text = row.Cells["MatKhau"].Value.ToString();
                comboBoxVaiTro.SelectedValue = row.Cells["MaVaiTro"].Value;
            }
        }
    }
}
