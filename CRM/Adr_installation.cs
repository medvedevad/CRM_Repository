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
    
    public partial class Adr_installation
    {
        public Adr_installation()
        {
            this.Request_reception = new HashSet<Request_reception>();
        }
    
        public int id_adr_installation { get; set; }
        public Nullable<int> id_client { get; set; }
        public string city_town { get; set; }
        public string raion { get; set; }
        public string street { get; set; }
        public Nullable<int> house { get; set; }
        public string korp { get; set; }
        public Nullable<int> flat { get; set; }
        public Nullable<int> id_metro { get; set; }
        public Nullable<int> podezd { get; set; }
        public Nullable<int> etag { get; set; }
        public string domofon { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Metro Metro { get; set; }
        public virtual ICollection<Request_reception> Request_reception { get; set; }
    }
}
