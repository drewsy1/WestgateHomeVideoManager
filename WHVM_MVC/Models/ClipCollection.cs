namespace WHVM_MVC.Models
{
    public partial class ClipCollection
    {
        public int ClipId { get; set; }
        public virtual Clip Clip { get; set; }

        public int CollectionsId { get; set; }
        public virtual Collection Collection { get; set; }
    }
}