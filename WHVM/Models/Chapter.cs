using System;
using System.Collections.Generic;

namespace WHVM.Models
{
    public partial class Chapter
    {
        public Chapter()
        {
            Clip = new HashSet<Clip>();
        }

        public int ChapterId { get; set; }
        public int? ChapterNumber { get; set; }
        public int? SourceId { get; set; }
        public string ChapterLength { get; set; }

        public virtual Source Source { get; set; }
        public virtual ICollection<Clip> Clip { get; set; }
    }
}
