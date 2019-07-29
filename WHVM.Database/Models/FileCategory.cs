using System.Collections.Generic;

namespace WHVM.Database.Models
{
    public class FileCategory
    {
        public int FileCategoryId { get; set; }
        public string FileCategoryName { get; set; }

        public virtual IEnumerable<FileFileCategory> FileFileCategories { get; set; }
    }
}