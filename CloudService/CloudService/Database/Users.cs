//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CloudService.Database
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    
    [DataContract(IsReference = true)]
    public partial class Users
    {
        public Users()
        {
            this.Routes = new HashSet<Routes>();
        }
    
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
    
        [DataMember]
        public virtual ICollection<Routes> Routes { get; set; }
    }
}
