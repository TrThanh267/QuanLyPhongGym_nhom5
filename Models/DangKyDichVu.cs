using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace QuanLyPhongGym_nhom5.Models;

public partial class DangKyDichVu
{
    public int MaDv { get; set; }

    public int MaKh { get; set; }

    public DateOnly? NgayBatDau { get; set; }

    public DateOnly? NgayKetThuc { get; set; }
    [Browsable(false)]
    public bool? TrangThai { get; set; }
    [Browsable(false)]
    public virtual DichVu MaDvNavigation { get; set; } = null!;
    [Browsable(false)]
    public virtual KhachHang MaKhNavigation { get; set; } = null!;
}
