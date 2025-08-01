using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuanLyPhongGym_nhom5.Models;

public partial class QlGymContext : DbContext
{
    public QlGymContext()
    {
    }

    public QlGymContext(DbContextOptions<QlGymContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietPhongTap> ChiTietPhongTaps { get; set; }

    public virtual DbSet<DangKyDichVu> DangKyDichVus { get; set; }

    public virtual DbSet<DangKyGoiTap> DangKyGoiTaps { get; set; }

    public virtual DbSet<DichVu> DichVus { get; set; }

    public virtual DbSet<GoiTap> GoiTaps { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<HoaDonChiTiet> HoaDonChiTiets { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<PhongTap> PhongTaps { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<ThietBi> ThietBis { get; set; }

    public virtual DbSet<VaiTro> VaiTros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=TROUBLE\\SQLEXPRESS03;Database=QL_Gym;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietPhongTap>(entity =>
        {
            entity.HasKey(e => new { e.MaPhong, e.MaThietBi }).HasName("PK__ChiTietP__48139944D54473A2");

            entity.ToTable("ChiTietPhongTap");

            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.TenPhong).HasMaxLength(100);

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.ChiTietPhongTaps)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK__ChiTietPho__MaNV__6754599E");

            entity.HasOne(d => d.MaPhongNavigation).WithMany(p => p.ChiTietPhongTaps)
                .HasForeignKey(d => d.MaPhong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPh__MaPho__656C112C");

            entity.HasOne(d => d.MaThietBiNavigation).WithMany(p => p.ChiTietPhongTaps)
                .HasForeignKey(d => d.MaThietBi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPh__MaThi__66603565");
        });

        modelBuilder.Entity<DangKyDichVu>(entity =>
        {
            entity.HasKey(e => new { e.MaDv, e.MaKh }).HasName("PK__DangKyDi__D557DAA6E21C208F");

            entity.ToTable("DangKyDichVu");

            entity.Property(e => e.MaDv).HasColumnName("MaDV");
            entity.Property(e => e.MaKh).HasColumnName("MaKH");

            entity.HasOne(d => d.MaDvNavigation).WithMany(p => p.DangKyDichVus)
                .HasForeignKey(d => d.MaDv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DangKyDich__MaDV__52593CB8");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.DangKyDichVus)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DangKyDich__MaKH__534D60F1");
        });

        modelBuilder.Entity<DangKyGoiTap>(entity =>
        {
            entity.HasKey(e => new { e.MaGoiTap, e.MaKh }).HasName("PK__DangKyGo__69428AA9C771A4FC");

            entity.ToTable("DangKyGoiTap");

            entity.Property(e => e.MaKh).HasColumnName("MaKH");

            entity.HasOne(d => d.MaGoiTapNavigation).WithMany(p => p.DangKyGoiTaps)
                .HasForeignKey(d => d.MaGoiTap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DangKyGoi__MaGoi__4E88ABD4");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.DangKyGoiTaps)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DangKyGoiT__MaKH__4F7CD00D");
        });

        modelBuilder.Entity<DichVu>(entity =>
        {
            entity.HasKey(e => e.MaDv).HasName("PK__DichVu__27258657D6FDF61A");

            entity.ToTable("DichVu");

            entity.Property(e => e.MaDv).HasColumnName("MaDV");
            entity.Property(e => e.Gia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SoBuoiDk).HasColumnName("SoBuoiDK");
            entity.Property(e => e.TenDv)
                .HasMaxLength(100)
                .HasColumnName("TenDV");
        });

        modelBuilder.Entity<GoiTap>(entity =>
        {
            entity.HasKey(e => e.MaGoiTap).HasName("PK__GoiTap__9B30D658C1E7C18E");

            entity.ToTable("GoiTap");

            entity.Property(e => e.Gia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TenGoiTap).HasMaxLength(100);
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHd).HasName("PK__HoaDon__2725A6E052B19076");

            entity.ToTable("HoaDon");

            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.HinhThucThanhToan).HasMaxLength(50);
            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.MaNv).HasColumnName("MaNV");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK__HoaDon__MaKH__59063A47");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK__HoaDon__MaNV__59FA5E80");
        });

        modelBuilder.Entity<HoaDonChiTiet>(entity =>
        {
            entity.HasKey(e => e.MaHdct).HasName("PK__HoaDonCh__1419C1298791C190");

            entity.ToTable("HoaDonChiTiet");

            entity.Property(e => e.MaHdct).HasColumnName("MaHDCT");
            entity.Property(e => e.DonGia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MaDv).HasColumnName("MaDV");
            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.ThanhTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaDvNavigation).WithMany(p => p.HoaDonChiTiets)
                .HasForeignKey(d => d.MaDv)
                .HasConstraintName("FK__HoaDonChiT__MaDV__5EBF139D");

            entity.HasOne(d => d.MaGoiTapNavigation).WithMany(p => p.HoaDonChiTiets)
                .HasForeignKey(d => d.MaGoiTap)
                .HasConstraintName("FK__HoaDonChi__MaGoi__5DCAEF64");

            entity.HasOne(d => d.MaHdNavigation).WithMany(p => p.HoaDonChiTiets)
                .HasForeignKey(d => d.MaHd)
                .HasConstraintName("FK__HoaDonChiT__MaHD__5CD6CB2B");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PK__KhachHan__2725CF1E9EAD4EAA");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.Sdt)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.TenTaiKhoan)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.TenTaiKhoanNavigation).WithMany(p => p.KhachHangs)
                .HasForeignKey(d => d.TenTaiKhoan)
                .HasConstraintName("FK__KhachHang__TenTa__4BAC3F29");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__NhanVien__2725D70ACC16890D");

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Luong).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Sdt)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.TenNhanVien).HasMaxLength(100);
            entity.Property(e => e.TenTaiKhoan)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.TenTaiKhoanNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.TenTaiKhoan)
                .HasConstraintName("FK__NhanVien__TenTai__5629CD9C");
        });

        modelBuilder.Entity<PhongTap>(entity =>
        {
            entity.HasKey(e => e.MaPhong).HasName("PK__PhongTap__20BD5E5B75D5339C");

            entity.ToTable("PhongTap");

            entity.Property(e => e.TenPhong).HasMaxLength(100);
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.TenTaiKhoan).HasName("PK__TaiKhoan__B106EAF97A1D8BC5");

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.TenTaiKhoan)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.MaVaiTroNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaVaiTro)
                .HasConstraintName("FK__TaiKhoan__MaVaiT__48CFD27E");
        });

        modelBuilder.Entity<ThietBi>(entity =>
        {
            entity.HasKey(e => e.MaThietBi).HasName("PK__ThietBi__8AEC71F7945FA0B1");

            entity.ToTable("ThietBi");

            entity.Property(e => e.TenThietBi).HasMaxLength(100);
        });

        modelBuilder.Entity<VaiTro>(entity =>
        {
            entity.HasKey(e => e.MaVaiTro).HasName("PK__VaiTro__C24C41CF41A55ACA");

            entity.ToTable("VaiTro");

            entity.Property(e => e.TenVaiTro).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
