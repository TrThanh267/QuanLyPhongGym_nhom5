using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym_nhom5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongGym_nhom5.DAL
{
    internal class QuanLyThietBi_DAL
    {
        QlGymContext _context;
        public QuanLyThietBi_DAL(QlGymContext context)
        {
            _context = context;
        }
        public List<ThietBi> GetAllThietBi()
        {
            return _context.ThietBis
                .Include(tb => tb.ChiTietPhongTaps)
                .Select(tb => new ThietBi
                {
                    MaThietBi = tb.MaThietBi,
                    TenThietBi = tb.TenThietBi,
                    SoLuongThietBi = tb.SoLuongThietBi,
                })
                .ToList();
        }
        public bool ThemThietBi(ThietBi thietBi)
        {
            try
            {
                _context.ThietBis.Add(thietBi);
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
        public bool XoaThietBi(ThietBi thietBi)
        {
            try
            {
                var tb = _context.ThietBis.FirstOrDefault(x => x.MaThietBi == thietBi.MaThietBi);
                if (tb != null)
                {
                    _context.ThietBis.Remove(tb);
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
                Console.WriteLine("Lỗi xóa thiết bị:\n" + fullError);
                return false;
            }
        }
        public bool CapNhatThietBi(ThietBi thietBi)
        {
            try
            {
                var tbToUpdate = _context.ThietBis.FirstOrDefault(x => x.MaThietBi == thietBi.MaThietBi);
                if (tbToUpdate != null)
                {
                    tbToUpdate.TenThietBi = thietBi.TenThietBi==null? tbToUpdate.TenThietBi : thietBi.TenThietBi;
                    tbToUpdate.SoLuongThietBi = thietBi.SoLuongThietBi == null ? tbToUpdate.SoLuongThietBi : thietBi.SoLuongThietBi;
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
                Console.WriteLine("Lỗi cập nhật thiết bị:\n" + fullError);
                return false;
            }
        }
        public List<ThietBi> TimKiemThietBi(string tenThietBi)
        {
            return _context.ThietBis
                .Where(tb => tb.TenThietBi.Contains(tenThietBi)).ToList();
        }
    }
}
