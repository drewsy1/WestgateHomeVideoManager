namespace WHVM.Database.Models
{
    public partial class ClipCollection
    {
        public int ClipId { get; set; }
        public virtual Clip Clip { get; set; }
        public int CollectionId { get; set; }
        public virtual Collection Collection { get; set; }
    }
}