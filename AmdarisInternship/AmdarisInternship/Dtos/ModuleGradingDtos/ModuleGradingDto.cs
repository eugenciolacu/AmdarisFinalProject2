using System;
using System.ComponentModel.DataAnnotations;

namespace AmdarisInternship.API.Dtos.ModuleGradingDtos
{
    public class ModuleGradingDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name length between 1 - 100")]
        public string Name { get; set; }

        [Required]
        [Range(typeof(float), "0", "1")]
        public float Weight { get; set; }

        public int ModuleId { get; set; }
    }
}
