using System.ComponentModel.DataAnnotations;

namespace BackeryShopDomain.Classes.Entities
{
    public class BackeryDto
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Naziv pekare")]
        public string Name { get; set; }

        [Range(1, 3)]
        [Display(Name = "Broj smena")]
        public int NumberOfShifts { get; set; }

        [Required]
        [Display(Name = "Cenovnik")]
        public int PriceListId { get; set; }
    }
}
