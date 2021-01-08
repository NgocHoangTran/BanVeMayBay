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
    public class NguoiDungsController : Controller
    {
        private DB_A6C0B2_Nhom13FlightTicketEntities db = new DB_A6C0B2_Nhom13FlightTicketEntities();

        // GET: Admin/NguoiDungs
        public ActionResult Index()
        {
            var nguoiDungs = db.NguoiDungs.Include(n => n.Quyen);
     
            return View(nguoiDungs.ToList());
        }

        // GET: Admin/NguoiDungs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        

        // POST: Admin/NguoiDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NguoiDung user)
        {
            try
            {
                db.NguoiDungs.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = "Lỗi tạo user" });
            }
           
        }

        // GET: Admin/NguoiDungs/Edit/5
        public ActionResult Edit(int id)
        {
            var user = db.NguoiDungs.Where(x => x.MaNguoiDung == id).FirstOrDefault();
            ViewBag.user = user;
            DateTime birthday = new DateTime();
            birthday =Convert.ToDateTime(user.NgaySinh);
            ViewBag.birthday = birthday;
           
            return PartialView("_EditUser");
           
        }

        // POST: Admin/NguoiDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NguoiDung nguoiDung)
        {
            
            if (ModelState.IsValid)
            {
                
                db.Entry(nguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Json(new { status = "error", message = "Lỗi chỉnh sửa user" });
        }

       
        // POST: Admin/NguoiDungs/Delete/5
       
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            db.NguoiDungs.Remove(nguoiDung);
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
