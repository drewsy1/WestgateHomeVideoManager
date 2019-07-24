using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WHVM.Database.Models
{
    public partial class Source
    {
        [Key]
        public int SourceId { get; set; }
        public string SourceName { get; set; }
        public DateTime? SourceDateCreated { get; set; }
        public DateTime? SourceDateImported { get; set; }
        public int SourceFormatId { get; set; }
        public string SourceFilePath { get; set; }
        public SourceFormat SourceFormat { get; set; }
        public ICollection<Clip> Clips { get; set; }
    }
}
