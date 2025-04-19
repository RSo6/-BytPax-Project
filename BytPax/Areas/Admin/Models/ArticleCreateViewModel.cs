using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BytPax.Areas.Admin.Models;
    public class ArticleCreateViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Topic { get; set; }

        [Required]
        public string BodyText { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IFormFile? ImageFile { get; set; }
        public string? ImagePath { get; set; } 
    }