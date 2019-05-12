using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace WHVM.Models
{
    public partial class HomeVideoDBContext : DbContext
    {
        private ConnectionString _connectionstring;

        public HomeVideoDBContext(IOptions<ConnectionString> connectionstringAccessor)
        {
            _connectionstring = connectionstringAccessor.Value;
        }

        public HomeVideoDBContext(DbContextOptions<HomeVideoDBContext> options, IOptions<ConnectionString> connectionstringAccessor)
            : base(options)
        {
            _connectionstring = connectionstringAccessor.Value;
        }

        public virtual DbSet<Chapter> Chapter { get; set; }
        public virtual DbSet<Clip> Clip { get; set; }
        public virtual DbSet<Source> Source { get; set; }
        public virtual DbSet<TagsCollections> TagsCollections { get; set; }
        public virtual DbSet<TagsPeople> TagsPeople { get; set; }

        // Unable to generate entity type for table 'dbo.Clip_TagsCollections'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Clip_TagsPeople'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionstring.DefaultConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Chapter>(entity =>
            {
                entity.HasIndex(e => new { e.SourceId, e.ChapterNumber })
                    .HasName("UQ_Chapter_DVDID_ChapterNumber")
                    .IsUnique();

                entity.Property(e => e.ChapterId).HasColumnName("ChapterID");

                entity.Property(e => e.ChapterLength).HasMaxLength(12);

                entity.Property(e => e.SourceId).HasColumnName("SourceID");

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.Chapter)
                    .HasForeignKey(d => d.SourceId)
                    .HasConstraintName("FK_Chapter_DVD");
            });

            modelBuilder.Entity<Clip>(entity =>
            {
                entity.HasIndex(e => new { e.ChapterId, e.ClipNumber })
                    .HasName("UQ_Clip_ChapterID_ClipNumber")
                    .IsUnique();

                entity.Property(e => e.ClipId).HasColumnName("ClipID");

                entity.Property(e => e.CameraOperator).HasMaxLength(30);

                entity.Property(e => e.ChapterId).HasColumnName("ChapterID");

                entity.Property(e => e.ClipReviewer).HasMaxLength(30);

                entity.Property(e => e.ClipTimeEnd).HasColumnType("datetime");

                entity.Property(e => e.ClipTimeStart).HasColumnType("datetime");

                entity.Property(e => e.ClipVidTimeEnd).HasMaxLength(12);

                entity.Property(e => e.ClipVidTimeLength).HasMaxLength(12);

                entity.Property(e => e.ClipVidTimeStart).HasMaxLength(12);

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.HasOne(d => d.Chapter)
                    .WithMany(p => p.Clip)
                    .HasForeignKey(d => d.ChapterId)
                    .HasConstraintName("FK_Clip_Chapter");
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.HasIndex(e => e.SourceLabel)
                    .HasName("UQ_DVD_DVDLabel")
                    .IsUnique();

                entity.Property(e => e.SourceId).HasColumnName("SourceID");

                entity.Property(e => e.SourceDateBurned).HasColumnType("datetime");

                entity.Property(e => e.SourceDateRipped).HasColumnType("datetime");

                entity.Property(e => e.SourceLabel).HasMaxLength(20);
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
        }
    }
}
