using APIBLOG.Models;
using APIBLOG.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace APIBLOG.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ApiblogContext _context;

        public CategoriaService(ApiblogContext contexto) 
        {
            _context = contexto;
        }

        public async Task<ICollection<Categoria>> Get()
        {
            try
            {
                return await _context.Categorias.ToListAsync();
            }
            catch (DbException ex)
            {
                throw new Exception(ExepcionDb(ex));
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al intentar obtener los datos", ex);
            }
            
        }

        public async Task<bool> Save(Categoria cat)
        {
            try
            {
                _context.Categorias.Add(cat);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(DbException ex) 
            {
                throw new Exception(ExepcionDb(ex));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la categoria",ex);
            }
        }

        public async Task<bool> Update(int id,Categoria cat) 
        {
            try
            {
                var catActual = await _context.Categorias.FindAsync(id);
                if (catActual == null) 
                {
                    return false;
                }
                else
                {
                    catActual.Descripcion = cat.Descripcion ?? catActual.Descripcion;
                    catActual.Nombre = cat.Nombre ?? catActual.Nombre;
                    _context.Categorias.Update(catActual);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (DbException ex)
            {
                throw new Exception(ExepcionDb(ex));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la categoria", ex);
            }
        }

        public async Task<bool> Delete(int id) 
        {
            try
            {
                var catActual = await _context.Categorias.FindAsync(id);
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
                throw new Exception(ExepcionDb(ex));
            }
            catch(Exception ex)
            {
                throw new Exception("Error al eliminar la categoria",ex);
            }
        }

        //Metodo para capturar y mostrar la exepcion a nivel de base de datos
        public  string ExepcionDb(DbException ex)
        {
            return $"Error en la base de datos Error: {ex.Message}";
        }
    }
}
