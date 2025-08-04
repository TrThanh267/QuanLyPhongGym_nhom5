using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym_nhom5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongGym_nhom5.DAL
{
    internal class QuanLyGoiTap_DAL
    {
        QlGymContext _context;
        public QuanLyGoiTap_DAL(QlGymContext context)
        {
            _context = context;
        }
        public List<GoiTap> GetAllGoiTap()
        {
            return _context.GoiTaps.Include(gt => gt.DangKyGoiTaps)
                                    .Include(gt => gt.HoaDonChiTiets)
                                    .Select(gt => new GoiTap
                                    {
                                        MaGoiTap = gt.MaGoiTap,
                                        TenGoiTap = gt.TenGoiTap,
                                        Gia = gt.Gia,
                                        ThoiHan = gt.ThoiHan
                                    })
                                    .ToList();
        }
        public bool ThemGoiTap(GoiTap goiTap)
        {
            try
            {
                _context.GoiTaps.Add(goiTap);
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
        public bool CapNhatGoiTap(GoiTap goiTap)
        {
            try
            {
                var goiTapToUpdate = _context.GoiTaps.FirstOrDefault(x => x.MaGoiTap == goiTap.MaGoiTap);
                if (goiTapToUpdate != null)
                {
                    goiTapToUpdate.TenGoiTap = goiTap.TenGoiTap==null?goiTapToUpdate.TenGoiTap:goiTap.TenGoiTap;
                    goiTapToUpdate.Gia = goiTap.Gia == null ? goiTapToUpdate.Gia:goiTap.Gia;
                    goiTapToUpdate.ThoiHan = goiTap.ThoiHan == null ? goiTapToUpdate.ThoiHan : goiTap.ThoiHan;
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
        public bool XoaGoiTap(GoiTap goiTap)
        {
            try
            {
                var goiTapToDelete = _context.GoiTaps.FirstOrDefault(x => x.MaGoiTap == goiTap.MaGoiTap);
                if (goiTapToDelete != null)
                {
                    _context.GoiTaps.Remove(goiTapToDelete);
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
                Console.WriteLine("Lỗi xóa CSDL:\n" + fullError);
                return false;
            }
        }
        public List<GoiTap> TimKiemGoiTap(string tenGoiTap)
        {
            return _context.GoiTaps.Where(gt => gt.TenGoiTap.Contains(tenGoiTap) || gt.MaGoiTap.ToString() == tenGoiTap).ToList();
        }
    }
}
