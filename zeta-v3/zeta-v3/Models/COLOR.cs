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
    
    public partial class COLOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public COLOR()
        {
            this.INGRESO_PRODUCTO = new HashSet<INGRESO_PRODUCTO>();
        }
    
        public decimal ID_COLOR { get; set; }
        public decimal ID_PRODUCTO { get; set; }
        public string NOMBRE_COLOR { get; set; }
        public string LINK_FOTO_COLOR { get; set; }
    
        public virtual PRODUCTO PRODUCTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INGRESO_PRODUCTO> INGRESO_PRODUCTO { get; set; }
    }
}