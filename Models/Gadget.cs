using System;
using System.ComponentModel.DataAnnotations;

namespace GadgetFreaks.Models
{

    public class Gadget
    {
        [Key]
        public int GadgetID { get; set; }


        [Required]
        public int CategoryID { get; set; }  // Foreign key

        // Navigation property for the associated Category
        public Category? Category { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public string? Manufacturer { get; set; }

        
    }

}
