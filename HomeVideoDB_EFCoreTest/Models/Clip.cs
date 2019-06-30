using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeVideoDB_EFCoreTest.Models
{
    public partial class Clip
    {
        public Clip()
        {
            this.TagsCollections = new HashSet<TagsCollections>();
            this.TagsPeoples = new HashSet<TagsPeople>();
        }

        public int ClipId { get; set; }
        public int SourceId { get; set; }
        public int? ClipNumber { get; set; }
        public DateTime? ClipTimeStart { get; set; }
        public DateTime? ClipTimeEnd { get; set; }
        public TimeSpan? ClipVidTimeStart { get; set; }
        public TimeSpan? ClipVidTimeEnd { get; set; }
        public TimeSpan? ClipVidTimeLength { get; set; }
        public string ClipReviewer { get; set; }
        public string CameraOperator { get; set; }
        public string Description { get; set; }
        public string ClipFilePath { get; set; }

        [ForeignKey("SourceId")]
        public virtual Source Source { get; set; }
        public virtual ICollection<TagsCollections> TagsCollections { get; set; }
        public virtual ICollection<TagsPeople> TagsPeoples { get; set; }
    }
}
