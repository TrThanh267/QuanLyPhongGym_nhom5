using System;
using System.Collections.Generic;

namespace QuanLyPhongGym_nhom5.Models;

public partial class DangKyGoiTap
{
    public int MaGoiTap { get; set; }

    public int MaKh { get; set; }

    public DateOnly? NgayBatDau { get; set; }

    public DateOnly? NgayKetThuc { get; set; }

    public bool? TrangThai { get; set; }

    public virtual GoiTap MaGoiTapNavigation { get; set; } = null!;

    public virtual KhachHang MaKhNavigation { get; set; } = null!;
}
