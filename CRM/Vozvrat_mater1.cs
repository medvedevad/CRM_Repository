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
    
    public partial class Vozvrat_mater1
    {
        public Vozvrat_mater1()
        {
            this.Vozvrat_mater2 = new HashSet<Vozvrat_mater2>();
        }
    
        public int id_vozvr_m1 { get; set; }
        public Nullable<System.DateTime> data_vydacha { get; set; }
        public Nullable<int> id_mont { get; set; }
    
        public virtual ICollection<Vozvrat_mater2> Vozvrat_mater2 { get; set; }
        public virtual Montazhniki Montazhniki { get; set; }
    }
}
