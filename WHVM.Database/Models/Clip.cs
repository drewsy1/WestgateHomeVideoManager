using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WHVM.Database.Models
{
    public class Clip
    {
        [Key]
        public int ClipId { get; set; }
        public int? SourceSegment { get; set; }
        public int? SourceId { get; set; }
        public int? ClipNumber { get; set; }
        public DateTime? ClipTimeStart { get; set; }
        public DateTime? ClipTimeEnd { get; set; }
        public TimeSpan? ClipVidTimeStart { get; set; }
        public TimeSpan? ClipVidTimeEnd { get; set; }
        public TimeSpan? ClipVidTimeLength => ClipVidTimeEnd - ClipVidTimeStart;
        public int? ClipReviewerId { get; set; }
        public int? ClipCameraOperatorId { get; set; }
        public string ClipName { get; set; }
        public virtual Person ClipCameraOperator { get; set; }
        public virtual Person ClipReviewer { get; set; }
        public virtual Source Source { get; set; }
        public virtual IEnumerable<ClipCollection> ClipCollections { get; set; }
        public virtual IEnumerable<ClipPerson> ClipPersons { get; set; }
        public virtual IEnumerable<File> Files { get; set; }

    }
}
