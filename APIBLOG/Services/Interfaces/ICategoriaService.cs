using APIBLOG.Models;

namespace APIBLOG.Services.Interfaces
{
    public interface ICategoriaService
    {
        public Task<ICollection<Categoria>>Get();
        public Task<bool> Save(Categoria categoria);
        public Task<bool> Delete(int id);
        public Task<bool> Update(int id, Categoria categoria);
    }
}
