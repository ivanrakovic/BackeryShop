using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackeryShopDomain.Classes
{
    public class Turnover
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int ShiftNo { get; set; }

        public int BackeryId { get; set; }
        public Backery Backery { get; set; }

        public ICollection<TurnoverDetail> TurnoverDetail { get; set; }
 
    }
}
