using APIBLOG.Models;
using APIBLOG.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Data.Common;

namespace APIBLOG.Services
{
    public class PostService:IPostService
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

        public async Task<ICollection<Post>> GetbyCategoria(int catId)
        {
            try 
            {
                var posts = await _context.Posts.Where(p => p.IdCategoria == catId).ToListAsync();
                return posts;
            }
            catch (DbException ex)
            {
                throw new Exception($"Ocurrio un error con la base de datos, Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error", ex);
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

        public async Task<ICollection<Post>>GetByEtiquetas(List<int> idsEti)
        {
            try
            {
                //buscar las etiquetas por sus ids
                var etiquetas = await _context.Etiquetas.Where(c => idsEti.Contains(c.IdEtiqueta)).ToListAsync();

                //obtener los ids de los posts asociados a las etiquetas
                var postsIds = etiquetas.SelectMany(c => c.IdPosts.Select(p=>p.IdPost)).Distinct().ToList();

                //obtener los posts con dichas ids
                var posts = await _context.Posts
                            .Include(p => p.IdEtiqueta)
                            .Where(p => p.IdEtiqueta.Any(c => idsEti.Contains(c.IdEtiqueta)))
                            .ToListAsync();
                return posts;
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
        public async Task<bool> Save(Post post,List<int> EtiquetasIds) 
        {
            try
            {
                _context.Add(post);

                if (EtiquetasIds != null && EtiquetasIds.Any())
                {
                    foreach (var etiquetaId in EtiquetasIds)
                    {
                        var etiqueta = await _context.Etiquetas.FindAsync(etiquetaId);
                        if (etiqueta != null) 
                        {
                            post.IdEtiqueta.Add(etiqueta);
                            etiqueta.IdPosts.Add(post);
                        }
                    }
                }
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

        public async Task<bool> Update(int id,Post post,List<int> EtiquetasIds) 
        {
            try
            {
                //buscar el post a modificar
                var postActual = await _context.Posts.FindAsync(id);
                if (postActual == null)
                    return false;
                else
                {
                    foreach (var etiqueta in postActual.IdEtiqueta.ToList())
                    {
                        //elimina tanto los posts de la coleccion de etiquetas,
                        //como la etiqueta de la coleccion de posts
                        etiqueta.IdPosts.Remove(postActual);
                        postActual.IdEtiqueta.Remove(etiqueta);
                    }
                    //modificar los atributos del post
                    postActual.Titulo = post.Titulo ?? postActual.Titulo;
                    postActual.IdCategoria = post.IdCategoria;
                    postActual.Activo = post.Activo ?? postActual.Activo;
                    postActual.Contenido = post.Contenido ?? postActual.Contenido;
                    postActual.Imagen = post.Imagen ?? postActual.Imagen;
                    postActual.FechaActualizacion = DateTime.UtcNow;

                    //Actualizar las colecciones de etiquetas y de posts.
                    foreach (var etiquetaId in EtiquetasIds)
                    {
                        //buscar y asignar las etiquetas recibidas al post
                        var etiqueta = await _context.Etiquetas.FindAsync(etiquetaId);
                        if (etiqueta!=null)
                        {
                            
                            postActual.IdEtiqueta.Add(etiqueta);
                            etiqueta.IdPosts.Add(postActual);
                        }
                    }

                    //guardas los cambios en la base de datos 
                    _context.Posts.Update(postActual);
                    await _context.SaveChangesAsync();

                    return true;
                }
            }
            //captura de excepciones a nivel de base de datos y a nivel de servidor
            catch (DbException ex)
            {
                throw new Exception($"Error en la base de datos, Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el post", ex);
            }
        }

        //Eliminar post
        public async Task<bool>Delete(int id)
        {
            try
            {
                //buscar post a eliminar
                var postAEliminar = await _context.Posts.FindAsync(id);

                if (postAEliminar==null)
                {
                    return false;
                }
                else
                {
                    // Eliminar el post de la colección de etiquetas asociadas
                    foreach (var etiqueta in postAEliminar.IdEtiqueta)
                    {
                        etiqueta.IdPosts.Remove(postAEliminar);
                    }

                    //Eliminar y guardar cambios en caso de encontrar el post
                    _context.Posts.Remove(postAEliminar);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            //captura de excepciones a nivel de base de datos y a nivel de servidor
            catch (DbException ex)
            {
                throw new Exception($"Error en la base de datos, Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error al intentar eliminar el post",ex);
            }

        }

        public async Task<ICollection<Post>> GetByUser(Guid idUser)
        {
            try
            {
                var posts = await _context.Posts.Where(c => c.IdUsuario == idUser).ToListAsync();
                if (posts.Count == 0)
                {
                    return null;
                }
                else
                {
                    return posts;
                }
            }
            catch (DbException ex)
            {
                throw new Exception($"Error en la base de datos, Error: {ex.Message}");
            }catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error", ex);
            }
        }
    }
}
