using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym_nhom5.Models;

namespace QuanLyPhongGym_nhom5.DAL
{
    internal class QuanLyChiTietHoaDon_DAL
    {
        QlGymContext _context;
        public QuanLyChiTietHoaDon_DAL(QlGymContext context)
        {
            _context = context;
        }
        public List<HoaDonChiTiet> GetHoaDonChiTiets()
        {
            return _context.HoaDonChiTiets.Include(h => h.MaHdNavigation).
                                           Include(h=>h.MaGoiTapNavigation).
                                           Include(h=>h.MaDvNavigation).
                                           Select(h=> new HoaDonChiTiet
                                           {
                                                  MaHdct = h.MaHdct,
                                                  MaHd = h.MaHd,
                                                  MaGoiTap = h.MaGoiTap,
                                                  MaDv = h.MaDv,
                                                  SoLuong = h.SoLuong,
                                                  DonGia = (h.MaGoiTapNavigation != null ? h.MaGoiTapNavigation.Gia : 0)+ (h.MaDvNavigation != null ? h.MaDvNavigation.Gia * (h.SoLuong) : 0),
                                                  ThanhTien = (h.MaGoiTapNavigation != null ? h.MaGoiTapNavigation.Gia : 0)+ (h.MaDvNavigation != null ? h.MaDvNavigation.Gia * (h.SoLuong) : 0)
                                           }).ToList();
        }
        public bool AddChiTietHoaDon(HoaDonChiTiet hoaDonChiTiet)
        {
            try
            {
                _context.HoaDonChiTiets.Add(hoaDonChiTiet);
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
        public bool DeleteChiTietHoaDon(HoaDonChiTiet hoaDonChiTiet)
        {
            try
            {
                var hdct = _context.HoaDonChiTiets.FirstOrDefault(x => x.MaHdct == hoaDonChiTiet.MaHdct);
                if (hdct != null)
                {
                    _context.HoaDonChiTiets.Remove(hdct);
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
        public bool UpdateChiTietHoaDon(HoaDonChiTiet hoaDonChiTiet)
        {
            try
            {
                var hdct = _context.HoaDonChiTiets.FirstOrDefault(x => x.MaHdct == hoaDonChiTiet.MaHdct);
                if (hdct != null)
                {
                    hdct.MaHd = hoaDonChiTiet.MaHd;
                    hdct.MaGoiTap = hoaDonChiTiet.MaGoiTap;
                    hdct.MaDv = hoaDonChiTiet.MaDv;

                    int giaGoiTap = 0;
                    int giaDv = 0;
                    hdct.SoLuong = hoaDonChiTiet.SoLuong;

                    if (hoaDonChiTiet.MaGoiTap != null)
                    {
                        var goiTap = _context.GoiTaps.FirstOrDefault(g => g.MaGoiTap == hoaDonChiTiet.MaGoiTap);
                        if (goiTap != null)
                        {
                            giaGoiTap = (int)goiTap.Gia;
                        }
                    }

                    if (hoaDonChiTiet.MaDv != null)
                    {
                        var dichVu = _context.DichVus.FirstOrDefault(d => d.MaDv == hoaDonChiTiet.MaDv);
                        if (dichVu != null)
                        {
                            giaDv = (int)dichVu.Gia;
                        }
                    } 
                    hdct.DonGia = giaGoiTap + (giaDv * hdct.SoLuong);
                    hdct.ThanhTien = hdct.DonGia;

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
        public List<HoaDonChiTiet> timkiemHDCT(string keyword)
        {
            return _context.HoaDonChiTiets
                .Include(h => h.MaHdNavigation)
                .Include(h => h.MaGoiTapNavigation)
                .Include(h => h.MaDvNavigation)
                .Where(h => h.MaHdNavigation.MaHd.ToString().Contains(keyword) ||
                            h.MaGoiTapNavigation.TenGoiTap.Contains(keyword) ||
                            h.MaDvNavigation.TenDv.Contains(keyword))
                .Select(h => new HoaDonChiTiet
                {
                    MaHdct = h.MaHdct,
                    MaHd = h.MaHd,
                    MaGoiTap = h.MaGoiTap,
                    MaDv = h.MaDv,
                    SoLuong = h.SoLuong,
                    DonGia = h.DonGia,
                    ThanhTien = h.ThanhTien,
                })
                .ToList();

        }
    }
}
