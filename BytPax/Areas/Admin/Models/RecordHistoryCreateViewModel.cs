using System.ComponentModel.DataAnnotations;

namespace BytPax.Areas.Admin.Models;

public class RecordHistoryCreateViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ім'я спортсмена обов'язкове")]
    public string AthleteName { get; set; }

    [Required(ErrorMessage = "Опис рекорду обов'язковий")]
    public string RecordDescription { get; set; }

    [Required(ErrorMessage = "Значення рекорду обов'язкове")]
    public double RecordValue { get; set; }

    [Required(ErrorMessage = "Дата досягнення обов'язкова")]
    [DataType(DataType.Date)]
    public DateTime DateAchieved { get; set; }

    [Required(ErrorMessage = "Вибір спорту обов'язковий")]
    public int SportId { get; set; }
}