namespace WHVM.Database.Models
{
    public partial class ClipPerson
    {
        public int ClipId { get; set; }
        public virtual Clip Clip { get; set; }

        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}