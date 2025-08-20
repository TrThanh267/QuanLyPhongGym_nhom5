using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym.DAL;
using QuanLyPhongGym_nhom5.BLL;
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

namespace QuanLyPhongGym.GUI
{
    public partial class UserControlQuanLyNhanVien : UserControl
    {
        QlGymContext _db = new QlGymContext();
        NhanVien_BLL _NhanVienBll;
        public UserControlQuanLyNhanVien()
        {
            InitializeComponent();
            _NhanVienBll = new NhanVien_BLL(_db);
            LoadData();
            VaiTro();
            ClearControls();
        }
        public void LoadData()
        {
            var listNhanVien = _NhanVienBll.GetNhanVienWithVaiTro();
            dataGridViewNV.DataSource = listNhanVien;
            dataGridViewNV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        public void VaiTro()
        {
            comboBoxTaiKhoan.DataSource = _db.TaiKhoans.ToList().Where(nv => nv.MaVaiTro == 3 || nv.MaVaiTro == 2).ToList();
            comboBoxTaiKhoan.DisplayMember = "TenTaiKhoan";
            comboBoxTaiKhoan.ValueMember = "TenTaiKhoan";
        }
        private void ClearControls()
        {
            foreach (Control control in groupBoxNhapThongTin.Controls)
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
            foreach (Control ctl in groupBoxHIenThi.Controls)
            {
                if (ctl is TextBox)
                {
                    (ctl as TextBox).Clear();
                }
                else if (ctl is ComboBox)
                {
                    (ctl as ComboBox).SelectedIndex = -1;
                }
                else if (ctl is DateTimePicker)
                {
                    (ctl as DateTimePicker).Value = DateTime.Now;
                }
            }
        }
        private void BlockControls()
        {
            GroupBox gr = groupBoxNhapThongTin;
            foreach (Control ctr in this.Controls)
            {
                if (ctr == gr)
                {
                    ctr.Enabled = true;
                }
                else
                {
                    ctr.Enabled = false;
                }
            }
        }

        private void unBlockControls()
        {
            foreach (Control control in this.Controls)
            {
                control.Enabled = true;
            }
        }
        bool isAddingNew = false;
        private void guna2ButtonThem_Click(object sender, EventArgs e)
        {
            ClearControls();
            groupBoxNhapThongTin.Visible = true;
            isAddingNew = true;
            BlockControls();
        }

        private void guna2ButtonXoa_Click(object sender, EventArgs e)
        {
            if (dataGridViewNV.SelectedRows.Count == 0
                    || textBoxgamal.Text == ""
                    || textBoxLocation.Text == ""
                    || textBoxnameNV.Text == ""
                    || textBoxNumberPhone.Text == ""
                    || textBoxSalary.Text == ""
                    || comboBoxaccount.Text == ""
                    || comboBoxTaiKhoan.SelectedValue == null
                    || textBoxidnv.Text==null)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa!");
                return;
            }
            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc muốn xóa nhân viên này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (confirm == DialogResult.No)
                return;

            try
            {
                NhanVien nhanVien = new NhanVien
                {
                    MaNv = int.Parse(dataGridViewNV.SelectedRows[0].Cells[0].Value.ToString()),
                    TenTaiKhoan = comboBoxTaiKhoan.SelectedValue.ToString(),
                    DiaChi = textBoxDiaChi.Text,
                    Email = textBoxEmail.Text,
                    NgayVaoLam = DateOnly.FromDateTime(dateTimePickerNgayVaoLam.Value),
                    Sdt = textBoxSDT.Text,
                    Luong = decimal.TryParse(textBoxLuong.Text, out decimal luong) ? luong : (decimal?)null
                };
                _NhanVienBll.DeleteNhanVien(nhanVien);
                LoadData();
                ClearControls();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

        }

        private void guna2ButtonCapNhap_Click(object sender, EventArgs e)
        {
            if (dataGridViewNV.SelectedRows.Count == 0
                    || textBoxgamal.Text == ""
                    || textBoxLocation.Text == ""
                    || textBoxnameNV.Text == ""
                    || textBoxNumberPhone.Text == ""
                    || textBoxSalary.Text == ""
                    || comboBoxaccount.Text == ""
                    || comboBoxTaiKhoan.SelectedValue == null
                    || textBoxidnv.Text ==null)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để cập nhật!");
                return;
            }
            else
            {
                isAddingNew = false;
                groupBoxNhapThongTin.Visible = true;
                BlockControls();
            }
        }

