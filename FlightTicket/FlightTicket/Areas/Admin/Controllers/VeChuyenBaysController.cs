using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FlightTicket.Models;

namespace FlightTicket.Areas.Admin.Controllers
{
    public class VeChuyenBaysController : Controller
    {
        private DB_A6C0B2_Nhom13FlightTicketEntities db = new DB_A6C0B2_Nhom13FlightTicketEntities();

        // GET: Admin/VeChuyenBays
        public ActionResult Index()
        {
            ViewBag.Gia = 0;
            ViewBag.MaGhe = db.ChoNgois.ToList() ;
            ViewBag.HangVe = new SelectList(db.HangVes, "MaHangVe", "TenHangVe");
            ViewBag.SanBayDi = new SelectList(db.SanBays, "MaSB", "TenSB");
            ViewBag.SanBayDen = new SelectList(db.SanBays, "MaSB", "TenSB");
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "TaiKhoan");
            List<DSVeCB> listVe = new List<DSVeCB>();
            var veChuyenBays = db.VeChuyenBays.ToList();
            for(int i=0;i<veChuyenBays.Count;i++)
            {
                DSVeCB vecb = new DSVeCB();
                vecb.MaVe = veChuyenBays[i].MaVeCB;
                vecb.HoTen = veChuyenBays[i].NguoiDung.HoTen;
                vecb.SoDT = veChuyenBays[i].NguoiDung.SoDT;
                vecb.SoGhe = veChuyenBays[i].ChoNgoi.SoGhe ?? 0;
                vecb.NgayDat = veChuyenBays[i].NgayDat;
                vecb.SBDi = veChuyenBays[i].ChoNgoi.ChuyenBay.SanBayDi??0;
                vecb.SBDen = veChuyenBays[i].ChoNgoi.ChuyenBay.SanBayDen ?? 0;
                vecb.TenDi = veChuyenBays[i].ChoNgoi.ChuyenBay.SanBay1.TenSB;
                vecb.TenDen = veChuyenBays[i].ChoNgoi.ChuyenBay.SanBay.TenSB;
                vecb.HangVe = veChuyenBays[i].ChoNgoi.HangVe.TenHangVe;
                vecb.Gia = db.getGia(veChuyenBays[i].ChoNgoi.MaCB, veChuyenBays[i].ChoNgoi.MaHangVe).FirstOrDefault()??0;
                listVe.Add(vecb);
            }
            return View(listVe);
        }

