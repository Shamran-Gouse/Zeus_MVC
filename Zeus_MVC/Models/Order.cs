using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zeus_MVC.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Total")]
        public decimal TotalBill { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }


        public List<OrderDetail> OrderDetails { get; set; }

        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        UnPaid = 0,
        Paid = 1
    }
}