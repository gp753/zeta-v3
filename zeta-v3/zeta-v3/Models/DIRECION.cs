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
    
    public partial class DIRECION
    {
        public decimal ID_DIRECCION { get; set; }
        public decimal ID_CIUDAD { get; set; }
        public string ID_USUARIO { get; set; }
        public string CALLE_1 { get; set; }
        public string CALLE_2 { get; set; }
        public string REFERENCIA { get; set; }
        public Nullable<int> NRO_CASA { get; set; }
        public string G_MAP { get; set; }
    
        public virtual CIUDAD CIUDAD { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}
