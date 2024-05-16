using APIBLOG.Models;
using APIBLOG.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace APIBLOG.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApiblogContext _context;

        public UsuarioService(ApiblogContext contexto) {
            _context = contexto;
        }

        public async Task<ICollection<Usuario>> Get()
        {
            try
            {
                return await _context.Usuarios.Include(u=>u.IdRolNavigation).ToListAsync();
            }
            catch (DbException ex)
            {
                throw new Exception($"Error al intentar obtener los datos de la base de datos{ex.Message}");
            }catch(Exception ex)
            {
                throw new Exception("Error",ex);
            }
        }

        public async Task<Usuario>GetbyId(Guid id)
        {
            try
            {

                var usuario = await _context.Usuarios.FindAsync(id);

                if (usuario != null)
                {
                    // Cargar el rol relacionado explícitamente
                    await _context.Entry(usuario).Reference(u => u.IdRolNavigation).LoadAsync();
                }

                return usuario;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Save(Usuario us)
        {
            try
            {
                us.IdRol = 3;
                _context.Add(us);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException ex) 
            {
                throw new Exception($"Error al guardar los datos en la base de datos, Error:{ex.Message}");
            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error al intentar guardar los datos ", ex);
            }
            
        } 

        public async Task<bool> Update(Guid id,Usuario us)
        {
            try
            {
                var usActual = await _context.Usuarios.FindAsync(id);
                if (usActual==null) 
                {
                    return false;
                }
                else
                {
                    usActual.NombreUsuario = us.NombreUsuario ?? usActual.NombreUsuario;
                    usActual.Activo = us.Activo ?? usActual.Activo;
                    usActual.Contraseña = us.Contraseña ?? usActual.Contraseña;
                    usActual.CorreoElectronico = us.CorreoElectronico ?? usActual.CorreoElectronico;
                    _context.Usuarios.Update(usActual);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch(DbUpdateException ex)
            {
                throw new Exception($"Error al modificar el usuario en la base de datos, Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar modificar los datos",ex);
            }
        }

        public async Task<bool>Delete(Guid id)
        {
            try
            {
                var usActual =await _context.Usuarios.FindAsync(id);
                if (usActual==null)
                {
                    return false;
                }
                else
                {
                    _context.Remove(usActual);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (DbException ex)
            {
                throw new Exception($"Ocurrio un error al intentar eliminar el usuario, Error:{ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar eliminar el usuario ", ex);
            }
        }

        public async Task<bool> UpdateRol(Guid idUsuario,int idRol)
        {
            try
            {
                var UsuarioAModificar = await _context.Usuarios.FindAsync(idUsuario);
                if (UsuarioAModificar == null)
                {
                    return false;
                }
                else
                {
                    UsuarioAModificar.IdRol = idRol;
                    _context.Usuarios.Update(UsuarioAModificar);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (DbException ex)
            {
                throw new Exception($"Ocurrio un error al intentar modificar el rol del usuario, Error:{ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar eliminar el rol del usuario ", ex);
            }
        }

    }
}
