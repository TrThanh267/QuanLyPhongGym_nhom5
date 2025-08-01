using System;
using System.Collections.Generic;

namespace QuanLyPhongGym_nhom5.Models;

public partial class VaiTro
{
    public int MaVaiTro { get; set; }

    public string? TenVaiTro { get; set; }

    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
}
