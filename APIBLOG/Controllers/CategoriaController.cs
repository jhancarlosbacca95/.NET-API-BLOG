using APIBLOG.Models;
using APIBLOG.Services;
using APIBLOG.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace APIBLOG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaS) {
            _categoriaService = categoriaS;
        }

        [HttpGet]
        public async Task<IActionResult> Listar() 
        {
            try
            {
                var lista = await _categoriaService.Get();
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

        [HttpPost]
        public async Task<IActionResult> Save([FromBody]Categoria cat) 
        {
            try
            {
                bool respuesta = await _categoriaService.Save(cat);
                if (!respuesta) 
                {
                    return NotFound(new { Message = "No se pudo guardar la categoria" });
                }
                else
                {
                    return Ok(new { Message = "Categoria guardada con exito" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] Categoria cat)
        {
            try
            {
                bool respuesta =await _categoriaService.Update(id, cat);
                if (!respuesta)
                {
                    return NotFound(new { message = "Categoria no encontrada" });
                }
                else
                {
                    return Ok(new { Message = "Categoria modificada correctamente" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool respuesta = await _categoriaService.Delete(id);
                if (!respuesta)
                {
                    return NotFound(new { message = "Categoria no encontrada" });
                }
                else
                {
                    return Ok(new { Message = "Categoria Eliminada correctamente" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
    }
}
