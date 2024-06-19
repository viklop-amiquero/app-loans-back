namespace HRA.Transversal.Interfaces
{
    public interface IAuthenticationJWT
    {
        string Issuer { get; set; }
        string Audience { get; set; }
        string Key { get; set; }
    }
}
