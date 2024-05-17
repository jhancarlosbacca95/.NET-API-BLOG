using APIBLOG.Models;

namespace APIBLOG.Services.Interfaces
{
    public interface IPostService
    {
        public Task<ICollection<Post>> Get();
        public Task<Post> GetById(int id);
        public Task<ICollection<Post>> GetByEtiquetas(List<int> idsEtiquetas);
        public Task<ICollection<Post>> GetByUser(Guid idUser);
        public Task<ICollection<Post>> GetbyCategoria(int catId);
        public Task<bool> Save(Post post, List<int> EtiquetasIds);
        public Task<bool> Update(int id, Post post, List<int> etiquetasIds);
        public Task<bool> Delete(int id);
    }
}
