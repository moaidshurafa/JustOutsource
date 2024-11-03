using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace JustOutsource.Models
{
    public class Freelancer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        [Required]

        public string Skills { get; set; }
        [Required]

        [StringLength(1000)]
        public string ProfileDescription { get; set; }

        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
    }
}
