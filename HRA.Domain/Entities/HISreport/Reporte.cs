using HRA.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA.Domain.Entities.HISreport
{
    public class Reporte : BaseEntity
    {
        public int ID_ID_REPORTE { get; set; }
        public int I_ID_TIPO_REPORTE { get; set; }
        public string V_NOMBRE { get; set; }
        public string V_URL { get; set; }
        public int I_DIAS { get; set; }
        public string B_CONFIDENCIAL { get; set; }
        public int I_VECES { get; set; }
        public string B_ESTADO { get; set; }


    }
}
