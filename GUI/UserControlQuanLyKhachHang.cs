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
        QuanLyKhachHang_DAL _DAL;
        QlGymContext _context = new QlGymContext();
        public UserControlQuanLyKhachHang()
        {
            InitializeComponent();
            _DAL = new QuanLyKhachHang_DAL(_context);
            loadData();
        }
        private void loadData()
        {
            var khachHangs = _DAL.GetKhachHangWithGoiTap();
            dataGridView1.DataSource = khachHangs;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

    }
}
