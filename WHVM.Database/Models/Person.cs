using System;
using System.Collections.Generic;

namespace WHVM.Database.Models
{
    public partial class Person
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public virtual IEnumerable<Clip> ClipsAsCameraOperator { get; set; }
        public virtual IEnumerable<Clip> ClipsAsReviewer { get; set; }
        public virtual IEnumerable<ClipPerson> ClipPersons { get; set; }
    }
}
