using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
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
            var cbay = Session["chuyenBay"] as ChuyenBay;
            int hangVe = Convert.ToInt32(Session["hangVe"]);
            int idCB = cbay.MaCB;
            var lstChoNgoi = db.ChoNgois.Where(x => x.MaCB == idCB && x.MaHangVe == hangVe).ToList();
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
            int id = Convert.ToInt32(MaGhe);
            using(var context=new DB_A6C0B2_Nhom13FlightTicketEntities())
            {
                var ghe = context.ChoNgois.Where(x => x.MaGhe == id).FirstOrDefault();
                ghe.TinhTrang = false;
                context.SaveChanges();
            }
            return Redirect("/Papal/PaymentWithPaypal");
        }
        private void sendEmail(string mail)
        {
            //email của dự án
            string email = "nhomltweb@gmail.com";
            string password = "123456789a@";
       
            

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 25);


            msg.From = new MailAddress(email);
            msg.To.Add(mail);
            msg.Subject = "Hello";
            msg.Body = "Ban da dat ve may bay thanh cong";
            msg.IsBodyHtml = true;

            //gán cho biến TempData để có thể kiểm tra xem user nhập đúng không 


            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);

            
        }
        public ActionResult PaySuccess()
        {
            ////luu thong tin ve
            var cbay = Session["chuyenBay"] as ChuyenBay;
            var hangve = Convert.ToInt32(Session["hangVe"]);
            var user = Session["nguoiDung"] as NguoiDung;
            var maGhe = Convert.ToInt32(Session["MaGhe"]);
            ////gui mail thong bao
            sendEmail(user.Gmail);
            using (var context = new DB_A6C0B2_Nhom13FlightTicketEntities())
            {
                VeChuyenBay ve = new VeChuyenBay() { MaNguoiDung = user.MaNguoiDung, NgayDat = DateTime.Now, MaGhe = maGhe };
                context.VeChuyenBays.Add(ve);
                context.SaveChanges();
            }
            ViewBag.cbay = cbay;
            ViewBag.hangve = db.HangVes.Where(x => x.MaHangVe == hangve).FirstOrDefault();
            ViewBag.Ghe = db.ChoNgois.Where(x => x.MaGhe == maGhe).FirstOrDefault();
            return View();
        }
    }
}