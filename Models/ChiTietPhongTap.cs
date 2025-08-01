using System;
using System.Collections.Generic;

namespace QuanLyPhongGym_nhom5.Models;

public partial class ChiTietPhongTap
{
    public int MaPhong { get; set; }

    public int MaThietBi { get; set; }

    public int? MaNv { get; set; }

    public string? TenPhong { get; set; }

    public int? ThoiGianTap { get; set; }

    public bool? TrangThai { get; set; }

    public virtual NhanVien? MaNvNavigation { get; set; }

    public virtual PhongTap MaPhongNavigation { get; set; } = null!;

    public virtual ThietBi MaThietBiNavigation { get; set; } = null!;
}
