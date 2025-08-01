using System;
using System.Collections.Generic;

namespace QuanLyPhongGym_nhom5.Models;

public partial class ThietBi
{
    public int MaThietBi { get; set; }

    public string? TenThietBi { get; set; }

    public int? ThoiGianTap { get; set; }

    public int? SoLuongThietBi { get; set; }

    public virtual ICollection<ChiTietPhongTap> ChiTietPhongTaps { get; set; } = new List<ChiTietPhongTap>();
}
