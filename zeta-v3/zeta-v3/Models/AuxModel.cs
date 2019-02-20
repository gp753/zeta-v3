using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zeta_v3.Models
{
    public class AuxModel
    {
        public class tamanosycolores
        {
            public decimal ID_PRODUCTO { get; set; }
            public string NOMBRE_COLOR { get; set; }
            public string NOMBRE_TAMANO { get; set; }
        }

        public class catdeproductos
        {
            public decimal ID_PRODUCTO { get; set; }
            public decimal ID_CATEGORIA { get; set; }
        }

       
    }
}