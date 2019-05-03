using System;
using System.ComponentModel.DataAnnotations;

namespace Zeus_MVC.Models
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Please enter Prduct Name.")]
        public string PrductName { get; set; }

        [Display(Name = "Product quantity")]
        [Required(ErrorMessage = "Please enter Prduct Quantity.")]
        public int Quantity { get; set; }

        [Display(Name = "Reorder quantity")]
        [Required(ErrorMessage = "Please enter Prduct ReOrder Level.")]
        public int ReOrder { get; set; }

        [Display(Name = "Unit Price")]
        [Required(ErrorMessage = "Please enter Unit Price.")]
        public int Price { get; set; }

        [Display(Name = "Product Details")]
        [Required(ErrorMessage = "Please enter Prduct Description.")]
        public string Description { get; set; }

        [Display(Name = "Product Image")]
        public string ImageUrl { get; set; }

        public DateTime DateAdded { get; set; }
    }
}