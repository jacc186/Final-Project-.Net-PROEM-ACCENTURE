using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class BillController : Controller
    {
        public BillController(ProjectContext context)
        {
            this.context = context;
        }

        public ProjectContext context { get; set; }
        public IActionResult Index(int id, int customerId, int itemId, int howMany)
        {
            if ((id == 0) && (customerId != 0) && (itemId == 0) && (howMany == 0))
            {
                context.Add(new Bill { CustomerId = customerId, ItemId = itemId, HowMany = howMany, DatePurchase = DateTime.Now});
                context.SaveChanges();
            }
            var modelList = new BillListClassModel();
            modelList.Bills = context.Bills.ToList();
            modelList.Customers = context.Customers.ToList();
            return View(modelList);
        }
    }
}
