using FinalProject.Models;
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

        public string CustomerName { get; set; }

        public virtual IList<BillDetail> BillDetails { get; set; }

        public float Total { get; set; }

        public DateTime DatePurchase { get; set; }

        public bool State { get; set; }
    }
}
