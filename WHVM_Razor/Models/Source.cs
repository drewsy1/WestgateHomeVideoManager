using System;
using System.Collections.Generic;

namespace WHVM_Razor.Models
{
    public partial class Source
    {
        public Source()
        {
            Chapter = new HashSet<Chapter>();
        }

        public int SourceId { get; set; }
        public string SourceLabel { get; set; }
        public DateTime? SourceDateBurned { get; set; }
        public DateTime? SourceDateRipped { get; set; }

        public virtual ICollection<Chapter> Chapter { get; set; }
    }
}
