﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DatabaseEntities : DbContext
    {
      public DatabaseEntities()
        : this(false) { }
    
        public DatabaseEntities(bool proxyCreationEnabled)	    
            : base("name=DatabaseEntities")
        {
            this.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
        }
    
        public DatabaseEntities(string connectionString)
          : this(connectionString, false) { }
    
        public DatabaseEntities(string connectionString, bool proxyCreationEnabled)
            : base(connectionString)
        {
            this.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
        }	
      
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Exercises> Exercises { get; set; }
        public DbSet<RoutePoints> RoutePoints { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
