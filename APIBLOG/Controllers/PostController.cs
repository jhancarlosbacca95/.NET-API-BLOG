using APIBLOG.Models;
using APIBLOG.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace APIBLOG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("todos")]
        public async Task<IActionResult> ObtenerPosts()
        {
            try
            {
                var posts = await _postService.Get();
                if (posts == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(new { Message = "Ok", Response = posts });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servidor", ex);
            }
        }

        [HttpGet("{idPost}")]
        public async Task<IActionResult> ObtenerPorId(int idPost)
        {
            try
            {
                var post = await _postService.GetById(idPost);
                if (post == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(new { Message = "Ok", Response = post });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servidor", ex);
            }
        }
        
        [HttpGet("PorUsuario{idUsuario}")]
        public async Task<IActionResult> ObtenerPorId(Guid idUsuario)
        {
            try
            {
                var posts = await _postService.GetByUser(idUsuario);
                if (posts == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(new { Message = "Ok", Response = posts });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servidor", ex);
            }
        }

        [HttpGet("PorEtiquetas")]
        public async Task<IActionResult> ObtenerPorId([FromQuery] List<int> idEtiquetas)
        {
            try
            {
                var posts = await _postService.GetByEtiquetas(idEtiquetas);
                if (posts == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(new { Message = "Ok", Response = posts });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servidor", ex);
            }
        }

        public class PostConEtiquetassDTO
        {
            public Post Post { get; set; }
            public List<int> EtiquetasIds { get; set; }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GuardarPost([FromBody] PostConEtiquetassDTO postYCategoria)
        {
            try
            {
                var resultado = await _postService.Save(postYCategoria.Post, postYCategoria.EtiquetasIds);
                if (!resultado)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok("Post guardado correctamente");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servidor", ex);
            }
        }
        [Authorize]
        [HttpPut("{idPost}")]
        public async Task<IActionResult> Modificar([FromBody] PostConEtiquetassDTO postConEtiquetasDTO, int idPost)
        {
            try
            {
                var resultado = await _postService.Update(idPost, postConEtiquetasDTO.Post, postConEtiquetasDTO.EtiquetasIds);
                if (!resultado)
                {
                    return NotFound("No se encontro el Post a eliminar");
                }
                else
                {
                    return Ok("Post modificado correctamente");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servidor", ex);
            }
        }
        [Authorize]
        [HttpDelete("{idPost}")]
        public async Task<IActionResult> EliminarPost(int idPost)
        {
            try
            {
                var resultado = await _postService.Delete(idPost);
                if (!resultado)
                {
                    return NotFound("No se encontro el post a eliminar");
                }
                else
                {
                    return Ok("Post eliminado");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servidor", ex);
            }
        }


    }
}
