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
    public class DonGiasController : Controller
    {
        private DB_A6C0B2_Nhom13FlightTicketEntities db = new DB_A6C0B2_Nhom13FlightTicketEntities();

        // GET: Admin/DonGias
        public ActionResult Index()
        {
            var donGias = db.DonGias.Include(d => d.ChuyenBay).Include(d => d.HangVe);
            ViewBag.HangVe = new SelectList(db.HangVes, "MaHangVe", "TenHangVe");
            ViewBag.SanBayDi = new SelectList(db.SanBays, "MaSB", "TenSB");
            ViewBag.SanBayDen = new SelectList(db.SanBays, "MaSB", "TenSB");
            return View(donGias.ToList());
        }

       

        

        // POST: Admin/DonGias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            

            if (ModelState.IsValid)
            {
                int gia = Convert.ToInt32(form["Gia"]);
                int sbdi = Convert.ToInt32(form["SanBayDi"]);
                int sbden = Convert.ToInt32(form["SanBayDen"]);
                int cb = (from c in db.ChuyenBays where c.SanBayDi == sbdi && c.SanBayDen == sbden select c.MaCB).FirstOrDefault();
                int hangve = Convert.ToInt32(form["HangVe"]);
                DonGia newdg = new DonGia() { MaCB = cb, MaHangVe=hangve, Gia = gia };
                db.DonGias.Add(newdg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Json(new { status = "error", message = "Lỗi thêm đơn giá" });
        }

        // GET: Admin/DonGias/Edit/5
        public ActionResult Edit(int id)
        {
           
            var dg = db.DonGias.Where(x => x.MaDonGia == id).FirstOrDefault();
            ViewBag.SanBayDi = new SelectList(db.SanBays, "MaSB", "TenSB");
            ViewBag.SanBayDen = new SelectList(db.SanBays, "MaSB", "TenSB");
            ViewBag.HangVe = new SelectList(db.HangVes, "MaHangVe", "TenHangVe");
            return PartialView("_EditDG",dg);
            
        }

        // POST: Admin/DonGias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection form)
        {
            

            if (ModelState.IsValid)
            {
                int madg = Convert.ToInt32(form["MaDonGia"]);
                int gia = Convert.ToInt32(form["Gia"]);
                int sbdi = Convert.ToInt32(form["ChuyenBay.SanBayDi"]);
                int sbden = Convert.ToInt32(form["ChuyenBay.SanBayDen"]);
                int cb = (from c in db.ChuyenBays where c.SanBayDi == sbdi && c.SanBayDen == sbden select c.MaCB).FirstOrDefault();
                int hangve = Convert.ToInt32(form["MaHangVe"]);
                DonGia newdg = new DonGia() {MaDonGia=madg, MaCB = cb, MaHangVe = hangve, Gia = gia };
                db.Entry(newdg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return Json(new { status = "error", message = "Lỗi chỉnh sửa đơn giá" });
        }

       

       
        public ActionResult Delete(int id)
        {
            DonGia donGia = db.DonGias.Find(id);
            db.DonGias.Remove(donGia);
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
