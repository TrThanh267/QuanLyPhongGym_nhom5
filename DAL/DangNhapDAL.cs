using Microsoft.EntityFrameworkCore;
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
        public TaiKhoan login(string tenTK, string matKhau)
        {
            return _context.TaiKhoans
                .Include(tk => tk.MaVaiTroNavigation)
                .FirstOrDefault(tk => tk.TenTaiKhoan == tenTK && tk.MatKhau == matKhau);

        }

    }
}
