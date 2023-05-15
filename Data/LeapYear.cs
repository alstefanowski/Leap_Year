using System.ComponentModel.DataAnnotations;

namespace Leap_Year.Data
{
    public class LeapYear
    {
        public int ID { get; set; }

        [Display(Name = "Rok", Prompt = "Wpisz Rok")]
        [Required, Range(1899, 2022, ErrorMessage = "Oczekiwana wartość {0} musi być z zakresu od {1} do {2}")]
        public int Year { get; set; }

        [Display(Name = "Imie", AutoGenerateField = false, Prompt = "Wpisz imie(Opcjonalne)")]
        [StringLength(100, ErrorMessage = "Wartosc slowa nie moze przekroczyc {1} liter")]
        public string? Name { get; set; }

        public string? Result { get; set; }
        public DateTime SearchTime { get; set; }
    }
}
