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
    
    public partial class CATEGORIA_PRODUCTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CATEGORIA_PRODUCTO()
        {
            this.CARACTERISTICAS = new HashSet<CARACTERISTICAS>();
            this.PRODUCTOXCATEGORIA = new HashSet<PRODUCTOXCATEGORIA>();
        }
    
        public decimal ID_CATEGORIA { get; set; }
        public Nullable<decimal> ID_CATEGORIA_SUPERIOR { get; set; }
        public string NOMBRE_CATEGORIA { get; set; }
        public string DETALLE_CATEGORIA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CARACTERISTICAS> CARACTERISTICAS { get; set; }
        public virtual CATEGORIA_SUPERIOR CATEGORIA_SUPERIOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCTOXCATEGORIA> PRODUCTOXCATEGORIA { get; set; }
    }
}
