using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TokenService
    {
        public string GerarToken(Funcionario user, string Secret)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);

            var token = new SecurityTokenDescriptor()
            {
                Expires = DateTime.Now.AddDays(1),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //Contepudo do payload do token
                    new Claim(ClaimTypes.Email, user.Email ),
                    new Claim(ClaimTypes.Role, user.Role)

                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            return handler.WriteToken(
                 handler.CreateToken(token));

        }
    }
}
