using System.Diagnostics.CodeAnalysis;
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

        public virtual DbSet<Clip> Clips { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<SourceFormat> SourceFormats { get; set; }
        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<ClipPerson> ClipPersons { get; set; }
        public virtual DbSet<ClipCollection> ClipCollections { get; set; }

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

                entity.Property(e => e.ClipName).HasMaxLength(250);

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

                entity.Property(e => e.SourceName).HasMaxLength(30);

                entity.HasOne(d => d.SourceFormat)
                    .WithMany(p => p.Sources)
                    .HasForeignKey(d => d.SourceFormatId)
                    .HasConstraintName("FK_Source_SourceFormat");
            });

            modelBuilder.Entity<SourceFormat>(entity =>
            {
                entity.Property(e => e.SourceFormatId)
                    .HasColumnName("SourceFormatID")
                    .ValueGeneratedNever();

                entity.Property(e => e.SourceFormatLogoPath).HasMaxLength(30);

                entity.Property(e => e.SourceFormatName).HasMaxLength(10);
            });

            modelBuilder.Entity<Collection>(entity =>
            {
                entity.HasKey(e => e.CollectionId);

                entity.Property(e => e.CollectionId).HasColumnName("CollectionsID");

                entity.Property(e => e.CollectionName).HasMaxLength(50);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.PersonId);

                entity.Property(e => e.PersonId).HasColumnName("PeopleID");

                entity.Property(e => e.PersonName).HasMaxLength(30);
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

            modelBuilder.Entity<ClipPerson>().HasKey(tp => new {tp.ClipId, tp.PeopleId});

            modelBuilder.Entity<ClipCollection>().HasKey(tp => new { tp.ClipId, tp.CollectionsId });
        }
    }
}
