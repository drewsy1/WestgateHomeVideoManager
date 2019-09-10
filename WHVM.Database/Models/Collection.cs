using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WHVM.Database.Models
{
    public partial class Collection
    {
        public int CollectionId { get; set; }
        public string CollectionName { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<ClipCollection> ClipCollections { get; set; }

    }
}
