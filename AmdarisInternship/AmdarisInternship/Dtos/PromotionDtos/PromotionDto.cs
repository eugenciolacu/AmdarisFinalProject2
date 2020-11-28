using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmdarisInternship.API.Dtos.PromotionDtos
{
    public class PromotionDto
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name length between 1 - 100")]
        public string Name { get; set; }
    }
}
