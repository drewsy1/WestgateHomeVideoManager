using System.Collections;
using System.Collections.Generic;

namespace WHVM_MVC.Models
{
    public partial class Clip
    {
        public virtual TagsPeople ClipReviewer { get; set; }
        public virtual TagsPeople ClipCameraOperator { get; set; }
        public virtual IList<Clip_TagsCollections> Clip_TagsCollections { get; set; }
        public virtual IList<Clip_TagsPeople> Clip_TagsPeople { get; set; }
    }
}