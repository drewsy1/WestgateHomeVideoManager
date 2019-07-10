using System;

namespace WHVM_MVC.Models
{
    public partial class View_SourceDates
    {
        public int SourceId { get; set; }
        public DateTime? SourceDateStart { get; set; }
        public DateTime? SourceDateEnd { get; set; }

        public virtual Source Source { get; set; }
    }
}