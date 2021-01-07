using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlightTicket.Models;
using FlightTicket.Areas.Employee.Controllers;

namespace FlightTicket.Areas.Employee.EmployeeQuery
{
    public class ChuyenBayQuery
    {
        public static List<DanhSachChuyenBay> danhSachChuyenBays()
        {
            using ( var _context= new DB_A6C0B2_Nhom13FlightTicketEntities())
            {
                var dbdscb = (from chuyenbay in _context.ChuyenBays
                              join sanbaydi in _context.SanBays on chuyenbay.SanBayDi equals sanbaydi.MaSB
                              join sanbayden in _context.SanBays on chuyenbay.SanBayDen equals sanbayden.MaSB
                              select new DanhSachChuyenBay { MaCB=chuyenbay.MaCB, SanBayDi = sanbaydi.TenSB, SanBayDen = sanbayden.TenSB, NgayGioKhoiHanh = chuyenbay.NgayGioKhoiHanh, NgayDatChamNhat = chuyenbay.NgayDatChamNhat, NgayHuyChamNhat = chuyenbay.NgayHuyChamNhat, SoGheConLai = chuyenbay.SoGheConLai }).ToList();
                return dbdscb;
            }
        }
        public static List<DanhSachVeChuyenBay> danhSachVeChuyenBays(int id)
        {
            using (var _context = new DB_A6C0B2_Nhom13FlightTicketEntities())
            {
                var dbdsvcb = (from nguoidung in _context.NguoiDungs
                               join vechuyenbay in _context.VeChuyenBays on nguoidung.MaNguoiDung equals vechuyenbay.MaNguoiDung
                               join chongoi in _context.ChoNgois on vechuyenbay.MaGhe equals chongoi.MaGhe
                               join chuyenbay in _context.ChuyenBays on chongoi.MaCB equals chuyenbay.MaCB
                               join hangve in _context.HangVes on chongoi.MaHangVe equals hangve.MaHangVe
                               join giatien in _context.DonGias on hangve.MaHangVe equals giatien.MaHangVe
                               where chuyenbay.MaCB==id
                               select new DanhSachVeChuyenBay { MaCB=chuyenbay.MaCB, HoTen= nguoidung.HoTen, CMND=nguoidung.CMND, SoDienThoai=nguoidung.SoDT, SoGhe= chongoi.SoGhe, HangVe= hangve.TenHangVe, GiaTien=giatien.Gia, MaGhe=chongoi.MaGhe }).ToList();
                return dbdsvcb;
            }
        }
        public static LayVeCuaND get01ve(int id)
        {
            using (var _context= new DB_A6C0B2_Nhom13FlightTicketEntities())
            {
                var get01vemb = (from nguoidung in _context.NguoiDungs
                                 join vechuyenbay in _context.VeChuyenBays on nguoidung.MaNguoiDung equals vechuyenbay.MaNguoiDung
                                 join chongoi in _context.ChoNgois on vechuyenbay.MaGhe equals chongoi.MaGhe
                                 join hangve in _context.HangVes on chongoi.MaHangVe equals hangve.MaHangVe
                                 join giatien in _context.DonGias on hangve.MaHangVe equals giatien.MaHangVe
                                 join chuyenbay in _context.ChuyenBays on chongoi.MaCB equals chuyenbay.MaCB
                                 where chongoi.MaGhe == id
                                 select new LayVeCuaND {id_ND=nguoidung.MaNguoiDung, HoTen = nguoidung.HoTen, CMND = nguoidung.CMND, SDT = nguoidung.SoDT, SoGhe=chongoi.SoGhe, HangVe= hangve.TenHangVe, giatien= giatien.Gia, maGhe=chongoi.MaGhe, MaCB=chongoi.MaCB}).SingleOrDefault();
                return get01vemb;
            }
        }
        
    }
}