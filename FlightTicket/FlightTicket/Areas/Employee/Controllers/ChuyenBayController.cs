//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using FlightTicket.Models;
//using FlightTicket.Areas.Employee.EmployeeQuery;

//namespace FlightTicket.Areas.Employee.Controllers
//{
//    public class ChuyenBayController : Controller
//    {
//        public ActionResult DanhSachChuyenBay()
//        {
//            List<getListChuyenBay> danhsachchuyenbay = new List<getListChuyenBay>();
//            danhsachchuyenbay = ChuyenBayQuery.getListChuyenBay();
//            ViewBag.danhsachchuyenbay = danhsachchuyenbay;
//            return View();
//        }
//        public ActionResult VeChuyenBay(int ID)
//        {
//            List<getVeChuyenBay> veChuyenBays = new List<getVeChuyenBay>();
//            veChuyenBays = ChuyenBayQuery.getVeChuyenBays(ID);
//            ViewBag.veChuyenBays = veChuyenBays;
//            return View();
//        }
//    }
//}