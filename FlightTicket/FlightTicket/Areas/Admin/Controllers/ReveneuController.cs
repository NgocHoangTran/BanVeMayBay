using FlightTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightTicket.Areas.Admin.Controllers
{
    public class ReveneuController : Controller
    {
        private DB_A6C0B2_Nhom13FlightTicketEntities db = new DB_A6C0B2_Nhom13FlightTicketEntities();
        // GET: Seller/Reveneu
        public ActionResult Index()
        {

            
            var MaCB =from maCB in db.ChuyenBays select maCB.MaCB;
            List<int> macb = MaCB.ToList();
            List<string> name = new List<string>();
            List<int> Doanhthu = new List<int>();
            List<int> SoVe = new List<int>();
            int sumrev = 0;
            int sumticket = 0;
            for (int i = 0; i < macb.Count; i++)
            {
                var NameSb = db.getNameSB(macb[i]).FirstOrDefault();
                string SB =   NameSb.SBDi  + "-" +  NameSb.SBDen ;
                name.Add(SB);
                var rev = db.getMonthTotal(DateTime.Now, macb[i]).FirstOrDefault();      
                int doanhthu= rev.Doanhthu??0;
                int sove = rev.SoVe ?? 0;
                Doanhthu.Add(doanhthu);
                SoVe.Add(sove);
            }
          for(int j=0;j<Doanhthu.Count;j++)
            {
                sumrev += Doanhthu[j];
                sumticket += SoVe[j];
            }
            var DT = string.Join(",", Doanhthu);
            var SV = string.Join(",", SoVe);
            var Name = string.Join(",", name);

            ViewBag.TotalRev = sumrev;
            ViewBag.TotalTicket = sumticket;
            ViewBag.text = "Total of ticket";
            ViewBag.Doanhthu = DT;
            ViewBag.SoVe = SV;
            ViewBag.ChuyenBay =Name;
            ViewBag.label1 = "Num of ticket";
            ViewBag.label2 = "Reveneu";
            return View();
        }
        public ActionResult MonthChart(DateTime Date)
        {

            var MaCB = db.ChuyenBays.Select(x => x.MaCB);
            List<int> macb = MaCB.ToList();
            List<string> name = new List<string>();
            List<int> Doanhthu = new List<int>();
            List<int> SoVe = new List<int>();
            int sumrev = 0;
            int sumticket = 0;
            for (int i = 0; i < macb.Count; i++)
            {
                var NameSb = db.getNameSB(macb[i]).FirstOrDefault();
                string SB = NameSb.SBDi + "-" + NameSb.SBDen;
                name.Add(SB);
                var rev = db.getMonthTotal(Date, macb[i]).FirstOrDefault();
                int doanhthu = rev.Doanhthu ?? 0;
                int sove = rev.SoVe ?? 0;
                Doanhthu.Add(doanhthu);
                SoVe.Add(sove);
            }
            for (int j = 0; j < Doanhthu.Count; j++)
            {
                sumrev += Doanhthu[j];
                sumticket += SoVe[j];
            }
            var DT = string.Join(",", Doanhthu);
            var SV = string.Join(",", SoVe);
            var Name = string.Join(",", name);

            ViewBag.TotalRev = sumrev;
            ViewBag.TotalTicket = sumticket;
            ViewBag.text = "Total of ticket";
            ViewBag.Doanhthu = DT;
            ViewBag.SoVe = SV;
            ViewBag.ChuyenBay = Name;
            ViewBag.label1 = "Num of ticket";
            ViewBag.label2 = "Reveneu";

            return PartialView("~/Areas/Admin/Views/Reveneu/ReveneuChart.cshtml");
        }
        public ActionResult YearChart(DateTime Date)
        {
           
            List<string> name = new List<string>();
            List<int> Doanhthu = new List<int>();
            List<int> SoCB = new List<int>();
            int sumrev = 0;
            int sumCB = 0;
            for (int i = 1; i <= 12; i++)
            {
                name.Add(i.ToString());
            }
            if (Date.Year==DateTime.Now.Year)
            {

                for (int i = 1; i <= DateTime.Now.Month; i++)
                {
                    var rev = db.getYearTotal(Date, i).FirstOrDefault();
                    int doanhthu = rev.Doanhthu ?? 0;
                    int socb = rev.SoCB ?? 0;
                    Doanhthu.Add(doanhthu);
                    SoCB.Add(socb);
                }
            }
            if(Date.Year < DateTime.Now.Year)
            {
                for (int i = 1; i <= 12; i++)
                {
                    var rev = db.getYearTotal(Date, i).FirstOrDefault();
                    int doanhthu = rev.Doanhthu ?? 0;
                    int socb = rev.SoCB ?? 0;
                    Doanhthu.Add(doanhthu);
                    SoCB.Add(socb);
                }
            }
            for (int j = 0; j < Doanhthu.Count; j++)
            {
                sumrev += Doanhthu[j];
                sumCB += SoCB[j];
            }
            var DT = string.Join(",", Doanhthu);
            var SCB = string.Join(",", SoCB);

            var Name = string.Join(",", name);
            ViewBag.TotalRev = sumrev;
            ViewBag.TotalTicket = sumCB;
          
            ViewBag.text = "Total of flight";
            ViewBag.Doanhthu = DT;
            ViewBag.SoVe = SCB;
            ViewBag.ChuyenBay = Name;
            ViewBag.label1 = "Num of flight";
            ViewBag.label2 = "Reveneu";
            return PartialView("~/Areas/Admin/Views/Reveneu/ReveneuChart.cshtml");
        }
       
    }
}