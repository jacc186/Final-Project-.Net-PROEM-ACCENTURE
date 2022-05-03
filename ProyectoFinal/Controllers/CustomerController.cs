using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class CustomerController : Controller
    {
        public CustomerController(ProjectContext context)
        {
            this.context = context;
        }

        public ProjectContext context { get; set; }
        public IActionResult Index(int id, string fullname, string email)
        {
            if((id == 0) && (fullname != null) && (email != null))
            {
                context.Add(new Customer { Name = fullname, Email = email, Active = true });
                context.SaveChanges();
            }
            var modelList = new CustomersListViewModel();
            modelList.Customers = context.Customers.ToList();
            return View(modelList);
        }
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            customer.Active = true;
            context.Attach(customer);
            context.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var model = context.Customers.Find(id);
            return View(model);
        }
    }
}
