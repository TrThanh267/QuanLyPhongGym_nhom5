using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym_nhom5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongGym_nhom5.DAL
{
    internal class QuanLyKhachHang_DAL
    {
        QlGymContext _context;
        public QuanLyKhachHang_DAL(QlGymContext context)
        {
            _context = context;
        }
        public List<KhachHang> GetKhachHangWithGoiTap()
        {
            return _context.KhachHangs
                .Include(kh => kh.TenTaiKhoanNavigation)
                .Select(kh => new KhachHang
                {
                    MaKh = kh.MaKh,
                    HoTen = kh.HoTen,
                    Sdt = kh.Sdt,
                    Email = kh.Email,
                    DiaChi = kh.DiaChi,
                    NgayDangKy = kh.NgayDangKy,
                    NgayHetHan = kh.NgayHetHan,
                    TrangThai = kh.TrangThai,
                    GioiTinh = kh.GioiTinh,
                    TenTaiKhoan = kh.TenTaiKhoan,
                })
                .ToList();
        }
        public bool ThemKhachHang(KhachHang khachHang)
        {
            try
            {
                _context.KhachHangs.Add(khachHang);
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
                MessageBox.Show("Lỗi thêm vào CSDL:\n" + fullError);
                return false;
            }
        }
        public bool XoaKhachHang(int khachHang)
        {
            try
            {
                var kh = _context.KhachHangs.FirstOrDefault(x => x.MaKh == khachHang);
                if (kh != null)
                {
                    _context.KhachHangs.Remove(kh);
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
                MessageBox.Show("Lỗi xóa khỏi CSDL:\n" + fullError);
                return false;
            }
        }
        public bool SuaKhachHang(KhachHang khachHang)
        {
            try
            {
                var kh = _context.KhachHangs.FirstOrDefault(x => x.MaKh == khachHang.MaKh);
                if (kh != null)
                {
                    kh.HoTen = khachHang.HoTen==null?kh.HoTen:khachHang.HoTen;
                    kh.Sdt = khachHang.Sdt==null?kh.Sdt:khachHang.Sdt;
                    kh.Email = khachHang.Email == null ? kh.Email : khachHang.Email;
                    kh.DiaChi = khachHang.DiaChi == null ? kh.DiaChi : khachHang.DiaChi;
                    kh.NgayDangKy = khachHang.NgayDangKy == null ? kh.NgayDangKy : khachHang.NgayDangKy;
                    kh.NgayHetHan = khachHang.NgayHetHan == null ? kh.NgayHetHan : khachHang.NgayHetHan;
                    kh.TrangThai = khachHang.TrangThai == null ? kh.TrangThai : khachHang.TrangThai;
                    kh.GioiTinh = khachHang.GioiTinh == null ? kh.GioiTinh : khachHang.GioiTinh;
                    kh.TenTaiKhoan = khachHang.TenTaiKhoan == null ? kh.TenTaiKhoan : khachHang.TenTaiKhoan;
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
                MessageBox.Show("Lỗi cập nhật CSDL:\n" + fullError);
                return false;
            }
        }
        public List<KhachHang> timkiemKH(string khachhang)
        {
            var nv = _context.KhachHangs
                .Where(x => x.HoTen.Contains(khachhang) || x.Email.Contains(khachhang) || x.TenTaiKhoan.Contains(khachhang))
                .ToList();
            return nv;
        }
        public bool taikhoandadung(string khachHang)
        {
            return _context.KhachHangs.Any(x=> x.TenTaiKhoan == khachHang);
        }
        public bool KiemTraTaiKhoanDaSuDungChoKhac(string tenTaiKhoan, int maKhachHang)
        {
            return _context.KhachHangs
                .Any(kh => kh.TenTaiKhoan == tenTaiKhoan && kh.MaKh != maKhachHang);
        }
    }
}
