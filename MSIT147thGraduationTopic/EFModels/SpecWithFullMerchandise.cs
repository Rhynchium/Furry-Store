﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MSIT147thGraduationTopic.EFModels
{
    public partial class SpecWithFullMerchandise
    {
        public int SpecId { get; set; }
        public string SpecName { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public int DiscountPercentage { get; set; }
        public int DisplayOrder { get; set; }
        public bool OnShelf { get; set; }
        public int MerchandiseId { get; set; }
        public string MerchandiseName { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Popularity { get; set; }
    }
}