using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA.Domain.Entities.HISreport
{
    public class Auditoria
    {
        public int I_ID_AUDITORIA { get; set; }
        public int I_ID_USER { get; set; }
        public int I_ID_REPORTE { get; set; }
        public string V_PC { get; set; }
        public string V_OBSERVACION { get; set; }
        public DateTime D_FECHA { get; set; }
   
    }
}
