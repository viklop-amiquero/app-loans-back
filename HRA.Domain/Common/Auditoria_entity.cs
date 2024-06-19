namespace HRA.Domain.Common
{
    public class Auditoria_entity : BaseEntity
    {
        public int? I_USUARIO_CREACION { get; set; }
        public DateTime? D_FECHA_CREACION { get; set; }
        public int? I_USUARIO_MODIFICA { get; set; }
        public DateTime? D_FECHA_MODIFICA { get; set; }
    }
}
