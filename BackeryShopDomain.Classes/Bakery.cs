﻿namespace BackeryShopDomain.Classes
{
	public class Bakery
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int NumberOfShifts { get; set; }

		public PriceList PriceList { get; set; }
		public int PriceListId { get; set; }
	}
}
