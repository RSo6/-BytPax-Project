using System.ComponentModel.DataAnnotations;

namespace BytPax.Areas.Admin.Models;

public class AthleteCreateViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Поле ПІБ обов’язкове")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Поле Країна обов’язкове")]
    public string Country { get; set; }

    [Required(ErrorMessage = "Поле Місто обов’язкове")]
    public string City { get; set; }

    [Range(0, 150, ErrorMessage = "Невірний вік")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Опис обов’язковий")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Оберіть категорію")]
    [Range(1, int.MaxValue, ErrorMessage = "Оберіть категорію")]
    public int? CategoryId { get; set; }


}