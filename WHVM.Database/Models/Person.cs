using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WHVM.Database.Models
{
    public partial class Person
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Clip> ClipsAsCameraOperator { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Clip> ClipsAsReviewer { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<ClipPerson> ClipPersons { get; set; }
    }
}
