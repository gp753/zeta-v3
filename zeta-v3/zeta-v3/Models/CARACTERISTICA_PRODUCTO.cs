//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace zeta_v3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CARACTERISTICA_PRODUCTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CARACTERISTICA_PRODUCTO()
        {
            this.CARACTERISTICAS = new HashSet<CARACTERISTICAS>();
        }
    
        public decimal ID_CAR_PRO { get; set; }
        public Nullable<decimal> ID_PRODUCTO { get; set; }
        public string INFO_CAR { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CARACTERISTICAS> CARACTERISTICAS { get; set; }
        public virtual PRODUCTO PRODUCTO { get; set; }
    }
}
