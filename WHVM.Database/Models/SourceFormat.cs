using System;
using System.Collections.Generic;

namespace WHVM.Database.Models
{
    public partial class SourceFormat
    {
        public int SourceFormatId { get; set; }
        public string SourceFormatName { get; set; }
        public string SourceFormatLogoPath { get; set; }

        public virtual ICollection<Source> Sources { get; set; }
    }
}
