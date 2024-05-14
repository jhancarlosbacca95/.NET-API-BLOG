using APIBLOG.Models;

namespace APIBLOG.Services.Interfaces
{
    public interface IPostService
    {
        public Task<ICollection<Post>> Get();
        public Task<Post> GetById(int id);
        public Task<ICollection<Post>> GetByCategoria(List<int> idsCat);
        public Task<bool> Save(Post post, List<int> CategoriasIds);
        public Task<bool> Update(int id, Post post, List<int> CategoriasIds);
        public Task<bool> Delete(int id);
    }
}
