using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym_nhom5.Models;

namespace QuanLyPhongGym.DAL
{
    internal class QuanLyNhanVien_DAL
    {
        QlGymContext _context;
        public QuanLyNhanVien_DAL(QlGymContext context)
        {
            _context = context;
        }
        public List<object> GetNhanVienWithVaiTro()
        {
            return _context.NhanViens
                .Include(nv => nv.TenTaiKhoanNavigation)
                .ThenInclude(tk => tk.MaVaiTroNavigation)
                .Select(nv => new
                {
                    nv.MaNv,
                    nv.TenNhanVien,
                    nv.Sdt,
                    nv.Email,
                    nv.DiaChi,
                    nv.NgayVaoLam,
                    nv.Luong,
                    TenVaiTro = nv.TenTaiKhoanNavigation.MaVaiTroNavigation.TenVaiTro
                })
                .ToList<object>();
        }
        public bool them(NhanVien nhanVien)
        {
            try
            {
                _context.NhanViens.Add(nhanVien);
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
        public bool xoa(NhanVien nhanVien)
        {
            try
            {
                var nv = _context.NhanViens.FirstOrDefault(x => x.MaNv == nhanVien.MaNv);
                if (nv != null)
                {
                    _context.NhanViens.Remove(nv);
                    _context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool sua(NhanVien nhanVien)
        {
            try
            {
                var nv = _context.NhanViens.FirstOrDefault(x => x.MaNv == nhanVien.MaNv);
                if (nv != null)
                {
                    nv.TenNhanVien = nhanVien.TenNhanVien==null?nv.TenNhanVien:nhanVien.TenNhanVien;
                    nv.NgayVaoLam = nhanVien.NgayVaoLam == null ? nv.NgayVaoLam : nhanVien.NgayVaoLam;
                    nv.DiaChi = nhanVien.DiaChi == null ? nv.DiaChi : nhanVien.DiaChi;
                    nv.Email = nhanVien.Email == null ? nv.Email : nhanVien.Email;
                    nv.Sdt = nhanVien.Sdt == null ? nv.Sdt : nhanVien.Sdt;
                    nv.Luong = nhanVien.Luong == null ? nv.Luong : nhanVien.Luong;
                    nv.TenTaiKhoan = nhanVien.TenTaiKhoan == null ? nv.TenTaiKhoan : nhanVien.TenTaiKhoan;
                }
                _context.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }
        public List<object> timkiem(string searchText)
        {
            var result = _context.NhanViens
                .Include(nv => nv.TenTaiKhoanNavigation)
                .ThenInclude(tk => tk.MaVaiTroNavigation)
                .Where(nv => nv.TenNhanVien.Contains(searchText) || nv.TenTaiKhoan.Contains(searchText))
                .Select(nv => new
                {
                    nv.TenNhanVien,
                    nv.Sdt,
                    nv.Email,
                    nv.DiaChi,
                    nv.NgayVaoLam,
                    nv.Luong,
                    nv.TenTaiKhoan,
                    TenVaiTro = nv.TenTaiKhoanNavigation.MaVaiTroNavigation.TenVaiTro
                })
                .Cast<object>() // ép về object trước khi ToList
                .ToList();

            return result;
        }

    }
}
