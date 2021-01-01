using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightTicket.Models
{
    public class DanhSachVeChuyenBay
    {
        public string HoTen { get; set; }
        public int MaCB { get; set; }
        public string SoDienThoai { get; set; }
        public string CMND { get; set; }
        public string HangVe { get; set; }
        public int? GiaTien { get; set; }
    }
}