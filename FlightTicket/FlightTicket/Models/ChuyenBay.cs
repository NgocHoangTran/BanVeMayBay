//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlightTicket.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChuyenBay
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChuyenBay()
        {
            this.ChiTietChuyenBays = new HashSet<ChiTietChuyenBay>();
            this.ChoNgois = new HashSet<ChoNgoi>();
            this.DonGias = new HashSet<DonGia>();
            this.HangVeCuaChuyenBays = new HashSet<HangVeCuaChuyenBay>();
        }
    
        public int MaCB { get; set; }
        public Nullable<int> SanBayDen { get; set; }
        public Nullable<int> SanBayDi { get; set; }
        public System.DateTime NgayGioKhoiHanh { get; set; }
        public int ThoiGianBay { get; set; }
        public Nullable<int> ThoiGianBayToiThieu { get; set; }
        public int NgayDatChamNhat { get; set; }
        public int NgayHuyChamNhat { get; set; }
        public Nullable<int> SoGheConLai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietChuyenBay> ChiTietChuyenBays { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChoNgoi> ChoNgois { get; set; }
        public virtual SanBay SanBay { get; set; }
        public virtual SanBay SanBay1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonGia> DonGias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HangVeCuaChuyenBay> HangVeCuaChuyenBays { get; set; }
    }
}
