using APIBLOG.Models;

namespace APIBLOG.Services.Interfaces
{
    public interface IEtiquetaService
    {
        public Task<ICollection<Etiqueta>>Get();
        public Task<bool> Save(Etiqueta etiqueta);
        public Task<bool> Delete(int id);
        public Task<bool> Update(int id, Etiqueta etiqueta);
    }
}
