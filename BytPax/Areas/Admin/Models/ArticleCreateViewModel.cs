using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BytPax.Areas.Admin.Models
{
    public class ArticleCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле Теми обов’язкове")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Тема має бути від 5 до 100 символів")]
        public string Topic { get; set; } = "";

        [Required(ErrorMessage = "Поле Тексту обов’язкове")]
        public string BodyText { get; set; } = "";

        [Range(1, int.MaxValue, ErrorMessage = "Оберіть коректну категорію")]
        public int CategoryId { get; set; }

        public IFormFile? ImageFile { get; set; }

        public string? ImagePath { get; set; }
    }
}