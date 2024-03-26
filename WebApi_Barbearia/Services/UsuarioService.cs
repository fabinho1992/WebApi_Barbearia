using Identity.Dtos;
using Identity.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi_Barbearia.Services
{
    public class UsuarioService
    {

        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _singManager;

        private TokenService _tokenService;


        public UsuarioService(UserManager<Usuario> userManager, SignInManager<Usuario> singManager, TokenService tokenService)
        {
            _userManager = userManager;
            _singManager = singManager;

            _tokenService = tokenService;
        }

        public async Task Cadastro(UsuarioDto usuarioDto)
        {
            var user = new Usuario
            {
                UserName = usuarioDto.Email,
                Email = usuarioDto.Email

            };

            IdentityResult resultado = await _userManager.CreateAsync(user, usuarioDto.Password);

            if (!resultado.Succeeded)
            {
                throw new ApplicationException("[ERRO] Usuário não cadastrado!");
            }
        }


        public async Task<UsuarioToken> Login(UsuarioDto usuarioDto)
        {
            var resultado = await _singManager.PasswordSignInAsync(usuarioDto.Email, usuarioDto.Password, false, false);
            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Email ou Senha Inválidos!");
            }


            var token = _tokenService.GeraToken(usuarioDto);
            return token;

        }


    }
}
