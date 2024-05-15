using System.Text.Json.Serialization;

namespace APIBLOG.Models
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion {  get; set; }
        public DateTime FechaCreacion { get; set; }

        [JsonIgnore]
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    }
}
