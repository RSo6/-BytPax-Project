using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BytPax.Areas.Admin.Models;
    public class ArticleCreateViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле Теми обов’язкове")]
        public string Topic { get; set; }

        [Required(ErrorMessage = "Поле Тексу обов’язкове")]
        public string BodyText { get; set; }

        [Required(ErrorMessage = "Вибір категорії обов’язкове")]
        public int CategoryId { get; set; }

        public IFormFile? ImageFile { get; set; }
        public string? ImagePath { get; set; } 
    }