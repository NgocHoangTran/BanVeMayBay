using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlightTicket.Models;
namespace FlightTicket.Controllers
{
    public class BookTicketController : Controller
    {
        // GET: BookTicket
        private DB_A6C0B2_Nhom13FlightTicketEntities db = new DB_A6C0B2_Nhom13FlightTicketEntities();
        public ActionResult Index()
        {
            ViewBag.lstCbay = db.ChuyenBays.Include("SanBay").ToList();
            ViewBag.lstHangVe = new SelectList(db.HangVes, "MaHangVe", "TenHangVe");
            return View();
        }
        public ActionResult SelectFlight(string idCB,string hangVe)
        {
            int id = Convert.ToInt32(idCB);
            ChuyenBay cb = db.ChuyenBays.Where(x => x.MaCB == id).FirstOrDefault();
            TimeSpan time = DateTime.Now - cb.NgayGioKhoiHanh;
            if (time.Days > cb.NgayDatChamNhat)
            {
                return Content("Tre han");
            }
            Session["chuyenBay"] = cb;
            Session["hangVe"] = hangVe;
            return RedirectToAction("Form");
        }
        public ActionResult Form()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetUserInfo(NguoiDung user)
        {
            Session["nguoiDung"] = user;
            return RedirectToAction("ShowSeats");
        }
        public ActionResult ShowSeats()
        {
            //var cbay = Session["chuyenBay"] as ChuyenBay;
            //int hangVe = Convert.ToInt32(Session["hangVe"]);
            //int idCB = cbay.MaCB;
            //var lstChoNgoi = db.ChoNgois.Where(x => x.MaCB == idCB && x.MaHangVe==hangVe).ToList();
            return View();
            
        }
        public ActionResult getData()
        {
            var lstChoNgoi = db.ChoNgois.ToList();
            var myList = (from c in lstChoNgoi
                          select new { c.MaGhe, c.SoGhe, c.TinhTrang }).ToList();
            return Json(myList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult selectSeat(string MaGhe)
        {
            Session["MaGhe"] = MaGhe;
            return Redirect("");
        }
    }
}