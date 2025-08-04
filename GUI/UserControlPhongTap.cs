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
    public partial class UserControlPhongTap : UserControl
    {
        QlGymContext _db = new QlGymContext();
        QuanLyPhongTap_DAL _DALPhongTap;
        QuanLyThietBi_DAL _DALThietBi;
        public UserControlPhongTap()
        {
            InitializeComponent();
            _DALPhongTap = new QuanLyPhongTap_DAL(_db);
            _DALThietBi = new QuanLyThietBi_DAL(_db);
            loadtable();
        }
        private void loadtable()
        {
            var list = _DALPhongTap.GetAllchitietPhongTap();
            dataGridViewPT.DataSource = list;
            dataGridViewPT.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            var listThietBi = _DALThietBi.GetAllThietBi();
            dataGridThietBi.DataSource = listThietBi;
            dataGridThietBi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            comboBoxNV.DataSource = _db.NhanViens.ToList();
            comboBoxNV.DisplayMember = "TenNhanVien";
            comboBoxNV.ValueMember = "MaNv";

            comboBoxmaPT.DataSource = _db.PhongTaps.ToList();
            comboBoxmaPT.DisplayMember = "TenPhong";
            comboBoxmaPT.ValueMember = "MaPhong";

            comboBoxmaTB.DataSource = _db.ThietBis.ToList();
            comboBoxmaTB.DisplayMember = "TenThietBi";
            comboBoxmaTB.ValueMember = "MaThietBi";
        }

        private void guna2ButtonThemPT_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxThoiGianTap.Text) == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                return;
            }
            else
            {
                ChiTietPhongTap chiTietPhongTap = new ChiTietPhongTap
                {
                    MaPhong = (int)comboBoxmaPT.SelectedValue,
                    MaNv = (int)comboBoxNV.SelectedValue,
                    MaThietBi = (int)comboBoxmaTB.SelectedValue,
                    ThoiGianTap = int.Parse(textBoxThoiGianTap.Text),
                    TrangThai = radioButtonhd.Checked ? true : false
                };
                if (_DALPhongTap.ThemChiTietPhongTap(chiTietPhongTap))
                {
                    MessageBox.Show("Thêm thành công!");
                    loadtable();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại!");
                }
            }
        }

        private void una2ButtonXoaPT_Click(object sender, EventArgs e)
        {
            if (dataGridViewPT.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hàng để xóa!");
                return;
            }
            else
            {
                ChiTietPhongTap chiTietPhongTap = new ChiTietPhongTap
                {
                    MaPhong = (int)comboBoxmaPT.SelectedValue,
                    MaNv = (int)comboBoxNV.SelectedValue,
                    MaThietBi = (int)comboBoxmaTB.SelectedValue,
                    ThoiGianTap = int.Parse(textBoxThoiGianTap.Text),
                    TrangThai = radioButtonhd.Checked ? true : false
                };
                if (_DALPhongTap.XoaChiTietPhongTap(chiTietPhongTap))
                {
                    MessageBox.Show("Xóa thành công!");
                    loadtable();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
        }

        private void una2ButtonSuaPT_Click(object sender, EventArgs e)
        {
            if (dataGridViewPT.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hàng để sửa!");
                return;
            }
            else
            {
                ChiTietPhongTap chiTietPhongTap = new ChiTietPhongTap
                {
                    MaPhong = (int)comboBoxmaPT.SelectedValue,
                    MaNv = (int)comboBoxNV.SelectedValue,
                    MaThietBi = (int)comboBoxmaTB.SelectedValue,
                    ThoiGianTap = int.Parse(textBoxThoiGianTap.Text),
                    TrangThai = radioButtonhd.Checked ? true : false
                };
                if (_DALPhongTap.CapNhatChiTietPhongTap(chiTietPhongTap))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    loadtable();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!");
                }
            }
        }

        private void guna2ButtonTkPT_Click(object sender, EventArgs e)
        {
            string searchText = textBoxTKTB.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!");
                return;
            }
            else
            {
                var result = _DALPhongTap.timkiem(searchText);
                dataGridViewPT.DataSource = result;
            }
        }

        private void dataGridViewPT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewPT.Rows.Count >= 0)
            {
                comboBoxmaPT.SelectedValue = dataGridViewPT.Rows[e.RowIndex].Cells["MaPhong"].Value;
                comboBoxmaTB.SelectedValue = dataGridViewPT.Rows[e.RowIndex].Cells["MaThietBi"].Value;
                comboBoxNV.SelectedValue = dataGridViewPT.Rows[e.RowIndex].Cells["MaNv"].Value;
                textBoxThoiGianTap.Text = dataGridViewPT.Rows[e.RowIndex].Cells["ThoiGianTap"].Value.ToString();
                if (dataGridViewPT.Rows[e.RowIndex].Cells["TrangThai"].Value.ToString() == "True")
                {
                    radioButtonhd.Checked = true;
                }
                else
                {
                    radioButtonhd.Checked = false;
                }
            }
        }

        private void guna2ButtonThemTB_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxMaTB.Text) || string.IsNullOrEmpty(textBoxSoluong.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                return;
            }
            else
            {
                ThietBi thietBi = new ThietBi
                {
                    TenThietBi = textBoxMaTB.Text,
                    SoLuongThietBi = int.Parse(textBoxSoluong.Text)
                };
                if (_DALThietBi.ThemThietBi(thietBi))
                {
                    MessageBox.Show("Thêm thành công!");
                    loadtable();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại!");
                }
            }
        }

        private void una2ButtonXoaTB_Click(object sender, EventArgs e)
        {
            if (dataGridThietBi.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hàng để xóa!");
                return;
            }
            else
            {
                ThietBi thietBi = new ThietBi
                {
                    MaThietBi = (int)dataGridThietBi.SelectedRows[0].Cells["MaThietBi"].Value,
                    TenThietBi = textBoxMaTB.Text,
                    SoLuongThietBi = int.Parse(textBoxSoluong.Text)
                };
                if (_DALThietBi.XoaThietBi(thietBi))
                {
                    MessageBox.Show("Xóa thành công!");
                    loadtable();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
        }

        private void una2ButtonSuaTB_Click(object sender, EventArgs e)
        {
            if (dataGridThietBi.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hàng để sửa!");
                return;
            }
            else
            {
                ThietBi thietBi = new ThietBi
                {
                    MaThietBi = (int)dataGridThietBi.SelectedRows[0].Cells["MaThietBi"].Value,
                    TenThietBi = textBoxMaTB.Text,
                    SoLuongThietBi = int.Parse(textBoxSoluong.Text)
                };
                if (_DALThietBi.CapNhatThietBi(thietBi))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    loadtable();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!");
                }
            }
        }

        private void guna2ButtonTkTB_Click(object sender, EventArgs e)
        {
            string searchText = textBoxTKTB.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!");
                return;
            }
            else
            {
                var result = _DALThietBi.TimKiemThietBi(searchText);
                dataGridThietBi.DataSource = result;
            }
        }

        private void dataGridThietBi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridThietBi.Rows.Count >= 0)
            {
                textBoxMaTB.Text = dataGridThietBi.Rows[e.RowIndex].Cells["TenThietBi"].Value.ToString();
                textBoxSoluong.Text = dataGridThietBi.Rows[e.RowIndex].Cells["SoLuongThietBi"].Value.ToString();
            }
        }
    }
}
