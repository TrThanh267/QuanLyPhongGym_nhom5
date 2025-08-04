using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym_nhom5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongGym_nhom5.DAL
{
    internal class QuanLyHoaDon_DAL
    {
        QlGymContext _context;

        public QuanLyHoaDon_DAL(QlGymContext context)
        {
            _context = context;
        }
        public List<HoaDon> GetHoaDonsWithKhachHang()
        {
            return _context.HoaDons
                .Include(hd => hd.MaKhNavigation)
                .Include(hd => hd.MaNvNavigation)
                .Select(hd => new HoaDon
                {
                    MaHd = hd.MaHd,
                    MaKh = hd.MaKh,
                    MaNv = hd.MaNv,
                    NgayTao = hd.NgayTao,
                    HinhThucThanhToan = hd.HinhThucThanhToan,
                })
                .ToList();
        }
        public bool ThemHoaDon(HoaDon hoaDon)
        {
            try
            {
                _context.HoaDons.Add(hoaDon);
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
        public bool XoaHoaDon(HoaDon hoaDon)
        {
            try
            {
                var hd = _context.HoaDons.FirstOrDefault(x => x.MaHd == hoaDon.MaHd);
                if (hd != null)
                {
                    _context.HoaDons.Remove(hd);
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
        public bool SuaHoaDon(HoaDon hoaDon)
        {
            try
            {
                var hd = _context.HoaDons.FirstOrDefault(x => x.MaHd == hoaDon.MaHd);
                if (hd != null)
                {
                    hd.MaKh = hoaDon.MaKh;
                    hd.MaNv = hoaDon.MaNv;
                    hd.NgayTao = hoaDon.NgayTao;
                    hd.HinhThucThanhToan = hoaDon.HinhThucThanhToan;
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
                MessageBox.Show("Lỗi sửa trong CSDL:\n" + fullError);
                return false;
            }
        }
        public List<HoaDon> TimKiemHoaDon(string searchText)
        {
            var hoadon= _context.HoaDons.Where(hd => hd.MaHd.ToString().Contains(searchText) ||
                                                hd.MaKhNavigation.HoTen.Contains(searchText) ||
                                                hd.MaNvNavigation.TenNhanVien.Contains(searchText)).ToList();
            return hoadon;
        }
    }
}
