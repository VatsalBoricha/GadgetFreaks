using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GadgetFreaks.Models
{
  
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public int GadgetId { get; set; }

        [Required]
        [StringLength(50)]
        public string? CategoryName { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        // Navigation property for the one-to-many relationship with Gadgets
        public List<Gadget>? Gadgets { get; set; }

    }

}
