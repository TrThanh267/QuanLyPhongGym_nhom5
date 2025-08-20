using QuanLyPhongGym_nhom5.DAL;
using QuanLyPhongGym_nhom5.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
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
            ClearControls();
        }
        public void loadData()
        {
            var listTaiKhoan = _QuanLyTaiKhoan_DAL.GetAllTaiKhoans();
            dataGridTaiKhoan.DataSource = listTaiKhoan;
            dataGridTaiKhoan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            comboBoxVaiTro.DataSource = _db.VaiTros.ToList();
            comboBoxVaiTro.DisplayMember = "TenVaiTro";
            comboBoxVaiTro.ValueMember = "MaVaiTro";

            comboBoxquyen.DataSource = _db.VaiTros.ToList();
            comboBoxquyen.DisplayMember = "TenVaiTro";
            comboBoxquyen.ValueMember = "MaVaiTro";

            int count = 0;
            foreach (DataGridViewRow row in dataGridTaiKhoan.Rows)
            {
                if (row.Cells["MaVaiTro"].Value != null && Convert.ToInt32(row.Cells["MaVaiTro"].Value) == 1)
                {
                    count++;
                }
            }
            labelTK.Text = "Tổng số lượng tài khoản: " + _QuanLyTaiKhoan_DAL.GetAllTaiKhoans().Count;
            labeltkchuadk.Visible = false;
        }
        private void ClearControls()
        {
            foreach (Control control in groupBoxChucNang.Controls)
            {
                if (control is TextBox)
                {
                    (control as TextBox).Clear();
                }
                else if (control is ComboBox)
                {
                    (control as ComboBox).SelectedIndex = -1;
                }
            }
            foreach(Control control2 in groupBoxThongTin.Controls)
            {
                if (control2 is TextBox)
                {
                    (control2 as TextBox).Clear();
                }
                else if (control2 is ComboBox)
                {
                    (control2 as ComboBox).SelectedIndex = -1;
                }
            }
        }
        private void blockControls()
        {
            GroupBox gr = groupBoxChucNang;
            foreach(Control crl in this.Controls)
            {
                if (crl == gr)
                    crl.Enabled = true; 
                else
                    crl.Enabled = false; 
            }
        }
        private void UnlockControls()
        {
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = true;
            }
        }
        bool isAddingNew = false;


        private void dataGridTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridTaiKhoan.Rows.Count)
            {
                var row = dataGridTaiKhoan.Rows[e.RowIndex];
                textBoxTenTK.Text = row.Cells["TenTaiKhoan"].Value.ToString();
                textBoxMK.Text = row.Cells["MatKhau"].Value.ToString();
                comboBoxVaiTro.SelectedValue = row.Cells["MaVaiTro"].Value;
                textBoxtk.Text = row.Cells["TenTaiKhoan"].Value.ToString();
                textBoxmatkhau.Text = row.Cells["MatKhau"].Value.ToString();
                comboBoxquyen.SelectedValue = row.Cells["MaVaiTro"].Value;
                comboBoxquyen.Enabled = false;
            }
        }

        private void guna2Buttonsave_Click(object sender, EventArgs e)
        {
            if (isAddingNew)
            {
                if (textBoxTenTK.Text == null
                   || textBoxMK.Text == null
                   || comboBoxVaiTro.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                    return;
                }
                if(textBoxTenTK.Text.Length < 6 || textBoxMK.Text.Length < 6)
                {
                    MessageBox.Show("Tên tài khoản và mật khẩu phải có ít nhất 6 ký tự!");
                    return;
                }
                else
                {
                    DialogResult confirm = MessageBox.Show(
                    "Bạn có chắc muốn thêm tài khoản này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                    );
                    if (confirm == DialogResult.No)
                        return;

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
                        ClearControls();
                        groupBoxChucNang.Visible = false;
                        UnlockControls();
                    }
                    else
                    {
                        MessageBox.Show("Thêm tài khoản thất bại!");
                    }
                }
            }
            else
            {
                if (dataGridTaiKhoan.SelectedRows.Count == 0
                    || textBoxtk.Text==""
                    || textBoxmatkhau.Text==""
                    || comboBoxVaiTro.SelectedValue==null)
                {
                    MessageBox.Show("Vui lòng chọn tài khoản để sửa!");
                    return;
                }
                else
                {
                    if (textBoxTenTK.Text == null
                   || textBoxMK.Text == null
                   || comboBoxVaiTro.SelectedValue == null)
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                        return;
                    }
                    if (textBoxTenTK.Text.Length < 6 || textBoxMK.Text.Length < 6)
                    {
                        MessageBox.Show("Tên tài khoản và mật khẩu phải có ít nhất 6 ký tự!");
                        return;
                    }
                    if (_QuanLyTaiKhoan_DAL.kiemtrasuaVaitro(textBoxTenTK.Text))
                    {
                        MessageBox.Show("Tài khoản đã được đăng kí không thể sửa!");
                        return;
                    }
                    DialogResult confirm = MessageBox.Show(
                    "Bạn có chắc muốn sửa tài khoản này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                    );
                    if (confirm == DialogResult.No)
                        return;
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
                        ClearControls();
                        groupBoxChucNang.Visible = false;
                        textBoxTenTK.Enabled = true;
                        labeltaikhoanthongbao.Visible = false;
                        UnlockControls();
                    }
                    else
                    {
                        MessageBox.Show("Sửa tài khoản thất bại!");
                    }
                }
            }
        }

        private void guna2Buttonhuy_Click(object sender, EventArgs e)
        {
            groupBoxChucNang.Visible = false;
            textBoxTenTK.Enabled = true;
            labeltaikhoanthongbao.Visible = false;
            UnlockControls();
        }

        private void radioButtonKH_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonKH.Checked)
            {
                var dsTaiKhoanKH = _QuanLyTaiKhoan_DAL.GetAllTaiKhoans().Where(tk => tk.MaVaiTro == 1).ToList();
                dataGridTaiKhoan.DataSource = dsTaiKhoanKH;
                labeltkchuadk.Visible = true;
                labelTK.Text = "Số lượng tài khoản khách hàng: " + dsTaiKhoanKH.Count;
                labeltkchuadk.Text = "Số lượng tài khoản chưa đăng ký: " + _QuanLyTaiKhoan_DAL.DemTaiKhoanDangKy();
            }
        }

        private void radioButtonHLV_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonHLV.Checked)
            {
                var dsTaiKhoanHLV = _QuanLyTaiKhoan_DAL.GetAllTaiKhoans().Where(tk => tk.MaVaiTro == 2).ToList();
                dataGridTaiKhoan.DataSource = dsTaiKhoanHLV;
                labeltkchuadk.Visible = true;
                labelTK.Text = "Số lượng tài khoản huấn luyện viên: " + dsTaiKhoanHLV.Count;
                labeltkchuadk.Text = "Số lượng tài khoản đã đăng ký: " + _QuanLyTaiKhoan_DAL.TaikhoanDangKyHLV();
            }
        }

        private void radioButtonThuNgan_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonThuNgan.Checked)
            {
                var dsTaiKhoanThuNgan = _QuanLyTaiKhoan_DAL.GetAllTaiKhoans().Where(tk => tk.MaVaiTro == 3).ToList();
                dataGridTaiKhoan.DataSource = dsTaiKhoanThuNgan;
                labeltkchuadk.Visible = true;
                labelTK.Text = "Số lượng tài khoản thu ngân: " + dsTaiKhoanThuNgan.Count;
                labeltkchuadk.Text = "Số lượng tài khoản đã đăng ký: " + _QuanLyTaiKhoan_DAL.TaikhoanDangKyThuNgan();

            }
        }

        private void radioButtonall_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonall.Checked)
            {
                loadData();
                labelTK.Text = "Tổng số lượng tài khoản: " + _QuanLyTaiKhoan_DAL.GetAllTaiKhoans().Count;
                labeltkchuadk.Visible = false;
            }
        }

        private void guna2ButtonThemTB_Click_1(object sender, EventArgs e)
        {
            groupBoxChucNang.Visible = true;
            isAddingNew = true;
            ClearControls();
            textBoxTenTK.Enabled = true;
            labeltaikhoanthongbao.Visible = false;
            blockControls();
        }

        private void una2ButtonSuaTK_Click_1(object sender, EventArgs e)
        {
            
            if (dataGridTaiKhoan.SelectedRows.Count == 0
                || textBoxtk.Text == ""
                || textBoxmatkhau.Text == ""
                || comboBoxquyen.Text == ""
                || comboBoxVaiTro.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản để sửa!");
                return;
            }
            else
            {
                groupBoxChucNang.Visible = true;
                isAddingNew = false;
                textBoxTenTK.Enabled = false;
                labeltaikhoanthongbao.Visible = true;
                blockControls();
            }

        }

        private void una2ButtonXoaTK_Click_1(object sender, EventArgs e)
        {
            groupBoxChucNang.Visible = false;
            if (dataGridTaiKhoan.SelectedRows.Count == 0 
                || textBoxtk.Text == "" 
                || textBoxmatkhau.Text == "" 
                || comboBoxVaiTro.SelectedValue==null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản để xóa!");
                return;
            }
            else
            {
                DialogResult confirm = MessageBox.Show(
                "Bạn có chắc muốn xóa tài khoản này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
                );
                if (confirm == DialogResult.No)
                    return;

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
                    ClearControls();
                }
                else
                {
                    MessageBox.Show("Xóa tài khoản thất bại!");
                }
            }
        }

        private void guna2ButtonTkTB_Click_1(object sender, EventArgs e)
        {
            radioButtonall.Checked = false;
            radioButtonKH.Checked = false;
            radioButtonHLV.Checked = false;
            radioButtonThuNgan.Checked = false;
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

        private void textBoxTenTK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.V || e.KeyCode == Keys.C || e.KeyCode == Keys.X))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void textBoxTenTK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxMK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.V || e.KeyCode == Keys.C || e.KeyCode == Keys.X))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void textBoxMK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
