﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WestgateHomeVideoManager.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HomeVideoDBEntities : DbContext
    {
        public HomeVideoDBEntities()
            : base("name=HomeVideoDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Chapter> Chapters { get; set; }
        public virtual DbSet<Clip> Clips { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<TagsCollection> TagsCollections { get; set; }
        public virtual DbSet<TagsPeople> TagsPeoples { get; set; }
        public virtual DbSet<Clip_Collections> Clip_Collections { get; set; }
        public virtual DbSet<Clip_People> Clip_People { get; set; }
        public virtual DbSet<DVD_Chapter_Clip> DVD_Chapter_Clip { get; set; }
    }
}
