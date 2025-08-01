using QuanLyPhongGym_nhom5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuanLyPhongGym.DAL
{
    internal class DangNhapDAL
    {
        QlGymContext _context;
        public DangNhapDAL(QlGymContext context)
        {
            _context = context;
        }
        public TaiKhoan login(string taiKhoan, string matKhau)
        {
            return _context.TaiKhoans.FirstOrDefault(x=>x.TenTaiKhoan.Trim()==taiKhoan&&x.MatKhau==matKhau);
        }

    }
}
