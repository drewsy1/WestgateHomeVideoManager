using System.Collections.Generic;

namespace WHVM_MVC.Models
{
    public partial class TagsCollections
    {
        public virtual IList<Clip_TagsCollections> Clip_TagsCollections { get; set; }
    }
}