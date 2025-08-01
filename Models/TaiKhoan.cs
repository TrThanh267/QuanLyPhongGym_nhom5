using System;
using System.Collections.Generic;

namespace QuanLyPhongGym_nhom5.Models;

public partial class TaiKhoan
{
    public string TenTaiKhoan { get; set; } = null!;

    public int? MaVaiTro { get; set; }

    public string? MatKhau { get; set; }

    public virtual ICollection<KhachHang> KhachHangs { get; set; } = new List<KhachHang>();

    public virtual VaiTro? MaVaiTroNavigation { get; set; }

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
