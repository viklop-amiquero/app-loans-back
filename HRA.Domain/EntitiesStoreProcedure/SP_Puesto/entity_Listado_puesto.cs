using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA.Domain.Common;

namespace HRA.Domain.EntitiesStoreProcedure.SP_Puesto
{
    public class entity_Listado_puesto : BaseEntity
    {
        public int I_ID { get; set; }
        public string? V_NOMBRE { get; set; }
        public DateTime? D_FECHA_CREACION { get; set; }
        public string? B_ESTADO { get; set; }
    }
}
