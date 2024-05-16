using APIBLOG.Models;

namespace APIBLOG.Services.Interfaces
{
    public interface IUsuarioService
    {
        public Task<ICollection<Usuario>> Get();
        public Task<bool> Save(Usuario us);
        Task<bool> Update(Guid id, Usuario us);
        Task<bool> UpdateRol(Guid id, int idRol);
        public Task<Usuario> GetbyId(Guid id);
        Task<bool> Delete(Guid id);
    }
}
