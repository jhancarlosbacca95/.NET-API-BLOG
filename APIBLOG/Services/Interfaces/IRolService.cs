using APIBLOG.Models;

namespace APIBLOG.Services.Interfaces
{
    public interface IRolService
    {
        public Task<List<Rol>> Get();
    }
}
