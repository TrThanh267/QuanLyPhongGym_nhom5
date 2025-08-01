using System;
using System.Collections.Generic;
using System.ComponentModel;

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
    [Browsable(false)]
    public string? TenTaiKhoan { get; set; }
    [Browsable(false)]
    public virtual ICollection<DangKyDichVu> DangKyDichVus { get; set; } = new List<DangKyDichVu>();
    [Browsable(false)]
    public virtual ICollection<DangKyGoiTap> DangKyGoiTaps { get; set; } = new List<DangKyGoiTap>();
    [Browsable(false)]
    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
    [Browsable(false)]
    public virtual TaiKhoan? TenTaiKhoanNavigation { get; set; }
}
