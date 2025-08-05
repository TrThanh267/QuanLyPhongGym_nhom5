using System;
using System.Collections.Generic;
using System.ComponentModel;

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
    [Browsable(false)]
    public virtual DichVu? MaDvNavigation { get; set; }
    [Browsable(false)]
    public virtual GoiTap? MaGoiTapNavigation { get; set; }
    [Browsable(false)]
    public virtual HoaDon? MaHdNavigation { get; set; }
}
