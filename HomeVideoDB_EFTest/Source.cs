//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HomeVideoDB_EFTest
{
    using System;
    using System.Collections.Generic;
    
    public partial class Source
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Source()
        {
            this.Clips = new HashSet<Clip>();
        }
    
        public int SourceID { get; set; }
        public string SourceLabel { get; set; }
        public Nullable<System.DateTime> SourceDateBurned { get; set; }
        public Nullable<System.DateTime> SourceDateRipped { get; set; }
        public Nullable<int> SourceFormatID { get; set; }
        public string SourceFilePath { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Clip> Clips { get; set; }
        public virtual SourceFormat SourceFormat { get; set; }
    }
}