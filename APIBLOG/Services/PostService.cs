using APIBLOG.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Data.Common;

namespace APIBLOG.Services
{
    public class PostService
    {
        private readonly ApiblogContext _context;

        public PostService(ApiblogContext contexto)
        {
            _context = contexto;
        }

        public async Task<ICollection<Post>> Get()
        {
            try
            {
                return await _context.Posts.ToListAsync();
            }
            catch (DbException ex)
            {

                throw new Exception($"Ha ocurrido un error en la base de datos, Error: {ex.Message}");
            }
            catch (Exception ex)
            {

                throw new Exception("Ha ocurrido un error",ex);
            }
        }

        public async Task<Post> GetById(int id)
        {
            try
            {
                var postEncontrado = await _context.Posts.FindAsync(id);
                if (postEncontrado == null)
                    return null;
                return postEncontrado;
            }
            catch (DbException ex)
            {
                throw new Exception($"Ocurrio un error con la base de datos, Error: {ex.Message}");
            }catch(Exception ex)
            {
                throw new Exception("Ha ocurrido un error", ex);
            }
        }

        public async Task<bool> Save(Post post) 
        {
            try
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException ex)
            {
                throw new Exception($"No se pudo guardar el post, error en la base de datos, Error:{ex.Message}");
            }catch(Exception ex)
            {
                throw new Exception("Error al intentar guardar el post",ex);
            }
        }

        public async Task<bool> Update(int id,Post post) 
        {
            try
            {
                var postActual = await _context.Posts.FindAsync(id);
                if (postActual == null)
                    return false;
                else
                {
                    postActual.Titulo = post.Titulo ?? postActual.Titulo;
                    postActual.Activo = post.Activo ?? postActual.Activo;
                    postActual.Contenido = post.Contenido ?? postActual.Contenido;
                    postActual.Imagen = post.Imagen ?? postActual.Imagen;
                    postActual.FechaActualizacion = DateTime.UtcNow;
                    _context.Posts.Update(postActual);
                    await _context.SaveChangesAsync();

                    return true;
                }
            }
            catch (DbException ex)
            {
                throw new Exception($"Error en la base de datos, Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el post", ex);
            }
        }

        public async Task<bool>Delete(int id)
        {
            try
            {
                var postAEliminar = await _context.Posts.FindAsync(id);

                if (postAEliminar==null)
                {
                    return false;
                }
                else
                {
                    _context.Posts.Remove(postAEliminar);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (DbException ex)
            {
                throw new Exception($"Error en la base de datos, Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error al intentar eliminar el post",ex);
            }

        }
    }
}
