﻿namespace MSIT147thGraduationTopic.Models.Dtos
{
    public class CartItemDisplayDto
    {
        public int CartItemId { get; set; }
        public string? CartItemName { get; set; }
        public int CartItemPrice { get; set; }
        public string? MerchandiseImageName { get; set; }
        public int MemberId { get; set; }
        public int SpecId { get; set; }
        public int Quantity { get; set; }
    }
}
