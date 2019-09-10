using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Castle.Core.Internal;

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
        public virtual SourceFormat SourceFormat { get; set; }
        public virtual IEnumerable<Clip> Clips { get; set; }
        public virtual IEnumerable<File> Files { get; set; }

        [NotMapped]
        public DateTime? SourceDateStart => (DateTime?) Clips.Min(clip => clip.ClipTimeStart);

        [NotMapped]
        public DateTime? SourceDateEnd => (DateTime?)Clips.Max(clip => clip.ClipTimeEnd);

        [NotMapped]
        public IEnumerable<object> SourcePersons => Clips.Where(clip => !clip.Persons.IsNullOrEmpty()).SelectMany(clip => clip.Persons).Distinct();

        [NotMapped]
        public IEnumerable<object> SourceCollections => Clips.Where(clip => !clip.Collections.IsNullOrEmpty()).SelectMany(clip => clip.Collections).Distinct();
    }
}
