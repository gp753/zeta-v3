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
    
    public partial class RECLAMO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RECLAMO()
        {
            this.ESTADO_RECLAMO = new HashSet<ESTADO_RECLAMO>();
        }
    
        public decimal ID_RECLAMO { get; set; }
        public decimal ID_USUARIO { get; set; }
        public decimal ID_PRODUCTO { get; set; }
        public string DESCRIPCION_RECLAMO { get; set; }
        public Nullable<System.DateTime> FECHA_RECLAMO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ESTADO_RECLAMO> ESTADO_RECLAMO { get; set; }
        public virtual PRODUCTO PRODUCTO { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}
