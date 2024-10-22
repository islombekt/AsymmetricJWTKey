using AuthService.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthService.API.Controllers
{
    [Route("api/identity/token")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(LoginUser user)
        {
            var resp = new LoginResponse();
            if (user.userName == "admin" && user.password == "admin")
            {
                resp.IsSuccess = true;
                resp.Token = GenerateJwtToken();
                resp.userName = user.userName;
            }

            return Ok(resp);
        }

        private string GenerateJwtToken()
        {
            // Load the private key from XML file
            string privateKeyXml = System.IO.File.ReadAllText("./Keys/PrivateKey.xml");

            using (var rsa = RSA.Create())
            {
                rsa.FromXmlString(privateKeyXml); // Import private key

                // Define signing credentials using RSA private key
                var signingCredentials = new SigningCredentials(
                    new RsaSecurityKey(rsa),
                    SecurityAlgorithms.RsaSha512Signature // Use RS512 algorithm
                );

                // Create token descriptor with claims
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new[]
                    {
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, "admin"),
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, "admin@ung.uz")
                }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = signingCredentials
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
    }
}
