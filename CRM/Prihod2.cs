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
    
    public partial class Prihod2
    {
        public int id_prihod2 { get; set; }
        public int id { get; set; }
        public int id_material { get; set; }
        public int kod_ed_izm { get; set; }
        public decimal kol { get; set; }
        public decimal cena { get; set; }
    
        public virtual Prihod1 Prihod1 { get; set; }
        public virtual Materialy Materialy { get; set; }
    }
}