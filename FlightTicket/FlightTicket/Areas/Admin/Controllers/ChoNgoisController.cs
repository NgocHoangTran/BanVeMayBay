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
    public class ChoNgoisController : Controller
    {
        private DB_A6C0B2_Nhom13FlightTicketEntities db = new DB_A6C0B2_Nhom13FlightTicketEntities();

        // GET: Admin/ChoNgois
        public ActionResult Index()
        {
            var choNgois = db.ChoNgois.Include(c => c.ChuyenBay).Include(c => c.HangVe);
            ViewBag.HangVe = new SelectList(db.HangVes, "MaHangVe", "TenHangVe");
            ViewBag.SanBayDi = new SelectList(db.SanBays, "MaSB", "TenSB");
            ViewBag.SanBayDen = new SelectList(db.SanBays, "MaSB", "TenSB");
            return View(choNgois.ToList());
        }

       

       

        // POST: Admin/ChoNgois/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            if (ModelState.IsValid)
            {
                int soghe = Convert.ToInt32(form["SoGhe"]);
                int sbdi = Convert.ToInt32(form["SanBayDi"]);
                int sbden = Convert.ToInt32(form["SanBayDen"]);
                int cb = (from c in db.ChuyenBays where c.SanBayDi == sbdi && c.SanBayDen == sbden select c.MaCB).FirstOrDefault();
                int hangve = Convert.ToInt32(form["HangVe"]);
                bool tinhtrang = false;
                ChoNgoi newcn = new ChoNgoi() { MaCB = cb, MaHangVe = hangve,SoGhe=soghe,TinhTrang=tinhtrang  };
                db.ChoNgois.Add(newcn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Json(new { status = "error", message = "Lỗi thêm chỗ ngồi" });
        }

        // GET: Admin/ChoNgois/Edit/5
        public ActionResult Edit(int id)
        {
            var cn = db.ChoNgois.Where(x => x.MaGhe == id).FirstOrDefault();
            ViewBag.SanBayDi = new SelectList(db.SanBays, "MaSB", "TenSB");
            ViewBag.SanBayDen = new SelectList(db.SanBays, "MaSB", "TenSB");
            ViewBag.HangVe = new SelectList(db.HangVes, "MaHangVe", "TenHangVe");
            return PartialView("_EditCN", cn);
        }

        // POST: Admin/ChoNgois/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection form)
        {
            if (ModelState.IsValid)
            {
                int maghe = Convert.ToInt32(form["MaGhe"]);
                int soghe = Convert.ToInt32(form["SoGhe"]);
                int sbdi = Convert.ToInt32(form["ChuyenBay.SanBayDi"]);
                int sbden = Convert.ToInt32(form["ChuyenBay.SanBayDen"]);
                int cb = (from c in db.ChuyenBays where c.SanBayDi == sbdi && c.SanBayDen == sbden select c.MaCB).FirstOrDefault();
                int hangve = Convert.ToInt32(form["MaHangVe"]);
                bool tinhtrang = false;
                ChoNgoi newcn = new ChoNgoi() { MaGhe = maghe, SoGhe = soghe, MaCB = cb, MaHangVe = hangve,TinhTrang=tinhtrang };
                db.Entry(newcn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return Json(new { status = "error", message = "Lỗi chỉnh sửa chỗ ngồi" });
        }

        
        public ActionResult Delete(int id)
        {
            ChoNgoi choNgoi = db.ChoNgois.Find(id);
            db.ChoNgois.Remove(choNgoi);
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
