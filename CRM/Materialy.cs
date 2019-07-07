//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRM
{
    using System;
    using System.Collections.Generic;
    
    public partial class Materialy
    {
        public Materialy()
        {
            this.Prihod2 = new HashSet<Prihod2>();
            this.Vozvrat_mater2 = new HashSet<Vozvrat_mater2>();
            this.Vydacha_mater2 = new HashSet<Vydacha_mater2>();
            this.Rashod_2 = new HashSet<Rashod_2>();
        }
    
        public int id_material { get; set; }
        public string NameMaterial { get; set; }
        public int id_ed_izm { get; set; }
        public Nullable<int> ves1shtuki { get; set; }
        public Nullable<int> kol_v_upak { get; set; }
        public Nullable<int> kol_v_tare { get; set; }
        public string mesto_hraneniya { get; set; }
        public Nullable<int> norma_rashoda_na1schetchik { get; set; }
        public string artikul { get; set; }
        public Nullable<System.DateTime> date_ostatok_prev { get; set; }
        public Nullable<decimal> ostatok_prev { get; set; }
        public Nullable<System.DateTime> date_ostatok_cur { get; set; }
        public Nullable<decimal> ostatok_cur { get; set; }
    
        public virtual Ed_izm Ed_izm { get; set; }
        public virtual ICollection<Prihod2> Prihod2 { get; set; }
        public virtual ICollection<Vozvrat_mater2> Vozvrat_mater2 { get; set; }
        public virtual ICollection<Vydacha_mater2> Vydacha_mater2 { get; set; }
        public virtual ICollection<Rashod_2> Rashod_2 { get; set; }
    }
}
