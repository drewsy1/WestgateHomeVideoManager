using System;
using System.Collections.Generic;
using WHVM.Database.Models;

namespace WHVM_MVC.Models
{
    public partial class Person
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public ICollection<Clip> ClipsAsCameraOperator { get; set; }
        public ICollection<Clip> ClipsAsReviewer { get; set; }
        public ICollection<WHVM.Database.Models.ClipPerson> ClipPersons { get; set; }
    }
}
