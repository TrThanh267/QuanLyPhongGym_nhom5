using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace QuanLyPhongGym_nhom5.Models;

public partial class TaiKhoan
{
    public string TenTaiKhoan { get; set; } = null!;

    public int? MaVaiTro { get; set; }

    public string? MatKhau { get; set; }
    [Browsable(false)]
    public virtual ICollection<KhachHang> KhachHangs { get; set; } = new List<KhachHang>();
    [Browsable(false)]
    public virtual VaiTro? MaVaiTroNavigation { get; set; }
    [Browsable(false)]
    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
