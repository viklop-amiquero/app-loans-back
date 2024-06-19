using HRA.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA.Domain.EntitiesStoreProcedure
{
    public class entity_Password_login : BaseEntity
    {
        public bool B_STATE { get; set; }
        public int I_IDENTITY { get; set; }
    }
}
