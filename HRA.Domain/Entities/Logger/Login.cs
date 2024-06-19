using HRA.Domain.Common;

namespace HRA.Domain.Entities.Logger
{
    public class Login : BaseEntity
    {
        public int? I_ID_LOGIN { get; set; }
        public int? I_ID_USER { get; set; }
        public string? V_LOGIN { get; set; }
        public string? V_ID_ROL { get; set; }
        public string? V_IP { get; set; }
        public int? I_INTENTO { get; set; }
        public DateTime D_FECHA_REGISTRO { get; set; }
    }
}
