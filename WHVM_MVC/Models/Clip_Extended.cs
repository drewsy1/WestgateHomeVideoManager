using System.Collections;
using System.Collections.Generic;

namespace WHVM_MVC.Models
{
    public partial class Clip
    {
        public virtual IList<Clip_TagsCollections> ClipTagsCollections { get; set; }
        public virtual IList<Clip_TagsPeople> ClipTagsPeople { get; set; }
    }
}