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
            return _context.KhachHangs
                .Include(dk => dk.DangKyGoiTaps)
                .Include(dk => dk.DangKyDichVus)
                .Select(dk => new BangDangKyDV_GT
                {
                    MaKh = dk.MaKh,
                    MaGoiTap = dk.DangKyGoiTaps.Select(gt => gt.MaGoiTap).FirstOrDefault(),
                    MaDichVu = dk.DangKyDichVus.Select(dv => dv.MaDv).FirstOrDefault(),
                    TenKhachHang = dk.HoTen
                })
                .ToList();
        }
        public string GetTenGoiTapById(int maGT)
        {
            return _context.GoiTaps.Where(gt => gt.MaGoiTap == maGT)
                .Select(gt => gt.TenGoiTap)
                .FirstOrDefault() ?? string.Empty;
        }
        public string GetTenDichVuById(int maDV)
        {
            return _context.DichVus.Where(dv => dv.MaDv == maDV)
                .Select(dv => dv.TenDv)
                .FirstOrDefault() ?? string.Empty;
        }
    }
}
