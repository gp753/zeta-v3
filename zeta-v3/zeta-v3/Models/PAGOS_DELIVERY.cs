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
    
    public partial class PAGOS_DELIVERY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PAGOS_DELIVERY()
        {
            this.ESTADO_DELIVERY = new HashSet<ESTADO_DELIVERY>();
        }
    
        public decimal ID_PAGOS_DELIVERY { get; set; }
        public decimal ID_ESTADO_DELIVERY { get; set; }
        public Nullable<System.DateTime> FECHA_PAGO_DELIVERY { get; set; }
        public Nullable<System.DateTime> HORA_PAGO_DELIVERY { get; set; }
        public string FACTURA_PAGO_DELIVERY { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ESTADO_DELIVERY> ESTADO_DELIVERY { get; set; }
        public virtual ESTADO_DELIVERY ESTADO_DELIVERY1 { get; set; }
    }
}
