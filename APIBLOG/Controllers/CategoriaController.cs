using APIBLOG.Models;
using APIBLOG.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIBLOG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService service)
        {
            _categoriaService = service;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            try
            {
                var categorias = await _categoriaService.Get();
                if (categorias == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(new { Message = "Ok", Response = categorias });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error",ex);
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> ConsultarPorId(int id)
        {
            try
            {
                var categoria = await _categoriaService.GetbyId(id);
                if (categoria == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(new { Message = "Ok", Response = categoria });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error", ex);
            }
        }
        [Authorize(Roles ="SuperAdmin,Admin")]
        [HttpPost]
        public async Task<IActionResult>Guardar([FromBody] Categoria cat)
        {
            try
            {
                var respuesta = await _categoriaService.Save(cat);
                if (!respuesta)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok("Categoria se ha guardado correctamente");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error", ex);
            }
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Modificar([FromBody] Categoria cat,int id)
        {
            try
            {
                var respuesta = await _categoriaService.Update(id,cat);
                if (!respuesta)
                {
                    return NotFound("No se encontro la categoria a modificar");
                }
                else
                {
                    return Ok("Categoria se ha modificado correctamente");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error", ex);
            }
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var respuesta = await _categoriaService.Delete(id);
                if (!respuesta)
                {
                    return NotFound("No se encontro la categoria a eliminar");
                }
                else
                {
                    return Ok("Categoria se ha eliminado correctamente");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error", ex);
            }
        }

    }
}
