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
    
    public partial class Montazhniki
    {
        public Montazhniki()
        {
            this.Vozvrat_mater1 = new HashSet<Vozvrat_mater1>();
            this.Vydacha_mater1 = new HashSet<Vydacha_mater1>();
            this.Rashod1 = new HashSet<Rashod1>();
            this.Request_attachment = new HashSet<Request_attachment>();
        }
    
        public int id_mont { get; set; }
        public string full_name { get; set; }
        public string tel { get; set; }
        public Nullable<System.DateTime> data_uvol { get; set; }
    
        public virtual ICollection<Vozvrat_mater1> Vozvrat_mater1 { get; set; }
        public virtual ICollection<Vydacha_mater1> Vydacha_mater1 { get; set; }
        public virtual ICollection<Rashod1> Rashod1 { get; set; }
        public virtual ICollection<Request_attachment> Request_attachment { get; set; }
    }
}
