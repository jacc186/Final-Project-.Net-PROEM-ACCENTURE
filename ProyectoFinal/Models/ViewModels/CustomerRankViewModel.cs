using ProyectoFinal.Models;

namespace FinalProject.Models.ViewModels
{
    public class CustomerRankViewModel
    {
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public float MoneySpent { get; set; }
        public float Payments { get; set; }
    }
}
