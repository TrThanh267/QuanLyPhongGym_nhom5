using System;
using System.Collections.Generic;

namespace QuanLyPhongGym_nhom5.Models;

public partial class HoaDon
{
    public int MaHd { get; set; }

    public int? MaKh { get; set; }

    public int? MaNv { get; set; }

    public DateOnly? NgayTao { get; set; }

    public string? HinhThucThanhToan { get; set; }

    public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; } = new List<HoaDonChiTiet>();

    public virtual KhachHang? MaKhNavigation { get; set; }

    public virtual NhanVien? MaNvNavigation { get; set; }
}
