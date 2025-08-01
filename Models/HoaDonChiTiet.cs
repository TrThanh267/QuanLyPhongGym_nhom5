using System;
using System.Collections.Generic;

namespace QuanLyPhongGym_nhom5.Models;

public partial class HoaDonChiTiet
{
    public int MaHdct { get; set; }

    public int? MaHd { get; set; }

    public int? MaGoiTap { get; set; }

    public int? MaDv { get; set; }

    public int? SoLuong { get; set; }

    public decimal? DonGia { get; set; }

    public decimal? ThanhTien { get; set; }

    public virtual DichVu? MaDvNavigation { get; set; }

    public virtual GoiTap? MaGoiTapNavigation { get; set; }

    public virtual HoaDon? MaHdNavigation { get; set; }
}
