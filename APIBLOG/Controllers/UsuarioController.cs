using APIBLOG.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIBLOG.Services;
using APIBLOG.Models;

namespace APIBLOG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var lista = await _usuarioService.Get();
                return Ok(new { message = "Ok", Response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] Usuario us)
        {
            try
            {
                await _usuarioService.Save(us);
                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Usuario us)
        {
            try
            {
                bool Actualizado = await _usuarioService.Update(id, us);
                if (Actualizado==false)
                {
                    return NotFound(new { message="No se encontro ningun usuario con ese Id" });
                }
                else
                {
                    return Ok(new {message = "Usuario Actualizado" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                bool Eliminado =await _usuarioService.Delete(id);
                if (Eliminado == false)
                {
                    return NotFound(new { message = "Usuario no encontrado" });
                }
                else
                {
                    return Ok(new { message = "Usuario eliminado con exito" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

    }
}
