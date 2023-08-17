

namespace FrontEnd.Models
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; } = "English";
        public int ProducerId { get; set; }
        public int DirectorId { get; set; }
    }
}
