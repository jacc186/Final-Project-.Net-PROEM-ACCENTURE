using ProyectoFinal.Models;
using System.Collections.Generic;

namespace FinalProject.Models.ViewModels
{
    public class PaymentModelView
    {
        public List<Payment> PaymentsList { get; set; }

        public Payment Payment { get; set; }

        public List<Customer> Customers { get; set; }
    }
}
