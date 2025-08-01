using System;
using System.Collections.Generic;

namespace QuanLyPhongGym_nhom5.Models;

public partial class KhachHang
{
    public int MaKh { get; set; }

    public string? HoTen { get; set; }

    public string? Sdt { get; set; }

    public string? GioiTinh { get; set; }

    public string? DiaChi { get; set; }

    public string? Email { get; set; }

    public DateOnly? NgayDangKy { get; set; }

    public DateOnly? NgayHetHan { get; set; }

    public bool? TrangThai { get; set; }

    public string? TenTaiKhoan { get; set; }

    public virtual ICollection<DangKyDichVu> DangKyDichVus { get; set; } = new List<DangKyDichVu>();

    public virtual ICollection<DangKyGoiTap> DangKyGoiTaps { get; set; } = new List<DangKyGoiTap>();

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual TaiKhoan? TenTaiKhoanNavigation { get; set; }
}
