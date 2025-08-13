using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym_nhom5.DTO;
using QuanLyPhongGym_nhom5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongGym_nhom5.DAL
{
    internal class HienThiDangKy_DAL
    {
        QlGymContext _context;
        public HienThiDangKy_DAL(QlGymContext context)
        {
            _context = context;
        }
        public List<BangDangKyDV_GT> GetDangKy()
        {
                var dangKyGoiTap = _context.DangKyGoiTaps
                .Select(x => new BangDangKyDV_GT
                {
                MaKh = x.MaKh,
                TenKh = x.MaKhNavigation.HoTen,
                TenGoiHoacDv = x.MaGoiTapNavigation.TenGoiTap,
                NgayBatDau = x.NgayBatDau,
                NgayKetThuc = x.NgayKetThuc,
                TrangThai = x.NgayKetThuc >= DateOnly.FromDateTime(DateTime.Now)
                });

                var dangKyDichVu = _context.DangKyDichVus
                .Select(x => new BangDangKyDV_GT
                {
                    MaKh = x.MaKh,
                    TenKh = x.MaKhNavigation.HoTen,
                    TenGoiHoacDv = x.MaDvNavigation.TenDv,
                    NgayBatDau = x.NgayBatDau,
                    NgayKetThuc = x.NgayKetThuc,
                    TrangThai = x.NgayKetThuc >= DateOnly.FromDateTime(DateTime.Now)
                });

                var danhSachTongHop = dangKyGoiTap
                .Concat(dangKyDichVu)
                .OrderByDescending(x => x.NgayBatDau)
                .ToList();
                return danhSachTongHop;
        }
        
        
    }
}
