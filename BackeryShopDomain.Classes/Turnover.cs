﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackeryShopDomain.Classes
{
    public class Turnover
    {
        public int Id { get; set; }
        [Display(Name = "Datum")]
        public DateTime Date { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        [Display(Name = "Smena")]
        public int ShiftNo { get; set; }

        [Display(Name = "Pekara")]
        public int BackeryId { get; set; }
        public Backery Backery { get; set; }

        public int LastTurnoverId { get; set; }
        public ICollection<TurnoverDetail> TurnoverDetail { get; set; }
 
    }
}
