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
    internal class QuanLyPhongTap_DAL
    {
        QlGymContext _context;
        public QuanLyPhongTap_DAL(QlGymContext context)
        {
            _context = context;
        }
        public List<ChiTietPhongTap_DTO> GetAllchitietPhongTap()
        {
            return _context.ChiTietPhongTaps
                .AsNoTracking()
        .Include(pt => pt.MaPhongNavigation)
        .Include(pt => pt.MaNvNavigation)
        .Include(pt => pt.MaThietBiNavigation)
        .Select(pt => new ChiTietPhongTap_DTO
        {
            MaPhong = pt.MaPhong,
            TenPhong = pt.MaPhongNavigation != null ? pt.MaPhongNavigation.TenPhong : null,

            MaNv = pt.MaNv,
            TenNhanVienHuongDan = pt.MaNvNavigation != null ? pt.MaNvNavigation.TenNhanVien : null,

            MaThietBi = pt.MaThietBi,
            TenThietBi = pt.MaThietBiNavigation != null ? pt.MaThietBiNavigation.TenThietBi : null,

            ThoiGianTap = pt.ThoiGianTap,
            TrangThai = pt.TrangThai
        })
        .ToList();
        }
        public bool ThemChiTietPhongTap(ChiTietPhongTap chiTietpt)
        {
            try
            {
                _context.ChiTietPhongTaps.Add(chiTietpt);
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
                Console.WriteLine("Lỗi thêm Chi Tiết Phòng Tập:\n" + fullError);
                return false;
            }
        }
        public bool CapNhatChiTietPhongTap(ChiTietPhongTap chiTietpt)
        {
            try
            {
                var ptToUpdate = _context.ChiTietPhongTaps.FirstOrDefault(x => x.MaPhong == chiTietpt.MaPhong);
                if (ptToUpdate != null)
                {
                    ptToUpdate.MaNv = chiTietpt.MaNv == null ? ptToUpdate.MaNv : chiTietpt.MaNv;
                    ptToUpdate.MaThietBi = chiTietpt.MaThietBi == null ? ptToUpdate.MaThietBi : chiTietpt.MaThietBi;
                    ptToUpdate.ThoiGianTap = chiTietpt.ThoiGianTap == null ? ptToUpdate.ThoiGianTap : chiTietpt.ThoiGianTap;
                    ptToUpdate.TrangThai = chiTietpt.TrangThai == null ? ptToUpdate.TrangThai : chiTietpt.TrangThai;
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
                Console.WriteLine("Lỗi cập nhật Chi Tiết Phòng Tập:\n" + fullError);
                return false;
            }
        }
        public bool XoaChiTietPhongTap(ChiTietPhongTap chiTietpt)
        {
            try
            {
                var ptToDelete = _context.ChiTietPhongTaps.FirstOrDefault(x => x.MaPhong == chiTietpt.MaPhong);
                if (ptToDelete != null)
                {
                    _context.ChiTietPhongTaps.Remove(ptToDelete);
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
                Console.WriteLine("Lỗi xóa Chi Tiết Phòng Tập:\n" + fullError);
                return false;
            }
        }
        public List<ChiTietPhongTap_DTO> timkiem(string keyword)
        {
            return _context.ChiTietPhongTaps
        .Include(pt => pt.MaPhongNavigation)
        .Include(pt => pt.MaNvNavigation)
        .Include(pt => pt.MaThietBiNavigation)
        .Where(pt =>
            pt.MaPhong.ToString().Contains(keyword) ||
            (pt.MaPhongNavigation.TenPhong != null && pt.MaPhongNavigation.TenPhong.Contains(keyword)) ||
            (pt.MaNvNavigation != null && pt.MaNvNavigation.TenNhanVien.Contains(keyword)) ||
            (pt.MaThietBiNavigation != null && pt.MaThietBiNavigation.TenThietBi.Contains(keyword))
        )
        .Select(pt => new ChiTietPhongTap_DTO
        {
            MaPhong = pt.MaPhong,
            TenPhong = pt.MaPhongNavigation.TenPhong,
            MaNv = pt.MaNv,
            TenNhanVienHuongDan = pt.MaNvNavigation != null ? pt.MaNvNavigation.TenNhanVien : null,
            MaThietBi = pt.MaThietBi,
            TenThietBi = pt.MaThietBiNavigation != null ? pt.MaThietBiNavigation.TenThietBi : null,
            ThoiGianTap = pt.ThoiGianTap,
            TrangThai = pt.TrangThai
        })
        .ToList();
        }

    }
}
