using System.Collections.Generic;

namespace WHVM.Database.Models
{
    public class File
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public int? SourceId { get; set; }
        public int? ClipId { get; set; }
        public int FileFormatId { get; set; }
        public string FilePath { get; set; }
        public string FileNotes { get; set; }

        public virtual Source Source { get; set; }
        public virtual Clip Clip { get; set; }
        public virtual FileFormat FileFormat { get; set; }
        public virtual IEnumerable<FileFileCategory> FileFileCategories { get; set; }

    }
}