using APIBLOG.Models;

namespace APIBLOG.Services.Interfaces
{
    public interface IUsuarioService
    {
        public ICollection<Usuario> Get();
        public Task Save(Usuario us);
    }
}
