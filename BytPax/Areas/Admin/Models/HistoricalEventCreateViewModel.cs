using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace BytPax.Areas.Admin.Models
{
    public class HistoricalEventCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва події обов'язкова")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Опис події обов'язковий")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Дата події обов'язкова")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Оберіть вид спорту")]
        public int SportId { get; set; }
        
        [Range(1, 5, ErrorMessage = "Рівень важливості має бути від 1 до 5")]
        public int ImportanceLevel { get; set; }
        
        public IFormFile? ImageFile { get; set; }
        public string? ImagePath { get; set; }
        public int CategoryId { get; set; } 
    }
}