using System;
using System.Collections.Generic;

namespace WHVM.Database.Models
{
    public partial class Person
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public ICollection<Clip> ClipsAsCameraOperator { get; set; }
        public ICollection<Clip> ClipsAsReviewer { get; set; }
        public ICollection<ClipPerson> ClipPersons { get; set; }
    }
}
