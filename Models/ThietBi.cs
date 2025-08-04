using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace QuanLyPhongGym_nhom5.Models;

public partial class ThietBi
{
    public int MaThietBi { get; set; }

    public string? TenThietBi { get; set; }
    [Browsable(false)]
    public int? ThoiGianTap { get; set; }

    public int? SoLuongThietBi { get; set; }
    [Browsable(false)]
    public virtual ICollection<ChiTietPhongTap> ChiTietPhongTaps { get; set; } = new List<ChiTietPhongTap>();
}
