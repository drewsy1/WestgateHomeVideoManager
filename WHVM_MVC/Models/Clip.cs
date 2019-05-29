using System;
using System.Collections.Generic;

namespace WHVM_MVC.Models
{
    public partial class Clip
    {
        public int ClipId { get; set; }
        public int? ChapterId { get; set; }
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

        public virtual Chapter Chapter { get; set; }
    }
}
