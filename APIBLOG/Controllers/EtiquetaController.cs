using APIBLOG.Models;
using APIBLOG.Services;
using APIBLOG.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace APIBLOG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtiquetaController : ControllerBase
    {
        private readonly IEtiquetaService _etiquetaService;
        public EtiquetaController(IEtiquetaService etiquetaService) {
            _etiquetaService = etiquetaService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar() 
        {
            try
            {
                var lista = await _etiquetaService.Get();
                if (lista == null)
                {
                    return NotFound();
                }
                else 
                {
                    return Ok(new { message = "Ok", Response = lista });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Save([FromBody]Etiqueta eti) 
        {
            try
            {
                bool respuesta = await _etiquetaService.Save(eti);
                if (!respuesta) 
                {
                    return NotFound(new { Message = "No se pudo guardar la etiqueta" });
                }
                else
                {
                    return Ok(new { Message = "Etiqueta guardada con exito" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] Etiqueta eti)
        {
            try
            {
                bool respuesta = await _etiquetaService.Update(id, eti);
                if (!respuesta)
                {
                    return NotFound(new { message = "Etiqueta no encontrada" });
                }
                else
                {
                    return Ok(new { Message = "Etiqueta modificada correctamente" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool respuesta = await _etiquetaService.Delete(id);
                if (!respuesta)
                {
                    return NotFound(new { message = "Etiqueta no encontrada" });
                }
                else
                {
                    return Ok(new { Message = "Etiqueta Eliminada correctamente" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
    }
}
