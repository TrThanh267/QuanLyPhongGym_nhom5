using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace QuanLyPhongGym_nhom5.Models;

public partial class NhanVien
{
    public int MaNv { get; set; }

    public string? TenNhanVien { get; set; }

    public string? DiaChi { get; set; }

    public string? Email { get; set; }

    public DateOnly? NgayVaoLam { get; set; }

    public string? Sdt { get; set; }

    public decimal? Luong { get; set; }
    public string? TenTaiKhoan { get; set; }
    [Browsable(false)]
    public virtual ICollection<ChiTietPhongTap> ChiTietPhongTaps { get; set; } = new List<ChiTietPhongTap>();
    [Browsable(false)]
    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
    [Browsable(false)]
    public virtual TaiKhoan? TenTaiKhoanNavigation { get; set; }
}
