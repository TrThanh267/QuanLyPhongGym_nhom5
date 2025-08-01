using System;
using System.Collections.Generic;

namespace QuanLyPhongGym_nhom5.Models;

public partial class DichVu
{
    public int MaDv { get; set; }

    public string? TenDv { get; set; }

    public decimal? Gia { get; set; }

    public int? SoBuoiDk { get; set; }

    public virtual ICollection<DangKyDichVu> DangKyDichVus { get; set; } = new List<DangKyDichVu>();

    public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; } = new List<HoaDonChiTiet>();
}
