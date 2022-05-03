using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        [Column(TypeName ="nvarchar(100)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public float Price { get; set; }

        [Column(TypeName = "bit")]
        public bool State { get; set; }
    }
}
