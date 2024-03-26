using Identity.Dtos;
using Identity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi_Barbearia.Services;

namespace WebApi_Barbearia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizaController : ControllerBase
    {
        
        private readonly UsuarioService _usuarioService;

        public AutorizaController( UsuarioService usuarioService)
        {
            
            _usuarioService = usuarioService;
        }

        [HttpPost("Cadastro Usuario")]
        public async Task<IActionResult> CriaUsuario([FromBody] UsuarioDto usuarioDto)
        {
            //if (string.IsNullOrWhiteSpace(usuarioDto.Email) || string.IsNullOrWhiteSpace(usuarioDto.Password))
            //{
            //    return Ok("Faltam Alguns dados");
            //}

            await _usuarioService.Cadastro(usuarioDto);
            return Ok("Usuario Cadastrado!"); 
        }

        [HttpPost("Login")]
        public async Task<ActionResult> LoginUsuario(UsuarioDto usuarioDto)
        {
            if (string.IsNullOrWhiteSpace(usuarioDto.Email) || string.IsNullOrWhiteSpace(usuarioDto.Password))
            {
                return Ok("Faltam Alguns dados");
            }

            var token = await _usuarioService.Login(usuarioDto);
            return Ok(token);
        }
    }
}
