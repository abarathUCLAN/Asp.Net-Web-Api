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
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<invitations> invitations { get; set; }
        public virtual DbSet<project_acceptance_protocols> project_acceptance_protocols { get; set; }
        public virtual DbSet<project_actual_states> project_actual_states { get; set; }
        public virtual DbSet<project_change_requests> project_change_requests { get; set; }
        public virtual DbSet<project_data> project_data { get; set; }
        public virtual DbSet<project_descriptions> project_descriptions { get; set; }
        public virtual DbSet<project_effort_estimations> project_effort_estimations { get; set; }
        public virtual DbSet<project_functional_requirements> project_functional_requirements { get; set; }
        public virtual DbSet<project_implementations> project_implementations { get; set; }
        public virtual DbSet<project_introductions> project_introductions { get; set; }
        public virtual DbSet<project_manuals> project_manuals { get; set; }
        public virtual DbSet<project_need_to_haves> project_need_to_haves { get; set; }
        public virtual DbSet<project_nice_to_haves> project_nice_to_haves { get; set; }
        public virtual DbSet<project_non_functional_requirements> project_non_functional_requirements { get; set; }
        public virtual DbSet<project_presentations> project_presentations { get; set; }
        public virtual DbSet<project_qualities> project_qualities { get; set; }
        public virtual DbSet<project_reports> project_reports { get; set; }
        public virtual DbSet<project_results> project_results { get; set; }
        public virtual DbSet<project_risks> project_risks { get; set; }
        public virtual DbSet<project_style_guides> project_style_guides { get; set; }
        public virtual DbSet<project_target_states> project_target_states { get; set; }
        public virtual DbSet<project_uses> project_uses { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<User_Project_Rel> User_Project_Rel { get; set; }
        public virtual DbSet<UserInfos> UserInfos { get; set; }
    }
}
