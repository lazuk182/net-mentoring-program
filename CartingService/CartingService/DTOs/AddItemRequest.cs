﻿namespace CartingService.API.DTOs
{
    public class AddItemToCartRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
