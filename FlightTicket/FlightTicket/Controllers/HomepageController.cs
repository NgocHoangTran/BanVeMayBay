using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlightTicket.Models;
namespace FlightTicket.Controllers
{
    public class HomepageController : Controller
    {
        // GET: Homepage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LoginCheck(FormCollection form)
        {
            string tk = form["account"];
            string pass = form["password"];
            using(var db=new DB_A6C0B2_Nhom13FlightTicketEntities())
            {
                var user = db.NguoiDungs.Where(x => x.TaiKhoan == tk && x.MatKhau == pass).FirstOrDefault();
                if(user != null)
                {
                    Session["nguoiDung"] = user;
                    return Content("Dang nhap thanh cong");
                }
                else
                {
                    return Content("Dang nhap that bai");
                }
            }
        }
    }
}