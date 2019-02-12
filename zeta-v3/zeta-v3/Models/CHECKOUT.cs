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
    
    public partial class CHECKOUT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHECKOUT()
        {
            this.CANCELACION = new HashSet<CANCELACION>();
            this.CARRITO = new HashSet<CARRITO>();
            this.FACTURA_VENTA1 = new HashSet<FACTURA_VENTA>();
            this.RECIBO_BANCARD1 = new HashSet<RECIBO_BANCARD>();
        }
    
        public decimal ID_CHECKOUT { get; set; }
        public decimal ID_CARRITO { get; set; }
        public Nullable<decimal> ID_RECIBO_BANCARD { get; set; }
        public Nullable<decimal> ID_CANCELACION { get; set; }
        public Nullable<decimal> ID_FACTURA { get; set; }
        public Nullable<decimal> ID_ESTADO_DELIVERY { get; set; }
        public Nullable<System.DateTime> FECHA_CHECKOUT { get; set; }
        public Nullable<System.DateTime> HORA_CHECKOUT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CANCELACION> CANCELACION { get; set; }
        public virtual CANCELACION CANCELACION1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CARRITO> CARRITO { get; set; }
        public virtual CARRITO CARRITO1 { get; set; }
        public virtual RECIBO_BANCARD RECIBO_BANCARD { get; set; }
        public virtual ESTADO_DELIVERY ESTADO_DELIVERY { get; set; }
        public virtual FACTURA_VENTA FACTURA_VENTA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FACTURA_VENTA> FACTURA_VENTA1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RECIBO_BANCARD> RECIBO_BANCARD1 { get; set; }
    }
}
