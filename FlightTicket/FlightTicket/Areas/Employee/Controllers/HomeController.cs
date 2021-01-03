using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlightTicket.Areas.Employee.EmployeeQuery;
using FlightTicket.Models;

namespace FlightTicket.Areas.Employee.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var us = Session["Employee"];
            if (us != null)
                return View();
            return RedirectToAction("LoginEmployee");
        }
        public ActionResult LoginEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginEmployee(NguoiDung User)
        {
            if (HomeQuery.LoginEmployee(User.TaiKhoan, User.MatKhau) == true)
            {
                NguoiDung us = HomeQuery.getUser(User.TaiKhoan);
                if (us != null)
                {
                    Session["Employee"] = us;
                    if (us.ID_Quyen == 2)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Login");
            }
            return RedirectToAction("Login");
        }
        
    }
}