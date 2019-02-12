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
    
    public partial class PRODUCTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCTO()
        {
            this.CALIFICACION = new HashSet<CALIFICACION>();
            this.CANTIDAD_PRODUCTO = new HashSet<CANTIDAD_PRODUCTO>();
            this.COLOR = new HashSet<COLOR>();
            this.FOTO_PRODUCTO = new HashSet<FOTO_PRODUCTO>();
            this.INGRESO_PRODUCTO = new HashSet<INGRESO_PRODUCTO>();
            this.PRODUCTO_FACTURA = new HashSet<PRODUCTO_FACTURA>();
            this.RECLAMO = new HashSet<RECLAMO>();
            this.TAMANO = new HashSet<TAMANO>();
            this.VISITA_PRODUCTO = new HashSet<VISITA_PRODUCTO>();
            this.LISTA_DESEOS = new HashSet<LISTA_DESEOS>();
            this.CATEGORIA_PRODUCTO = new HashSet<CATEGORIA_PRODUCTO>();
        }
    
        public decimal ID_PRODUCTO { get; set; }
        public string NOMBRE_PRODUCTO { get; set; }
        public string DESCRIPCION_LARGA { get; set; }
        public string DESCRIPCION_CORTA { get; set; }
        public Nullable<decimal> PRECIO_VENTA { get; set; }
        public Nullable<decimal> PRECIO_OFERTA { get; set; }
        public Nullable<bool> ESTADO_OFERTA { get; set; }
        public Nullable<System.DateTime> FECHA_PUBLICACION { get; set; }
        public Nullable<int> ESTADO_PUBLICACION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CALIFICACION> CALIFICACION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CANTIDAD_PRODUCTO> CANTIDAD_PRODUCTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COLOR> COLOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FOTO_PRODUCTO> FOTO_PRODUCTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INGRESO_PRODUCTO> INGRESO_PRODUCTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCTO_FACTURA> PRODUCTO_FACTURA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RECLAMO> RECLAMO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAMANO> TAMANO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VISITA_PRODUCTO> VISITA_PRODUCTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LISTA_DESEOS> LISTA_DESEOS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CATEGORIA_PRODUCTO> CATEGORIA_PRODUCTO { get; set; }
    }
}
