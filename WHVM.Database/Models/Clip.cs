using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

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

        [JsonIgnore]
        public virtual Source Source { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<ClipCollection> ClipCollections { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<ClipPerson> ClipPersons { get; set; }
        public virtual IEnumerable<File> Files { get; set; }

        [NotMapped] public IEnumerable<object> Persons => ClipPersons?.Select(cp => cp.Person);

        [NotMapped] public IEnumerable<object> Collections => ClipCollections?.Select(cc => cc.Collection);

    }
}
