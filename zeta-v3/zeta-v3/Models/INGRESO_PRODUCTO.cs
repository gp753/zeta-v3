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
    
    public partial class INGRESO_PRODUCTO
    {
        public decimal ID_INGRESO_PRODUCTO { get; set; }
        public decimal ID_PRODUCTO { get; set; }
        public string ID_USUARIO { get; set; }
        public Nullable<decimal> ID_COLOR { get; set; }
        public Nullable<decimal> ID_TAMANO { get; set; }
        public Nullable<int> CANTIDAD_INGRESO_PRODUCTO { get; set; }
    
        public virtual COLOR COLOR { get; set; }
        public virtual TAMANO TAMANO { get; set; }
        public virtual PRODUCTO PRODUCTO { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}
