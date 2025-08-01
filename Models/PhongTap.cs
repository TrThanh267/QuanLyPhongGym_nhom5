using System;
using System.Collections.Generic;

namespace QuanLyPhongGym_nhom5.Models;

public partial class PhongTap
{
    public int MaPhong { get; set; }

    public string? TenPhong { get; set; }

    public virtual ICollection<ChiTietPhongTap> ChiTietPhongTaps { get; set; } = new List<ChiTietPhongTap>();
}
