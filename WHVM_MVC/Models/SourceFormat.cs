using System;
using System.Collections.Generic;

namespace WHVM_MVC.Models
{
    public partial class SourceFormat
    {
        public SourceFormat()
        {
            Source = new HashSet<Source>();
        }

        public int SourceFormatId { get; set; }
        public string SourceFormatText { get; set; }
        public string SourceFormatLogoPath { get; set; }

        public virtual ICollection<Source> Source { get; set; }
        public static List<SourceFormat> AllFormats;
    }
}
