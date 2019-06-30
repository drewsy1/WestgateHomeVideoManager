using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HomeVideoDB_EFCoreTest.Models
{
    public partial class Source
    {
        public Source()
        {
            Clips = new HashSet<Clip>();
        }

        public int SourceId { get; set; }
        public string SourceLabel { get; set; }
        public DateTime? SourceDateBurned { get; set; }
        public DateTime? SourceDateRipped { get; set; }
        public int? SourceFormatId { get; set; }
        public string SourceFilePath { get; set; }

        public virtual SourceFormat SourceFormat { get; set; }
        public virtual ICollection<Clip> Clips { get; set; }
    }
}
