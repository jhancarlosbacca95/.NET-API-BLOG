using APIBLOG.Models;

namespace APIBLOG.Services.Interfaces
{
    public interface IComentarioService
    {
        public Task<List<Comentario>> GetbyPost(int idPost);
        public Task<Comentario> GetbyId(int idCom);
        public Task<List<Comentario>> GetbyUser(Guid idUser);
        public Task<bool> Save(Comentario comentario);
        public Task<bool> Update(int idComentario, Comentario comentario);
        public Task<bool> Delete(int idCom);
    }
}
