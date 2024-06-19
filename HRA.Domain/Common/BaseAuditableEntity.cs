namespace HRA.Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public DateTime? DRegistro { get; set; }
        public string? VUsrRegistro { get; set; }
        public DateTime? DModificacion { get; set; }
        public string? VUsrModificacion { get; set; }
    }
}
