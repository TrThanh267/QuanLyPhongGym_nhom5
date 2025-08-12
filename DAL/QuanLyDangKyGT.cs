using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym_nhom5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongGym_nhom5.DAL
{
    internal class QuanLyDangKyGT
    {
        private readonly QlGymContext _context;

        public QuanLyDangKyGT(QlGymContext context)
        {
            _context = context;
        }

        public List<DangKyGoiTap> LayTatCa()
        {
                return _context.DangKyGoiTaps.Include(gt => gt.MaGoiTapNavigation)
                                             .Include(gt => gt.MaKhNavigation)
                                             .Select(DangKyGoiTap => new DangKyGoiTap
                                             {
                                                 MaGoiTap = DangKyGoiTap.MaGoiTap,
                                                 MaKh = DangKyGoiTap.MaKh,
                                                 NgayBatDau = DangKyGoiTap.NgayBatDau,
                                                 NgayKetThuc = DangKyGoiTap.NgayKetThuc,
                                                 TrangThai = DangKyGoiTap.TrangThai
                                             })
                                             .ToList();
        }
        public bool Them(DangKyGoiTap dk)
        {
            try
            {
                _context.DangKyGoiTaps.Add(dk);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm ĐK Gói Tập: " + ex.Message);
                return false;
            }
        }

        public bool Xoa(int maGoiTap, int maKh)
        {
            try
            {
                var dk = _context.DangKyGoiTaps.FirstOrDefault(x => x.MaGoiTap == maGoiTap && x.MaKh == maKh);
                if (dk != null)
                {
                    _context.DangKyGoiTaps.Remove(dk);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xoá ĐK Gói Tập: " + ex.Message);
                return false;
            }
        }
    }
}
