namespace WHVM_MVC.Models
{
    public partial class Clip_TagsPeople
    {
        public int ClipId { get; set; }
        public virtual Clip Clip { get; set; }

        public int PeopleId { get; set; }
        public virtual TagsPeople Person { get; set; }
    }
}