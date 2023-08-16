using Humanizer;

namespace BackEnd.Models
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; } = "English";
        public int ProducerId { get; set; }
    }
}
