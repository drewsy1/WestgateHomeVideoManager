namespace WHVM_MVC.Models
{
    public partial class Clip_TagsCollections
    {
        public int ClipId { get; set; }
        public virtual Clip Clip { get; set; }

        public int CollectionsId { get; set; }
        public virtual TagsCollections Collection { get; set; }
    }
}