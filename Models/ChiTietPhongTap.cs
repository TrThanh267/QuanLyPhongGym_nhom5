using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace QuanLyPhongGym_nhom5.Models;

public partial class ChiTietPhongTap
{
    public int MaPhong { get; set; }

    public int MaThietBi { get; set; }

    public int? MaNv { get; set; }

    public string? TenPhong { get; set; }
    [Browsable(false)]
    public int? ThoiGianTap { get; set; }

    public bool? TrangThai { get; set; }
    [Browsable(false)]
    public virtual NhanVien? MaNvNavigation { get; set; }
    [Browsable(false)]
    public virtual PhongTap MaPhongNavigation { get; set; } = null!;
    [Browsable(false)]
    public virtual ThietBi MaThietBiNavigation { get; set; } = null!;
}
