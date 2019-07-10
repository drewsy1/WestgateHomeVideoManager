using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WHVM_MVC.Models
{
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

        // Unable to generate entity type for table 'dbo.Clip_TagsPeople'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Clip_TagsCollections'. Please see the warning messages.

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

                entity.Property(e => e.CameraOperator).HasMaxLength(50);

                entity.Property(e => e.ClipFilePath).HasMaxLength(300);

                entity.Property(e => e.ClipReviewer).HasMaxLength(50);

                entity.Property(e => e.ClipTimeEnd).HasColumnType("datetime");

                entity.Property(e => e.ClipTimeStart).HasColumnType("datetime");

                entity.Property(e => e.ClipVidTimeEnd).HasColumnType("time(3)");

                entity.Property(e => e.ClipVidTimeLength)
                    .HasColumnType("time(3)")
                    .HasComputedColumnSql("(CONVERT([time](3),dateadd(millisecond,datediff(millisecond,[ClipVidTimeStart],[ClipVidTimeEnd]),(0))))");

                entity.Property(e => e.ClipVidTimeStart).HasColumnType("time(3)");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.SourceId).HasColumnName("SourceID");

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.Clips)
                    .HasForeignKey(d => d.SourceId)
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
        }
    }
}
