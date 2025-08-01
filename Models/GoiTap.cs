using System;
using System.Collections.Generic;

namespace QuanLyPhongGym_nhom5.Models;

public partial class GoiTap
{
    public int MaGoiTap { get; set; }

    public string? TenGoiTap { get; set; }

    public int? ThoiHan { get; set; }

    public decimal? Gia { get; set; }

    public virtual ICollection<DangKyGoiTap> DangKyGoiTaps { get; set; } = new List<DangKyGoiTap>();

    public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; } = new List<HoaDonChiTiet>();
}
