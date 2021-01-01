using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlightTicket.Models;

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
                              select new DanhSachChuyenBay { MaCB = chuyenbay.MaCB, SanBayDi = sanbaydi.TenSB, SanBayDen = sanbayden.TenSB, NgayGioKhoiHanh = chuyenbay.NgayGioKhoiHanh, NgayDatChamNhat = chuyenbay.NgayDatChamNhat, NgayHuyChamNhat = chuyenbay.NgayHuyChamNhat, SoGheConLai = chuyenbay.SoGheConLai }).ToList();
                return dbdscb;
            }
        }
        public static List<DanhSachVeChuyenBay> danhSachVeChuyenBays()
        {
            using (var _context= new DB_A6C0B2_Nhom13FlightTicketEntities())
            {
                var dbdsvcb = (from nguoidung in _context.NguoiDungs
                               join vechuyenbay in _context.VeChuyenBays on nguoidung.MaNguoiDung equals vechuyenbay.MaNguoiDung
                               join chuyenbay in _context.ChuyenBays on vechuyenbay.MaCB equals chuyenbay.MaCB
                               join hangve in _context.HangVes on vechuyenbay.MaVe equals hangve.MaHangVe
                               join dongia in _context.DonGias on hangve.MaHangVe equals dongia.MaHangVe
                               where dongia.MaCB == chuyenbay.MaCB
                               select new DanhSachVeChuyenBay {MaCB=chuyenbay.MaCB, HoTen=nguoidung.HoTen, SoDienThoai=nguoidung.SoDT, CMND=nguoidung.CMND, HangVe=hangve.TenHangVe, GiaTien=dongia.Gia}).ToList();
                return dbdsvcb;
            }
        }
    }
}