using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightTicket.Models
{
    public class LayVeCuaND
    {
        public int id_ND { get; set; }
        public int? MaCB { get; set; }
        public string HoTen { get; set; }
        public string CMND { get; set; }
        public string SDT { get; set; }
        public int maGhe { get; set; }
        public int? SoGhe { get; set; }
        public string HangVe { get; set; }
        public int? giatien { get; set; }
    }
}