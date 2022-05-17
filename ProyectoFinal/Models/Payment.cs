using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer Required")]
        [Column(TypeName = "int")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Amount Required")]
        public float Amount { get; set; }
        public Customer Customer { get; set; }

    }
}
