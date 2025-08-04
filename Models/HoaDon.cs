using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace QuanLyPhongGym_nhom5.Models;

public partial class HoaDon
{
    public int MaHd { get; set; }

    public int? MaKh { get; set; }

    public int? MaNv { get; set; }

    public DateOnly? NgayTao { get; set; }

    public string? HinhThucThanhToan { get; set; }
    [Browsable(false)]

    public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; } = new List<HoaDonChiTiet>();
    [Browsable(false)]
    public virtual KhachHang? MaKhNavigation { get; set; }
    [Browsable(false)]
    public virtual NhanVien? MaNvNavigation { get; set; }
}
