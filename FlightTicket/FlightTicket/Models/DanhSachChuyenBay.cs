using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightTicket.Models
{
    public class DanhSachChuyenBay
    {
        public int MaCB { get; set; }
        public string SanBayDen { get; set; }
        public string SanBayDi { get; set; }
        public System.DateTime NgayGioKhoiHanh { get; set; }
        public int ThoiGianBay { get; set; }
        public int ThoiGianBayToiThieu { get; set; }
        public int NgayDatChamNhat { get; set; }
        public int NgayHuyChamNhat { get; set; }
        public int SoGheConLai { get; set; }
    }
}