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
    
    public partial class CARRITO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CARRITO()
        {
            this.CANTIDAD_PRODUCTO = new HashSet<CANTIDAD_PRODUCTO>();
            this.CHECKOUT1 = new HashSet<CHECKOUT>();
        }
    
        public decimal ID_CARRITO { get; set; }
        public Nullable<decimal> ID_CHECKOUT { get; set; }
        public string ID_USUARIO { get; set; }
        public Nullable<System.DateTime> FECHA_CREACION_CARRITO { get; set; }
        public Nullable<int> TIPO_CARRITO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CANTIDAD_PRODUCTO> CANTIDAD_PRODUCTO { get; set; }
        public virtual CHECKOUT CHECKOUT { get; set; }
        public virtual USUARIO USUARIO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHECKOUT> CHECKOUT1 { get; set; }
    }
}