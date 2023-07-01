﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RaterTravel.Common.EntityValidationConstants.RestaurantReviewAndRate;

namespace RatedTravel.Data.DataModels
{
    public class RestaurantReviewAndRate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ReviewTextMaxLength)]
        public string ReviewText { get; set; } = null!;

        public int LocationRate { get; set; }

        public int FoodRate { get; set; }

        public int PriceRate { get; set; }

        public int ServiceRate { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }

        // Navigation property
        public virtual Restaurant Restaurant { get; set; } = null!;
    }
}
