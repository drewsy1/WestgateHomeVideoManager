using System;
using System.Collections.Generic;

namespace WHVM_MVC.Models
{
    public partial class TagsPeople
    {
        public TagsPeople()
        {
            ClipsAsCameraOperator = new HashSet<Clip>();
            ClipsAsReviewer = new HashSet<Clip>();
        }

        public int PeopleId { get; set; }
        public string PeopleName { get; set; }

        public virtual ICollection<Clip> ClipsAsCameraOperator { get; set; }
        public virtual ICollection<Clip> ClipsAsReviewer { get; set; }
    }
}
