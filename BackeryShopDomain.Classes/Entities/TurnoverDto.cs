using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackeryShopDomain.Classes.Entities
{
    public class TurnoverDto
    {
        public int Id { get; set; }
        [Display(Name = "Datum")]
        public DateTime Date { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        [Display(Name = "Smena")]
        public int ShiftNo { get; set; }
        [Display(Name = "Pekara")]
        public int BackeryId { get; set; }
        public IEnumerable<TurnoverDetailDto> TurnoverDetails { get; set; }
    }
}