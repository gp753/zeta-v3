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
    
    public partial class LOG
    {
        public decimal ID_LOG { get; set; }
        public string ID_USUARIO { get; set; }
        public string ACCION_LOG { get; set; }
    
        public virtual USUARIO USUARIO { get; set; }
    }
}
