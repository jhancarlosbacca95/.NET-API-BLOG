using APIBLOG.Models;
using APIBLOG.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace APIBLOG.Services
{
    public class EtiquetaService : IEtiquetaService
    {
        private readonly ApiblogContext _context;

        public EtiquetaService(ApiblogContext contexto) 
        {
            _context = contexto;
        }

        public async Task<ICollection<Etiqueta>> Get()
        {
            try
            {
                return await _context.Etiquetas.ToListAsync();
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

        public async Task<bool> Save(Etiqueta cat)
        {
            try
            {
                _context.Etiquetas.Add(cat);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(DbException ex) 
            {
                throw new Exception(ExepcionDb(ex));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la etiqueta",ex);
            }
        }

        public async Task<bool> Update(int id,Etiqueta cat) 
        {
            try
            {
                var etiActual = await _context.Etiquetas.FindAsync(id);
                if (etiActual == null) 
                {
                    return false;
                }
                else
                {
                    etiActual.Nombre = cat.Nombre ?? etiActual.Nombre;
                    _context.Etiquetas.Update(etiActual);
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
                throw new Exception("Error al modificar la etiqueta", ex);
            }
        }

        public async Task<bool> Delete(int id) 
        {
            try
            {
                var etiActual = await _context.Etiquetas.FindAsync(id);
                if (etiActual == null)
                {
                    return false;
                }
                else 
                { 
                    _context.Etiquetas.Remove(etiActual);
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
                throw new Exception("Error al eliminar la etiqueta",ex);
            }
        }

        //Metodo para capturar y mostrar la exepcion a nivel de base de datos
        public  string ExepcionDb(DbException ex)
        {
            return $"Error en la base de datos Error: {ex.Message}";
        }
    }
}
