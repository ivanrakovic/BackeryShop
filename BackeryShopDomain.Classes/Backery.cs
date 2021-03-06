﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackeryShopDomain.Classes
{
	public class Backery
	{
		public int Id { get; set; }

		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
		[Index("IX_BakeryName", IsUnique = true)]
        [Display(Name = "Naziv pekare")]
        public string Name { get; set; }

		[Range(1,3)]
        [Display(Name = "Broj smena")]
        public int NumberOfShifts { get; set; }
		
		public PriceList PriceList { get; set; }

		[Required]
        [Display(Name = "Cenovnik")]
        public int PriceListId { get; set; }

	    public ICollection<Turnover> Turnover { get; set; }
    }
}
