using System.Collections.Generic;

namespace WHVM.Database.Models
{
    public class FileFormat
    {
        public int FileFormatId { get; set; }
        public string FileFormatName { get; set; }

        public virtual IEnumerable<File> Files { get; set; }
    }
}