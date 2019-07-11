using System.Collections.Generic;

namespace WHVM_MVC.Models
{
    public partial class TagsPeople
    {
        public static IList<TagsPeople> AllPeople { get; set; }
        public virtual IList<Clip_TagsPeople> ClipTagsPeople { get; set; }
    }
}