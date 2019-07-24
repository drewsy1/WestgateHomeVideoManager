namespace WHVM_MVC.Models
{
    public partial class ClipPerson
    {
        public int ClipId { get; set; }
        public Clip Clip { get; set; }

        public int PeopleId { get; set; }
        public Person Person { get; set; }
    }
}