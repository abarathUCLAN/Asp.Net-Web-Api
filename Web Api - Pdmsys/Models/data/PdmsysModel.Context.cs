﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web_Api___Pdmsys.Models.data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class pdmsysEntities : DbContext
    {
        public pdmsysEntities()
            : base("name=pdmsysEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<invitations> invitations { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<Table> Table { get; set; }
        public virtual DbSet<User_Project_Rel> User_Project_Rel { get; set; }
        public virtual DbSet<UserInfos> UserInfos { get; set; }
    }
}