        private void guna2ButtonTk_Click(object sender, EventArgs e)
        {
            radioButtonAll.Checked = false;
            radioButtonThuNgan.Checked = false;
            adioButtonHLVr.Checked = false;
            radioButtonHD.Checked = false;
            ButtonNgungradioHD.Checked = false;
            string searchText = textBoxTK.Text.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                var ketQua = _NhanVienBll.timkiem(searchText);
                dataGridViewNV.DataSource = ketQua;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên hoặc mã nhân viên để tìm kiếm!");
                LoadData();
            }
        }

        private void dataGridViewNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridViewNV.Rows[e.RowIndex];
                textBoxMaNV.Text = row.Cells["MaNv"].Value?.ToString();
                textBoxTenNhanVien.Text = row.Cells["TenNhanVien"].Value?.ToString();
                textBoxSDT.Text = row.Cells["Sdt"].Value?.ToString();
                textBoxEmail.Text = row.Cells["Email"].Value?.ToString();
                textBoxDiaChi.Text = row.Cells["DiaChi"].Value?.ToString();
                textBoxLuong.Text = row.Cells["Luong"].Value?.ToString();

                if (DateTime.TryParse(row.Cells["NgayVaoLam"].Value?.ToString(), out DateTime ngay))
                {
                    dateTimePickerNgayVaoLam.Value = ngay;
                }

                comboBoxTaiKhoan.Text = row.Cells["TenTaiKhoan"].Value?.ToString();

                // Hien thi
                textBoxidnv.Text = row.Cells["MaNv"].Value?.ToString();
                textBoxnameNV.Text = row.Cells["TenNhanVien"].Value?.ToString();
                textBoxNumberPhone.Text = row.Cells["Sdt"].Value?.ToString();
                textBoxgamal.Text = row.Cells["Email"].Value?.ToString();
                textBoxLocation.Text = row.Cells["DiaChi"].Value?.ToString();
                textBoxSalary.Text = row.Cells["Luong"].Value?.ToString();

                if (DateTime.TryParse(row.Cells["NgayVaoLam"].Value?.ToString(), out DateTime ngayvao))
                {
                    dateoBoxaccounNVL.Value = ngayvao;
                }

