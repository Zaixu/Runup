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
    public partial class RoutePoints
    {
        [DataMember]
        public int idRoutePoints { get; set; }
        [DataMember]
        public double Longitude { get; set; }
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public System.DateTime Time { get; set; }
        [DataMember]
        public int Route_idRoute { get; set; }
    
        [DataMember]
        public virtual Routes Routes { get; set; }
    }
}
