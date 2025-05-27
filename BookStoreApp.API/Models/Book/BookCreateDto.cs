using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.Dto
{
    public class BookCreateDto
    {
        [Required(ErrorMessage = "Il titolo è obbligatorio.")]
        [StringLength(200, ErrorMessage = "Il titolo non può superare i 200 caratteri.")]
        public string Title { get; set; } = null!;

        public int? Year { get; set; }

        [Required(ErrorMessage = "L'ISBN è obbligatorio.")]
        [RegularExpression(@"\d{3}-\d{10}", ErrorMessage = "Formato ISBN non valido (es. 978-1234567890).")]
        public string Isbn { get; set; } = null!;

        public string? Summary { get; set; }

       
        public string ImageData { get; set; }
        public string OriginalImageName { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Il prezzo deve essere un valore positivo.")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "L'ID dell'autore è obbligatorio.")]
        public int AuthorId { get; set; }
    }
}
