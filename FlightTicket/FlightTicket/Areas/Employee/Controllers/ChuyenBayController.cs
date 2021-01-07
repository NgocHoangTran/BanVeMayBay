using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlightTicket.Models;
using FlightTicket.Areas.Employee.EmployeeQuery;


namespace FlightTicket.Areas.Employee.Controllers
{
    public class ChuyenBayController : Controller
    {
        public ActionResult DSChuyenBay()
        {
            List<DanhSachChuyenBay> dscb = new List<DanhSachChuyenBay>();
            dscb = ChuyenBayQuery.danhSachChuyenBays();
            ViewBag.dscb = dscb;
            return View();
        }
        public ActionResult VeChuyenBay(int ID)
        {
            List<DanhSachVeChuyenBay> dsvcb = new List<DanhSachVeChuyenBay>();
            dsvcb = ChuyenBayQuery.danhSachVeChuyenBays(ID);
            ViewBag.dsvcb = dsvcb;
            return View();
        }
        public ActionResult XuatVe(int id)
        {
            LayVeCuaND veCuaND = new LayVeCuaND();
            veCuaND = ChuyenBayQuery.get01ve(id);
            return View(veCuaND);
        }

    }
}