    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongGym_nhom5.DTO
{
    internal class ChiTietPhongTap_DTO
    {

            public int MaPhong { get; set; }
            public string? TenPhong { get; set; }
            
            public int? MaNv { get; set; }
            public string? TenNhanVienHuongDan { get; set; }

            public int MaThietBi { get; set; }
            public string? TenThietBi { get; set; }

            public int? ThoiGianTap { get; set; }

            public bool? TrangThai { get; set; }
        
}
}
