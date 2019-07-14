﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace WHVM_MVC.Models
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public partial class HomeVideoDBContext : DbContext
    {
        public HomeVideoDBContext()
        {
        }

        public HomeVideoDBContext(DbContextOptions<HomeVideoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Clip> Clip { get; set; }
        public virtual DbSet<Source> Source { get; set; }
        public virtual DbSet<SourceFormat> SourceFormat { get; set; }
        public virtual DbSet<TagsCollections> TagsCollections { get; set; }
        public virtual DbSet<TagsPeople> TagsPeople { get; set; }
        public virtual DbSet<Clip_TagsPeople> Clip_TagsPeople { get; set; }
        public virtual DbSet<Clip_TagsCollections> Clip_TagsCollections { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Clip>(entity =>
            {
                entity.Property(e => e.ClipId).HasColumnName("ClipID");

                entity.Property(e => e.SourceSegment);

                entity.Property(e => e.ClipCameraOperatorId).HasColumnName("ClipCameraOperatorID");

                entity.Property(e => e.ClipDescription).HasMaxLength(250);

                entity.Property(e => e.ClipFilePath).HasMaxLength(300);

                entity.Property(e => e.ClipReviewerId).HasColumnName("ClipReviewerID");

                entity.Property(e => e.ClipTimeEnd).HasColumnType("datetime");

                entity.Property(e => e.ClipTimeStart).HasColumnType("datetime");

                entity.Property(e => e.ClipVidTimeEnd).HasColumnType("time(3)");

                entity.Property(e => e.ClipVidTimeLength)
                    .HasColumnType("time(3)")
                    .HasComputedColumnSql("(CONVERT([time](3),dateadd(millisecond,datediff(millisecond,[ClipVidTimeStart],[ClipVidTimeEnd]),(0))))");

                entity.Property(e => e.ClipVidTimeStart).HasColumnType("time(3)");

                entity.Property(e => e.SourceId).HasColumnName("SourceID");

                entity.HasOne(d => d.ClipCameraOperator)
                    .WithMany(p => p.ClipsAsCameraOperator)
                    .HasForeignKey(d => d.ClipCameraOperatorId)
                    .HasConstraintName("FK_Clip_TagsPeople_ClipCameraOperator");

                entity.HasOne(d => d.ClipReviewer)
                    .WithMany(p => p.ClipsAsReviewer)
                    .HasForeignKey(d => d.ClipReviewerId)
                    .HasConstraintName("FK_Clip_TagsPeople_ClipReviewer");

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.Clips)
                    .HasForeignKey(d => d.SourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clip_Source");
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.Property(e => e.SourceId).HasColumnName("SourceID");

                entity.Property(e => e.SourceDateCreated).HasColumnType("datetime");

                entity.Property(e => e.SourceDateImported).HasColumnType("datetime");

                entity.Property(e => e.SourceFilePath).HasMaxLength(300);

                entity.Property(e => e.SourceFormatId).HasColumnName("SourceFormatID");

                entity.Property(e => e.SourceLabel).HasMaxLength(30);

                entity.HasOne(d => d.SourceFormat)
                    .WithMany(p => p.Source)
                    .HasForeignKey(d => d.SourceFormatId)
                    .HasConstraintName("FK_Source_SourceFormat");
            });

            modelBuilder.Entity<SourceFormat>(entity =>
            {
                entity.Property(e => e.SourceFormatId)
                    .HasColumnName("SourceFormatID")
                    .ValueGeneratedNever();

                entity.Property(e => e.SourceFormatLogoPath).HasMaxLength(30);

                entity.Property(e => e.SourceFormatText).HasMaxLength(10);
            });

            modelBuilder.Entity<TagsCollections>(entity =>
            {
                entity.HasKey(e => e.CollectionsId);

                entity.Property(e => e.CollectionsId).HasColumnName("CollectionsID");

                entity.Property(e => e.CollectionsText).HasMaxLength(50);
            });

            modelBuilder.Entity<TagsPeople>(entity =>
            {
                entity.HasKey(e => e.PeopleId);

                entity.Property(e => e.PeopleId).HasColumnName("PeopleID");

                entity.Property(e => e.PeopleName).HasMaxLength(30);
            });

            modelBuilder.Entity<View_SourceDates>(entity =>
            {
                entity.HasKey(e => e.SourceId);

                entity.Property(e => e.SourceId).HasColumnName("SourceID");

                entity.Property(e => e.SourceDateStart);

                entity.Property(e => e.SourceDateEnd);

                entity.HasOne(d => d.Source)
                    .WithOne(p => p.SourceDates);
            });

            modelBuilder.Entity<Clip_TagsPeople>().HasKey(tp => new {tp.ClipId, tp.PeopleId});

            modelBuilder.Entity<Clip_TagsCollections>().HasKey(tp => new { tp.ClipId, tp.CollectionsId });
        }
    }
}
