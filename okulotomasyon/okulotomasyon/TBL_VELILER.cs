//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace okulotomasyon
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_VELILER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_VELILER()
        {
            this.TBL_OGRENCİLER = new HashSet<TBL_OGRENCİLER>();
        }
    
        public int VELIID { get; set; }
        public string VELIANNE { get; set; }
        public string VELİBABA { get; set; }
        public string VELITEL1 { get; set; }
        public string VELİTEL2 { get; set; }
        public string VELIMAIL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_OGRENCİLER> TBL_OGRENCİLER { get; set; }
    }
}
