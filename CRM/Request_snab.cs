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
    
    public partial class Request_snab
    {
        public Request_snab()
        {
            this.Vydacha_mater1 = new HashSet<Vydacha_mater1>();
            this.Request_reception = new HashSet<Request_reception>();
        }
    
        public int id_request_snab { get; set; }
        public Nullable<int> id_hol_schetch { get; set; }
        public Nullable<int> id_gor_schetch { get; set; }
        public Nullable<int> id_teplo_schetch { get; set; }
        public Nullable<int> kol_teplo { get; set; }
        public Nullable<int> kol_hol { get; set; }
        public Nullable<int> kol_gor { get; set; }
    
        public virtual G_Schetchik G_Schetchik { get; set; }
        public virtual H_Schetchik H_Schetchik { get; set; }
        public virtual Teplo_Schetchik Teplo_Schetchik { get; set; }
        public virtual ICollection<Vydacha_mater1> Vydacha_mater1 { get; set; }
        public virtual ICollection<Request_reception> Request_reception { get; set; }
    }
}
