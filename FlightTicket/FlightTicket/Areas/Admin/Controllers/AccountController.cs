using FlightTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightTicket.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        DB_A6C0B2_Nhom13FlightTicketEntities db = new DB_A6C0B2_Nhom13FlightTicketEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["userID"] != null)
            {
                
                return View();
            }

            else
            {
                return View("Login");
            }
        }
        public ActionResult Login()
        {
            return View();

        }
        [HttpPost]// thuc hien dang nhap
        public ActionResult VerifyLogin(NguoiDung user)
        {

            // kiem tra du lieu nhap
          
                // truy van csdl 
                using (var _context = new DB_A6C0B2_Nhom13FlightTicketEntities())
                {
                    // query id tu email va password de kiem tra dang nhap
                    var obj = (from u in _context.NguoiDungs where u.Gmail == user.Gmail && u.MatKhau == user.MatKhau select u).FirstOrDefault();
                    
                    if (obj != null)
                    {
                        Session["userID"] = obj.MaNguoiDung.ToString();
                        Session["username"] = obj.TaiKhoan.ToString();
                       
                        //string username = obj.Username.ToString();
                        return RedirectToAction("Index", "Account");
                    }
                    else
                    {
                        return Json(new { status = "error", message = "Lỗi đăng nhập" });
                    }
                }
          
           
        }
    }
}