﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WHVM_MVC.Models
{
    public partial class Source
    {
        public Source()
        {
            Clips = new HashSet<Clip>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SourceId { get; set; }
        public string SourceLabel { get; set; }
        public DateTime? SourceDateCreated { get; set; }
        public DateTime? SourceDateImported { get; set; }
        public int? SourceFormatId { get; set; }
        public string SourceFilePath { get; set; }

        public virtual SourceFormat SourceFormat { get; set; }
        public virtual ICollection<Clip> Clips { get; set; }
    }
}
