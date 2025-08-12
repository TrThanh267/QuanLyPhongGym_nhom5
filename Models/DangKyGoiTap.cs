using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace QuanLyPhongGym_nhom5.Models;

public partial class DangKyGoiTap
{
    public int MaGoiTap { get; set; }

    public int MaKh { get; set; }

    public DateOnly? NgayBatDau { get; set; }

    public DateOnly? NgayKetThuc { get; set; }
    
    public bool? TrangThai { get; set; }
    [Browsable(false)]
    public virtual GoiTap MaGoiTapNavigation { get; set; } = null!;
    [Browsable(false)]
    public virtual KhachHang MaKhNavigation { get; set; } = null!;
}
