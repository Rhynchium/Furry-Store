﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MSIT147thGraduationTopic.EFModels
{
    public partial class Evaluation
    {
        public int EvaluationId { get; set; }
        public int MerchandiseId { get; set; }
        public int MemberId { get; set; }
        public int OrderId { get; set; }
        public string Comment { get; set; }
        public string MerchandiseName { get; set; }
        public string ImageUrl { get; set; }
        public int Score { get; set; }

        public virtual Member Member { get; set; }
        public virtual Merchandise Merchandise { get; set; }
        public virtual Order Order { get; set; }
    }
}