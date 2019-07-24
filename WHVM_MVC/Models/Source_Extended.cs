using System.Collections.Generic;

namespace WHVM_MVC.Models
{
    public partial class Source
    {
        public View_SourceDates SourceDates { get; set; }
        public static ICollection<Source> AllSources { get; set; }
    }
}