using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RaterTravel.Common.EntityValidationConstants.BarReviewAndRate;

namespace RatedTravel.Data.DataModels
{
    public class BarReviewAndRate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ReviewTextMaxLength)]
        public string ReviewText { get; set; } = null!;


        public int LocationRate { get; set; }

       
        public int PriceRate { get; set; }

        
        public int ServiceRate { get; set; }

       
        public int MusicRate { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("Bar")]
        public int BarId { get; set; }

        public virtual Bar Bar { get; set; } = null!;
    }
}
