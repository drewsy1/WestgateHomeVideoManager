using System.Collections.Generic;

namespace WHVM_MVC.Models
{
    public partial class TagsPeople
    {
        public virtual ICollection<Clip> ClipsAsReviewer { get; set; }
        public virtual ICollection<Clip> ClipsAsCameraOperator { get; set; }
        public virtual IList<Clip_TagsPeople> Clip_TagsPeople { get; set; }
    }
}