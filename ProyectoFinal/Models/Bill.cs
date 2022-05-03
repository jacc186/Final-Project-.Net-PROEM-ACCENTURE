using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer Required")]
        [Column(TypeName = "int")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Item Required")]
        [Column(TypeName = "int")]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "HowMany Required")]
        [Column(TypeName = "int")]
        public int HowMany { get; set; }

        public Item Item { get; set; }

        public  Customer Costumer { get; set; }

        public DateTime DatePurchase { get; set; }
    }
}
