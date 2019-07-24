using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WHVM.Database.Models
{
    public partial class HomeVideoDBContext : DbContext
    {
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClipCollection>()
                .HasKey(cc => new {cc.ClipId, cc.CollectionId});
            modelBuilder.Entity<ClipCollection>()
                .HasOne(cc => cc.Clip)
                .WithMany(c => c.ClipCollections)
                .HasForeignKey(cc => cc.ClipId);
            modelBuilder.Entity<ClipCollection>()
                .HasOne(cc => cc.Collection)
                .WithMany(c => c.ClipCollections)
                .HasForeignKey(cc => cc.CollectionId);

            modelBuilder.Entity<ClipPerson>()
                .HasKey(cp => new {cp.ClipId, cp.PersonId});
            modelBuilder.Entity<ClipPerson>()
                .HasOne(cp => cp.Clip)
                .WithMany(c => c.ClipPersons)
                .HasForeignKey(cp => cp.ClipId);
            modelBuilder.Entity<ClipPerson>()
                .HasOne(cp => cp.Person)
                .WithMany(p => p.ClipPersons)
                .HasForeignKey(cp => cp.PersonId);

            modelBuilder.Entity<Clip>()
                .HasOne(c => c.ClipCameraOperator)
                .WithMany(c => c.ClipsAsCameraOperator)
                .HasForeignKey(c => c.ClipCameraOperatorId);
            modelBuilder.Entity<Clip>()
                .HasOne(c => c.ClipReviewer)
                .WithMany(c => c.ClipsAsReviewer)
                .HasForeignKey(c => c.ClipReviewerId);
            modelBuilder.Entity<Clip>()
                .HasOne(c => c.Source)
                .WithMany(s => s.Clips)
                .HasForeignKey(c => c.SourceId);

            modelBuilder.Entity<Source>()
                .HasOne(s => s.SourceFormat)
                .WithMany(sf => sf.Sources)
                .HasForeignKey(s => s.SourceFormatId)
                .OnDelete(DeleteBehavior.Restrict);

#if DEBUG
            modelBuilder.Entity<Collection>().HasData(
                new Collection() {CollectionId = 1, CollectionName = "Baby Moments"},
                new Collection() {CollectionId = 2, CollectionName = "Birthday"},
                new Collection() {CollectionId = 3, CollectionName = "Blackmail"},
                new Collection() {CollectionId = 4, CollectionName = "Christmas"},
                new Collection() {CollectionId = 5, CollectionName = "Church"},
                new Collection() {CollectionId = 6, CollectionName = "Driving"},
                new Collection() {CollectionId = 7, CollectionName = "Easter"},
                new Collection() {CollectionId = 8, CollectionName = "Frost-McKay Cousins"},
                new Collection() {CollectionId = 9, CollectionName = "Frost-McKay Family Gatherings"},
                new Collection() {CollectionId = 10, CollectionName = "Grandma's House"},
                new Collection() {CollectionId = 11, CollectionName = "Halloween"},
                new Collection() {CollectionId = 12, CollectionName = "Hot Springs"},
                new Collection() {CollectionId = 13, CollectionName = "Misc. Holidays"},
                new Collection() {CollectionId = 14, CollectionName = "Outside Playtime"},
                new Collection() {CollectionId = 15, CollectionName = "Play/Performance"},
                new Collection() {CollectionId = 16, CollectionName = "Playtime"},
                new Collection() {CollectionId = 17, CollectionName = "Pre-School"},
                new Collection() {CollectionId = 18, CollectionName = "School"},
                new Collection() {CollectionId = 19, CollectionName = "Swimming"},
                new Collection() {CollectionId = 20, CollectionName = "Swinging"},
                new Collection() {CollectionId = 21, CollectionName = "Thatchers"},
                new Collection() {CollectionId = 22, CollectionName = "Trips/Vacations"},
                new Collection() {CollectionId = 23, CollectionName = "Westgate Cousins"},
                new Collection() {CollectionId = 24, CollectionName = "Westgate Family Gatherings"}
            );

            modelBuilder.Entity<Person>().HasData(
                new Person() {PersonId = 1, PersonName = "Bernard Westgate"},
                new Person() {PersonId = 2, PersonName = "Beth Westgate"},
                new Person() {PersonId = 3, PersonName = "Dennis Frost Sr."},
                new Person() {PersonId = 4, PersonName = "Dennis Westgate"},
                new Person() {PersonId = 5, PersonName = "Drew Westgate"},
                new Person() {PersonId = 6, PersonName = "Edward Frost"},
                new Person() {PersonId = 7, PersonName = "Hilda Frost"},
                new Person() {PersonId = 8, PersonName = "Irene McKay"},
                new Person() {PersonId = 9, PersonName = "Mary Westgate"},
                new Person() {PersonId = 10, PersonName = "Merideth Westgate"},
                new Person() {PersonId = 11, PersonName = "Zachary Westgate"},
                new Person() {PersonId = 12, PersonName = "Gary Frost"}
            );

            modelBuilder.Entity<SourceFormat>().HasData(
                new SourceFormat()
                    {SourceFormatId = 1, SourceFormatName = "DVD", SourceFormatLogoPath = "images/DVD_logo.svg"},
                new SourceFormat()
                {
                    SourceFormatId = 2, SourceFormatName = "Digital8", SourceFormatLogoPath = "images/Digital8_logo.svg"
                },
                new SourceFormat()
                    {SourceFormatId = 3, SourceFormatName = "Hi8", SourceFormatLogoPath = "images/Hi8_logo.svg"},
                new SourceFormat()
                    {SourceFormatId = 4, SourceFormatName = "VHS", SourceFormatLogoPath = "images/VHS_logo.svg"}
            );

            modelBuilder.Entity<Source>().HasData(
                new Source()
                {
                    SourceId = 1, SourceName = "1994-02_1996-12",
                    SourceDateCreated = DateTime.Parse("2009-06-21 07:25:00.000"),
                    SourceDateImported = DateTime.Parse("2018-07-25 19:28:52.000"), SourceFormatId = 1
                },
                new Source()
                {
                    SourceId = 2, SourceName = "1996-12_1997-05",
                    SourceDateCreated = DateTime.Parse("2009-06-21 07:34:49.000"),
                    SourceDateImported = DateTime.Parse("2018-07-16 01:07:19.000"), SourceFormatId = 1
                },
                new Source()
                {
                    SourceId = 3, SourceName = "1997-05_1997-08",
                    SourceDateCreated = DateTime.Parse("2009-06-21 07:38:13.000"),
                    SourceDateImported = DateTime.Parse("2018-07-16 09:34:49.000"), SourceFormatId = 1
                },
                new Source()
                {
                    SourceId = 4, SourceName = "1997-10_1998-03",
                    SourceDateCreated = DateTime.Parse("2009-06-21 07:44:41.000"),
                    SourceDateImported = DateTime.Parse("2018-07-16 01:18:24.000"), SourceFormatId = 1
                },
                new Source()
                {
                    SourceId = 5, SourceName = "1998-11_1999-02",
                    SourceDateCreated = DateTime.Parse("2009-06-21 07:48:05.000"),
                    SourceDateImported = DateTime.Parse("2018-07-16 08:58:06.000"), SourceFormatId = 1
                },
                new Source()
                {
                    SourceId = 6, SourceName = "1999-02_1999-06",
                    SourceDateCreated = DateTime.Parse("2009-06-21 07:51:08.000"),
                    SourceDateImported = DateTime.Parse("2018-07-16 09:44:23.000"), SourceFormatId = 1
                },
                new Source()
                {
                    SourceId = 7, SourceName = "1999-11_2000-04",
                    SourceDateCreated = DateTime.Parse("2009-06-21 07:59:32.000"),
                    SourceDateImported = DateTime.Parse("2018-07-16 09:45:52.000"), SourceFormatId = 1
                },
                new Source()
                {
                    SourceId = 8, SourceName = "2000-11_2001-01",
                    SourceDateCreated = DateTime.Parse("2009-06-21 08:06:06.000"),
                    SourceDateImported = DateTime.Parse("2018-07-16 10:00:11.000"), SourceFormatId = 1
                },
                new Source()
                {
                    SourceId = 9, SourceName = "2001-02_2001-05",
                    SourceDateCreated = DateTime.Parse("2009-06-22 02:48:17.000"),
                    SourceDateImported = DateTime.Parse("2018-07-16 22:25:18.000"), SourceFormatId = 1
                },
                new Source()
                {
                    SourceId = 10, SourceName = "2001-05_2001-08",
                    SourceDateCreated = DateTime.Parse("2009-06-22 02:42:47.000"),
                    SourceDateImported = DateTime.Parse("2018-08-01 09:41:45.000"), SourceFormatId = 1
                },
                new Source()
                {
                    SourceId = 11, SourceName = "2001-12_2002-02",
                    SourceDateCreated = DateTime.Parse("2009-06-22 02:51:46.000"),
                    SourceDateImported = DateTime.Parse("2018-08-01 09:41:45.000"), SourceFormatId = 1
                },
                new Source()
                {
                    SourceId = 12, SourceName = "2002-09_2002-11",
                    SourceDateCreated = DateTime.Parse("2009-06-22 03:08:08.000"),
                    SourceDateImported = DateTime.Parse("2018-07-16 10:07:34.000"), SourceFormatId = 1
                },
                new Source()
                {
                    SourceId = 13, SourceName = "2002-11_2002-12",
                    SourceDateCreated = DateTime.Parse("2009-06-22 03:10:13.000"),
                    SourceDateImported = DateTime.Parse("2018-07-25 20:13:59.000"), SourceFormatId = 1
                },
                new Source()
                {
                    SourceId = 14, SourceName = "2003-04_2003-06",
                    SourceDateCreated = DateTime.Parse("2009-06-24 08:48:39.000"),
                    SourceDateImported = DateTime.Parse("2018-07-25 22:39:46.000"), SourceFormatId = 1
                },
                new Source()
                {
                    SourceId = 15, SourceName = "2003-07_2003-10",
                    SourceDateCreated = DateTime.Parse("2009-06-22 03:16:10.000"),
                    SourceDateImported = DateTime.Parse("2018-07-29 21:22:21.000"), SourceFormatId = 1
                },
                new Source()
                {
                    SourceId = 16, SourceName = "2003-10_2003-11",
                    SourceDateCreated = DateTime.Parse("2009-06-22 05:52:20.000"),
                    SourceDateImported = DateTime.Parse("2018-08-01 18:10:05.000"), SourceFormatId = 1
                },
                new Source()
                {
                    SourceId = 17, SourceName = "2003-12_2004-03",
                    SourceDateCreated = DateTime.Parse("2009-06-24 08:46:14.000"),
                    SourceDateImported = DateTime.Parse("2018-07-29 21:27:50.000"), SourceFormatId = 1
                },
                new Source()
                {
                    SourceId = 18, SourceName = "2005-01_2005_05",
                    SourceDateCreated = DateTime.Parse("2009-06-22 06:27:41.000"),
                    SourceDateImported = DateTime.Parse("2018-07-30 00:36:26.000"), SourceFormatId = 1
                },
                new Source()
                {
                    SourceId = 19, SourceName = "2005-05_2005-11",
                    SourceDateCreated = DateTime.Parse("2009-06-22 06:45:52.000"),
                    SourceDateImported = DateTime.Parse("2018-07-30 00:36:45.000"), SourceFormatId = 1
                },
                new Source()
                {
                    SourceId = 20, SourceName = "2005-11_2005-12",
                    SourceDateCreated = DateTime.Parse("2009-06-22 06:20:39.000"),
                    SourceDateImported = DateTime.Parse("2018-07-30 20:03:08.000"), SourceFormatId = 1
                },
                new Source()
                {
                    SourceId = 22, SourceName = "20060523_20060526_Digital8",
                    SourceDateCreated = DateTime.Parse("2006-05-23 08:58:39.000"),
                    SourceDateImported = DateTime.Parse("2019-03-23 12:11:00.000"), SourceFormatId = 2
                }
            );

            modelBuilder.Entity<Clip>().HasData(
                new Clip()
                {
                    ClipId = 1,
                    SourceSegment = 1,
                    SourceId = 1,
                    ClipNumber = 1,
                    ClipTimeStart = DateTime.Parse("1995-12-24 23:14:24.000"),
                    ClipTimeEnd = DateTime.Parse("1995-12-24 23:32:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:00:00.968"),
                    ClipVidTimeEnd = TimeSpan.Parse("00:04:03.710"),
                    ClipReviewerId = 5,
                    ClipCameraOperatorId = 2,
                    ClipName = "Christmas Eve, 1995 - Christmas Eve at the Frosts' House"
                },
                new Clip()
                {
                    ClipId = 2, SourceSegment = 1, SourceId = 1, ClipNumber = 2,
                    ClipTimeStart = DateTime.Parse("1996-02-16 21:03:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-02-16 21:15:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:00:11.678"), ClipVidTimeEnd = TimeSpan.Parse("00:10:20.253"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-02-16 - Dennis Plays With Drew"
                },
                new Clip()
                {
                    ClipId = 3, SourceSegment = 1, SourceId = 1, ClipNumber = 3,
                    ClipTimeStart = DateTime.Parse("1996-02-18 14:09:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-02-18 14:10:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:10:20.286"), ClipVidTimeEnd = TimeSpan.Parse("00:11:34.661"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1996-02-18 - Dennis & Beth Watch Drew Play With a Balloon"
                },
                new Clip()
                {
                    ClipId = 4, SourceSegment = 1, SourceId = 1, ClipNumber = 4,
                    ClipTimeStart = DateTime.Parse("1996-02-18 20:58:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-02-18 21:01:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:11:34.694"), ClipVidTimeEnd = TimeSpan.Parse("00:14:01.874"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1996-02-18 - Dennis & Beth Feed Drew on Bed"
                },
                new Clip()
                {
                    ClipId = 5, SourceSegment = 1, SourceId = 1, ClipNumber = 5,
                    ClipTimeStart = DateTime.Parse("1996-02-19 19:28:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-02-19 19:35:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:14:01.908"), ClipVidTimeEnd = TimeSpan.Parse("00:16:54.080"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-02-19 - Dennis & Beth Play With Drew"
                },
                new Clip()
                {
                    ClipId = 6, SourceSegment = 1, SourceId = 1, ClipNumber = 6,
                    ClipTimeStart = DateTime.Parse("1994-02-12 20:08:00.000"),
                    ClipTimeEnd = DateTime.Parse("1994-02-12 20:23:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:16:59.118"), ClipVidTimeEnd = TimeSpan.Parse("00:27:57.910"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "[Redacted at Beth's Request]"
                },
                new Clip()
                {
                    ClipId = 7, SourceSegment = 1, SourceId = 1, ClipNumber = 7,
                    ClipTimeStart = DateTime.Parse("1994-02-12 20:18:00.000"),
                    ClipTimeEnd = DateTime.Parse("1994-02-12 20:19:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:27:57.943"), ClipVidTimeEnd = TimeSpan.Parse("00:28:53.498"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "[Redacted at Beth's Request]"
                },
                new Clip()
                {
                    ClipId = 8, SourceSegment = 2, SourceId = 1, ClipNumber = 8,
                    ClipTimeStart = DateTime.Parse("1996-04-12 14:02:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-04-12 14:10:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:00:11.645"), ClipVidTimeEnd = TimeSpan.Parse("00:08:06.252"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 12,
                    ClipName = "1996-04-12 - Dennis' & Beth's Wedding Ceremony"
                },
                new Clip()
                {
                    ClipId = 9, SourceSegment = 2, SourceId = 1, ClipNumber = 9,
                    ClipTimeStart = DateTime.Parse("1996-04-12 14:18:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-04-12 14:31:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:08:06.286"), ClipVidTimeEnd = TimeSpan.Parse("00:20:29.795"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 12,
                    ClipName = "1996-04-12 - Dennis' & Beth's Wedding Reception"
                },
                new Clip()
                {
                    ClipId = 10, SourceSegment = 2, SourceId = 1, ClipNumber = 10,
                    ClipTimeStart = DateTime.Parse("1996-04-17 18:04:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-04-17 18:05:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:20:35.734"), ClipVidTimeEnd = TimeSpan.Parse("00:21:26.185"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-04-17 - Ronnie Climbs a Tree"
                },
                new Clip()
                {
                    ClipId = 11, SourceSegment = 2, SourceId = 1, ClipNumber = 11,
                    ClipTimeStart = DateTime.Parse("1996-04-16 11:40:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-04-16 11:42:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:21:26.218"), ClipVidTimeEnd = TimeSpan.Parse("00:23:33.779"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1996-04-17 - Beth Films Around Grandma's House"
                },
                new Clip()
                {
                    ClipId = 12, SourceSegment = 2, SourceId = 1, ClipNumber = 12,
                    ClipTimeStart = DateTime.Parse("1996-04-16 15:40:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-04-16 15:57:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:23:33.813"), ClipVidTimeEnd = TimeSpan.Parse("00:25:20.085"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-04-16 - Dennis & Beth Play With Drew"
                },
                new Clip()
                {
                    ClipId = 13, SourceSegment = 2, SourceId = 1, ClipNumber = 13,
                    ClipTimeStart = DateTime.Parse("1996-04-16 17:17:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-04-16 17:38:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:25:20.119"), ClipVidTimeEnd = TimeSpan.Parse("00:28:22.601"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-04-16 - Car Ride With Brenda"
                },
                new Clip()
                {
                    ClipId = 14, SourceSegment = 2, SourceId = 1, ClipNumber = 14,
                    ClipTimeStart = DateTime.Parse("1996-04-17 11:37:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-04-17 11:46:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:28:22.634"), ClipVidTimeEnd = TimeSpan.Parse("00:33:42.521"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-04-16 - Driving to Grandpa's House"
                },
                new Clip()
                {
                    ClipId = 15, SourceSegment = 2, SourceId = 1, ClipNumber = 15,
                    ClipTimeStart = DateTime.Parse("1996-04-17 12:02:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-04-17 12:25:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:33:42.554"), ClipVidTimeEnd = TimeSpan.Parse("00:40:33.464"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1996-04-17 - Dennis, Beth & Drew Explore Grandpa's Cabin"
                },
                new Clip()
                {
                    ClipId = 16, SourceSegment = 2, SourceId = 1, ClipNumber = 16,
                    ClipTimeStart = DateTime.Parse("1996-04-17 18:08:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-04-17 18:12:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:40:39.137"), ClipVidTimeEnd = TimeSpan.Parse("00:44:05.343"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-04-17 - Dennis & Ronnie Play Outside"
                },
                new Clip()
                {
                    ClipId = 17, SourceSegment = 2, SourceId = 1, ClipNumber = 17,
                    ClipTimeStart = DateTime.Parse("1996-04-18 20:49:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-04-18 20:51:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:44:11.215"), ClipVidTimeEnd = TimeSpan.Parse("00:45:46.544"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-04-18 - Dennis & Beth Play With Drew"
                },
                new Clip()
                {
                    ClipId = 18, SourceSegment = 2, SourceId = 1, ClipNumber = 18,
                    ClipTimeStart = DateTime.Parse("1996-04-19 18:14:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-04-19 18:16:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:46:06.931"), ClipVidTimeEnd = TimeSpan.Parse("00:48:02.479"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-04-19 - Beth Tours the New House"
                },
                new Clip()
                {
                    ClipId = 19, SourceSegment = 2, SourceId = 1, ClipNumber = 19,
                    ClipTimeStart = DateTime.Parse("1996-04-21 21:18:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-04-21 21:24:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:48:09.120"), ClipVidTimeEnd = TimeSpan.Parse("00:53:05.449"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 4, ClipName = "1996-04-21 - Dennis Films Drew"
                },
                new Clip()
                {
                    ClipId = 20, SourceSegment = 2, SourceId = 1, ClipNumber = 20,
                    ClipTimeStart = DateTime.Parse("1996-05-08 12:29:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-05-08 15:25:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:53:18.529"), ClipVidTimeEnd = TimeSpan.Parse("00:58:36.446"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-05-08 - Drew & Jordan Play"
                },
                new Clip()
                {
                    ClipId = 21, SourceSegment = 2, SourceId = 1, ClipNumber = 21,
                    ClipTimeStart = DateTime.Parse("1996-05-09 18:13:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-05-09 18:25:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:58:43.253"), ClipVidTimeEnd = TimeSpan.Parse("01:02:43.493"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-05-09 - Dennis Swings Drew"
                },
                new Clip()
                {
                    ClipId = 22, SourceSegment = 2, SourceId = 1, ClipNumber = 22,
                    ClipTimeStart = DateTime.Parse("1996-05-09 21:19:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-05-09 21:22:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:02:43.526"), ClipVidTimeEnd = TimeSpan.Parse("01:05:43.139"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-05-09 - Beth Gives Drew a Bath"
                },
                new Clip()
                {
                    ClipId = 23, SourceSegment = 2, SourceId = 1, ClipNumber = 23,
                    ClipTimeStart = DateTime.Parse("1996-05-11 08:11:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-05-11 08:11:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:05:43.173"), ClipVidTimeEnd = TimeSpan.Parse("01:06:07.563"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-05-11 - Beth Wakes Drew Up"
                },
                new Clip()
                {
                    ClipId = 24, SourceSegment = 2, SourceId = 1, ClipNumber = 24,
                    ClipTimeStart = DateTime.Parse("1996-05-12 17:40:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-05-12 17:41:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:06:13.202"), ClipVidTimeEnd = TimeSpan.Parse("01:06:57.080"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1996-05-12 - Dennis Plays With Drew In His Crib"
                },
                new Clip()
                {
                    ClipId = 25, SourceSegment = 2, SourceId = 1, ClipNumber = 25,
                    ClipTimeStart = DateTime.Parse("1996-05-14 20:59:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-05-14 21:03:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:07:02.752"), ClipVidTimeEnd = TimeSpan.Parse("01:12:05.188"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-05-14 - Dennis Plays With Drew"
                },
                new Clip()
                {
                    ClipId = 26, SourceSegment = 2, SourceId = 1, ClipNumber = 26,
                    ClipTimeStart = DateTime.Parse("1996-05-15 21:03:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-05-15 21:14:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:12:05.221"), ClipVidTimeEnd = TimeSpan.Parse("01:17:53.168"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 4, ClipName = "1996-05-15 - Drew Learns to Crawl"
                },
                new Clip()
                {
                    ClipId = 27, SourceSegment = 2, SourceId = 1, ClipNumber = 27,
                    ClipTimeStart = DateTime.Parse("1996-05-26 15:49:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-05-26 15:50:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:17:59.441"), ClipVidTimeEnd = TimeSpan.Parse("01:18:49.358"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-05-26 - Gary Plays With Drew"
                },
                new Clip()
                {
                    ClipId = 28, SourceSegment = 2, SourceId = 1, ClipNumber = 28,
                    ClipTimeStart = DateTime.Parse("1996-05-28 20:32:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-05-28 20:34:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:18:55.063"), ClipVidTimeEnd = TimeSpan.Parse("01:19:57.092"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-05-28 - Drew Plays While Dennis Eats"
                },
                new Clip()
                {
                    ClipId = 29, SourceSegment = 2, SourceId = 1, ClipNumber = 29,
                    ClipTimeStart = DateTime.Parse("1996-06-14 10:05:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-06-14 10:06:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:20:03.399"), ClipVidTimeEnd = TimeSpan.Parse("01:21:40.663"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-06-14 - Drew Explores the Bathroom"
                },
                new Clip()
                {
                    ClipId = 30, SourceSegment = 2, SourceId = 1, ClipNumber = 30,
                    ClipTimeStart = DateTime.Parse("1996-06-15 13:32:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-06-15 13:32:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:21:46.668"), ClipVidTimeEnd = TimeSpan.Parse("01:22:26.775"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1996-06-15 - Ed Plays With Drew in the Frosts' Backyard"
                },
                new Clip()
                {
                    ClipId = 31, SourceSegment = 2, SourceId = 1, ClipNumber = 31,
                    ClipTimeStart = DateTime.Parse("1996-06-26 19:15:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-06-26 19:15:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:22:32.581"), ClipVidTimeEnd = TimeSpan.Parse("01:22:53.135"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-06-26 - Drew Plays With a Coaster"
                },
                new Clip()
                {
                    ClipId = 32, SourceSegment = 2, SourceId = 1, ClipNumber = 32,
                    ClipTimeStart = DateTime.Parse("1996-06-30 13:52:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-06-30 13:54:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:22:58.774"), ClipVidTimeEnd = TimeSpan.Parse("01:24:46.314"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1996-06-30 - Dennis & Beth Play With Drew in the Kiddie Pool"
                },
                new Clip()
                {
                    ClipId = 33, SourceSegment = 2, SourceId = 1, ClipNumber = 33,
                    ClipTimeStart = DateTime.Parse("1996-06-30 21:33:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-06-30 21:36:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:24:51.720"), ClipVidTimeEnd = TimeSpan.Parse("01:28:07.048"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1996-06-30 - Drew Eats His First Watermelon"
                },
                new Clip()
                {
                    ClipId = 34, SourceSegment = 2, SourceId = 1, ClipNumber = 34,
                    ClipTimeStart = DateTime.Parse("1996-07-01 21:09:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-07-01 21:17:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:28:07.082"), ClipVidTimeEnd = TimeSpan.Parse("01:29:09.444"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-07-01 - Dennis & Beth Play With Drew"
                },
                new Clip()
                {
                    ClipId = 35, SourceSegment = 2, SourceId = 1, ClipNumber = 35,
                    ClipTimeStart = DateTime.Parse("1996-07-06 10:44:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-07-06 10:55:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:29:09.478"), ClipVidTimeEnd = TimeSpan.Parse("01:34:08.543"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1996-07-06 - Dennis Plays With Drew, Austin & Ronnie"
                },
                new Clip()
                {
                    ClipId = 36, SourceSegment = 2, SourceId = 1, ClipNumber = 36,
                    ClipTimeStart = DateTime.Parse("1996-08-03 19:00:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-08-03 19:18:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:34:14.849"), ClipVidTimeEnd = TimeSpan.Parse("01:36:15.670"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-08-03 - Ed's Birthday Party"
                },
                new Clip()
                {
                    ClipId = 37, SourceSegment = 2, SourceId = 1, ClipNumber = 37,
                    ClipTimeStart = DateTime.Parse("1996-08-03 21:49:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-08-03 21:52:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:36:15.703"), ClipVidTimeEnd = TimeSpan.Parse("01:37:40.822"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-08-03 - Dennis & Beth Play With Drew"
                },
                new Clip()
                {
                    ClipId = 38, SourceSegment = 2, SourceId = 1, ClipNumber = 38,
                    ClipTimeStart = DateTime.Parse("1996-08-04 21:28:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-08-04 21:29:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:37:40.855"), ClipVidTimeEnd = TimeSpan.Parse("01:38:39.380"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-08-04 - Drew Takes a Bath"
                },
                new Clip()
                {
                    ClipId = 39, SourceSegment = 2, SourceId = 1, ClipNumber = 39,
                    ClipTimeStart = DateTime.Parse("1996-08-21 15:08:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-08-21 15:19:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:38:44.752"), ClipVidTimeEnd = TimeSpan.Parse("01:45:34.662"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-08-21 - Beth Plays With Drew"
                },
                new Clip()
                {
                    ClipId = 40, SourceSegment = 2, SourceId = 1, ClipNumber = 40,
                    ClipTimeStart = DateTime.Parse("1996-08-22 18:29:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-08-22 18:36:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:45:34.695"), ClipVidTimeEnd = TimeSpan.Parse("01:52:10.924"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1996-08-22 - Dennis Plays With Drew on the Driveway"
                },
                new Clip()
                {
                    ClipId = 41, SourceSegment = 2, SourceId = 1, ClipNumber = 41,
                    ClipTimeStart = DateTime.Parse("1996-09-17 15:52:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-09-17 16:51:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:52:16.396"), ClipVidTimeEnd = TimeSpan.Parse("01:56:52.339"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-09-17 - Beth Plays With Drew"
                },
                new Clip()
                {
                    ClipId = 42, SourceSegment = 2, SourceId = 1, ClipNumber = 42,
                    ClipTimeStart = DateTime.Parse("1996-09-20 21:21:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-09-20 21:22:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:56:57.911"), ClipVidTimeEnd = TimeSpan.Parse("01:57:21.234"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1996-09-20 - Drew Walks Around the Living Room"
                },
                new Clip()
                {
                    ClipId = 43, SourceSegment = 3, SourceId = 1, ClipNumber = 43,
                    ClipTimeStart = DateTime.Parse("1996-09-26 15:00:51.000"),
                    ClipTimeEnd = DateTime.Parse("1996-09-26 15:01:29.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:00:05.806"), ClipVidTimeEnd = TimeSpan.Parse("00:00:44.278"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-09-26 - Drew Dances To The Radio"
                },
                new Clip()
                {
                    ClipId = 44, SourceSegment = 3, SourceId = 1, ClipNumber = 44,
                    ClipTimeStart = DateTime.Parse("1996-09-27 22:19:03.000"),
                    ClipTimeEnd = DateTime.Parse("1996-09-27 22:23:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:00:44.311"), ClipVidTimeEnd = TimeSpan.Parse("00:04:07.347"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1996-09-27 - Drew Plays With Austin & Ronnie"
                },
                new Clip()
                {
                    ClipId = 45, SourceSegment = 3, SourceId = 1, ClipNumber = 45,
                    ClipTimeStart = DateTime.Parse("1996-12-25 08:49:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-12-25 08:51:19.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:04:07.381"), ClipVidTimeEnd = TimeSpan.Parse("00:06:14.140"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "Christmas, 1996 - Dennis, Beth & Drew Open Gifts On Christmas Morning"
                },
                new Clip()
                {
                    ClipId = 46, SourceSegment = 4, SourceId = 1, ClipNumber = 46,
                    ClipTimeStart = DateTime.Parse("1996-06-15 17:23:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-06-15 19:37:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:00:10.911"), ClipVidTimeEnd = TimeSpan.Parse("00:17:47.833"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1996-06-15 - Chelsea Williams' Birthday Party"
                },
                new Clip()
                {
                    ClipId = 47, SourceSegment = 4, SourceId = 1, ClipNumber = 47,
                    ClipTimeStart = DateTime.Parse("1996-08-24 13:20:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-08-24 14:49:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:17:53.940"), ClipVidTimeEnd = TimeSpan.Parse("00:34:33.171"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-08-24 - Kim's Baby Shower"
                },
                new Clip()
                {
                    ClipId = 48, SourceSegment = 4, SourceId = 1, ClipNumber = 48,
                    ClipTimeStart = DateTime.Parse("1996-09-28 17:46:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-09-28 17:47:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:34:39.110"), ClipVidTimeEnd = TimeSpan.Parse("00:35:38.636"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-09-28 - Drew & Matthew Play"
                },
                new Clip()
                {
                    ClipId = 49, SourceSegment = 4, SourceId = 1, ClipNumber = 49,
                    ClipTimeStart = DateTime.Parse("1996-09-29 13:28:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-09-29 15:00:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:35:38.670"), ClipVidTimeEnd = TimeSpan.Parse("00:44:48.853"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-09-29 - Drew's Birthday Party"
                },
                new Clip()
                {
                    ClipId = 50, SourceSegment = 4, SourceId = 1, ClipNumber = 50,
                    ClipTimeStart = DateTime.Parse("1996-10-02 20:37:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-10-02 20:51:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:44:54.292"), ClipVidTimeEnd = TimeSpan.Parse("00:52:13.597"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-10-02 - Dennis & Beth Play With Drew"
                },
                new Clip()
                {
                    ClipId = 51, SourceSegment = 4, SourceId = 1, ClipNumber = 51,
                    ClipTimeStart = DateTime.Parse("1996-10-03 19:14:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-10-03 19:22:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:52:13.631"), ClipVidTimeEnd = TimeSpan.Parse("00:54:11.048"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-10-03 - Dennis & Beth Play With Drew"
                },
                new Clip()
                {
                    ClipId = 52, SourceSegment = 5, SourceId = 1, ClipNumber = 52,
                    ClipTimeStart = DateTime.Parse("1996-10-31 17:58:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-10-31 19:58:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:00:07.508"), ClipVidTimeEnd = TimeSpan.Parse("00:02:11.098"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "Halloween, 1996 - Drew Wears His Costume, Plays With Dennis & Beth"
                },
                new Clip()
                {
                    ClipId = 53, SourceSegment = 5, SourceId = 1, ClipNumber = 53,
                    ClipTimeStart = DateTime.Parse("1996-11-15 15:50:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-11-15 15:52:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:02:16.403"), ClipVidTimeEnd = TimeSpan.Parse("00:02:34.321"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-11-15 - Beth Plays With Drew"
                },
                new Clip()
                {
                    ClipId = 54, SourceSegment = 5, SourceId = 1, ClipNumber = 54,
                    ClipTimeStart = DateTime.Parse("1996-11-29 18:32:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-11-29 18:37:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:02:39.660"), ClipVidTimeEnd = TimeSpan.Parse("00:04:03.210"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1996-11-29 - Dennis & Beth Dance With Drew"
                },
                new Clip()
                {
                    ClipId = 55, SourceSegment = 5, SourceId = 1, ClipNumber = 55,
                    ClipTimeStart = DateTime.Parse("1996-12-07 17:39:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-12-07 17:39:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:04:08.782"), ClipVidTimeEnd = TimeSpan.Parse("00:04:34.975"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-12-07 - Drew Watches TV"
                },
                new Clip()
                {
                    ClipId = 56, SourceSegment = 5, SourceId = 1, ClipNumber = 56,
                    ClipTimeStart = DateTime.Parse("1996-12-08 18:37:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-12-08 18:37:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:04:35.008"), ClipVidTimeEnd = TimeSpan.Parse("00:04:54.261"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1996-12-08 - Dennis & Beth Play With Drew While He Eats"
                },
                new Clip()
                {
                    ClipId = 57, SourceSegment = 5, SourceId = 1, ClipNumber = 57,
                    ClipTimeStart = DateTime.Parse("1996-12-09 18:32:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-12-09 18:37:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:04:54.294"), ClipVidTimeEnd = TimeSpan.Parse("00:09:08.715"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1996-12-09 - Drew Plays Around the Christmas Tree"
                },
                new Clip()
                {
                    ClipId = 58, SourceSegment = 5, SourceId = 1, ClipNumber = 58,
                    ClipTimeStart = DateTime.Parse("1996-12-13 16:39:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-12-13 16:42:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:09:08.748"), ClipVidTimeEnd = TimeSpan.Parse("00:10:12.312"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-12-13 - Dennis & Beth Play With Drew"
                },
                new Clip()
                {
                    ClipId = 59, SourceSegment = 5, SourceId = 1, ClipNumber = 59,
                    ClipTimeStart = DateTime.Parse("1996-12-16 16:22:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-12-16 16:24:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:10:12.345"), ClipVidTimeEnd = TimeSpan.Parse("00:12:09.963"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-12-16 - Drew Watches TV"
                },
                new Clip()
                {
                    ClipId = 60, SourceSegment = 5, SourceId = 1, ClipNumber = 60,
                    ClipTimeStart = DateTime.Parse("1996-12-19 12:20:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-12-19 12:22:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:12:09.996"), ClipVidTimeEnd = TimeSpan.Parse("00:13:47.860"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1996-12-19 - Beth Surveys the Christmas Decorations"
                },
                new Clip()
                {
                    ClipId = 61, SourceSegment = 5, SourceId = 1, ClipNumber = 61,
                    ClipTimeStart = DateTime.Parse("1996-12-20 21:58:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-12-20 22:05:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:13:53.866"), ClipVidTimeEnd = TimeSpan.Parse("00:20:20.519"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1996-12-20 - Dennis & Beth Play With Drew"
                },
                new Clip()
                {
                    ClipId = 62, SourceSegment = 5, SourceId = 1, ClipNumber = 62,
                    ClipTimeStart = DateTime.Parse("1996-12-24 12:20:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-12-24 12:20:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:20:20.519"), ClipVidTimeEnd = TimeSpan.Parse("00:20:23.189"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "Christmas Eve, 1996 - Dennis Plays With Drew"
                },
                new Clip()
                {
                    ClipId = 63, SourceSegment = 6, SourceId = 2, ClipNumber = 1,
                    ClipTimeStart = DateTime.Parse("1996-12-24 12:20:50.087"),
                    ClipTimeEnd = DateTime.Parse("1996-12-24 12:29:22.613"),
                    ClipVidTimeStart = TimeSpan.Parse("00:00:00.000"), ClipVidTimeEnd = TimeSpan.Parse("00:06:54.981"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "Christmas Eve, 1996 - Dennis Plays With Drew"
                },
                new Clip()
                {
                    ClipId = 64, SourceSegment = 6, SourceId = 2, ClipNumber = 2,
                    ClipTimeStart = DateTime.Parse("1996-12-24 17:37:56.197"),
                    ClipTimeEnd = DateTime.Parse("1996-12-24 22:51:40.407"),
                    ClipVidTimeStart = TimeSpan.Parse("00:07:03.014"), ClipVidTimeEnd = TimeSpan.Parse("00:18:30.409"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "Christmas Eve, 1996 - Ed & Hilda's Christmas Eve Party"
                },
                new Clip()
                {
                    ClipId = 65, SourceSegment = 6, SourceId = 2, ClipNumber = 3,
                    ClipTimeStart = DateTime.Parse("1996-12-25 00:18:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-12-25 00:41:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:18:30.442"), ClipVidTimeEnd = TimeSpan.Parse("00:18:51.497"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "Christmas Eve, 1996 - Midnight"
                },
                new Clip()
                {
                    ClipId = 66, SourceSegment = 6, SourceId = 2, ClipNumber = 4,
                    ClipTimeStart = DateTime.Parse("1996-12-25 08:13:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-12-25 08:46:18.150"),
                    ClipVidTimeStart = TimeSpan.Parse("00:18:51.530"), ClipVidTimeEnd = TimeSpan.Parse("00:49:03.840"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "Christmas Day, 1996 - Morning, Opening Gifts"
                },
                new Clip()
                {
                    ClipId = 67, SourceSegment = 7, SourceId = 2, ClipNumber = 5,
                    ClipTimeStart = DateTime.Parse("1996-12-25 11:55:00.000"),
                    ClipTimeEnd = DateTime.Parse("1996-12-25 12:38:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:00:09.609"), ClipVidTimeEnd = TimeSpan.Parse("00:08:34.679"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "Christmas Day, 1996 - Ed & Hilda's House"
                },
                new Clip()
                {
                    ClipId = 68, SourceSegment = 7, SourceId = 2, ClipNumber = 6,
                    ClipTimeStart = DateTime.Parse("1997-01-03 22:04:30.757"),
                    ClipTimeEnd = DateTime.Parse("1997-01-03 22:05:05.873"),
                    ClipVidTimeStart = TimeSpan.Parse("00:08:50.082"), ClipVidTimeEnd = TimeSpan.Parse("00:09:25.197"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-01-03 - Dennis Holds Zach, Drew Finds Wipes"
                },
                new Clip()
                {
                    ClipId = 69, SourceSegment = 7, SourceId = 2, ClipNumber = 7,
                    ClipTimeStart = DateTime.Parse("1997-01-05 19:02:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-01-05 19:19:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:09:34.607"), ClipVidTimeEnd = TimeSpan.Parse("00:20:14.112"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-01-05 - Dennis & Beth Play With Drew, Zach Naps"
                },
                new Clip()
                {
                    ClipId = 70, SourceSegment = 7, SourceId = 2, ClipNumber = 8,
                    ClipTimeStart = DateTime.Parse("1997-01-06 16:22:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-01-06 16:22:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:20:14.146"), ClipVidTimeEnd = TimeSpan.Parse("00:20:27.192"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-01-06 - Zach Swings"
                },
                new Clip()
                {
                    ClipId = 71, SourceSegment = 7, SourceId = 2, ClipNumber = 9,
                    ClipTimeStart = DateTime.Parse("1997-01-07 22:35:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-01-07 22:38:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:20:27.226"), ClipVidTimeEnd = TimeSpan.Parse("00:23:46.725"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-01-07 - Dennis & Beth Play With Drew"
                },
                new Clip()
                {
                    ClipId = 72, SourceSegment = 7, SourceId = 2, ClipNumber = 10,
                    ClipTimeStart = DateTime.Parse("1997-01-13 21:59:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-01-13 21:59:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:23:46.758"), ClipVidTimeEnd = TimeSpan.Parse("00:24:15.687"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-01-13 - Zach Sucks His Thumb While Napping"
                },
                new Clip()
                {
                    ClipId = 73, SourceSegment = 7, SourceId = 2, ClipNumber = 11,
                    ClipTimeStart = DateTime.Parse("1997-01-20 15:26:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-01-20 15:26:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:24:15.720"), ClipVidTimeEnd = TimeSpan.Parse("00:24:39.511"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-01-20 - Zach Naps"
                },
                new Clip()
                {
                    ClipId = 74, SourceSegment = 7, SourceId = 2, ClipNumber = 12,
                    ClipTimeStart = DateTime.Parse("1997-01-21 19:38:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-01-21 19:39:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:24:45.350"), ClipVidTimeEnd = TimeSpan.Parse("00:25:25.323"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-01-21 - Beth Plays With Zach"
                },
                new Clip()
                {
                    ClipId = 75, SourceSegment = 7, SourceId = 2, ClipNumber = 13,
                    ClipTimeStart = DateTime.Parse("1997-01-28 15:00:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-01-28 15:15:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:25:30.762"), ClipVidTimeEnd = TimeSpan.Parse("00:34:31.670"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-01-28 - Beth Talks To Zach While He Tries To Move"
                },
                new Clip()
                {
                    ClipId = 76, SourceSegment = 7, SourceId = 2, ClipNumber = 14,
                    ClipTimeStart = DateTime.Parse("1997-02-09 14:20:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-02-09 14:22:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:35:06.737"), ClipVidTimeEnd = TimeSpan.Parse("00:37:16.701"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-02-09 - Beth Films Drew Pottying"
                },
                new Clip()
                {
                    ClipId = 77, SourceSegment = 7, SourceId = 2, ClipNumber = 15,
                    ClipTimeStart = DateTime.Parse("1997-01-28 18:32:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-01-28 18:33:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:34:37.442"), ClipVidTimeEnd = TimeSpan.Parse("00:35:06.705"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-01-28 - Ed Plays With Zachary"
                },
                new Clip()
                {
                    ClipId = 78, SourceSegment = 7, SourceId = 2, ClipNumber = 16,
                    ClipTimeStart = DateTime.Parse("1997-02-09 20:46:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-02-09 21:14:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:37:16.734"), ClipVidTimeEnd = TimeSpan.Parse("00:59:43.646"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-02-09 - Dennis & Beth Play With Drew & Zach"
                },
                new Clip()
                {
                    ClipId = 79, SourceSegment = 7, SourceId = 2, ClipNumber = 17,
                    ClipTimeStart = DateTime.Parse("1997-02-27 16:17:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-02-27 16:20:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:59:49.052"), ClipVidTimeEnd = TimeSpan.Parse("01:02:48.030"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-02-27 - Zach Swings"
                },
                new Clip()
                {
                    ClipId = 80, SourceSegment = 7, SourceId = 2, ClipNumber = 18,
                    ClipTimeStart = DateTime.Parse("1997-03-02 15:35:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-03-02 15:36:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:02:53.603"), ClipVidTimeEnd = TimeSpan.Parse("01:04:27.096"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 4,
                    ClipName = "1997-03-02 - Dennis & Zach Entertain Each Other"
                },
                new Clip()
                {
                    ClipId = 81, SourceSegment = 7, SourceId = 2, ClipNumber = 19,
                    ClipTimeStart = DateTime.Parse("1997-03-04 09:07:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-03-04 10:59:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:04:27.129"), ClipVidTimeEnd = TimeSpan.Parse("01:10:35.731"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-03-04 - Drew & Jordan Play, Beth Plays With Zach"
                },
                new Clip()
                {
                    ClipId = 82, SourceSegment = 7, SourceId = 2, ClipNumber = 20,
                    ClipTimeStart = DateTime.Parse("1997-03-05 09:49:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-03-05 11:30:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:10:41.103"), ClipVidTimeEnd = TimeSpan.Parse("01:20:10.572"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-03-05 - Drew & Jordan Play"
                },
                new Clip()
                {
                    ClipId = 83, SourceSegment = 7, SourceId = 2, ClipNumber = 21,
                    ClipTimeStart = DateTime.Parse("1997-03-06 14:29:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-03-06 14:47:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:20:16.077"), ClipVidTimeEnd = TimeSpan.Parse("01:29:33.134"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-03-06 - Drew & Jordan Play"
                },
                new Clip()
                {
                    ClipId = 84, SourceSegment = 7, SourceId = 2, ClipNumber = 22,
                    ClipTimeStart = DateTime.Parse("1997-03-10 09:18:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-03-10 15:40:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:29:37.638"), ClipVidTimeEnd = TimeSpan.Parse("01:33:54.996"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-03-10 - Drew & Jordan Play, Beth Plays With Elizabeth & Zach"
                },
                new Clip()
                {
                    ClipId = 85, SourceSegment = 7, SourceId = 2, ClipNumber = 23,
                    ClipTimeStart = DateTime.Parse("1997-03-12 22:12:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-03-12 22:15:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:33:55.029"), ClipVidTimeEnd = TimeSpan.Parse("01:37:29.143"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-03-12 - Dennis & Drew Play With Zach"
                },
                new Clip()
                {
                    ClipId = 86, SourceSegment = 7, SourceId = 2, ClipNumber = 24,
                    ClipTimeStart = DateTime.Parse("1997-03-16 19:08:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-03-16 19:10:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:37:29.176"), ClipVidTimeEnd = TimeSpan.Parse("01:39:07.241"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-03-16 - Dennis Plays With Zach"
                },
                new Clip()
                {
                    ClipId = 87, SourceSegment = 7, SourceId = 2, ClipNumber = 25,
                    ClipTimeStart = DateTime.Parse("1997-03-27 16:49:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-03-27 17:37:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:39:07.274"), ClipVidTimeEnd = TimeSpan.Parse("01:44:50.450"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 4,
                    ClipName = "1997-03-27 - Westgates Play Outside at Brenda's House"
                },
                new Clip()
                {
                    ClipId = 88, SourceSegment = 7, SourceId = 2, ClipNumber = 26,
                    ClipTimeStart = DateTime.Parse("1997-03-30 09:50:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-03-30 12:03:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:44:50.484"), ClipVidTimeEnd = TimeSpan.Parse("02:03:34.306"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "Easter, 1997 - Westgates Play Outside at Grandma's House (1)"
                },
                new Clip()
                {
                    ClipId = 89, SourceSegment = 8, SourceId = 2, ClipNumber = 27,
                    ClipTimeStart = DateTime.Parse("1997-03-30 12:15:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-03-30 17:08:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:00:07.974"), ClipVidTimeEnd = TimeSpan.Parse("00:09:28.968"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "Easter, 1997 - Westgates Play Outside at Grandma's House (2)"
                },
                new Clip()
                {
                    ClipId = 90, SourceSegment = 8, SourceId = 2, ClipNumber = 28,
                    ClipTimeStart = DateTime.Parse("1997-04-10 17:17:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-04-10 17:18:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:09:33.472"), ClipVidTimeEnd = TimeSpan.Parse("00:10:15.615"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-04-10 - Dennis, Drew & Zach Play"
                },
                new Clip()
                {
                    ClipId = 91, SourceSegment = 8, SourceId = 2, ClipNumber = 29,
                    ClipTimeStart = DateTime.Parse("1997-04-11 11:41:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-04-11 11:44:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:10:15.658"), ClipVidTimeEnd = TimeSpan.Parse("00:10:47.513"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-04-11 - Dennis Frost Sr.'s Birthday"
                },
                new Clip()
                {
                    ClipId = 92, SourceSegment = 8, SourceId = 2, ClipNumber = 30,
                    ClipTimeStart = DateTime.Parse("1997-04-11 21:30:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-04-11 21:35:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:10:52.952"), ClipVidTimeEnd = TimeSpan.Parse("00:15:35.534"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-04-11 - Beth, Drew & Zach Play"
                },
                new Clip()
                {
                    ClipId = 93, SourceSegment = 8, SourceId = 2, ClipNumber = 31,
                    ClipTimeStart = DateTime.Parse("1997-04-14 11:10:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-04-14 11:11:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:15:35.567"), ClipVidTimeEnd = TimeSpan.Parse("00:16:46.872"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-04-14 - Zach Plays With His Mobile"
                },
                new Clip()
                {
                    ClipId = 94, SourceSegment = 8, SourceId = 2, ClipNumber = 32,
                    ClipTimeStart = DateTime.Parse("1997-04-23 10:11:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-04-23 10:15:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:16:52.211"), ClipVidTimeEnd = TimeSpan.Parse("00:21:44.369"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-04-23 - Beth, Drew & Zach Play"
                },
                new Clip()
                {
                    ClipId = 95, SourceSegment = 8, SourceId = 2, ClipNumber = 33,
                    ClipTimeStart = DateTime.Parse("1997-04-24 16:02:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-04-24 20:46:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:21:44.403"), ClipVidTimeEnd = TimeSpan.Parse("00:26:26.394"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-04-24 - Gary, Sarah & Beth Play With Drew & Zach, Zach Swings"
                },
                new Clip()
                {
                    ClipId = 96, SourceSegment = 8, SourceId = 2, ClipNumber = 34,
                    ClipTimeStart = DateTime.Parse("1997-05-01 13:43:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-05-01 13:46:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:26:36.428"), ClipVidTimeEnd = TimeSpan.Parse("00:28:46.191"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-05-01 - Beth Plays With Zach"
                },
                new Clip()
                {
                    ClipId = 97, SourceSegment = 8, SourceId = 2, ClipNumber = 35,
                    ClipTimeStart = DateTime.Parse("1997-05-05 17:16:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-05-05 17:22:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:28:46.224"), ClipVidTimeEnd = TimeSpan.Parse("00:34:36.574"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-05-05 - Beth Plays With Drew & Zach While Dennis Works On The Playground"
                },
                new Clip()
                {
                    ClipId = 98, SourceSegment = 8, SourceId = 2, ClipNumber = 36,
                    ClipTimeStart = DateTime.Parse("1997-05-08 22:08:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-05-08 22:11:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:34:36.607"), ClipVidTimeEnd = TimeSpan.Parse("00:35:28.226"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-05-08 - Dennis & Beth Play With Drew & Zach"
                },
                new Clip()
                {
                    ClipId = 99, SourceSegment = 8, SourceId = 2, ClipNumber = 37,
                    ClipTimeStart = DateTime.Parse("1997-05-09 12:42:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-05-09 18:07:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:35:28.259"), ClipVidTimeEnd = TimeSpan.Parse("00:45:12.142"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-05-08 - Beth Plays With Zach While Drew & Dennis Play Outside"
                },
                new Clip()
                {
                    ClipId = 100, SourceSegment = 8, SourceId = 2, ClipNumber = 38,
                    ClipTimeStart = DateTime.Parse("1997-05-10 08:17:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-05-10 08:19:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:45:12.176"), ClipVidTimeEnd = TimeSpan.Parse("00:47:02.119"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-05-10 - Zach Swings"
                },
                new Clip()
                {
                    ClipId = 101, SourceSegment = 8, SourceId = 2, ClipNumber = 39,
                    ClipTimeStart = DateTime.Parse("1997-05-11 18:43:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-05-11 18:19:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:47:02.152"), ClipVidTimeEnd = TimeSpan.Parse("00:51:12.769"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName =
                        "1997-05-11 - Drew Explores the Playground With Beth While Dennis Continues Working, Zach Plays Inside"
                },
                new Clip()
                {
                    ClipId = 102, SourceSegment = 8, SourceId = 2, ClipNumber = 40,
                    ClipTimeStart = DateTime.Parse("1997-05-16 08:15:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-05-16 08:18:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:51:17.541"), ClipVidTimeEnd = TimeSpan.Parse("00:54:07.143"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-05-16 - Beth Wakes Up Drew & Zach"
                },
                new Clip()
                {
                    ClipId = 103, SourceSegment = 8, SourceId = 2, ClipNumber = 41,
                    ClipTimeStart = DateTime.Parse("1997-05-17 07:03:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-05-17 08:03:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:54:07.177"), ClipVidTimeEnd = TimeSpan.Parse("00:54:29.299"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-05-17 - Drew Plays With Elizabeth"
                },
                new Clip()
                {
                    ClipId = 104, SourceSegment = 8, SourceId = 2, ClipNumber = 42,
                    ClipTimeStart = DateTime.Parse("1997-05-17 16:11:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-05-17 16:14:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:54:29.332"), ClipVidTimeEnd = TimeSpan.Parse("00:57:32.348"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-05-17 - Drew Plays Outside In the Pool While Dennis Works On the Playground"
                },
                new Clip()
                {
                    ClipId = 105, SourceSegment = 8, SourceId = 2, ClipNumber = 43,
                    ClipTimeStart = DateTime.Parse("1997-05-23 17:25:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-05-23 18:41:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:57:32.382"), ClipVidTimeEnd = TimeSpan.Parse("00:59:02.105"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-05-23 - Maw-Maw's Birthday Party (1)"
                },
                new Clip()
                {
                    ClipId = 106, SourceSegment = 8, SourceId = 2, ClipNumber = 44,
                    ClipTimeStart = DateTime.Parse("1997-05-23 18:44:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-05-23 19:32:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:01:53.476"), ClipVidTimeEnd = TimeSpan.Parse("01:11:51.774"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-05-23 - Maw-Maw's Birthday Party (2)"
                },
                new Clip()
                {
                    ClipId = 107, SourceSegment = 1, SourceId = 4, ClipNumber = 1,
                    ClipTimeStart = DateTime.Parse("1997-10-19 17:34:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-10-19 17:36:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:00:08.809"), ClipVidTimeEnd = TimeSpan.Parse("00:02:03.056"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-10-19 - Drew Plays With Tonka Trucks"
                },
                new Clip()
                {
                    ClipId = 108, SourceSegment = 1, SourceId = 4, ClipNumber = 2,
                    ClipTimeStart = DateTime.Parse("1997-10-24 19:34:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-10-24 19:36:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:02:03.090"), ClipVidTimeEnd = TimeSpan.Parse("00:03:08.255"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-10-24 - Drew & Zach Play With Toy Bucket"
                },
                new Clip()
                {
                    ClipId = 109, SourceSegment = 1, SourceId = 4, ClipNumber = 3,
                    ClipTimeStart = DateTime.Parse("1997-10-25 21:37:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-10-25 21:39:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:03:08.288"), ClipVidTimeEnd = TimeSpan.Parse("00:05:17.717"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-10-25 - Drew Eats While Dennis Plays with Zach"
                },
                new Clip()
                {
                    ClipId = 110, SourceSegment = 1, SourceId = 4, ClipNumber = 4,
                    ClipTimeStart = DateTime.Parse("1997-10-29 14:18:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-10-29 14:19:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:05:17.751"), ClipVidTimeEnd = TimeSpan.Parse("00:05:57.290"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-10-29 - Dennis Plays With Zach"
                },
                new Clip()
                {
                    ClipId = 111, SourceSegment = 1, SourceId = 4, ClipNumber = 5,
                    ClipTimeStart = DateTime.Parse("1997-11-05 20:55:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-11-05 21:51:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:05:57.324"), ClipVidTimeEnd = TimeSpan.Parse("00:30:05.871"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-11-05 - Dennis & Beth Play With Drew & Zach, Zach Walks"
                },
                new Clip()
                {
                    ClipId = 112, SourceSegment = 1, SourceId = 4, ClipNumber = 6,
                    ClipTimeStart = DateTime.Parse("1997-11-07 16:32:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-11-07 16:33:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:30:12.244"), ClipVidTimeEnd = TimeSpan.Parse("00:31:25.283"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-11-07 - Jamie, Drew & Zach Play in the Kitchen"
                },
                new Clip()
                {
                    ClipId = 113, SourceSegment = 1, SourceId = 4, ClipNumber = 7,
                    ClipTimeStart = DateTime.Parse("1997-11-13 14:21:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-11-13 19:46:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:31:25.317"), ClipVidTimeEnd = TimeSpan.Parse("00:44:42.480"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-11-13 - Zach Explores House, Drew & Zach Play"
                },
                new Clip()
                {
                    ClipId = 114, SourceSegment = 1, SourceId = 4, ClipNumber = 8,
                    ClipTimeStart = DateTime.Parse("1997-11-15 19:03:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-11-15 20:06:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:44:42.513"), ClipVidTimeEnd = TimeSpan.Parse("00:55:51.715"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-11-15 - Beth & Dennis Play With Drew & Zach, Ed & Hilda Visit"
                },
                new Clip()
                {
                    ClipId = 115, SourceSegment = 1, SourceId = 4, ClipNumber = 9,
                    ClipTimeStart = DateTime.Parse("1997-11-17 17:02:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-11-17 17:15:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:55:51.748"), ClipVidTimeEnd = TimeSpan.Parse("01:07:25.608"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-11-17 - Beth & Dennis Play With Drew & Zach"
                },
                new Clip()
                {
                    ClipId = 116, SourceSegment = 1, SourceId = 4, ClipNumber = 10,
                    ClipTimeStart = DateTime.Parse("1997-11-21 22:35:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-11-21 22:50:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:07:31.147"), ClipVidTimeEnd = TimeSpan.Parse("01:19:36.539"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-11-21 - Beth & Dennis Play With Drew & Zach"
                },
                new Clip()
                {
                    ClipId = 117, SourceSegment = 1, SourceId = 4, ClipNumber = 11,
                    ClipTimeStart = DateTime.Parse("1997-11-24 15:21:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-11-24 15:50:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:19:36.572"), ClipVidTimeEnd = TimeSpan.Parse("01:29:16.852"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-11-24 - Beth, Drew & Zach's Video Birthday Card for Austin"
                },
                new Clip()
                {
                    ClipId = 118, SourceSegment = 1, SourceId = 4, ClipNumber = 12,
                    ClipTimeStart = DateTime.Parse("1997-11-27 12:58:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-11-27 13:00:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:29:22.391"), ClipVidTimeEnd = TimeSpan.Parse("01:30:29.458"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "Thanksgiving, 1997 - Lunch at the Frosts' House With Jeff & Sue"
                },
                new Clip()
                {
                    ClipId = 119, SourceSegment = 1, SourceId = 4, ClipNumber = 13,
                    ClipTimeStart = DateTime.Parse("1997-11-27 20:48:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-11-27 21:23:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:30:29.491"), ClipVidTimeEnd = TimeSpan.Parse("01:47:52.466"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 12,
                    ClipName = "Thanksgiving, 1997 - Night at the Frosts' House"
                },
                new Clip()
                {
                    ClipId = 120, SourceSegment = 1, SourceId = 4, ClipNumber = 14,
                    ClipTimeStart = DateTime.Parse("1997-12-03 21:39:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-12-03 20:02:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:47:57.571"), ClipVidTimeEnd = TimeSpan.Parse("01:51:40.026"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-12-03 - Beth, Dennis, Drew & Zach Decorate the Christmas Tree"
                },
                new Clip()
                {
                    ClipId = 121, SourceSegment = 1, SourceId = 4, ClipNumber = 15,
                    ClipTimeStart = DateTime.Parse("1997-12-05 20:09:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-12-05 20:13:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:51:40.060"), ClipVidTimeEnd = TimeSpan.Parse("01:55:27.788"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-12-05 - Beth Plays With Drew & Zach Around the Christmas Tree"
                },
                new Clip()
                {
                    ClipId = 122, SourceSegment = 1, SourceId = 4, ClipNumber = 16,
                    ClipTimeStart = DateTime.Parse("1997-12-11 14:56:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-12-11 16:02:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:55:33.026"), ClipVidTimeEnd = TimeSpan.Parse("02:04:25.358"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-12-11 - Beth, Drew & Zach Open Gifts with Julie & Carly"
                },
                new Clip()
                {
                    ClipId = 123, SourceSegment = 2, SourceId = 4, ClipNumber = 17,
                    ClipTimeStart = DateTime.Parse("1997-12-15 11:58:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-12-15 15:04:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:00:11.445"), ClipVidTimeEnd = TimeSpan.Parse("00:01:58.885"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-12-15 - Beth Plays With Zach"
                },
                new Clip()
                {
                    ClipId = 124, SourceSegment = 2, SourceId = 4, ClipNumber = 18,
                    ClipTimeStart = DateTime.Parse("1997-12-19 17:55:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-12-19 20:09:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:02:04.591"), ClipVidTimeEnd = TimeSpan.Parse("00:22:48.634"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1997-12-19 - Frost Family Christmas Party"
                },
                new Clip()
                {
                    ClipId = 125, SourceSegment = 2, SourceId = 4, ClipNumber = 19,
                    ClipTimeStart = DateTime.Parse("1997-12-23 11:15:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-12-23 11:21:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:22:55.875"), ClipVidTimeEnd = TimeSpan.Parse("00:26:08.867"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-12-23 - Dennis, Drew, Zachary & Austin Play at Grandma's House"
                },
                new Clip()
                {
                    ClipId = 126, SourceSegment = 2, SourceId = 4, ClipNumber = 20,
                    ClipTimeStart = DateTime.Parse("1997-12-24 18:51:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-12-24 19:57:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:26:08.901"), ClipVidTimeEnd = TimeSpan.Parse("00:36:54.779"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "Christmas Eve, 1997 - Westgate Family Christmas Eve at Grandpa's House"
                },
                new Clip()
                {
                    ClipId = 127, SourceSegment = 2, SourceId = 4, ClipNumber = 21,
                    ClipTimeStart = DateTime.Parse("1997-12-25 08:58:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-12-25 11:08:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:37:15.300"), ClipVidTimeEnd = TimeSpan.Parse("00:52:45.095"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "Christmas Day, 1997 - Westgate Family Christmas at Grandma's House"
                },
                new Clip()
                {
                    ClipId = 128, SourceSegment = 2, SourceId = 4, ClipNumber = 22,
                    ClipTimeStart = DateTime.Parse("1997-12-28 15:42:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-12-28 17:03:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:52:45.128"), ClipVidTimeEnd = TimeSpan.Parse("01:04:32.168"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-12-28 - Zachary's Birthday Party at Grandma's House"
                },
                new Clip()
                {
                    ClipId = 129, SourceSegment = 2, SourceId = 4, ClipNumber = 23,
                    ClipTimeStart = DateTime.Parse("1997-12-30 11:51:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-12-30 12:14:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:04:58.695"), ClipVidTimeEnd = TimeSpan.Parse("01:20:34.630"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-12-30 - Dennis & Beth Play With Drew & Cousins Outside Grandma's House"
                },
                new Clip()
                {
                    ClipId = 130, SourceSegment = 2, SourceId = 4, ClipNumber = 24,
                    ClipTimeStart = DateTime.Parse("1998-01-09 19:05:00.000"),
                    ClipTimeEnd = DateTime.Parse("1998-01-09 20:23:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:20:40.002"), ClipVidTimeEnd = TimeSpan.Parse("01:42:48.796"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1998-01-09 - Zachary's Birthday Party with Frost Family"
                },
                new Clip()
                {
                    ClipId = 131, SourceSegment = 2, SourceId = 4, ClipNumber = 25,
                    ClipTimeStart = DateTime.Parse("1998-02-04 20:18:00.000"),
                    ClipTimeEnd = DateTime.Parse("1998-02-04 20:19:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:42:54.168"), ClipVidTimeEnd = TimeSpan.Parse("01:44:21.355"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1998-02-04 - Dennis & Beth Play With Drew & Zachary"
                },
                new Clip()
                {
                    ClipId = 132, SourceSegment = 2, SourceId = 4, ClipNumber = 26,
                    ClipTimeStart = DateTime.Parse("1998-02-10 16:55:00.000"),
                    ClipTimeEnd = DateTime.Parse("1998-02-10 16:56:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:44:21.389"), ClipVidTimeEnd = TimeSpan.Parse("01:44:52.986"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1998-02-10 - Drew Tells the Rain to Stop"
                },
                new Clip()
                {
                    ClipId = 133, SourceSegment = 2, SourceId = 4, ClipNumber = 27,
                    ClipTimeStart = DateTime.Parse("1998-02-25 19:34:00.000"),
                    ClipTimeEnd = DateTime.Parse("1998-02-25 19:34:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:44:55.623"), ClipVidTimeEnd = TimeSpan.Parse("01:45:08.669"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1998-02-25 - Beth Says Hi to Zachary"
                },
                new Clip()
                {
                    ClipId = 134, SourceSegment = 2, SourceId = 4, ClipNumber = 28,
                    ClipTimeStart = DateTime.Parse("1998-03-07 14:15:00.000"),
                    ClipTimeEnd = DateTime.Parse("1998-03-07 14:19:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:45:13.441"), ClipVidTimeEnd = TimeSpan.Parse("01:47:49.896"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1998-03-07 - Beth Plays With Zachary"
                },
                new Clip()
                {
                    ClipId = 135, SourceSegment = 2, SourceId = 4, ClipNumber = 29,
                    ClipTimeStart = DateTime.Parse("1998-03-14 13:52:00.000"),
                    ClipTimeEnd = DateTime.Parse("1998-03-14 13:53:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:47:54.301"), ClipVidTimeEnd = TimeSpan.Parse("01:48:43.350"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "Dennis Plays With Drew, Zachary & Carly"
                },
                new Clip()
                {
                    ClipId = 136, SourceSegment = 2, SourceId = 4, ClipNumber = 30,
                    ClipTimeStart = DateTime.Parse("1998-03-22 15:16:00.000"),
                    ClipTimeEnd = DateTime.Parse("1998-03-22 15:25:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:48:48.422"), ClipVidTimeEnd = TimeSpan.Parse("01:51:45.065"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1998-03-22 - Dennis & Beth Play With Drew & Zachary On the Playground"
                },
                new Clip()
                {
                    ClipId = 137, SourceSegment = 2, SourceId = 4, ClipNumber = 31,
                    ClipTimeStart = DateTime.Parse("1998-03-30 09:49:00.000"),
                    ClipTimeEnd = DateTime.Parse("1998-03-30 10:01:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:51:50.437"), ClipVidTimeEnd = TimeSpan.Parse("01:52:29.976"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "1998-03-30 - Zachary & Matt Swing Outside"
                },
                new Clip()
                {
                    ClipId = 138, SourceSegment = 2, SourceId = 4, ClipNumber = 32,
                    ClipTimeStart = DateTime.Parse("1998-03-30 11:19:00.000"),
                    ClipTimeEnd = DateTime.Parse("1998-03-30 11:20:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:52:30.010"), ClipVidTimeEnd = TimeSpan.Parse("01:53:15.088"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1998-03-30 - Drew, Zachary & Matt Eat Lunch"
                },
                new Clip()
                {
                    ClipId = 139, SourceSegment = 2, SourceId = 4, ClipNumber = 33,
                    ClipTimeStart = DateTime.Parse("1998-03-31 07:11:00.000"),
                    ClipTimeEnd = DateTime.Parse("1998-03-31 19:33:00.000"),
                    ClipVidTimeStart = TimeSpan.Parse("01:53:15.122"), ClipVidTimeEnd = TimeSpan.Parse("01:57:48.028"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1998-03-31 - Drew, Zachary & Matt Eat Breakfast, Play"
                },
                new Clip()
                {
                    ClipId = 140, SourceSegment = 1, SourceId = 3, ClipNumber = 1,
                    ClipTimeStart = DateTime.Parse("1997-10-18 22:22:00.000"),
                    ClipTimeEnd = DateTime.Parse("1997-10-18 22:22:10.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:00:07.941"), ClipVidTimeEnd = TimeSpan.Parse("00:00:17.851"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-10-18 - Zach Cries While Playing With Dennis"
                },
                new Clip()
                {
                    ClipId = 141, SourceSegment = 1, SourceId = 3, ClipNumber = 2,
                    ClipTimeStart = DateTime.Parse("1997-10-08 16:17:09.000"),
                    ClipTimeEnd = DateTime.Parse("1997-10-08 16:19:03.000"),
                    ClipVidTimeStart = TimeSpan.Parse("00:00:17.917"), ClipVidTimeEnd = TimeSpan.Parse("00:02:11.931"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2,
                    ClipName = "1997-10-08 - Drew & Zach Play While Beth Films Their Room"
                },
                new Clip()
                {
                    ClipId = 142, SourceSegment = 1, SourceId = 19, ClipNumber = 1,
                    ClipTimeStart = DateTime.Parse("1999-02-05 20:59:02.310"),
                    ClipTimeEnd = DateTime.Parse("1999-02-05 21:04:24.727"),
                    ClipVidTimeStart = TimeSpan.Parse("00:00:00.000"), ClipVidTimeEnd = TimeSpan.Parse("00:03:03.617"),
                    ClipReviewerId = 5, ClipCameraOperatorId = 2, ClipName = "Dennis & Beth Play With Drew & Zach"
                }
            );

            modelBuilder.Entity<ClipCollection>().HasData(
                new ClipCollection() {ClipId = 1, CollectionId = 4},
                new ClipCollection() {ClipId = 1, CollectionId = 8},
                new ClipCollection() {ClipId = 1, CollectionId = 9},
                new ClipCollection() {ClipId = 2, CollectionId = 16},
                new ClipCollection() {ClipId = 3, CollectionId = 16},
                new ClipCollection() {ClipId = 5, CollectionId = 16},
                new ClipCollection() {ClipId = 8, CollectionId = 5},
                new ClipCollection() {ClipId = 8, CollectionId = 8},
                new ClipCollection() {ClipId = 8, CollectionId = 9},
                new ClipCollection() {ClipId = 9, CollectionId = 5},
                new ClipCollection() {ClipId = 9, CollectionId = 8},
                new ClipCollection() {ClipId = 9, CollectionId = 9},
                new ClipCollection() {ClipId = 10, CollectionId = 10},
                new ClipCollection() {ClipId = 10, CollectionId = 12},
                new ClipCollection() {ClipId = 10, CollectionId = 14},
                new ClipCollection() {ClipId = 10, CollectionId = 23},
                new ClipCollection() {ClipId = 11, CollectionId = 10},
                new ClipCollection() {ClipId = 11, CollectionId = 12},
                new ClipCollection() {ClipId = 11, CollectionId = 23},
                new ClipCollection() {ClipId = 12, CollectionId = 10},
                new ClipCollection() {ClipId = 12, CollectionId = 12},
                new ClipCollection() {ClipId = 12, CollectionId = 16},
                new ClipCollection() {ClipId = 13, CollectionId = 12},
                new ClipCollection() {ClipId = 13, CollectionId = 23},
                new ClipCollection() {ClipId = 14, CollectionId = 12},
                new ClipCollection() {ClipId = 15, CollectionId = 12},
                new ClipCollection() {ClipId = 15, CollectionId = 14},
                new ClipCollection() {ClipId = 16, CollectionId = 12},
                new ClipCollection() {ClipId = 16, CollectionId = 14},
                new ClipCollection() {ClipId = 16, CollectionId = 23},
                new ClipCollection() {ClipId = 17, CollectionId = 16},
                new ClipCollection() {ClipId = 19, CollectionId = 16},
                new ClipCollection() {ClipId = 20, CollectionId = 8},
                new ClipCollection() {ClipId = 20, CollectionId = 14},
                new ClipCollection() {ClipId = 20, CollectionId = 16},
                new ClipCollection() {ClipId = 21, CollectionId = 14},
                new ClipCollection() {ClipId = 21, CollectionId = 20},
                new ClipCollection() {ClipId = 22, CollectionId = 3},
                new ClipCollection() {ClipId = 24, CollectionId = 16},
                new ClipCollection() {ClipId = 25, CollectionId = 16},
                new ClipCollection() {ClipId = 27, CollectionId = 8},
                new ClipCollection() {ClipId = 27, CollectionId = 9},
                new ClipCollection() {ClipId = 27, CollectionId = 16},
                new ClipCollection() {ClipId = 28, CollectionId = 16},
                new ClipCollection() {ClipId = 29, CollectionId = 16},
                new ClipCollection() {ClipId = 30, CollectionId = 14},
                new ClipCollection() {ClipId = 30, CollectionId = 20},
                new ClipCollection() {ClipId = 31, CollectionId = 16},
                new ClipCollection() {ClipId = 32, CollectionId = 14},
                new ClipCollection() {ClipId = 34, CollectionId = 16},
                new ClipCollection() {ClipId = 35, CollectionId = 10},
                new ClipCollection() {ClipId = 35, CollectionId = 12},
                new ClipCollection() {ClipId = 35, CollectionId = 23},
                new ClipCollection() {ClipId = 36, CollectionId = 2},
                new ClipCollection() {ClipId = 36, CollectionId = 8},
                new ClipCollection() {ClipId = 36, CollectionId = 9},
                new ClipCollection() {ClipId = 37, CollectionId = 16},
                new ClipCollection() {ClipId = 38, CollectionId = 3},
                new ClipCollection() {ClipId = 39, CollectionId = 16},
                new ClipCollection() {ClipId = 40, CollectionId = 14},
                new ClipCollection() {ClipId = 41, CollectionId = 16},
                new ClipCollection() {ClipId = 42, CollectionId = 16},
                new ClipCollection() {ClipId = 43, CollectionId = 16},
                new ClipCollection() {ClipId = 44, CollectionId = 16},
                new ClipCollection() {ClipId = 44, CollectionId = 23},
                new ClipCollection() {ClipId = 45, CollectionId = 4},
                new ClipCollection() {ClipId = 46, CollectionId = 2},
                new ClipCollection() {ClipId = 46, CollectionId = 8},
                new ClipCollection() {ClipId = 46, CollectionId = 9},
                new ClipCollection() {ClipId = 46, CollectionId = 16},
                new ClipCollection() {ClipId = 47, CollectionId = 8},
                new ClipCollection() {ClipId = 47, CollectionId = 9},
                new ClipCollection() {ClipId = 48, CollectionId = 8},
                new ClipCollection() {ClipId = 48, CollectionId = 16},
                new ClipCollection() {ClipId = 49, CollectionId = 2},
                new ClipCollection() {ClipId = 49, CollectionId = 8},
                new ClipCollection() {ClipId = 49, CollectionId = 9},
                new ClipCollection() {ClipId = 49, CollectionId = 16},
                new ClipCollection() {ClipId = 50, CollectionId = 16},
                new ClipCollection() {ClipId = 51, CollectionId = 16},
                new ClipCollection() {ClipId = 52, CollectionId = 11},
                new ClipCollection() {ClipId = 52, CollectionId = 16},
                new ClipCollection() {ClipId = 53, CollectionId = 16},
                new ClipCollection() {ClipId = 54, CollectionId = 16},
                new ClipCollection() {ClipId = 55, CollectionId = 16},
                new ClipCollection() {ClipId = 56, CollectionId = 4},
                new ClipCollection() {ClipId = 57, CollectionId = 4},
                new ClipCollection() {ClipId = 57, CollectionId = 16},
                new ClipCollection() {ClipId = 58, CollectionId = 16},
                new ClipCollection() {ClipId = 59, CollectionId = 16},
                new ClipCollection() {ClipId = 60, CollectionId = 4},
                new ClipCollection() {ClipId = 61, CollectionId = 4},
                new ClipCollection() {ClipId = 61, CollectionId = 16},
                new ClipCollection() {ClipId = 62, CollectionId = 4},
                new ClipCollection() {ClipId = 62, CollectionId = 16},
                new ClipCollection() {ClipId = 63, CollectionId = 4},
                new ClipCollection() {ClipId = 63, CollectionId = 16},
                new ClipCollection() {ClipId = 64, CollectionId = 4},
                new ClipCollection() {ClipId = 64, CollectionId = 8},
                new ClipCollection() {ClipId = 64, CollectionId = 9},
                new ClipCollection() {ClipId = 65, CollectionId = 4},
                new ClipCollection() {ClipId = 66, CollectionId = 4},
                new ClipCollection() {ClipId = 67, CollectionId = 4},
                new ClipCollection() {ClipId = 67, CollectionId = 8},
                new ClipCollection() {ClipId = 67, CollectionId = 9},
                new ClipCollection() {ClipId = 68, CollectionId = 1},
                new ClipCollection() {ClipId = 69, CollectionId = 1},
                new ClipCollection() {ClipId = 69, CollectionId = 16},
                new ClipCollection() {ClipId = 70, CollectionId = 1},
                new ClipCollection() {ClipId = 70, CollectionId = 16},
                new ClipCollection() {ClipId = 70, CollectionId = 20},
                new ClipCollection() {ClipId = 71, CollectionId = 16},
                new ClipCollection() {ClipId = 71, CollectionId = 20},
                new ClipCollection() {ClipId = 72, CollectionId = 1},
                new ClipCollection() {ClipId = 73, CollectionId = 1},
                new ClipCollection() {ClipId = 74, CollectionId = 1},
                new ClipCollection() {ClipId = 74, CollectionId = 16},
                new ClipCollection() {ClipId = 75, CollectionId = 1},
                new ClipCollection() {ClipId = 75, CollectionId = 16},
                new ClipCollection() {ClipId = 76, CollectionId = 1},
                new ClipCollection() {ClipId = 76, CollectionId = 3},
                new ClipCollection() {ClipId = 77, CollectionId = 1},
                new ClipCollection() {ClipId = 78, CollectionId = 16},
                new ClipCollection() {ClipId = 79, CollectionId = 1},
                new ClipCollection() {ClipId = 79, CollectionId = 16},
                new ClipCollection() {ClipId = 79, CollectionId = 20},
                new ClipCollection() {ClipId = 80, CollectionId = 1},
                new ClipCollection() {ClipId = 80, CollectionId = 16},
                new ClipCollection() {ClipId = 81, CollectionId = 1},
                new ClipCollection() {ClipId = 81, CollectionId = 8},
                new ClipCollection() {ClipId = 81, CollectionId = 16},
                new ClipCollection() {ClipId = 82, CollectionId = 8},
                new ClipCollection() {ClipId = 82, CollectionId = 16},
                new ClipCollection() {ClipId = 83, CollectionId = 8},
                new ClipCollection() {ClipId = 83, CollectionId = 16},
                new ClipCollection() {ClipId = 84, CollectionId = 1},
                new ClipCollection() {ClipId = 84, CollectionId = 8},
                new ClipCollection() {ClipId = 84, CollectionId = 16},
                new ClipCollection() {ClipId = 84, CollectionId = 20},
                new ClipCollection() {ClipId = 85, CollectionId = 1},
                new ClipCollection() {ClipId = 85, CollectionId = 16},
                new ClipCollection() {ClipId = 86, CollectionId = 1},
                new ClipCollection() {ClipId = 86, CollectionId = 16},
                new ClipCollection() {ClipId = 87, CollectionId = 12},
                new ClipCollection() {ClipId = 87, CollectionId = 14},
                new ClipCollection() {ClipId = 87, CollectionId = 16},
                new ClipCollection() {ClipId = 87, CollectionId = 23},
                new ClipCollection() {ClipId = 88, CollectionId = 7},
                new ClipCollection() {ClipId = 88, CollectionId = 10},
                new ClipCollection() {ClipId = 88, CollectionId = 12},
                new ClipCollection() {ClipId = 88, CollectionId = 14},
                new ClipCollection() {ClipId = 88, CollectionId = 16},
                new ClipCollection() {ClipId = 88, CollectionId = 22},
                new ClipCollection() {ClipId = 88, CollectionId = 23},
                new ClipCollection() {ClipId = 88, CollectionId = 24},
                new ClipCollection() {ClipId = 89, CollectionId = 7},
                new ClipCollection() {ClipId = 89, CollectionId = 10},
                new ClipCollection() {ClipId = 89, CollectionId = 12},
                new ClipCollection() {ClipId = 89, CollectionId = 14},
                new ClipCollection() {ClipId = 89, CollectionId = 23},
                new ClipCollection() {ClipId = 89, CollectionId = 24},
                new ClipCollection() {ClipId = 90, CollectionId = 16},
                new ClipCollection() {ClipId = 91, CollectionId = 2},
                new ClipCollection() {ClipId = 92, CollectionId = 1},
                new ClipCollection() {ClipId = 92, CollectionId = 16},
                new ClipCollection() {ClipId = 93, CollectionId = 1},
                new ClipCollection() {ClipId = 93, CollectionId = 16},
                new ClipCollection() {ClipId = 94, CollectionId = 1},
                new ClipCollection() {ClipId = 94, CollectionId = 16},
                new ClipCollection() {ClipId = 94, CollectionId = 20},
                new ClipCollection() {ClipId = 95, CollectionId = 1},
                new ClipCollection() {ClipId = 95, CollectionId = 8},
                new ClipCollection() {ClipId = 95, CollectionId = 16},
                new ClipCollection() {ClipId = 95, CollectionId = 20},
                new ClipCollection() {ClipId = 96, CollectionId = 1},
                new ClipCollection() {ClipId = 96, CollectionId = 16},
                new ClipCollection() {ClipId = 97, CollectionId = 14},
                new ClipCollection() {ClipId = 97, CollectionId = 16},
                new ClipCollection() {ClipId = 98, CollectionId = 16},
                new ClipCollection() {ClipId = 99, CollectionId = 1},
                new ClipCollection() {ClipId = 99, CollectionId = 14},
                new ClipCollection() {ClipId = 99, CollectionId = 16},
                new ClipCollection() {ClipId = 100, CollectionId = 1},
                new ClipCollection() {ClipId = 100, CollectionId = 16},
                new ClipCollection() {ClipId = 100, CollectionId = 20},
                new ClipCollection() {ClipId = 101, CollectionId = 14},
                new ClipCollection() {ClipId = 101, CollectionId = 16},
                new ClipCollection() {ClipId = 102, CollectionId = 1},
                new ClipCollection() {ClipId = 103, CollectionId = 8},
                new ClipCollection() {ClipId = 103, CollectionId = 16},
                new ClipCollection() {ClipId = 104, CollectionId = 14},
                new ClipCollection() {ClipId = 104, CollectionId = 16},
                new ClipCollection() {ClipId = 104, CollectionId = 19},
                new ClipCollection() {ClipId = 105, CollectionId = 2},
                new ClipCollection() {ClipId = 105, CollectionId = 9},
                new ClipCollection() {ClipId = 106, CollectionId = 2},
                new ClipCollection() {ClipId = 106, CollectionId = 9},
                new ClipCollection() {ClipId = 107, CollectionId = 16},
                new ClipCollection() {ClipId = 108, CollectionId = 16},
                new ClipCollection() {ClipId = 109, CollectionId = 1},
                new ClipCollection() {ClipId = 109, CollectionId = 16},
                new ClipCollection() {ClipId = 110, CollectionId = 1},
                new ClipCollection() {ClipId = 110, CollectionId = 16},
                new ClipCollection() {ClipId = 111, CollectionId = 1},
                new ClipCollection() {ClipId = 111, CollectionId = 16},
                new ClipCollection() {ClipId = 112, CollectionId = 16},
                new ClipCollection() {ClipId = 112, CollectionId = 21},
                new ClipCollection() {ClipId = 113, CollectionId = 1},
                new ClipCollection() {ClipId = 113, CollectionId = 16},
                new ClipCollection() {ClipId = 114, CollectionId = 16},
                new ClipCollection() {ClipId = 115, CollectionId = 16},
                new ClipCollection() {ClipId = 116, CollectionId = 16},
                new ClipCollection() {ClipId = 117, CollectionId = 2},
                new ClipCollection() {ClipId = 117, CollectionId = 16},
                new ClipCollection() {ClipId = 117, CollectionId = 23},
                new ClipCollection() {ClipId = 118, CollectionId = 8},
                new ClipCollection() {ClipId = 118, CollectionId = 9},
                new ClipCollection() {ClipId = 118, CollectionId = 23},
                new ClipCollection() {ClipId = 119, CollectionId = 4},
                new ClipCollection() {ClipId = 119, CollectionId = 8},
                new ClipCollection() {ClipId = 119, CollectionId = 9},
                new ClipCollection() {ClipId = 119, CollectionId = 16},
                new ClipCollection() {ClipId = 120, CollectionId = 1},
                new ClipCollection() {ClipId = 120, CollectionId = 4},
                new ClipCollection() {ClipId = 120, CollectionId = 16},
                new ClipCollection() {ClipId = 121, CollectionId = 4},
                new ClipCollection() {ClipId = 121, CollectionId = 16},
                new ClipCollection() {ClipId = 122, CollectionId = 4},
                new ClipCollection() {ClipId = 122, CollectionId = 8},
                new ClipCollection() {ClipId = 122, CollectionId = 16},
                new ClipCollection() {ClipId = 123, CollectionId = 1},
                new ClipCollection() {ClipId = 123, CollectionId = 16},
                new ClipCollection() {ClipId = 124, CollectionId = 4},
                new ClipCollection() {ClipId = 124, CollectionId = 8},
                new ClipCollection() {ClipId = 124, CollectionId = 9},
                new ClipCollection() {ClipId = 125, CollectionId = 10},
                new ClipCollection() {ClipId = 125, CollectionId = 12},
                new ClipCollection() {ClipId = 125, CollectionId = 16},
                new ClipCollection() {ClipId = 125, CollectionId = 22},
                new ClipCollection() {ClipId = 125, CollectionId = 23},
                new ClipCollection() {ClipId = 126, CollectionId = 4},
                new ClipCollection() {ClipId = 126, CollectionId = 12},
                new ClipCollection() {ClipId = 126, CollectionId = 16},
                new ClipCollection() {ClipId = 126, CollectionId = 22},
                new ClipCollection() {ClipId = 126, CollectionId = 23},
                new ClipCollection() {ClipId = 126, CollectionId = 24},
                new ClipCollection() {ClipId = 127, CollectionId = 4},
                new ClipCollection() {ClipId = 127, CollectionId = 10},
                new ClipCollection() {ClipId = 127, CollectionId = 12},
                new ClipCollection() {ClipId = 127, CollectionId = 16},
                new ClipCollection() {ClipId = 127, CollectionId = 22},
                new ClipCollection() {ClipId = 127, CollectionId = 23},
                new ClipCollection() {ClipId = 127, CollectionId = 24},
                new ClipCollection() {ClipId = 128, CollectionId = 2},
                new ClipCollection() {ClipId = 128, CollectionId = 10},
                new ClipCollection() {ClipId = 128, CollectionId = 12},
                new ClipCollection() {ClipId = 128, CollectionId = 22},
                new ClipCollection() {ClipId = 128, CollectionId = 23},
                new ClipCollection() {ClipId = 128, CollectionId = 24},
                new ClipCollection() {ClipId = 129, CollectionId = 10},
                new ClipCollection() {ClipId = 129, CollectionId = 12},
                new ClipCollection() {ClipId = 129, CollectionId = 14},
                new ClipCollection() {ClipId = 129, CollectionId = 22},
                new ClipCollection() {ClipId = 129, CollectionId = 23},
                new ClipCollection() {ClipId = 130, CollectionId = 2},
                new ClipCollection() {ClipId = 130, CollectionId = 8},
                new ClipCollection() {ClipId = 130, CollectionId = 9},
                new ClipCollection() {ClipId = 130, CollectionId = 16},
                new ClipCollection() {ClipId = 131, CollectionId = 1},
                new ClipCollection() {ClipId = 131, CollectionId = 16},
                new ClipCollection() {ClipId = 132, CollectionId = 16},
                new ClipCollection() {ClipId = 133, CollectionId = 1},
                new ClipCollection() {ClipId = 133, CollectionId = 16},
                new ClipCollection() {ClipId = 134, CollectionId = 1},
                new ClipCollection() {ClipId = 134, CollectionId = 16},
                new ClipCollection() {ClipId = 135, CollectionId = 8},
                new ClipCollection() {ClipId = 135, CollectionId = 16},
                new ClipCollection() {ClipId = 136, CollectionId = 14},
                new ClipCollection() {ClipId = 136, CollectionId = 20},
                new ClipCollection() {ClipId = 137, CollectionId = 8},
                new ClipCollection() {ClipId = 137, CollectionId = 14},
                new ClipCollection() {ClipId = 137, CollectionId = 20},
                new ClipCollection() {ClipId = 138, CollectionId = 8},
                new ClipCollection() {ClipId = 139, CollectionId = 8},
                new ClipCollection() {ClipId = 139, CollectionId = 16},
                new ClipCollection() {ClipId = 140, CollectionId = 16},
                new ClipCollection() {ClipId = 141, CollectionId = 16}
            );

            modelBuilder.Entity<ClipPerson>().HasData(
                new ClipPerson() {ClipId = 1, PersonId = 2},
                new ClipPerson() {ClipId = 1, PersonId = 5},
                new ClipPerson() {ClipId = 1, PersonId = 6},
                new ClipPerson() {ClipId = 1, PersonId = 7},
                new ClipPerson() {ClipId = 2, PersonId = 2},
                new ClipPerson() {ClipId = 2, PersonId = 4},
                new ClipPerson() {ClipId = 2, PersonId = 5},
                new ClipPerson() {ClipId = 3, PersonId = 2},
                new ClipPerson() {ClipId = 3, PersonId = 4},
                new ClipPerson() {ClipId = 3, PersonId = 5},
                new ClipPerson() {ClipId = 4, PersonId = 2},
                new ClipPerson() {ClipId = 4, PersonId = 4},
                new ClipPerson() {ClipId = 4, PersonId = 5},
                new ClipPerson() {ClipId = 5, PersonId = 2},
                new ClipPerson() {ClipId = 5, PersonId = 4},
                new ClipPerson() {ClipId = 5, PersonId = 5},
                new ClipPerson() {ClipId = 6, PersonId = 2},
                new ClipPerson() {ClipId = 7, PersonId = 2},
                new ClipPerson() {ClipId = 8, PersonId = 2},
                new ClipPerson() {ClipId = 8, PersonId = 3},
                new ClipPerson() {ClipId = 8, PersonId = 4},
                new ClipPerson() {ClipId = 8, PersonId = 5},
                new ClipPerson() {ClipId = 8, PersonId = 6},
                new ClipPerson() {ClipId = 8, PersonId = 7},
                new ClipPerson() {ClipId = 8, PersonId = 8},
                new ClipPerson() {ClipId = 9, PersonId = 2},
                new ClipPerson() {ClipId = 9, PersonId = 3},
                new ClipPerson() {ClipId = 9, PersonId = 4},
                new ClipPerson() {ClipId = 9, PersonId = 6},
                new ClipPerson() {ClipId = 9, PersonId = 7},
                new ClipPerson() {ClipId = 9, PersonId = 8},
                new ClipPerson() {ClipId = 10, PersonId = 2},
                new ClipPerson() {ClipId = 10, PersonId = 4},
                new ClipPerson() {ClipId = 11, PersonId = 2},
                new ClipPerson() {ClipId = 11, PersonId = 9},
                new ClipPerson() {ClipId = 12, PersonId = 2},
                new ClipPerson() {ClipId = 12, PersonId = 4},
                new ClipPerson() {ClipId = 12, PersonId = 5},
                new ClipPerson() {ClipId = 13, PersonId = 2},
                new ClipPerson() {ClipId = 13, PersonId = 4},
                new ClipPerson() {ClipId = 14, PersonId = 2},
                new ClipPerson() {ClipId = 14, PersonId = 4},
                new ClipPerson() {ClipId = 14, PersonId = 5},
                new ClipPerson() {ClipId = 15, PersonId = 2},
                new ClipPerson() {ClipId = 15, PersonId = 4},
                new ClipPerson() {ClipId = 15, PersonId = 5},
                new ClipPerson() {ClipId = 16, PersonId = 2},
                new ClipPerson() {ClipId = 16, PersonId = 4},
                new ClipPerson() {ClipId = 16, PersonId = 5},
                new ClipPerson() {ClipId = 17, PersonId = 2},
                new ClipPerson() {ClipId = 17, PersonId = 4},
                new ClipPerson() {ClipId = 17, PersonId = 5},
                new ClipPerson() {ClipId = 17, PersonId = 6},
                new ClipPerson() {ClipId = 17, PersonId = 7},
                new ClipPerson() {ClipId = 18, PersonId = 2},
                new ClipPerson() {ClipId = 19, PersonId = 4},
                new ClipPerson() {ClipId = 19, PersonId = 5},
                new ClipPerson() {ClipId = 20, PersonId = 2},
                new ClipPerson() {ClipId = 20, PersonId = 5},
                new ClipPerson() {ClipId = 21, PersonId = 2},
                new ClipPerson() {ClipId = 21, PersonId = 4},
                new ClipPerson() {ClipId = 21, PersonId = 5},
                new ClipPerson() {ClipId = 22, PersonId = 2},
                new ClipPerson() {ClipId = 22, PersonId = 5},
                new ClipPerson() {ClipId = 23, PersonId = 2},
                new ClipPerson() {ClipId = 23, PersonId = 5},
                new ClipPerson() {ClipId = 24, PersonId = 2},
                new ClipPerson() {ClipId = 24, PersonId = 4},
                new ClipPerson() {ClipId = 24, PersonId = 5},
                new ClipPerson() {ClipId = 25, PersonId = 2},
                new ClipPerson() {ClipId = 25, PersonId = 4},
                new ClipPerson() {ClipId = 25, PersonId = 5},
                new ClipPerson() {ClipId = 26, PersonId = 2},
                new ClipPerson() {ClipId = 26, PersonId = 4},
                new ClipPerson() {ClipId = 26, PersonId = 5},
                new ClipPerson() {ClipId = 27, PersonId = 2},
                new ClipPerson() {ClipId = 27, PersonId = 4},
                new ClipPerson() {ClipId = 27, PersonId = 5},
                new ClipPerson() {ClipId = 27, PersonId = 6},
                new ClipPerson() {ClipId = 27, PersonId = 7},
                new ClipPerson() {ClipId = 28, PersonId = 2},
                new ClipPerson() {ClipId = 28, PersonId = 4},
                new ClipPerson() {ClipId = 28, PersonId = 5},
                new ClipPerson() {ClipId = 29, PersonId = 2},
                new ClipPerson() {ClipId = 29, PersonId = 5},
                new ClipPerson() {ClipId = 30, PersonId = 2},
                new ClipPerson() {ClipId = 30, PersonId = 5},
                new ClipPerson() {ClipId = 30, PersonId = 6},
                new ClipPerson() {ClipId = 31, PersonId = 2},
                new ClipPerson() {ClipId = 31, PersonId = 4},
                new ClipPerson() {ClipId = 31, PersonId = 5},
                new ClipPerson() {ClipId = 32, PersonId = 2},
                new ClipPerson() {ClipId = 32, PersonId = 4},
                new ClipPerson() {ClipId = 32, PersonId = 5},
                new ClipPerson() {ClipId = 33, PersonId = 2},
                new ClipPerson() {ClipId = 33, PersonId = 4},
                new ClipPerson() {ClipId = 33, PersonId = 5},
                new ClipPerson() {ClipId = 34, PersonId = 2},
                new ClipPerson() {ClipId = 34, PersonId = 4},
                new ClipPerson() {ClipId = 34, PersonId = 5},
                new ClipPerson() {ClipId = 35, PersonId = 2},
                new ClipPerson() {ClipId = 35, PersonId = 4},
                new ClipPerson() {ClipId = 35, PersonId = 5},
                new ClipPerson() {ClipId = 35, PersonId = 9},
                new ClipPerson() {ClipId = 36, PersonId = 2},
                new ClipPerson() {ClipId = 36, PersonId = 3},
                new ClipPerson() {ClipId = 36, PersonId = 4},
                new ClipPerson() {ClipId = 36, PersonId = 5},
                new ClipPerson() {ClipId = 36, PersonId = 6},
                new ClipPerson() {ClipId = 36, PersonId = 7},
                new ClipPerson() {ClipId = 36, PersonId = 8},
                new ClipPerson() {ClipId = 37, PersonId = 2},
                new ClipPerson() {ClipId = 37, PersonId = 4},
                new ClipPerson() {ClipId = 37, PersonId = 5},
                new ClipPerson() {ClipId = 38, PersonId = 2},
                new ClipPerson() {ClipId = 38, PersonId = 5},
                new ClipPerson() {ClipId = 39, PersonId = 2},
                new ClipPerson() {ClipId = 39, PersonId = 5},
                new ClipPerson() {ClipId = 40, PersonId = 2},
                new ClipPerson() {ClipId = 40, PersonId = 4},
                new ClipPerson() {ClipId = 40, PersonId = 5},
                new ClipPerson() {ClipId = 41, PersonId = 2},
                new ClipPerson() {ClipId = 41, PersonId = 5},
                new ClipPerson() {ClipId = 42, PersonId = 2},
                new ClipPerson() {ClipId = 42, PersonId = 4},
                new ClipPerson() {ClipId = 42, PersonId = 5},
                new ClipPerson() {ClipId = 43, PersonId = 2},
                new ClipPerson() {ClipId = 43, PersonId = 5},
                new ClipPerson() {ClipId = 44, PersonId = 2},
                new ClipPerson() {ClipId = 44, PersonId = 4},
                new ClipPerson() {ClipId = 44, PersonId = 5},
                new ClipPerson() {ClipId = 45, PersonId = 2},
                new ClipPerson() {ClipId = 45, PersonId = 4},
                new ClipPerson() {ClipId = 45, PersonId = 5},
                new ClipPerson() {ClipId = 46, PersonId = 2},
                new ClipPerson() {ClipId = 46, PersonId = 3},
                new ClipPerson() {ClipId = 46, PersonId = 4},
                new ClipPerson() {ClipId = 46, PersonId = 5},
                new ClipPerson() {ClipId = 46, PersonId = 6},
                new ClipPerson() {ClipId = 46, PersonId = 7},
                new ClipPerson() {ClipId = 47, PersonId = 2},
                new ClipPerson() {ClipId = 47, PersonId = 5},
                new ClipPerson() {ClipId = 47, PersonId = 7},
                new ClipPerson() {ClipId = 48, PersonId = 2},
                new ClipPerson() {ClipId = 48, PersonId = 5},
                new ClipPerson() {ClipId = 49, PersonId = 2},
                new ClipPerson() {ClipId = 49, PersonId = 3},
                new ClipPerson() {ClipId = 49, PersonId = 4},
                new ClipPerson() {ClipId = 49, PersonId = 5},
                new ClipPerson() {ClipId = 49, PersonId = 6},
                new ClipPerson() {ClipId = 49, PersonId = 7},
                new ClipPerson() {ClipId = 50, PersonId = 2},
                new ClipPerson() {ClipId = 50, PersonId = 4},
                new ClipPerson() {ClipId = 50, PersonId = 5},
                new ClipPerson() {ClipId = 51, PersonId = 2},
                new ClipPerson() {ClipId = 51, PersonId = 4},
                new ClipPerson() {ClipId = 51, PersonId = 5},
                new ClipPerson() {ClipId = 52, PersonId = 2},
                new ClipPerson() {ClipId = 52, PersonId = 4},
                new ClipPerson() {ClipId = 52, PersonId = 5},
                new ClipPerson() {ClipId = 53, PersonId = 2},
                new ClipPerson() {ClipId = 53, PersonId = 5},
                new ClipPerson() {ClipId = 54, PersonId = 2},
                new ClipPerson() {ClipId = 54, PersonId = 4},
                new ClipPerson() {ClipId = 54, PersonId = 5},
                new ClipPerson() {ClipId = 55, PersonId = 2},
                new ClipPerson() {ClipId = 55, PersonId = 5},
                new ClipPerson() {ClipId = 56, PersonId = 2},
                new ClipPerson() {ClipId = 56, PersonId = 4},
                new ClipPerson() {ClipId = 56, PersonId = 5},
                new ClipPerson() {ClipId = 57, PersonId = 2},
                new ClipPerson() {ClipId = 57, PersonId = 4},
                new ClipPerson() {ClipId = 57, PersonId = 5},
                new ClipPerson() {ClipId = 58, PersonId = 2},
                new ClipPerson() {ClipId = 58, PersonId = 4},
                new ClipPerson() {ClipId = 58, PersonId = 5},
                new ClipPerson() {ClipId = 59, PersonId = 2},
                new ClipPerson() {ClipId = 59, PersonId = 5},
                new ClipPerson() {ClipId = 60, PersonId = 2},
                new ClipPerson() {ClipId = 61, PersonId = 2},
                new ClipPerson() {ClipId = 61, PersonId = 4},
                new ClipPerson() {ClipId = 61, PersonId = 5},
                new ClipPerson() {ClipId = 62, PersonId = 2},
                new ClipPerson() {ClipId = 62, PersonId = 4},
                new ClipPerson() {ClipId = 62, PersonId = 5},
                new ClipPerson() {ClipId = 63, PersonId = 2},
                new ClipPerson() {ClipId = 63, PersonId = 4},
                new ClipPerson() {ClipId = 63, PersonId = 5},
                new ClipPerson() {ClipId = 64, PersonId = 2},
                new ClipPerson() {ClipId = 64, PersonId = 3},
                new ClipPerson() {ClipId = 64, PersonId = 4},
                new ClipPerson() {ClipId = 64, PersonId = 5},
                new ClipPerson() {ClipId = 64, PersonId = 6},
                new ClipPerson() {ClipId = 64, PersonId = 7},
                new ClipPerson() {ClipId = 64, PersonId = 8},
                new ClipPerson() {ClipId = 65, PersonId = 2},
                new ClipPerson() {ClipId = 65, PersonId = 4},
                new ClipPerson() {ClipId = 66, PersonId = 2},
                new ClipPerson() {ClipId = 66, PersonId = 4},
                new ClipPerson() {ClipId = 66, PersonId = 5},
                new ClipPerson() {ClipId = 67, PersonId = 2},
                new ClipPerson() {ClipId = 67, PersonId = 3},
                new ClipPerson() {ClipId = 67, PersonId = 4},
                new ClipPerson() {ClipId = 67, PersonId = 5},
                new ClipPerson() {ClipId = 67, PersonId = 6},
                new ClipPerson() {ClipId = 67, PersonId = 7},
                new ClipPerson() {ClipId = 67, PersonId = 8},
                new ClipPerson() {ClipId = 68, PersonId = 2},
                new ClipPerson() {ClipId = 68, PersonId = 4},
                new ClipPerson() {ClipId = 68, PersonId = 5},
                new ClipPerson() {ClipId = 68, PersonId = 11},
                new ClipPerson() {ClipId = 69, PersonId = 2},
                new ClipPerson() {ClipId = 69, PersonId = 4},
                new ClipPerson() {ClipId = 69, PersonId = 5},
                new ClipPerson() {ClipId = 69, PersonId = 11},
                new ClipPerson() {ClipId = 70, PersonId = 2},
                new ClipPerson() {ClipId = 70, PersonId = 11},
                new ClipPerson() {ClipId = 71, PersonId = 2},
                new ClipPerson() {ClipId = 71, PersonId = 4},
                new ClipPerson() {ClipId = 71, PersonId = 5},
                new ClipPerson() {ClipId = 71, PersonId = 11},
                new ClipPerson() {ClipId = 72, PersonId = 2},
                new ClipPerson() {ClipId = 72, PersonId = 11},
                new ClipPerson() {ClipId = 73, PersonId = 2},
                new ClipPerson() {ClipId = 73, PersonId = 11},
                new ClipPerson() {ClipId = 74, PersonId = 2},
                new ClipPerson() {ClipId = 74, PersonId = 11},
                new ClipPerson() {ClipId = 75, PersonId = 2},
                new ClipPerson() {ClipId = 75, PersonId = 11},
                new ClipPerson() {ClipId = 76, PersonId = 2},
                new ClipPerson() {ClipId = 76, PersonId = 4},
                new ClipPerson() {ClipId = 76, PersonId = 5},
                new ClipPerson() {ClipId = 77, PersonId = 2},
                new ClipPerson() {ClipId = 77, PersonId = 4},
                new ClipPerson() {ClipId = 77, PersonId = 6},
                new ClipPerson() {ClipId = 77, PersonId = 7},
                new ClipPerson() {ClipId = 77, PersonId = 11},
                new ClipPerson() {ClipId = 78, PersonId = 2},
                new ClipPerson() {ClipId = 78, PersonId = 4},
                new ClipPerson() {ClipId = 78, PersonId = 5},
                new ClipPerson() {ClipId = 78, PersonId = 11},
                new ClipPerson() {ClipId = 79, PersonId = 2},
                new ClipPerson() {ClipId = 79, PersonId = 11},
                new ClipPerson() {ClipId = 80, PersonId = 4},
                new ClipPerson() {ClipId = 80, PersonId = 11},
                new ClipPerson() {ClipId = 81, PersonId = 2},
                new ClipPerson() {ClipId = 81, PersonId = 5},
                new ClipPerson() {ClipId = 81, PersonId = 11},
                new ClipPerson() {ClipId = 82, PersonId = 2},
                new ClipPerson() {ClipId = 82, PersonId = 5},
                new ClipPerson() {ClipId = 83, PersonId = 2},
                new ClipPerson() {ClipId = 83, PersonId = 5},
                new ClipPerson() {ClipId = 84, PersonId = 2},
                new ClipPerson() {ClipId = 84, PersonId = 5},
                new ClipPerson() {ClipId = 84, PersonId = 11},
                new ClipPerson() {ClipId = 85, PersonId = 2},
                new ClipPerson() {ClipId = 85, PersonId = 4},
                new ClipPerson() {ClipId = 85, PersonId = 5},
                new ClipPerson() {ClipId = 85, PersonId = 11},
                new ClipPerson() {ClipId = 86, PersonId = 2},
                new ClipPerson() {ClipId = 86, PersonId = 4},
                new ClipPerson() {ClipId = 86, PersonId = 11},
                new ClipPerson() {ClipId = 87, PersonId = 1},
                new ClipPerson() {ClipId = 87, PersonId = 2},
                new ClipPerson() {ClipId = 87, PersonId = 4},
                new ClipPerson() {ClipId = 87, PersonId = 5},
                new ClipPerson() {ClipId = 88, PersonId = 2},
                new ClipPerson() {ClipId = 88, PersonId = 4},
                new ClipPerson() {ClipId = 88, PersonId = 5},
                new ClipPerson() {ClipId = 88, PersonId = 9},
                new ClipPerson() {ClipId = 89, PersonId = 2},
                new ClipPerson() {ClipId = 89, PersonId = 4},
                new ClipPerson() {ClipId = 89, PersonId = 5},
                new ClipPerson() {ClipId = 89, PersonId = 9},
                new ClipPerson() {ClipId = 90, PersonId = 2},
                new ClipPerson() {ClipId = 90, PersonId = 4},
                new ClipPerson() {ClipId = 90, PersonId = 5},
                new ClipPerson() {ClipId = 91, PersonId = 2},
                new ClipPerson() {ClipId = 91, PersonId = 3},
                new ClipPerson() {ClipId = 91, PersonId = 5},
                new ClipPerson() {ClipId = 92, PersonId = 2},
                new ClipPerson() {ClipId = 92, PersonId = 5},
                new ClipPerson() {ClipId = 92, PersonId = 11},
                new ClipPerson() {ClipId = 93, PersonId = 2},
                new ClipPerson() {ClipId = 93, PersonId = 11},
                new ClipPerson() {ClipId = 94, PersonId = 2},
                new ClipPerson() {ClipId = 94, PersonId = 5},
                new ClipPerson() {ClipId = 94, PersonId = 11},
                new ClipPerson() {ClipId = 95, PersonId = 2},
                new ClipPerson() {ClipId = 95, PersonId = 5},
                new ClipPerson() {ClipId = 95, PersonId = 11},
                new ClipPerson() {ClipId = 96, PersonId = 2},
                new ClipPerson() {ClipId = 96, PersonId = 11},
                new ClipPerson() {ClipId = 97, PersonId = 2},
                new ClipPerson() {ClipId = 97, PersonId = 4},
                new ClipPerson() {ClipId = 97, PersonId = 5},
                new ClipPerson() {ClipId = 97, PersonId = 11},
                new ClipPerson() {ClipId = 98, PersonId = 2},
                new ClipPerson() {ClipId = 98, PersonId = 4},
                new ClipPerson() {ClipId = 98, PersonId = 5},
                new ClipPerson() {ClipId = 98, PersonId = 11},
                new ClipPerson() {ClipId = 99, PersonId = 2},
                new ClipPerson() {ClipId = 99, PersonId = 4},
                new ClipPerson() {ClipId = 99, PersonId = 5},
                new ClipPerson() {ClipId = 99, PersonId = 11},
                new ClipPerson() {ClipId = 100, PersonId = 2},
                new ClipPerson() {ClipId = 100, PersonId = 11},
                new ClipPerson() {ClipId = 101, PersonId = 2},
                new ClipPerson() {ClipId = 101, PersonId = 4},
                new ClipPerson() {ClipId = 101, PersonId = 5},
                new ClipPerson() {ClipId = 101, PersonId = 11},
                new ClipPerson() {ClipId = 102, PersonId = 2},
                new ClipPerson() {ClipId = 102, PersonId = 4},
                new ClipPerson() {ClipId = 102, PersonId = 5},
                new ClipPerson() {ClipId = 102, PersonId = 11},
                new ClipPerson() {ClipId = 103, PersonId = 2},
                new ClipPerson() {ClipId = 103, PersonId = 5},
                new ClipPerson() {ClipId = 104, PersonId = 2},
                new ClipPerson() {ClipId = 104, PersonId = 4},
                new ClipPerson() {ClipId = 104, PersonId = 5},
                new ClipPerson() {ClipId = 105, PersonId = 2},
                new ClipPerson() {ClipId = 105, PersonId = 4},
                new ClipPerson() {ClipId = 105, PersonId = 5},
                new ClipPerson() {ClipId = 105, PersonId = 6},
                new ClipPerson() {ClipId = 105, PersonId = 7},
                new ClipPerson() {ClipId = 105, PersonId = 8},
                new ClipPerson() {ClipId = 105, PersonId = 11},
                new ClipPerson() {ClipId = 106, PersonId = 2},
                new ClipPerson() {ClipId = 106, PersonId = 4},
                new ClipPerson() {ClipId = 106, PersonId = 5},
                new ClipPerson() {ClipId = 106, PersonId = 6},
                new ClipPerson() {ClipId = 106, PersonId = 7},
                new ClipPerson() {ClipId = 106, PersonId = 8},
                new ClipPerson() {ClipId = 106, PersonId = 11},
                new ClipPerson() {ClipId = 107, PersonId = 2},
                new ClipPerson() {ClipId = 107, PersonId = 5},
                new ClipPerson() {ClipId = 108, PersonId = 2},
                new ClipPerson() {ClipId = 108, PersonId = 5},
                new ClipPerson() {ClipId = 108, PersonId = 11},
                new ClipPerson() {ClipId = 109, PersonId = 2},
                new ClipPerson() {ClipId = 109, PersonId = 4},
                new ClipPerson() {ClipId = 109, PersonId = 5},
                new ClipPerson() {ClipId = 109, PersonId = 11},
                new ClipPerson() {ClipId = 110, PersonId = 2},
                new ClipPerson() {ClipId = 110, PersonId = 4},
                new ClipPerson() {ClipId = 110, PersonId = 11},
                new ClipPerson() {ClipId = 111, PersonId = 2},
                new ClipPerson() {ClipId = 111, PersonId = 4},
                new ClipPerson() {ClipId = 111, PersonId = 5},
                new ClipPerson() {ClipId = 111, PersonId = 11},
                new ClipPerson() {ClipId = 112, PersonId = 2},
                new ClipPerson() {ClipId = 112, PersonId = 5},
                new ClipPerson() {ClipId = 112, PersonId = 11},
                new ClipPerson() {ClipId = 113, PersonId = 2},
                new ClipPerson() {ClipId = 113, PersonId = 5},
                new ClipPerson() {ClipId = 113, PersonId = 11},
                new ClipPerson() {ClipId = 114, PersonId = 2},
                new ClipPerson() {ClipId = 114, PersonId = 4},
                new ClipPerson() {ClipId = 114, PersonId = 5},
                new ClipPerson() {ClipId = 114, PersonId = 6},
                new ClipPerson() {ClipId = 114, PersonId = 7},
                new ClipPerson() {ClipId = 114, PersonId = 11},
                new ClipPerson() {ClipId = 115, PersonId = 2},
                new ClipPerson() {ClipId = 115, PersonId = 4},
                new ClipPerson() {ClipId = 115, PersonId = 5},
                new ClipPerson() {ClipId = 115, PersonId = 11},
                new ClipPerson() {ClipId = 116, PersonId = 4},
                new ClipPerson() {ClipId = 116, PersonId = 5},
                new ClipPerson() {ClipId = 116, PersonId = 11},
                new ClipPerson() {ClipId = 117, PersonId = 2},
                new ClipPerson() {ClipId = 117, PersonId = 5},
                new ClipPerson() {ClipId = 117, PersonId = 11},
                new ClipPerson() {ClipId = 118, PersonId = 2},
                new ClipPerson() {ClipId = 118, PersonId = 4},
                new ClipPerson() {ClipId = 118, PersonId = 5},
                new ClipPerson() {ClipId = 118, PersonId = 6},
                new ClipPerson() {ClipId = 118, PersonId = 7},
                new ClipPerson() {ClipId = 118, PersonId = 8},
                new ClipPerson() {ClipId = 118, PersonId = 11},
                new ClipPerson() {ClipId = 119, PersonId = 2},
                new ClipPerson() {ClipId = 119, PersonId = 4},
                new ClipPerson() {ClipId = 119, PersonId = 5},
                new ClipPerson() {ClipId = 119, PersonId = 6},
                new ClipPerson() {ClipId = 119, PersonId = 7},
                new ClipPerson() {ClipId = 119, PersonId = 8},
                new ClipPerson() {ClipId = 119, PersonId = 11},
                new ClipPerson() {ClipId = 120, PersonId = 2},
                new ClipPerson() {ClipId = 120, PersonId = 4},
                new ClipPerson() {ClipId = 120, PersonId = 5},
                new ClipPerson() {ClipId = 120, PersonId = 11},
                new ClipPerson() {ClipId = 121, PersonId = 2},
                new ClipPerson() {ClipId = 121, PersonId = 5},
                new ClipPerson() {ClipId = 121, PersonId = 11},
                new ClipPerson() {ClipId = 122, PersonId = 2},
                new ClipPerson() {ClipId = 122, PersonId = 5},
                new ClipPerson() {ClipId = 122, PersonId = 11},
                new ClipPerson() {ClipId = 123, PersonId = 2},
                new ClipPerson() {ClipId = 123, PersonId = 11},
                new ClipPerson() {ClipId = 124, PersonId = 2},
                new ClipPerson() {ClipId = 124, PersonId = 3},
                new ClipPerson() {ClipId = 124, PersonId = 4},
                new ClipPerson() {ClipId = 124, PersonId = 5},
                new ClipPerson() {ClipId = 124, PersonId = 6},
                new ClipPerson() {ClipId = 124, PersonId = 7},
                new ClipPerson() {ClipId = 124, PersonId = 8},
                new ClipPerson() {ClipId = 124, PersonId = 11},
                new ClipPerson() {ClipId = 125, PersonId = 2},
                new ClipPerson() {ClipId = 125, PersonId = 4},
                new ClipPerson() {ClipId = 125, PersonId = 5},
                new ClipPerson() {ClipId = 125, PersonId = 11},
                new ClipPerson() {ClipId = 126, PersonId = 1},
                new ClipPerson() {ClipId = 126, PersonId = 2},
                new ClipPerson() {ClipId = 126, PersonId = 4},
                new ClipPerson() {ClipId = 126, PersonId = 5},
                new ClipPerson() {ClipId = 126, PersonId = 10},
                new ClipPerson() {ClipId = 126, PersonId = 11},
                new ClipPerson() {ClipId = 127, PersonId = 2},
                new ClipPerson() {ClipId = 127, PersonId = 4},
                new ClipPerson() {ClipId = 127, PersonId = 5},
                new ClipPerson() {ClipId = 127, PersonId = 9},
                new ClipPerson() {ClipId = 127, PersonId = 11},
                new ClipPerson() {ClipId = 128, PersonId = 2},
                new ClipPerson() {ClipId = 128, PersonId = 4},
                new ClipPerson() {ClipId = 128, PersonId = 5},
                new ClipPerson() {ClipId = 128, PersonId = 9},
                new ClipPerson() {ClipId = 128, PersonId = 11},
                new ClipPerson() {ClipId = 129, PersonId = 2},
                new ClipPerson() {ClipId = 129, PersonId = 4},
                new ClipPerson() {ClipId = 129, PersonId = 5},
                new ClipPerson() {ClipId = 129, PersonId = 9},
                new ClipPerson() {ClipId = 129, PersonId = 11},
                new ClipPerson() {ClipId = 130, PersonId = 2},
                new ClipPerson() {ClipId = 130, PersonId = 3},
                new ClipPerson() {ClipId = 130, PersonId = 4},
                new ClipPerson() {ClipId = 130, PersonId = 5},
                new ClipPerson() {ClipId = 130, PersonId = 6},
                new ClipPerson() {ClipId = 130, PersonId = 7},
                new ClipPerson() {ClipId = 130, PersonId = 8},
                new ClipPerson() {ClipId = 130, PersonId = 11},
                new ClipPerson() {ClipId = 131, PersonId = 2},
                new ClipPerson() {ClipId = 131, PersonId = 4},
                new ClipPerson() {ClipId = 131, PersonId = 5},
                new ClipPerson() {ClipId = 131, PersonId = 11},
                new ClipPerson() {ClipId = 132, PersonId = 2},
                new ClipPerson() {ClipId = 132, PersonId = 5},
                new ClipPerson() {ClipId = 133, PersonId = 2},
                new ClipPerson() {ClipId = 133, PersonId = 11},
                new ClipPerson() {ClipId = 134, PersonId = 2},
                new ClipPerson() {ClipId = 134, PersonId = 11},
                new ClipPerson() {ClipId = 135, PersonId = 2},
                new ClipPerson() {ClipId = 135, PersonId = 4},
                new ClipPerson() {ClipId = 135, PersonId = 5},
                new ClipPerson() {ClipId = 135, PersonId = 11},
                new ClipPerson() {ClipId = 136, PersonId = 2},
                new ClipPerson() {ClipId = 136, PersonId = 4},
                new ClipPerson() {ClipId = 136, PersonId = 5},
                new ClipPerson() {ClipId = 136, PersonId = 11},
                new ClipPerson() {ClipId = 137, PersonId = 2},
                new ClipPerson() {ClipId = 137, PersonId = 11},
                new ClipPerson() {ClipId = 138, PersonId = 2},
                new ClipPerson() {ClipId = 138, PersonId = 5},
                new ClipPerson() {ClipId = 138, PersonId = 11},
                new ClipPerson() {ClipId = 139, PersonId = 2},
                new ClipPerson() {ClipId = 139, PersonId = 4},
                new ClipPerson() {ClipId = 139, PersonId = 5},
                new ClipPerson() {ClipId = 139, PersonId = 11},
                new ClipPerson() {ClipId = 140, PersonId = 2},
                new ClipPerson() {ClipId = 140, PersonId = 4},
                new ClipPerson() {ClipId = 140, PersonId = 11},
                new ClipPerson() {ClipId = 141, PersonId = 2},
                new ClipPerson() {ClipId = 141, PersonId = 5},
                new ClipPerson() {ClipId = 141, PersonId = 11}
            );

#endif
        }
    }
}