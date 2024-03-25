using Identity.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class TokenService
    {

        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UsuarioToken GeraToken(UsuarioDto usuarioDto)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, usuarioDto.Email),
                new Claim("Barbearia", "UsuarioBarbearia"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) 
            };

            //chave
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:KeyJwt"]));

            //Assinatura digital
            var credenciais = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            //Tempo de Expiração
            var expiracao = _configuration["Configuration: ExpireHours"];
            var tempoExpiracao = DateTime.UtcNow.AddHours(double.Parse(expiracao));

            //Classe que gera o Token
            JwtSecurityToken token = new JwtSecurityToken(

                issuer: _configuration["Configuration: Issuer"],
                audience: _configuration["Configuration: Audience"],
                expires: tempoExpiracao,
                claims: claims,
                signingCredentials: credenciais

            );

            return new UsuarioToken
            {
                Authenticated = true,

                Expiration = tempoExpiracao,

                Token = new JwtSecurityTokenHandler().WriteToken(token),

                Message = "Token Jwt Ok"
            };

        }

    }
}
