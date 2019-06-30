using System;
using System.Collections.Generic;

namespace HomeVideoDB_EFCoreTest.Models
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
    }
}
