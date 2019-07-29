namespace WHVM.Database.Models
{
    public class FileFileCategory
    {
        public int FileId { get; set; }
        public virtual File File { get; set; }
        public int FileCategoryId { get; set; }
        public virtual FileCategory FileCategory { get; set; }
    }
}