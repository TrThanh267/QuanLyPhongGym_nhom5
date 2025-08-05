using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongGym_nhom5.Models;

namespace QuanLyPhongGym_nhom5.DAL
{
    internal class DangKyDichVu_DAL
    {
        private readonly QlGymContext _context;

        public DangKyDichVu_DAL(QlGymContext context)
        {
            _context = context;
        }

        public List<DangKyDichVu> GetAll()
        {
                return _context.DangKyDichVus.Include(Index => Index.MaDvNavigation)
                                              .Include(Index => Index.MaKhNavigation)
                                              .Select(dk => new DangKyDichVu
                                              {
                                                  MaDv = dk.MaDv,
                                                  MaKh = dk.MaKh,
                                                  NgayBatDau = dk.NgayBatDau,
                                                  NgayKetThuc = dk.NgayKetThuc,
                                              })
                                            .ToList();
        }

        public bool Them(DangKyDichVu dk)
        {
            try
            {
                _context.DangKyDichVus.Add(dk);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi thêm ĐKDV: " + ex.Message);
                return false;
            }
        }

        public bool Xoa(int maDv, int maKh)
        {
            try
            {
                var dk = _context.DangKyDichVus.FirstOrDefault(x => x.MaDv == maDv && x.MaKh == maKh);
                if (dk != null)
                {
                    _context.DangKyDichVus.Remove(dk);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi xoá ĐKDV: " + ex.Message);
                return false;
            }
        }

    }
}