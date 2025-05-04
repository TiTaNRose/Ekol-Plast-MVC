using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Ekol_Plast_MVC.Models
{
    public class Posts
    {
        public int Id { get; set; }
        public int? Views { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Subname { get; set; }
        [Required]
        [AllowHtml]
        public string? Text { get; set; }
        public string? Text2 { get; set; }
        public string? Text3 { get; set; }
        [Required]
        public string? ImageURL { get; set; }
        public string? ImageURL2 { get; set; }
        public string? ImageURL3 { get; set; }
        public string? ImageURL4 { get; set; }
        public string? ImageURL5 { get; set; }
    }
}
