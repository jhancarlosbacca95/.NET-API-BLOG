using APIBLOG.Models;
using APIBLOG.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO.Pipelines;

namespace APIBLOG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly ComentarioService _comentarioService;
        public ComentarioController(ComentarioService comentarioService)
        {
            _comentarioService = comentarioService;
        }
        [Authorize]
        [HttpGet("{idCom}")]
        public async Task<IActionResult> ObtenerComentarioPorId(int idCom)
        {
            try
            {
                var comentario = await _comentarioService.GetbyId(idCom);
                if (comentario == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(comentario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error ", ex);
            }
        }
        [Authorize(Roles ="Admin,SuperAdmin")]
        [HttpGet("ByUser/{idUser}")]
        public async Task<IActionResult> ObtenerComentarioPorUsuario(Guid idUser)
        {
            try
            {
                var comentarios = await _comentarioService.GetbyUser(idUser);
                if (comentarios.Count == 0)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(comentarios);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error ", ex);
            }
        }


        [HttpGet("ByPost/{idPost}")]
        public async Task<IActionResult> ObtenerComentarioPorPost(int idPost)
        {
            try
            {
                var comentarios = await _comentarioService.GetbyPost(idPost);
                if (comentarios.Count == 0)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(comentarios);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error ", ex);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GuardarComentario(Comentario comentario)
        {
            try
            {
                var respuesta = await _comentarioService.Save(comentario);

                if (!respuesta)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok("Comentario guardado");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error ", ex);
            }
        }

        [Authorize]
        [HttpPut("{idCom}")]
        public async Task<IActionResult> ModificarComentario(int idCom, [FromBody] Comentario comentario)
        {
            try
            {
                var respuesta = await _comentarioService.Update(idCom, comentario);
                if (!respuesta)
                {
                    return NotFound("No se encontro el comentario a modificar");
                }
                else
                {
                    return Ok("Modificado Correctamente");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error ", ex);
            }
        }
        [Authorize]
        [HttpDelete("{idCom}")]
        public async Task<IActionResult> EliminarComentario(int idCom)
        {
            try
            {
                var respuesta = await _comentarioService.Delete(idCom);
                if (!respuesta)
                {
                    return NotFound("No se encontro el comentario a eliminar");
                }
                else
                {
                    return Ok("Eliminado Correctamente");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error ", ex);
            }
        } 
    }
}
