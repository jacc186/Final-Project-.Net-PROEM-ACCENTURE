using ProyectoFinal.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class BillDetail
    {
        [Column(TypeName = "int")]
        [Key]
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Amount { get; set; }        
        public Item Item { get; set; }
        public bool State { get; set; }

        public int BillId { get; set; }
        public Bill Bill { get; set; }
    }
}
