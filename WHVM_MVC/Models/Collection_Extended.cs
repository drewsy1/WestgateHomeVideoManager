using System.Collections.Generic;

namespace WHVM_MVC.Models
{
    public partial class Collection
    {
        public static ICollection<Collection> AllCollections { get; set; }
    }
}