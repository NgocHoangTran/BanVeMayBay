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
    public class HangVesController : Controller
    {
        private DB_A6C0B2_Nhom13FlightTicketEntities db = new DB_A6C0B2_Nhom13FlightTicketEntities();

        // GET: Admin/HangVes
        public ActionResult Index()
        {
            return View(db.HangVes.ToList());
        }

      

        // POST: Admin/HangVes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HangVe hangVe)
        {
            if (ModelState.IsValid)
            {
                db.HangVes.Add(hangVe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Json(new { status = "error", message = "Lỗi thêm hạng vé" });
        }

        // GET: Admin/HangVes/Edit/5
        public ActionResult Edit(int id)
        {
            var hangve = db.HangVes.Where(x => x.MaHangVe == id).FirstOrDefault();
            ViewBag.hangve = hangve;

            return PartialView("_EditHV");
        }

        // POST: Admin/HangVes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( HangVe hangVe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hangVe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return Json(new { status = "error", message = "Lỗi chỉnh sửa hạng vé" });
        }

        

       
        public ActionResult Delete(int id)
        {
            HangVe hangVe = db.HangVes.Find(id);
            db.HangVes.Remove(hangVe);
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
