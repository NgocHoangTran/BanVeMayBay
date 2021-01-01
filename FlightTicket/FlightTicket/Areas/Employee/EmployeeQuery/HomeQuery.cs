using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlightTicket.Models;

namespace FlightTicket.Areas.Employee.EmployeeQuery
{
    public class HomeQuery
    {
        public static bool LoginEmployee(string taikhoan, string matkhau)
        {
            using (var _context= new DB_A6C0B2_Nhom13FlightTicketEntities())
            {
                var dbnhanvien = (from nguoidung in _context.NguoiDungs
                                  where nguoidung.TaiKhoan == taikhoan && nguoidung.MatKhau == matkhau
                                  select nguoidung).ToList();
                if (dbnhanvien.Count == 1)
                    return true;
                return false;
            }
        }
        public static NguoiDung getUser(string taikhoan)
        {
            using (var _contet= new DB_A6C0B2_Nhom13FlightTicketEntities())
            {
                var dbuser = (from nguoidung in _contet.NguoiDungs
                              where nguoidung.TaiKhoan == taikhoan
                              select nguoidung).SingleOrDefault();
                return dbuser;
            }
        }
    }
}