                comboBoxaccount.Text = row.Cells["TenTaiKhoan"].Value?.ToString();
            }
        }

        private void dataGridViewNV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void guna2Buttonsave_Click(object sender, EventArgs e)
        {
            if (isAddingNew)
            {
                try
                {
                    if (string.IsNullOrEmpty(textBoxTenNhanVien.Text)
                        || string.IsNullOrEmpty(textBoxSDT.Text)
                        || string.IsNullOrEmpty(textBoxDiaChi.Text)
                        || string.IsNullOrEmpty(textBoxEmail.Text)
                        || string.IsNullOrEmpty(textBoxSDT.Text)
                        || comboBoxTaiKhoan.SelectedValue == null)
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                        return;
                    }
                    string tenTaiKhoan = comboBoxTaiKhoan.SelectedValue.ToString().Trim();
                    if (_NhanVienBll.KiemTraTaiKhoanDaSuDung(tenTaiKhoan))
                    {
                        MessageBox.Show("Tài khoản đã được sử dụng, vui lòng chọn tài khoản khác!");
                        return;
                    }
                    string email = textBoxEmail.Text.Trim();
                    if (!email.EndsWith("@gmail.com") || !email.Contains("@") || email.StartsWith("@") || string.IsNullOrEmpty(email))
                    {
                        MessageBox.Show("Email phải có định dạng hợp lệ và kết thúc bằng @gmail.com!");
                        return;
                    }
                    string sdt = textBoxSDT.Text.Trim();
                    bool checkSDT = _NhanVienBll.GetNhanVienWithVaiTro().Any(nv => nv.Sdt == sdt);
                    if (checkSDT)
                    {
                        MessageBox.Show("Số điện thoại đã được sử dụng, vui lòng nhập số khác!");
                        return;
                    }
                    if (sdt.Length != 10 || !sdt.All(char.IsDigit) || !sdt.StartsWith("0") || string.IsNullOrEmpty(sdt))
                    {
                        MessageBox.Show("Số điện thoại phải có 10 chữ số và không chứa ký tự khác và bắt đầu là số 0!");
                        return;
                    }
                    DateTime ngayVaoLam = dateTimePickerNgayVaoLam.Value;
                    if (ngayVaoLam >= DateTime.Now)
                    {
                        MessageBox.Show("Ngày vào làm không thể lớn hơn ngày hôm nay!");
                        return;
                    }
                    string Luong = textBoxLuong.Text.Trim().Replace(",", "");
                    if (!Luong.All(char.IsDigit) || string.IsNullOrWhiteSpace(Luong))
                    {
                        MessageBox.Show("Vui lòng nhập lương hợp lệ!");
                        return;
                    }
                    DialogResult confirm = MessageBox.Show(
                    "Bạn có chắc muốn thêm nhân viên này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                    );
                    if (confirm == DialogResult.No)
                        return;
                    {
                        NhanVien nhanVien = new NhanVien
                        {
                            TenNhanVien = textBoxTenNhanVien.Text,
                            TenTaiKhoan = comboBoxTaiKhoan.SelectedValue.ToString(),
                            DiaChi = textBoxDiaChi.Text,
                            Email = textBoxEmail.Text,
                            NgayVaoLam = DateOnly.FromDateTime(dateTimePickerNgayVaoLam.Value),
                            Sdt = textBoxSDT.Text,
                            Luong = decimal.TryParse(textBoxLuong.Text, out decimal luong) ? luong : (decimal?)null
                        };
                        _NhanVienBll.AddNhanVien(nhanVien);
                        LoadData();
                        ClearControls();
                        unBlockControls();
                        groupBoxNhapThongTin.Visible = false;

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
            else
            {
                if (dataGridViewNV.SelectedRows.Count == 0
                    || textBoxgamal.Text == ""
                    || textBoxLocation.Text == ""
                    || textBoxnameNV.Text == ""
                    || textBoxNumberPhone.Text == ""
                    || textBoxSalary.Text == ""
                    || comboBoxaccount.Text == ""
                    || comboBoxTaiKhoan.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên để cập nhật!");
                    return;
                }
                if (string.IsNullOrEmpty(textBoxTenNhanVien.Text)
                        || string.IsNullOrEmpty(textBoxSDT.Text)
                        || string.IsNullOrEmpty(textBoxDiaChi.Text)
                        || string.IsNullOrEmpty(textBoxEmail.Text)
                        || string.IsNullOrEmpty(textBoxSDT.Text)
                        || comboBoxTaiKhoan.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                    return;
                }
                string tenTaiKhoan = comboBoxTaiKhoan.SelectedValue.ToString().Trim();
                if (_NhanVienBll.KiemTraTaiKhoanDaSuDung(tenTaiKhoan))
                {
                    MessageBox.Show("Tài khoản đã được sử dụng, vui lòng chọn tài khoản khác!");
                    return;
                }
                string email = textBoxEmail.Text.Trim();
                if (!email.EndsWith("@gmail.com") || !email.Contains("@") || email.StartsWith("@") || string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Email phải có định dạng hợp lệ và kết thúc bằng @gmail.com!");
                    return;
                }
                string sdt = textBoxSDT.Text.Trim();
                bool checkSDT = _NhanVienBll.GetNhanVienWithVaiTro().Any(nv => nv.Sdt == sdt);
                if (checkSDT)
                {
                    MessageBox.Show("Số điện thoại đã được sử dụng, vui lòng nhập số khác!");
                    return;
                }
                if (sdt.Length != 10 || !sdt.All(char.IsDigit) || !sdt.StartsWith("0") || string.IsNullOrEmpty(sdt))
                {
                    MessageBox.Show("Số điện thoại phải có 10 chữ số và không chứa ký tự khác và bắt đầu là số 0!");
                    return;
                }
                DateTime ngayVaoLam = dateTimePickerNgayVaoLam.Value;
                if (ngayVaoLam >= DateTime.Now)
                {
                    MessageBox.Show("Ngày vào làm không thể lớn hơn ngày hôm nay!");
                    return;
                }
                string Luong = textBoxLuong.Text.Trim().Replace(",", "");
                if (!Luong.All(char.IsDigit) || string.IsNullOrWhiteSpace(Luong))
                {
                    MessageBox.Show("Vui lòng nhập lương hợp lệ!");
                    return;
                }

                DialogResult confirm = MessageBox.Show(
                    "Bạn có chắc muốn cập nhập nhân viên này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                if (confirm == DialogResult.No)
                    return;
                try
                {

                    NhanVien nhanVien = new NhanVien
                    {
                        MaNv = int.Parse(textBoxMaNV.Text),
                        TenNhanVien = textBoxTenNhanVien.Text,
                        TenTaiKhoan = comboBoxTaiKhoan.SelectedValue.ToString(),
                        DiaChi = textBoxDiaChi.Text,
                        Email = textBoxEmail.Text,
                        NgayVaoLam = DateOnly.FromDateTime(dateTimePickerNgayVaoLam.Value),
                        Sdt = textBoxSDT.Text,
                        Luong = decimal.TryParse(textBoxLuong.Text, out decimal luong) ? luong : (decimal?)null
                    }; ;
                    if (_NhanVienBll.UpdateNhanVien(nhanVien))
                    {
                        MessageBox.Show("Cập nhật nhân viên thành công!");
                        LoadData();
                        ClearControls();
                        unBlockControls();
                        groupBoxNhapThongTin.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật nhân viên thất bại!");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void guna2Buttonhuy_Click(object sender, EventArgs e)
        {
            groupBoxNhapThongTin.Visible = false;
            unBlockControls();
        }

        private void adioButtonHLVr_CheckedChanged(object sender, EventArgs e)
        {
            if (adioButtonHLVr.Checked)
            {
                var listHLV = _NhanVienBll.ListHLV();
                dataGridViewNV.DataSource = listHLV;
            }
        }

        private void radioButtonThuNgan_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonThuNgan.Checked)
            {
                var listThuNgan = _NhanVienBll.ListThuNgan();
                dataGridViewNV.DataSource = listThuNgan;
            }
        }

        private void radioButtonAll_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAll.Checked)
            {
                LoadData();
            }
        }

        private void radioButtonHD_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonHD.Checked)
            {
                var listHD = _NhanVienBll.GetNhanVienWithVaiTro().Where(nv => nv.TrangThai == "Online").ToList();
                dataGridViewNV.DataSource = listHD;
            }
        }

        private void ButtonNgungradioHD_CheckedChanged(object sender, EventArgs e)
        {
            if (ButtonNgungradioHD.Checked)
            {
                var listNgungHD = _NhanVienBll.GetNhanVienWithVaiTro().Where(nv => nv.TrangThai == "Offline").ToList();
                dataGridViewNV.DataSource = listNgungHD;
            }
        }

        private void textBoxTenNhanVien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.V || e.KeyCode == Keys.C || e.KeyCode == Keys.X))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void textBoxTenNhanVien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                return;
            }
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            e.Handled = true;

        }

        private void textBoxLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxDiaChi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || char.IsPunctuation(e.KeyChar))
            {
                return;
            }
        }

        private void textBoxSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void groupBoxLocDanhSach_Enter(object sender, EventArgs e)
        {

        }
    }
}
