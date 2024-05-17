using APIBLOG.Models;
using APIBLOG.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace APIBLOG.Services
{
    public class CategoriaService:ICategoriaService
    {
        private readonly ApiblogContext _context;

        public CategoriaService(ApiblogContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Categoria>> Get()
        {
            try
            {
                return await _context.Categorias.ToListAsync();
            }
            catch (DbException ex)
            {
                throw new Exception($"Error al obener los datos de la base de datos, Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error",ex);
            }
        }

        public async Task<Categoria> GetbyId(int id)
        {
            try
            {
                var categoria = await _context.Categorias.FindAsync(id);
                if (categoria == null)
                {
                    return null;
                }
                return categoria;
            }
            catch (DbException ex)
            {
                throw new Exception($"Error con la base de datos, Error:{ex.Message}");
            }catch  (Exception ex)
            {
                throw new Exception("Ha ocurrido un error", ex);
            }
        }

        public async Task<bool> Save(Categoria categoria)
        {
            try
            {
                //Comprobamos que no exista una categoria con ese nombre
                var nombreCategoria = categoria.Nombre.ToLower();
                bool categoriaExistente = await _context.Categorias.AnyAsync(e => e.Nombre.ToLower() == nombreCategoria);
                if (categoriaExistente)
                {
                    throw new Exception("El nombre de la categoria ya existe");
                }
                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException ex)
            {
                throw new Exception($"Error con la base de datos, Error:{ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error", ex);
            }
        }

        public async Task<bool> Update(int id, Categoria categoria)
        {
            try
            {
                var catActual = _context.Categorias.Find(id);

                if (catActual == null)
                {
                    return false;
                }
                else 
                {
                    catActual.Nombre = categoria.Nombre ?? catActual.Nombre;
                    catActual.Descripcion = categoria.Descripcion ?? catActual.Descripcion;
                    _context.Categorias.Update(catActual);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (DbException ex)
            {
                throw new Exception($"Error con la base de datos, Error:{ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error", ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var catActual = _context.Categorias.Find(id);

                if (catActual == null)
                {
                    return false;
                }
                else
                {
                    _context.Categorias.Remove(catActual);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (DbException ex)
            {
                throw new Exception($"Error con la base de datos, Error:{ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error", ex);
            }
        }

    }
}
