namespace WestgateHomeVideoManager.Models
{
    public class PersonTag
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public TagsPeople PersonObject { get; set; }
        public bool Checked { get; set; }
    }
}