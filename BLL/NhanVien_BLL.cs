using QuanLyPhongGym.DAL;
using QuanLyPhongGym_nhom5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongGym_nhom5.BLL
{
    internal class NhanVien_BLL
    {
        private readonly QuanLyNhanVien_DAL _nhanVienDal;
        public NhanVien_BLL(QlGymContext dbContext)
        {
            _nhanVienDal = new QuanLyNhanVien_DAL(dbContext);
        }
        public List<NhanVien> GetNhanVienWithVaiTro()
        {
            return _nhanVienDal.GetNhanVienWithVaiTro();
        }
        public bool AddNhanVien(NhanVien nhanVien)
        {
            _nhanVienDal.them(nhanVien);
            if(nhanVien.MaNv > 0) // Assuming MaNv is auto-incremented and greater than 0 if added successfully
            {
                MessageBox.Show("Thêm nhân viên thành công!");
                return true;
            }
            else
            {
                MessageBox.Show("Thêm nhân viên không thành công!");
                return false;
            }
        }
        public bool UpdateNhanVien(NhanVien nhanVien)
        {
            return _nhanVienDal.sua(nhanVien);
            
        }
        public bool DeleteNhanVien(NhanVien nhanVien)
        {
            _nhanVienDal.xoa(nhanVien);
            if (nhanVien.MaNv > 0) // Assuming MaNv is auto-incremented and greater than 0 if deleted successfully
            {
                MessageBox.Show("Xóa nhân viên thành công!");
                return true;
            }
            else
            {
                MessageBox.Show("Xóa nhân viên không thành công!");
                return false;
            }
        }
        public bool KiemTraTaiKhoanDaSuDung(string tenTaiKhoan)
        {
            return  _nhanVienDal.kiemtrataikhoan(tenTaiKhoan);
        }
        public List<NhanVien> timkiem(string tenNhanVien)
        {
            return _nhanVienDal.timkiem(tenNhanVien);
        }
        
    }
}
