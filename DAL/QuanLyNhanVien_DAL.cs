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
        public List<NhanVien> GetNhanVienWithVaiTro()
        {
            return _context.NhanViens
                .Include(nv => nv.TenTaiKhoanNavigation)
                .Select(nv => new NhanVien
                {
                    MaNv = nv.MaNv,
                    TenNhanVien = nv.TenNhanVien,
                    Sdt = nv.Sdt,
                    Email = nv.Email,
                    DiaChi = nv.DiaChi,
                    NgayVaoLam = nv.NgayVaoLam,
                    Luong = nv.Luong,
                    TenTaiKhoan = nv.TenTaiKhoan,
                }).ToList();
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
                    nv.TenNhanVien = nhanVien.TenNhanVien == null ? nv.TenNhanVien : nhanVien.TenNhanVien;
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
        public List<NhanVien> timkiem(string searchText)
        {
            return _context.NhanViens.Where(nv => nv.TenNhanVien.Contains(searchText) || nv.Sdt.Contains(searchText) || nv.Email.Contains(searchText)).ToList();
        }
        public bool kiemtrataikhoan(string taikhoan)
        {
            return _context.NhanViens.Any(x => x.TenTaiKhoan == taikhoan);
        }
    }
}