        // GET: Admin/VeChuyenBays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VeChuyenBay veChuyenBay = db.VeChuyenBays.Find(id);
            if (veChuyenBay == null)
            {
                return HttpNotFound();
            }
            return View(veChuyenBay);
        }

        // GET: Admin/VeChuyenBays/Create
        public ActionResult Create()
        {
            ViewBag.MaGhe = new SelectList(db.ChoNgois, "MaGhe", "MaGhe");
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "TaiKhoan");
            return View();
        }

        // POST: Admin/VeChuyenBays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            int sbdi = Convert.ToInt32(form["SanBayDi"]);
            int sbden = Convert.ToInt32(form["SanBayDen"]);
            ChuyenBay cb = (from c in db.ChuyenBays where c.SanBayDi ==sbdi && c.SanBayDen == sbden select c).FirstOrDefault();
            double day = (Convert.ToDateTime(form["Ngaydi"]) - cb.NgayGioKhoiHanh).TotalDays;
            if ((Convert.ToDateTime(form["Ngaydi"])-cb.NgayGioKhoiHanh).TotalDays>cb.NgayDatChamNhat)
            {
                try
                {
                    DateTime ngaydat = Convert.ToDateTime(form["Ngaydi"]);
                  
                    int maghe  =Convert.ToInt32( form["selector"]);
                    int mauser= Convert.ToInt32(form["MaNguoiDung"]);
                    VeChuyenBay newve = new VeChuyenBay() { NgayDat=ngaydat,MaGhe=maghe,MaNguoiDung=mauser  };
                    db.VeChuyenBays.Add(newve);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {

                }
            }
            return Json(new { status = "error", message = "Lỗi thêm vé" });
        }

        // GET: Admin/VeChuyenBays/Edit/5
        public ActionResult Edit(int id)
        {
            var vecb = db.VeChuyenBays.Where(x => x.MaVeCB == id).FirstOrDefault();
           
            DSVeCB ve = new DSVeCB();
            ve.MaVe = vecb.MaVeCB;
            ve.HoTen = vecb.NguoiDung.HoTen;
            ve.SoDT = vecb.NguoiDung.SoDT;
            ve.SoGhe = vecb.ChoNgoi.SoGhe ?? 0;
            ve.NgayDat = vecb.NgayDat;
            ve.SBDi = vecb.ChoNgoi.ChuyenBay.SanBayDi ?? 0;
            ve.SBDen = vecb.ChoNgoi.ChuyenBay.SanBayDen ?? 0;
            ve.TenDi = vecb.ChoNgoi.ChuyenBay.SanBay1.TenSB;
            ve.TenDen = vecb.ChoNgoi.ChuyenBay.SanBay.TenSB;
            ve.HangVe = vecb.ChoNgoi.HangVe.TenHangVe;
            ve.Gia = db.getGia(vecb.ChoNgoi.MaCB, vecb.ChoNgoi.MaHangVe).FirstOrDefault() ?? 0;
            ViewBag.VeCB = ve;
            ViewBag.SanBayDi = new SelectList(db.SanBays, "MaSB", "TenSB");
            ViewBag.SanBayDen = new SelectList(db.SanBays, "MaSB", "TenSB");
            ViewBag.HangVe = new SelectList(db.HangVes, "MaHangVe", "TenHangVe");
            var from = new SqlParameter("@SBDi", ve.SBDi);
            var to = new SqlParameter("@SBDen", ve.SBDen);
            var hv = new SqlParameter("@HangVe", vecb.ChoNgoi.MaHangVe);
            var result = db.Database.SqlQuery<getChoNgoi>("getChoNgoi @SBDi,@SBDen,@HangVe", from, to, hv).ToList<getChoNgoi>();

            ViewBag.MaGhe = new SelectList(result,"MaGhe","SoGhe");
            ViewBag.Gia = ve.Gia;
            return PartialView("_EditVeCB",ve);
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //VeChuyenBay veChuyenBay = db.VeChuyenBays.Find(id);
            //if (veChuyenBay == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.MaGhe = new SelectList(db.ChoNgois, "MaGhe", "MaGhe", veChuyenBay.MaGhe);
            //ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "TaiKhoan", veChuyenBay.MaNguoiDung);
            //return View(veChuyenBay);
        }

        // POST: Admin/VeChuyenBays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaVeCB,MaNguoiDung,NgayDat,MaGhe")] VeChuyenBay veChuyenBay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(veChuyenBay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaGhe = new SelectList(db.ChoNgois, "MaGhe", "MaGhe", veChuyenBay.MaGhe);
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "TaiKhoan", veChuyenBay.MaNguoiDung);
            return View(veChuyenBay);
        }

        // GET: Admin/VeChuyenBays/Delete/5
       

        // POST: Admin/VeChuyenBays/Delete/5
        
        public ActionResult Delete(int id)
        {
            VeChuyenBay veChuyenBay = db.VeChuyenBays.Find(id);
            db.VeChuyenBays.Remove(veChuyenBay);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult _ChoNgoi(int sbdi,int sbden,int hangve)
        {
            
            var from = new SqlParameter("@SBDi", sbdi);
            var to = new SqlParameter("@SBDen", sbden);
            var hv = new SqlParameter("@HangVe", hangve);
            var result = db.Database.SqlQuery<getChoNgoi>("getChoNgoi @SBDi,@SBDen,@HangVe", from,to,hv).ToList<getChoNgoi>();
            // var soghe = db.getChoNgoi(sbdi, sbden, hangve).ToList();

            //for(int i=0;i<soghe.Count;i++)
            //{

            //    cn.MaGhe=soghe[i]
            //}
          
            ViewBag.MaGhe = result.AsEnumerable();
           
            return PartialView("_ChoNgoi");
            
        }
        public ActionResult Gia(int sbdi, int sbden, int hangve)
        {
            var gia = db.sp_getGia(sbdi, sbden, hangve).FirstOrDefault();
            ViewBag.Gia = gia;
            var info = new
            {
                value = gia
            };

            return Json(info,JsonRequestBehavior.AllowGet);

        }
        public ActionResult ChoNgoiEdit(int MaVe,int sbdi, int sbden, int hangve,DateTime Ngaydi,string hoten)
        {
            DSVeCB ds = new DSVeCB();
            ds.MaVe = MaVe;
            ds.SBDi = sbdi;
            ds.SBDen = sbden;
            ds.MaHV = hangve;
            ds.HangVe = (from c in db.HangVes where c.MaHangVe == hangve select c.TenHangVe).FirstOrDefault();
            ds.NgayDat = Ngaydi;
            ds.HoTen = hoten;
            ViewBag.SanBayDi = new SelectList(db.SanBays, "MaSB", "TenSB");
            ViewBag.SanBayDen = new SelectList(db.SanBays, "MaSB", "TenSB");
            ViewBag.HangVe = new SelectList(db.HangVes, "MaHangVe", "TenHangVe");
            var from = new SqlParameter("@SBDi", sbdi);
            var to = new SqlParameter("@SBDen", sbden);
            var hv = new SqlParameter("@HangVe", hangve);
            var result = db.Database.SqlQuery<getChoNgoi>("getChoNgoi @SBDi,@SBDen,@HangVe", from, to, hv).ToList<getChoNgoi>();
            // var soghe = db.getChoNgoi(sbdi, sbden, hangve).ToList();

            //for(int i=0;i<soghe.Count;i++)
            //{

            //    cn.MaGhe=soghe[i]
            //}

            ViewBag.MaGhe = result;

            return PartialView("_EditVeCB");

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
