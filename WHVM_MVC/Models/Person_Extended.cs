using System.Collections.Generic;

namespace WHVM_MVC.Models
{
    public partial class Person
    {
        public static IList<Person> AllPeople { get; set; }
        public virtual IList<ClipPerson> ClipTagsPeople { get; set; }
    }
}