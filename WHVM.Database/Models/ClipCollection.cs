namespace WHVM.Database.Models
{
    public partial class ClipCollection
    {
        public int ClipId { get; set; }
        public Clip Clip { get; set; }
        public int CollectionId { get; set; }
        public Collection Collection { get; set; }
    }
}