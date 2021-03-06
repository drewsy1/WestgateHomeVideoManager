﻿using System;
using System.Collections.Generic;

namespace WHVM.Database.Models
{
    public partial class Collection
    {
        public int CollectionId { get; set; }
        public string CollectionName { get; set; }
        public virtual IEnumerable<ClipCollection> ClipCollections { get; set; }

    }
}
