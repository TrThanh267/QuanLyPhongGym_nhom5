using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongGym_nhom5.DTO
{
    internal class BangDangKyDV_GT
    {
        public int MaKh { get; set; }
        public string TenKh { get; set; }
        public string TenGoiHoacDv { get; set; }
        public DateOnly? NgayBatDau { get; set; }
        public DateOnly? NgayKetThuc { get; set; }
        public bool? TrangThai { get; set; }
    }
}
