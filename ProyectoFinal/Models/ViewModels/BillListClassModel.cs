using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class BillListClassModel
    {
        public List<Bill> Bills { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Item> Items { get; set; }
        public Bill Bill { get; set; }
        public BillDetail BillDetail { get; set; }

    }
}
