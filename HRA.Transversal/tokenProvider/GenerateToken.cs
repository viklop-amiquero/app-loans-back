using HRA.Transversal.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace HRA.Transversal.tokenProvider
{
    public class GenerateToken
    {
        private readonly IAuthenticationJWT _authenticationJWT;
        public GenerateToken(IAuthenticationJWT jwt)
        {
            _authenticationJWT = jwt;
        }

        public string GenerarToken<T>(List<T> Data)
        {
            var key = Encoding.ASCII.GetBytes(_authenticationJWT.Key);
            var tokenHandler = new JwtSecurityTokenHandler();

            var claimsList = new List<Claim>();

            foreach (T value in Data)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    string propertyName = property.Name;
                    var propertyValue = property.GetValue(value);

                    if (propertyValue != null)
                    {
                        //Add roles as claims if the property is named "Role"
                        if (propertyName.Equals("Role"))
                        {
                            if (propertyValue is string stringValue)
                            {
                                string[] roles = stringValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string role in roles)
                                {
                                    claimsList.Add(new Claim(ClaimTypes.Role, role.Trim()));
                                    //claimsList.Add(new Claim(ClaimTypes.Role, ""));
                                }
                            }
                            else if (propertyValue is IEnumerable<string> enumerableRoles)
                            {
                                foreach (string role in enumerableRoles)
                                {
                                    claimsList.Add(new Claim(ClaimTypes.Role, role));
                                }
                            }
                        }
                        else
                        {
                            claimsList.Add(new Claim(propertyName, propertyValue.ToString()));
                        }
                    }
                }
            }
            claimsList.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claimsList.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString("dd/MM/yyyy")));

            ClaimsIdentity claims = new ClaimsIdentity(claimsList);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Issuer = _authenticationJWT.Issuer,
                Audience = _authenticationJWT.Audience,
                Expires = DateTime.UtcNow.AddDays(40),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(createdToken);
        }
    }
}
