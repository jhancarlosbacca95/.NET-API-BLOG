using APIBLOG.Models;
using APIBLOG.Services.Interfaces;
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

        [HttpGet("PorCategorias")]
        public async Task<IActionResult> ObtenerPorId([FromQuery] List<int> idCategorias)
        {
            try
            {
                var posts = await _postService.GetByCategoria(idCategorias);
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

        public class PostConCategoriasDTO
        {
            public Post Post { get; set; }
            public List<int> CategoriasIds { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> GuardarPost([FromBody] PostConCategoriasDTO postYCategoria)
        {
            try
            {
                var resultado = await _postService.Save(postYCategoria.Post, postYCategoria.CategoriasIds);
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

        [HttpPut("{idPost}")]
        public async Task<IActionResult> Modificar([FromBody] PostConCategoriasDTO postConCategoriasDTO, int idPost)
        {
            try
            {
                var resultado = await _postService.Update(idPost, postConCategoriasDTO.Post, postConCategoriasDTO.CategoriasIds);
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
