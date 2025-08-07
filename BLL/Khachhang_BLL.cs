using QuanLyPhongGym_nhom5.DAL;
using QuanLyPhongGym_nhom5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongGym_nhom5.BLL
{
    internal class Khachhang_BLL
    {
        private readonly QuanLyKhachHang_DAL _khachhangDal;
        public Khachhang_BLL(QlGymContext dbContext)
        {
            _khachhangDal = new QuanLyKhachHang_DAL(dbContext);
        }
        public List<KhachHang> GetKhachHangWithGoiTap()
        {
            return _khachhangDal.GetKhachHangWithGoiTap();
        }
        public bool AddKhachHang(KhachHang khachHang)
        {
            var kh= _khachhangDal.ThemKhachHang(khachHang);
            if (kh)
            {
                MessageBox.Show("Thêm khách hàng thành công!");
                return true;
            }
            else
            {
                MessageBox.Show("Thêm khách hàng không thành công!");
                return false;
            }
        }
        public bool UpdateKhachHang(KhachHang khachHang)
        {
            return _khachhangDal.SuaKhachHang(khachHang);
        }
        public bool DeleteKhachHang(int khachHang)
        {
            return _khachhangDal.XoaKhachHang(khachHang);
        }
        public List<KhachHang> TimKiem(string tenKhachHang)
        {
            return _khachhangDal.timkiemKH(tenKhachHang);
        }
        public bool KiemTraTaiKhoanDaSuDung(string tenTaiKhoan)
        {
            return _khachhangDal.taikhoandadung(tenTaiKhoan);
        }
        public bool KiemTraTaiKhoanDaSuDungChoKhac(string tenTaiKhoan, int maKhachHang)
        {
            return _khachhangDal.KiemTraTaiKhoanDaSuDungChoKhac(tenTaiKhoan, maKhachHang);
        }
    }
}
