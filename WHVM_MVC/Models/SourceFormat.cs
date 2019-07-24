using System;
using System.Collections.Generic;

namespace WHVM_MVC.Models
{
    public partial class SourceFormat
    {
        public int SourceFormatId { get; set; }
        public string SourceFormatName { get; set; }
        public string SourceFormatLogoPath { get; set; }
        public ICollection<Source> Sources { get; set; }
    }
}
