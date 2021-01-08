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
    public class SanBaysController : Controller
    {
        private DB_A6C0B2_Nhom13FlightTicketEntities db = new DB_A6C0B2_Nhom13FlightTicketEntities();

        // GET: Admin/SanBays
        public ActionResult Index()
        {
            return View(db.SanBays.ToList());
        }

       

       

        // POST: Admin/SanBays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( SanBay sanBay)
        {
            if (ModelState.IsValid)
            {
                db.SanBays.Add(sanBay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Json(new { status = "error", message = "Lỗi thêm sân bay" });
        }

        // GET: Admin/SanBays/Edit/5
        public ActionResult Edit(int id)
        {
            var sanbay = db.SanBays.Where(x => x.MaSB == id).FirstOrDefault();
            ViewBag.sanbay = sanbay;
            
            return PartialView("_EditSB");
        }

        // POST: Admin/SanBays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( SanBay sanBay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanBay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return Json(new { status = "error", message = "Lỗi chỉnh sửa sân bay" });
        }

       
        // POST: Admin/SanBays/Delete/5
       
       
        public ActionResult Delete(int id)
        {
            SanBay sanBay = db.SanBays.Find(id);
            db.SanBays.Remove(sanBay);
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
