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
    
    public partial class Ed_izm
    {
        public Ed_izm()
        {
            this.Materialy = new HashSet<Materialy>();
        }
    
        public int id_ed_izm { get; set; }
        public string nameEI { get; set; }
    
        public virtual ICollection<Materialy> Materialy { get; set; }
    }
}