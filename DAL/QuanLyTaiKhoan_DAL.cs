using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym_nhom5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongGym_nhom5.DAL
{
    internal class QuanLyTaiKhoan_DAL
    {
        QlGymContext _context;
        public QuanLyTaiKhoan_DAL(QlGymContext context)
        {
            _context = context;
        }
        public List<TaiKhoan> GetAllTaiKhoans()
        {
            return _context.TaiKhoans
                .Include(tk => tk.MaVaiTroNavigation)
                .Select(tk => new TaiKhoan
                {
                    TenTaiKhoan = tk.TenTaiKhoan,
                    MatKhau = tk.MatKhau,
                    MaVaiTro = tk.MaVaiTro,
                })
                .ToList();
        }
        public bool AddTaiKhoan(TaiKhoan taiKhoan)
        {
            try
            {
                _context.TaiKhoans.Add(taiKhoan);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string fullError = ex.Message;
                if (ex.InnerException != null)
                {
                    fullError += "\nChi tiết: " + ex.InnerException.Message;
                }
                Console.WriteLine("Lỗi thêm vào CSDL:\n" + fullError);
                return false;
            }
        }
        public bool DeleteTaiKhoan(TaiKhoan taiKhoan)
        {
            try
            {
                var tk = _context.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan == taiKhoan.TenTaiKhoan);
                if (tk != null)
                {
                    _context.TaiKhoans.Remove(tk);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string fullError = ex.Message;
                if (ex.InnerException != null)
                {
                    fullError += "\nChi tiết: " + ex.InnerException.Message;
                }
                Console.WriteLine("Lỗi xóa khỏi CSDL:\n" + fullError);
                return false;
            }
        }
        public bool UpdateTaiKhoan(TaiKhoan taiKhoan)
        {
            try
            {
                var tk = _context.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan == taiKhoan.TenTaiKhoan);
                if (tk != null)
                {
                    tk.MatKhau = taiKhoan.MatKhau == null ? tk.MatKhau : taiKhoan.MatKhau;
                    tk.MaVaiTro = taiKhoan.MaVaiTro == null ? tk.MaVaiTro : taiKhoan.MaVaiTro;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string fullError = ex.Message;
                if (ex.InnerException != null)
                {
                    fullError += "\nChi tiết: " + ex.InnerException.Message;
                }
                Console.WriteLine("Lỗi cập nhật CSDL:\n" + fullError);
                return false;
            }
        }
        public List<TaiKhoan> timkiemTK(string taikohan)
        {
            return _context.TaiKhoans.Where(x => x.TenTaiKhoan.Contains(taikohan)).ToList();
        }
    }
}
