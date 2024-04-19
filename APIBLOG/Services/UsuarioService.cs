using APIBLOG.Models;
using APIBLOG.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace APIBLOG.Services
{
    public class UsuarioService:IUsuarioService
    {
        private readonly ApiblogContext _context;

        public UsuarioService(ApiblogContext contexto) {
            _context = contexto;
        }

        public ICollection<Usuario> Get()
        {
            return _context.Usuarios.ToList();
        }

        public async Task Save(Usuario us)
        {

            _context.Add(us);
            await _context.SaveChangesAsync();
        } 

    }
}
