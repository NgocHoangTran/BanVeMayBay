using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FlightTicket.Models;

namespace FlightTicket.Areas.Admin.Controllers
{
    public class ChuyenBaysController : Controller
    {
        private DB_A6C0B2_Nhom13FlightTicketEntities db = new DB_A6C0B2_Nhom13FlightTicketEntities();

        // GET: Admin/ChuyenBays
        public ActionResult Index()
        {
            ViewBag.SanBayDi = new SelectList(db.SanBays, "MaSB", "TenSB");
            ViewBag.SanBayDen = new SelectList(db.SanBays, "MaSB", "TenSB");
            var chuyenBays = db.ChuyenBays.Include(c => c.SanBay).Include(c => c.SanBay1);
            return View(chuyenBays.ToList());
        }

        // GET: Admin/ChuyenBays/Details/5
        public ActionResult Details(int id)
        {
            var cb = db.ChuyenBays.Where(x => x.MaCB == id).FirstOrDefault();
            var hvcb = (from c in db.HangVeCuaChuyenBays where c.MaCB == id select c).ToList();
            var sbtg = (from c in db.ChiTietChuyenBays where c.MaCB == id select c).ToList();
            
            ViewBag.SoHVCB = hvcb;
            ViewBag.SoSBTG = sbtg;
            ViewBag.MaHangVe = new SelectList(db.HangVes, "MaHangVe", "TenHangVe");
            ViewBag.SanBayTrungGian = new SelectList(db.SanBays, "MaSB", "TenSB");
            return View(cb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateHV(HangVeCuaChuyenBay hvcb)
        {
            if(ModelState.IsValid)
            {
                db.HangVeCuaChuyenBays.Add(hvcb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Json(new { status = "error", message = "Lỗi thêm hạng vé chuyến bay" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSBTG(ChiTietChuyenBay sbtg)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietChuyenBays.Add(sbtg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Json(new { status = "error", message = "Lỗi thêm sân bay trung gian" });
        }

        // POST: Admin/ChuyenBays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ChuyenBay chuyenBay)
        {
            chuyenBay.SoGheConLai = 0;
            if (ModelState.IsValid)
            {
                db.ChuyenBays.Add(chuyenBay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Json(new { status = "error", message = "Lỗi thêm chuyến bay" });
        }
        public ActionResult EditSBTG(int idcb,int idhv)
        {
            var sbtg = (from c in db.ChiTietChuyenBays where c.MaCB == idcb && c.SanBayTrungGian==idhv select c).FirstOrDefault();
           
            ViewBag.SanBayTrungGian = new SelectList(db.SanBays, "MaSB", "TenSB");
            return PartialView("_EditSBTG", sbtg);

        }
        public ActionResult EditHVCB(int idcb, int idhv)
        {
            var hvcb = (from c in db.HangVeCuaChuyenBays where c.MaCB == idcb && c.MaHangVe == idhv select c).FirstOrDefault();

            ViewBag.HangVe = new SelectList(db.HangVes, "MaHangVe", "TenHangVe");
            return PartialView("_EditHVCB", hvcb);

        }
        // GET: Admin/ChuyenBays/Edit/5
        public ActionResult Edit(int id)
        {
            var cb = db.ChuyenBays.Where(x => x.MaCB == id).FirstOrDefault();
            ViewBag.SanBayDi = new SelectList(db.SanBays, "MaSB", "TenSB");
            ViewBag.SanBayDen = new SelectList(db.SanBays, "MaSB", "TenSB");
            return PartialView("_EditCB", cb);
           
        }

        // POST: Admin/ChuyenBays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection form)
        {
           
            if (ModelState.IsValid)
            {
                int macb = Convert.ToInt32(form["MaCB"]);
                int tgbay = Convert.ToInt32(form["ThoiGianBay"]);
                int sbdi = Convert.ToInt32(form["SanBay1.MaSB"]);
                int sbden = Convert.ToInt32(form["SanBay.MaSB"]);
                
                DateTime tgkh = Convert.ToDateTime(form["NgayGioKhoiHanh"]);
                int ndvc = Convert.ToInt32(form["NgayDatChamNhat"]);
                int nhvc = Convert.ToInt32(form["NgayHuyChamNhat"]);
                ChuyenBay newcb = new ChuyenBay() { MaCB = macb, SanBayDi = sbdi, SanBayDen = sbden,ThoiGianBay=tgbay,NgayGioKhoiHanh=tgkh,NgayDatChamNhat=ndvc,NgayHuyChamNhat=nhvc };
                db.Entry(newcb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return Json(new { status = "error", message = "Lỗi chỉnh sửa chuyến bay" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSBTG(ChiTietChuyenBay sbtg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sbtg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return Json(new { status = "error", message = "Lỗi chỉnh sửa sân bay trung gian" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditHVCB(HangVeCuaChuyenBay hvcb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hvcb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return Json(new { status = "error", message = "Lỗi chỉnh sửa hạng vé chuyến bay" });
        }

        public ActionResult Delete(int id)
        {
            ChuyenBay chuyenBay = db.ChuyenBays.Find(id);
            db.ChuyenBays.Remove(chuyenBay);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteSBTG(int idcb,int idhv)
        {
            var sbtg = (from c in db.ChiTietChuyenBays where c.MaCB == idcb && c.SanBayTrungGian == idhv select c).FirstOrDefault();
            db.ChiTietChuyenBays.Remove(sbtg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteHVCB(int idcb,int idhv)
        {
            var hvcb = (from c in db.HangVeCuaChuyenBays where c.MaCB == idcb && c.MaHangVe == idhv select c).FirstOrDefault();
          
            db.HangVeCuaChuyenBays.Remove(hvcb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
