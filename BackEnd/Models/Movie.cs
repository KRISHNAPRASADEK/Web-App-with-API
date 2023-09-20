using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BackEnd.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public int ReleaseYear { get; set; } = 0;
        public int ProducerId { get; set; }
        public int DirectorId { get; set; }
    }
}
