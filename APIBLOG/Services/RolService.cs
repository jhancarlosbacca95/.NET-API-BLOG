using APIBLOG.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace APIBLOG.Services
{
    public class RolService
    {
        private readonly ApiblogContext _context;

        public RolService(ApiblogContext context)
        {
            _context = context;
        }

        public async Task<List<Rol>> Get()
        {
            try
            {
                return await _context.Roles.ToListAsync();
            }
            catch (DbException ex)
            {
                throw new Exception($"Error en la base de datos, Error:{ex.Message}");
            }catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error",ex);
            }
        }

    }
}
