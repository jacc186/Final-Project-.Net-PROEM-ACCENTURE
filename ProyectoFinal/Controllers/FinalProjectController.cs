using FinalProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class FinalProjectController : Controller
    {
        public ProjectContext context;

        public FinalProjectController(ProjectContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            var bills = context.Bills.ToList();
            var billss = bills.GroupBy(b => b.CustomerId).Select(x=> new CustomerRankViewModel {
                CustomerId = x.First().CustomerId,
                CustomerName = x.First().CustomerName,
                MoneySpent = x.Sum(x=> x.Total)
            }).OrderByDescending(x=>x.MoneySpent).ToList();
            return View(billss);
        }
    }
}
