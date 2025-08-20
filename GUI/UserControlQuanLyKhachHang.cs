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
            ResetForm();
        }
        public void loadData()
        {
            var khachHangs = _BLL.GetKhachHangWithGoiTap();
            dataGridViewKH.DataSource = khachHangs;
            dataGridViewKH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            comboBoxtaikhoan.DataSource = _context.TaiKhoans.ToList().Where(tk => tk.MaVaiTro == 1).ToList();
            comboBoxtaikhoan.DisplayMember = "TenTaiKhoan";
            comboBoxtaikhoan.ValueMember = "TenTaiKhoan";

            comboBoxaccount.DataSource = _context.TaiKhoans.ToList().Where(tk => tk.MaVaiTro == 1).ToList();
            comboBoxaccount.DisplayMember = "TenTaiKhoan";
            comboBoxaccount.ValueMember = "TenTaiKhoan";
        }
        public void ResetForm()
        {
            foreach (Control control in groupBoxNhapThongTin.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear();
                }
                else if (control is RadioButton radioButton)
                {
                    radioButton.Checked = false;
                }
                else if (control is CheckBox checkBox)
                {
                    checkBox.Checked = false;
                }
                else if (control is ComboBox combox)
                {
                    combox.SelectedIndex = -1;
                }
            }

            foreach (Control control in groupBoxHienThi.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear();
                }
                else if (control is RadioButton radioButton)
                {
                    radioButton.Checked = false;
                }
                else if (control is CheckBox checkBox)
                {
                    checkBox.Checked = false;
                }
                else if (control is ComboBox combox)
                {
                    combox.SelectedIndex = -1;
                }
            }
        }
        private void blockcontrols()
        {
            foreach (Control crl in this.Controls)
            {
                if (crl == groupBoxNhapThongTin)
                {
                    groupBoxNhapThongTin.Enabled = true;
                }
                else
                {
                    crl.Enabled = false;
                }
            }
        }
        private void unblockcontrols()
        {
            foreach (Control crl in this.Controls)
            {
                crl.Enabled = true;
            }
        }
        bool isadd;
        private void guna2ButtonThem_Click(object sender, EventArgs e)
        {
            isadd = true;
            ResetForm();
            groupBoxNhapThongTin.Visible = true;
            blockcontrols();
        }


        private void una2ButtonXoa_Click(object sender, EventArgs e)
        {
            if (dataGridViewKH.SelectedRows.Count == 0
                || textBoxKH.Text == ""
                || textBoxEmal.Text == ""
                || textBoxLocation.Text == ""
                || textBoxnumberPhone.Text == ""
                || comboBoxaccount.Text == ""
                || comboBoxtaikhoan.SelectedValue == null
                || textBoxTenKH.Text == null)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xóa.");
                return;
            }
            DialogResult confirm = MessageBox.Show(
            "Bạn có chắc muốn xóa khách hàng này không?",
            "Xác nhận xóa",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning
            );
            if (confirm == DialogResult.No)
                return;


            KhachHang khachHang = new KhachHang()
            {
                MaKh = int.Parse(dataGridViewKH.SelectedRows[0].Cells[0].Value.ToString()),
                HoTen = textBoxTenKH.Text,
                DiaChi = textBoxDiaChi.Text,
                Email = textBoxEmail.Text,
                Sdt = textBoxSDT.Text,
                GioiTinh = radioButtonNam.Checked ? "Nam" : "Nữ",
                TenTaiKhoan = comboBoxtaikhoan.SelectedValue.ToString(),
                TrangThai = checkBoxHD.Checked ? true : false
            };

            if (_BLL.DeleteKhachHang(khachHang.MaKh))
            {
                loadData();
                MessageBox.Show("Xóa khách hàng thành công!");
                ResetForm();
            }
            else
            {
                MessageBox.Show("Xóa khách hàng thất bại!");
            }
        }

        private void una2ButtonSua_Click(object sender, EventArgs e)
        {
            if (dataGridViewKH.SelectedRows.Count == 0
                || textBoxKH.Text == ""
                || textBoxEmal.Text == ""
                || textBoxLocation.Text == ""
                || textBoxnumberPhone.Text == ""
                || comboBoxaccount.Text == ""
                || comboBoxtaikhoan.SelectedValue == null
                || textBoxTenKH.Text == null)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để sửa.");
                return;
            }
            else
            {
                isadd = false;
                groupBoxNhapThongTin.Visible = true;
                blockcontrols();
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
                radioButtonall.Checked = false;
                radioButtonboy.Checked = false;
                radioButtongirl.Checked = false;
                radioButtonLocTheoDay.Checked = false;
                radioButtonOnline.Checked = false;
                radioButtonoffline.Checked = false;
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

            if (e.RowIndex >= 0)
            {
                var selectedRow = dataGridViewKH.Rows[e.RowIndex];
                textBoxTenKH.Text = selectedRow.Cells["HoTen"].Value.ToString();
                textBoxDiaChi.Text = selectedRow.Cells["DiaChi"].Value.ToString();
                textBoxEmail.Text = selectedRow.Cells["Email"].Value.ToString();
                textBoxSDT.Text = selectedRow.Cells["Sdt"].Value.ToString();
                

                if (selectedRow.Cells["TrangThai"].Value != null)
                {
                    bool trangThai = Convert.ToBoolean(selectedRow.Cells["TrangThai"].Value);

                    if (trangThai)
                    {
                        checkBoxHD.Checked = true;
                        checkBoxoffline.Checked = false;
                    }
                    else
                    {
                        checkBoxHD.Checked = false;
                        checkBoxoffline.Checked = true;
                    }
                }


                string gioiTinh = selectedRow.Cells["GioiTinh"].Value.ToString().Trim();
                if (gioiTinh == "Nam")
                {
                    radioButtonNam.Checked = true;
                }
                else if (gioiTinh == "Nữ")
                {
                    radioButtonNu.Checked = true;
                }


                if (selectedRow?.Cells["TenTaiKhoan"]?.Value != null)
                {
                    comboBoxtaikhoan.SelectedValue = selectedRow.Cells["TenTaiKhoan"].Value.ToString();
                }
                else
                {
                    comboBoxtaikhoan.SelectedIndex = -1;
                }

                //Hien Thi
                textBoxKH.Text = selectedRow.Cells["HoTen"].Value.ToString();
                textBoxLocation.Text = selectedRow.Cells["DiaChi"].Value.ToString();
                textBoxEmal.Text = selectedRow.Cells["Email"].Value.ToString();
                textBoxnumberPhone.Text = selectedRow.Cells["Sdt"].Value.ToString();
                if (selectedRow.Cells["NgayDangKy"].Value != null)
                {
                    dateTimePickerDK.Value = DateTime.Parse(selectedRow.Cells["NgayDangKy"].Value.ToString());
                }
                else
                {
                    MessageBox.Show("Tài khoản vẫn chưa đăng kí gói tập hay gói dịch vụ nào");
                }
                if (selectedRow.Cells["NgayHetHan"].Value != null)
                {
                    dateTimePickerHH.Value = DateTime.Parse(selectedRow.Cells["NgayHetHan"].Value.ToString());
                }
                if (selectedRow.Cells["TrangThai"].Value != null)
                {
                    bool trangThai = Convert.ToBoolean(selectedRow.Cells["TrangThai"].Value);

                    if (trangThai)
                    {
                        checkBoxON.Checked = true;
                        checkBoxOFF.Checked = false;
                    }
                    else
                    {
                        checkBoxON.Checked = false;
                        checkBoxOFF.Checked = true;
                    }
                }



                string sex = selectedRow.Cells["GioiTinh"].Value.ToString().Trim();
                if (sex == "Nam")
                {
                    radioButtonMan.Checked = true;
                }
                else if (sex == "Nữ")
                {
                    radioButtonLady.Checked = true;
                }



                if (selectedRow?.Cells["TenTaiKhoan"]?.Value != null)
                {
                    comboBoxaccount.SelectedValue = selectedRow.Cells["TenTaiKhoan"].Value.ToString();
                }
                else
                {
                    comboBoxaccount.SelectedIndex = -1;
                }

            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xem chi tiết!");
            }
        }

        private void UserControlQuanLyKhachHang_Load(object sender, EventArgs e)
        {
        }

        private void guna2Buttonsave_Click(object sender, EventArgs e)
        {
            if (isadd)
            {
                if (string.IsNullOrEmpty(textBoxTenKH.Text)
                    || string.IsNullOrEmpty(textBoxSDT.Text)
                    || string.IsNullOrEmpty(textBoxDiaChi.Text)
                    || string.IsNullOrEmpty(textBoxEmail.Text)
                    || comboBoxtaikhoan.SelectedValue == null
                    || radioButtonNam.Checked == false && radioButtonNu.Checked == false
                    || checkBoxHD.Checked == false && checkBoxoffline.Checked == false)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                    return;
                }
                string tenKH = textBoxTenKH.Text.Trim();
                if (string.IsNullOrEmpty(tenKH))
                {
                    MessageBox.Show("Tên khách hàng không được để trống!");
                    return;
                }
                string sdt = textBoxSDT.Text.Trim();
                if (sdt.Length != 10 || !sdt.All(char.IsDigit) || !sdt.StartsWith("0"))
                {
                    MessageBox.Show("Số điện thoại phải có 10 chữ số và không chứa ký tự khác và bắt đầu từ con số 0!");
                    return;
                }
                string tenTaiKhoan = comboBoxtaikhoan.SelectedValue.ToString().Trim();
                if (_BLL.KiemTraTaiKhoanDaSuDung(tenTaiKhoan))
                {
                    MessageBox.Show("Tài khoản đã được sử dụng, vui lòng chọn tài khoản khác!");
                    return;
                }
                string email = textBoxEmail.Text.Trim();
                if (!email.EndsWith("@gmail.com") || !email.Contains("@") || email.StartsWith("@"))
                {
                    MessageBox.Show("Email phải có định dạng hợp lệ và kết thúc bằng @gmail.com!");
                    return;
                }
                string diaChi = textBoxDiaChi.Text.Trim();
                if (string.IsNullOrEmpty(diaChi))
                {
                    MessageBox.Show("Địa chỉ không được để trống");
                    return;
                }
                
                string gioiTinh = radioButtonNam.Checked ? "Nam" : "Nữ";
                if (gioiTinh != "Nam" && gioiTinh != "Nữ")
                {
                    MessageBox.Show("Vui lòng chọn giới tính hợp lệ!");
                    return;
                }
                if ((checkBoxHD.Checked == false && checkBoxoffline.Checked == false)|| (checkBoxHD.Checked == true && checkBoxoffline.Checked == true))
                {
                    MessageBox.Show("Vui lòng chọn trạng thái và chỉ được chọn là hoạt động hoặc không!");
                    return;
                }

                DialogResult confirm = MessageBox.Show(
                "Bạn có chắc muốn thêm khách hàng này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
                );
                if (confirm == DialogResult.No)
                    return;
                KhachHang khachHang = new KhachHang
                {
                    HoTen = textBoxTenKH.Text,
                    DiaChi = textBoxDiaChi.Text,
                    Email = textBoxEmail.Text,
                    Sdt = textBoxSDT.Text,
                    GioiTinh = radioButtonNam.Checked ? "Nam" : "Nữ",
                    TrangThai = checkBoxHD.Checked,
                    TenTaiKhoan = comboBoxtaikhoan.SelectedValue.ToString()
                };

                if (_BLL.AddKhachHang(khachHang))
                {
                    MessageBox.Show("Thêm khách hàng thành công!");
                    loadData();
                    groupBoxNhapThongTin.Visible = false;
                    unblockcontrols();
                }
                else
                {
                    MessageBox.Show("Thêm khách hàng thất bại!");
                }
            }
            else
            {
                if (dataGridViewKH.SelectedRows.Count == 0
                || textBoxKH.Text == ""
                || textBoxEmal.Text == ""
                || textBoxLocation.Text == ""
                || textBoxnumberPhone.Text == ""
                || comboBoxaccount.Text == ""
                || comboBoxtaikhoan.SelectedValue == null
                || textBoxTenKH.Text == null)
                {
                    MessageBox.Show("Vui lòng chọn một khách hàng để sửa.");
                    return;
                }

                int maKH = int.Parse(dataGridViewKH.SelectedRows[0].Cells[0].Value.ToString());
                string tenTaiKhoan = comboBoxtaikhoan.SelectedValue.ToString().Trim();

                if (string.IsNullOrEmpty(textBoxTenKH.Text)
                    || string.IsNullOrEmpty(textBoxSDT.Text)
                    || string.IsNullOrEmpty(textBoxDiaChi.Text)
                    || string.IsNullOrEmpty(textBoxEmail.Text)
                    || comboBoxtaikhoan.SelectedValue == null
                    || radioButtonNam.Checked == false && radioButtonNu.Checked == false
                    || checkBoxHD.Checked == false && checkBoxoffline.Checked == false)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                    return;
                }
                string tenKH = textBoxTenKH.Text.Trim();
                if (string.IsNullOrEmpty(tenKH))
                {
                    MessageBox.Show("Tên khách hàng không được để trống!");
                    return;
                }
                string sdt = textBoxSDT.Text.Trim();
                if (sdt.Length != 10 || !sdt.All(char.IsDigit) || !sdt.StartsWith("0"))
                {
                    MessageBox.Show("Số điện thoại phải có 10 chữ số và không chứa ký tự khác và bắt đầu từ số 0!");
                    return;
                }
                if (_BLL.KiemTraTaiKhoanDaSuDungChoKhac(tenTaiKhoan, maKH))
                {
                    MessageBox.Show("Tài khoản đã được sử dụng bởi khách hàng khác, vui lòng chọn tài khoản khác!");
                    return;
                }
                string email = textBoxEmail.Text.Trim();
                if (!email.EndsWith("@gmail.com") || !email.Contains("@") || email.StartsWith("@"))
                {
                    MessageBox.Show("Email phải có định dạng hợp lệ và kết thúc bằng @gmail.com!");
                    return;
                }
                string diaChi = textBoxDiaChi.Text.Trim();
                if (string.IsNullOrEmpty(diaChi))
                {
                    MessageBox.Show("Địa chỉ không được để trống");
                    return;
                }
                string gioiTinh = radioButtonNam.Checked ? "Nam" : "Nữ";
                if (gioiTinh != "Nam" && gioiTinh != "Nữ")
                {
                    MessageBox.Show("Vui lòng chọn giới tính hợp lệ!");
                    return;
                }
                if ((checkBoxHD.Checked == false && checkBoxoffline.Checked == false) || (checkBoxHD.Checked == true && checkBoxoffline.Checked == true))
                {
                    MessageBox.Show("Vui lòng chọn trạng thái và chỉ được chọn là hoạt động hoặc không!");
                    return;
                }


                DialogResult confirm = MessageBox.Show(
                "Bạn có chắc muốn sửa khách hàng này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
                );
                if (confirm == DialogResult.No)
                    return;


                KhachHang khachHang = new KhachHang()
                {
                    MaKh = maKH,
                    HoTen = textBoxTenKH.Text,
                    DiaChi = textBoxDiaChi.Text,
                    Email = textBoxEmail.Text,
                    Sdt = textBoxSDT.Text,
                    GioiTinh = radioButtonNam.Checked ? "Nam" : "Nữ",
                    TrangThai = checkBoxHD.Checked,
                    TenTaiKhoan = tenTaiKhoan
                };

                if (_BLL.UpdateKhachHang(khachHang))
                {
                    MessageBox.Show("Sửa khách hàng thành công!");
                    loadData();
                    groupBoxNhapThongTin.Visible = false;
                    unblockcontrols();
                }
                else
                {
                    MessageBox.Show("Sửa khách hàng thất bại!");
                }
            }
        }

        private void guna2Buttonhuy_Click(object sender, EventArgs e)
        {
            groupBoxNhapThongTin.Visible = false;
            unblockcontrols();
        }

        private void radioButtonOnline_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerloctheotime.Enabled = false;
            if (radioButtonOnline.Checked)
            {
                var khachHangsOnline = _BLL.GetKhachHangWithGoiTap()
                            .Where(kh => kh.TrangThai.HasValue && kh.TrangThai.Value)
                            .ToList();
                dataGridViewKH.DataSource = khachHangsOnline;
            }
        }

        private void radioButtonoffline_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerloctheotime.Enabled = false;
            if (radioButtonoffline.Checked)
            {
                var khachHangsOffline = _BLL.GetKhachHangWithGoiTap()
                            .Where(kh => kh.TrangThai.HasValue && !kh.TrangThai.Value)
                            .ToList();
                dataGridViewKH.DataSource= khachHangsOffline;
            }
        }

        private void radioButtonboy_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerloctheotime.Enabled = false;
            if (radioButtonboy.Checked)
            {
                var khachHangsnam = _BLL.GetKhachHangWithGoiTap().Where(kh=>kh.GioiTinh=="Nam").ToList();
                dataGridViewKH.DataSource = khachHangsnam;
            }
        }

        private void radioButtongirl_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerloctheotime.Enabled = false;
            if (radioButtongirl.Checked)
            {
                var khachHangsnu = _BLL.GetKhachHangWithGoiTap().Where(kh=>kh.GioiTinh=="Nữ").ToList();
                dataGridViewKH.DataSource = khachHangsnu;
            }
        }

        private void radioButtonall_CheckedChanged(object sender, EventArgs e)
        {
            loadData();
            dateTimePickerloctheotime.Enabled = false;
        }


        private void radioButtonLocTheoDay_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLocTheoDay.Checked) 
            {
                dateTimePickerloctheotime.Enabled = true;
                DateOnly ngayChon = DateOnly.FromDateTime(dateTimePickerloctheotime.Value);

                var khachHangs = _BLL.GetKhachHangWithGoiTap()
                                     .Where(kh => kh.NgayDangKy.HasValue && kh.NgayDangKy.Value == ngayChon || kh.NgayHetHan.HasValue && kh.NgayHetHan.Value == ngayChon)
                                     .ToList();

                dataGridViewKH.DataSource = khachHangs;
            }
            else
            {
                dateTimePickerloctheotime.Enabled = false;
                dataGridViewKH.DataSource = _BLL.GetKhachHangWithGoiTap();
            }
        }

        private void dateTimePickerloctheotime_ValueChanged(object sender, EventArgs e)
        {
            DateOnly ngayChon = DateOnly.FromDateTime(dateTimePickerloctheotime.Value);

            var khachHangs = _BLL.GetKhachHangWithGoiTap()
                                 .Where(kh => kh.NgayDangKy.HasValue && kh.NgayDangKy.Value == ngayChon|| kh.NgayHetHan.HasValue && kh.NgayHetHan.Value == ngayChon)
                                 .ToList();

            dataGridViewKH.DataSource = khachHangs;
        }

        private void textBoxTenKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        private void textBoxSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }
    }
}
