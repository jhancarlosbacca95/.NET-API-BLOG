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
        public IActionResult Listar()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new { message = "Ok", Response = _usuarioService.Get() });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario us)
        {
            try
            {
                await _usuarioService.Save(us);
                return  Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

    }
}
