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
    
    public partial class Rashod1
    {
        public Rashod1()
        {
            this.Rashod_2 = new HashSet<Rashod_2>();
        }
    
        public int id { get; set; }
        public System.DateTime data_nakl { get; set; }
        public int id_mont { get; set; }
    
        public virtual Montazhniki Montazhniki { get; set; }
        public virtual ICollection<Rashod_2> Rashod_2 { get; set; }
    }
}
