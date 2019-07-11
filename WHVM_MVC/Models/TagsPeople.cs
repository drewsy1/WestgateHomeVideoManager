using System;
using System.Collections.Generic;

namespace WHVM_MVC.Models
{
    public partial class TagsPeople
    {
        public TagsPeople()
        {
            ClipsAsClipCameraOperator = new HashSet<Clip>();
            ClipsAsClipReviewer = new HashSet<Clip>();
        }

        public int PeopleId { get; set; }
        public string PeopleName { get; set; }

        public virtual ICollection<Clip> ClipsAsClipCameraOperator { get; set; }
        public virtual ICollection<Clip> ClipsAsClipReviewer { get; set; }
    }
}
