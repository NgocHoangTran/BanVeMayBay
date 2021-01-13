using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlightTicket.Models;

namespace FlightTicket.Controllers
{
    public class Homepage01Controller : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginCheck(FormCollection form)
        {
            string tk = form["account"];
            string pass = Encrypt.GetMD5(form["password"]);
            using (var db = new DB_A6C0B2_Nhom13FlightTicketEntities())
            {
                var user = db.NguoiDungs.Where(x => x.TaiKhoan == tk && x.MatKhau == pass).FirstOrDefault();
                if (user != null)
                {
                    Session["nguoiDung"] = user;
                    return Redirect("/BookTicket01/Index");
                }
                else
                {
                    ViewBag.mess = "Dang nhap that bai";
                    return View("Error");
                }
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterCheck(FormCollection form)
        {
            string tk = form["account"];
            string pass = Encrypt.GetMD5(form["password"]);
            NguoiDung u = new NguoiDung() { TaiKhoan = tk, MatKhau = pass, CMND="",Gmail="",DiaChi="",ID_Quyen=3,SoDT="",NgaySinh=DateTime.Now,HoTen="" };
            using (var db = new DB_A6C0B2_Nhom13FlightTicketEntities())
            {
                var user = db.NguoiDungs.Where(x => x.TaiKhoan == tk).FirstOrDefault();
                if (user == null)
                {
                    using(var context=new DB_A6C0B2_Nhom13FlightTicketEntities())
                    {
                        context.NguoiDungs.Add(u);
                        context.SaveChanges();
                    }
                    
                    Session["nguoiDung"] = user;
                    return Redirect("/BookTicket01/Index");
                }
                else
                {
                    ViewBag.mess = "Dang ky that bai";
                    return View("Error");
                }
            }
        }
    }
}