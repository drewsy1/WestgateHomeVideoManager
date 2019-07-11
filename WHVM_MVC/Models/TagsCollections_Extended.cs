using System.Collections.Generic;

namespace WHVM_MVC.Models
{
    public partial class TagsCollections
    {
        public static IList<TagsCollections> AllCollections { get; set; }
        public virtual IList<Clip_TagsCollections> ClipTagsCollections { get; set; }
    }
}