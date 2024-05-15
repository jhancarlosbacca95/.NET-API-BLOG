using APIBLOG.Models;
using APIBLOG.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Data.Common;

namespace APIBLOG.Services
{
    public class ComentarioService:IComentarioService
    {
        private readonly ApiblogContext _context;

        public ComentarioService(ApiblogContext context)
        {
            _context = context;
        }

        public async Task<List<Comentario>> GetbyPost(int idPost)
        {
            try
            {
                    return await _context.Comentarios.Where(c => c.IdPost == idPost).ToListAsync();
            }
            catch (DbException ex)
            {
                throw new Exception($"Error en la base de datos, Error:{ex.Message}");
            }catch (Exception ex)
            {
                throw new Exception("Se ha producido un error", ex);
            }
        }

        public async Task<Comentario> GetbyId(int idCom)
        {
            try
            {
                return await _context.Comentarios.FindAsync(idCom);
            }
            catch (DbException ex)
            {
                throw new Exception($"Error en la base de datos, Error:{ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error", ex);
            }
        }

        public async Task<List<Comentario>> GetbyUser(Guid idUser)
        {
            try
            {
                return await _context.Comentarios.Where(c => c.IdUsuario == idUser).ToListAsync(); ;
            }
            catch (DbException ex)
            {
                throw new Exception($"Error en la base de datos, Error:{ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error", ex);
            }
        }

        public async Task<bool> Save(Comentario comentario)
        {
            try
            {
                _context.Comentarios.Add(comentario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException ex)
            {
                throw new Exception($"Error al intentar guardar el comentario, Error:{ex.Message}");
            }catch(Exception ex)
            {
                throw new Exception("Se ha producido un error");
            }
        }
        public async Task<bool> Update(int idComentario,Comentario comentario)
        {
            try
            {
                var comentarioActual = await _context.Comentarios.FindAsync(idComentario);
                if (comentarioActual==null)
                {
                    return false;
                }
                else
                {
                    comentarioActual.Contenido = comentario.Contenido ?? comentarioActual.Contenido;
                    comentarioActual.Activo = comentario.Activo ?? comentarioActual.Activo;

                    _context.Comentarios.Update(comentarioActual);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (DbException ex)
            {
                throw new Exception($"Error en la base de datos, Error: {ex.Message}");
            }catch (Exception ex)
            {
                throw new Exception("Se ha producido un error", ex);
            }
        }

        public async Task<bool> Delete(int idCom)
        {
            try
            {
                var comentarioEliminar = await _context.Comentarios.FindAsync(idCom);

                if(comentarioEliminar == null)
                {
                    return false;
                }
                else
                {
                    _context.Remove(comentarioEliminar);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (DbException ex)
            {
                throw new Exception($"Error en la base de datos, Error: {ex.Message}");
            }catch  (Exception ex)
            {
                throw new Exception("Se ha producido un error",ex);
            }
        }
    }
}
