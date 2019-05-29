using System;
using System.Collections.Generic;

namespace WHVM_MVC.Models
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
        public TimeSpan? ChapterLength { get; set; }
        public string ChapterFilePath { get; set; }

        public virtual Source Source { get; set; }
        public virtual ICollection<Clip> Clip { get; set; }
    }
}
