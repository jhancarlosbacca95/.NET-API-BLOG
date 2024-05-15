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

        public async Task<ICollection<Post>>GetByCategoria(List<int> idsCat)
        {
            try
            {
                //buscar las categorias por sus ids
                var categorias = await _context.Categorias.Where(c => idsCat.Contains(c.IdCategoria)).ToListAsync();

                //obtener los ids de los posts asociados a las categorias
                var postsIds = categorias.SelectMany(c => c.IdPosts.Select(p=>p.IdPost)).Distinct().ToList();

                //obtener los posts con dichas ids
                var posts = await _context.Posts
                            .Include(p => p.IdCategoria)
                            .Where(p => p.IdCategoria.Any(c => idsCat.Contains(c.IdCategoria)))
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
        public async Task<bool> Save(Post post,List<int> CategoriasIds) 
        {
            try
            {
                _context.Add(post);

                if (CategoriasIds != null && CategoriasIds.Any())
                {
                    foreach (var categoriaId in CategoriasIds)
                    {
                        var categoria = await _context.Categorias.FindAsync(categoriaId);
                        if (categoria != null) 
                        {
                            post.IdCategoria.Add(categoria);
                            categoria.IdPosts.Add(post);
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

        public async Task<bool> Update(int id,Post post,List<int> CategoriasIds) 
        {
            try
            {
                //buscar el post a modificar
                var postActual = await _context.Posts.FindAsync(id);
                if (postActual == null)
                    return false;
                else
                {
                    foreach (var categoria in postActual.IdCategoria.ToList())
                    {
                        //elimina tanto los posts de la coleccion de categorias,
                        //como la categoria de la coleccion de posts
                        categoria.IdPosts.Remove(postActual);
                        postActual.IdCategoria.Remove(categoria);
                    }

                    //modificar los atributos del post
                    postActual.Titulo = post.Titulo ?? postActual.Titulo;
                    postActual.Activo = post.Activo ?? postActual.Activo;
                    postActual.Contenido = post.Contenido ?? postActual.Contenido;
                    postActual.Imagen = post.Imagen ?? postActual.Imagen;
                    postActual.FechaActualizacion = DateTime.UtcNow;

                    //Actualizar las colecciones de categoria y de posts.
                    foreach (var categoriaId in CategoriasIds)
                    {
                        //buscar y asignar las categorias recibidas al post
                        var categoria = await _context.Categorias.FindAsync(categoriaId);
                        if (categoria!=null)
                        {
                            
                            postActual.IdCategoria.Add(categoria);
                            categoria.IdPosts.Add(postActual);
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
                    // Eliminar el post de la colección de categorías asociadas
                    foreach (var categoria in postAEliminar.IdCategoria)
                    {
                        categoria.IdPosts.Remove(postAEliminar);
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
