﻿using System;
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
        public string ClipFilePath { get; set; }
        public Person ClipCameraOperator { get; set; }
        public Person ClipReviewer { get; set; }
        public Source Source { get; set; }
        public ICollection<ClipCollection> ClipCollections { get; set; }
        public ICollection<ClipPerson> ClipPersons { get; set; }

    }
}
