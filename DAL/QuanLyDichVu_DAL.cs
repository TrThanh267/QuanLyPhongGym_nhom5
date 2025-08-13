using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym_nhom5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongGym_nhom5.DAL
{
    internal class QuanLyDichVu_DAL
    {
        QlGymContext _context;
        public QuanLyDichVu_DAL(QlGymContext context)
        {
            _context = context;
        }
        public List<DichVu> GetAllDichVu()
        {
            return _context.DichVus.Include(d => d.DangKyDichVus)
                                    .Include(d => d.HoaDonChiTiets)
                                    .Select(d => new DichVu
                                    {
                                        MaDv = d.MaDv,
                                        TenDv = d.TenDv,
                                        Gia = d.Gia,
                                        SoBuoiDk = d.SoBuoiDk,
                                    })
                                    .ToList();
        }
        public bool themdichvu(DichVu dichVu)
        {
            try
            {
                _context.DichVus.Add(dichVu);
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
        public bool capnhapDV(DichVu dichVu)
        {
            try
            {
                var dichVuToUpdate = _context.DichVus.FirstOrDefault(x=>x.MaDv==dichVu.MaDv);
                if (dichVuToUpdate != null)
                {
                    dichVuToUpdate.TenDv = dichVu.TenDv==null ? dichVuToUpdate.TenDv:dichVu.TenDv;
                    dichVuToUpdate.Gia = dichVu.Gia ==null ? dichVuToUpdate.Gia : dichVu.Gia;
                    dichVuToUpdate.SoBuoiDk = dichVu.SoBuoiDk ==null ? dichVuToUpdate.SoBuoiDk:dichVu.SoBuoiDk;
                    _context.SaveChanges();
                    
                }
                return true;
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
        public bool DeleteDichVu(int dichVu)
        {
            try
            {
                var dv = _context.DichVus.FirstOrDefault(x=>x.MaDv==dichVu);
                if (dv != null)
                {
                    _context.DichVus.Remove(dv);
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
        public List<DichVu> SearchDichVu(string searchTerm)
        {
            return _context.DichVus.Where(d => d.TenDv.Contains(searchTerm) || d.MaDv.ToString() == searchTerm).ToList();
        }
    }
}
