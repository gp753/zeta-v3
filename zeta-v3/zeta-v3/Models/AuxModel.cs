using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public class caracteristicaproduct
        {
            public decimal ID_PRODUCTO { get; set; }
            public decimal ID_CARACTERISTICA { get; set; }
            public string informacion { get; set; }
        }

        public class productoacarrito
        {
            [Required]
            public decimal ID_PRODUCTO { get; set; }
            public decimal ID_COLOR { get; set; }
            public decimal ID_TAMANO { get; set; }
            [Required]
            public int CANTIDAD { get; set; }
        }

        public class usuariosxeliminar
        {
            public decimal ID_USUARIO { get; set; }

        }

        public class buscar
        {
           
            public class filtro
            {
                public decimal ID_CARACTERISTICA { get; set;}
                public string FILTRO { get; set;}
             }

            public List<filtro> FILTROS { get; set; }

            public string PALABRA_CLAVE { get; set; }
        }

        public class cat_nueva
        {
            public decimal ID_CATEGORIA { get; set; }
            public string NOMBRE_CATEGORIA { get; set; }
            public string DETALLE_cATEGORIA { get; set; }

            public List <string> caracteristicas { get; set; }
        }

       
    }
}