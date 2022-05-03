using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class ItemController : Controller
    {
        public ItemController(ProjectContext context)
        {
            this.context = context;
        }
        public ProjectContext context { get; set; }
        public IActionResult Index(int id, string name, float price)
        {
            if(id==0 && name != null && price != 0)
            {
                context.Items.Add(new Item { Name = name, Price = price, State = true });
                context.SaveChanges();
            }
            var model = new ItemListViewModel();
            model.Items = context.Items.ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Item item)
        {
            item.State = true;
            context.Attach(item);
            context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var model = context.Items.Find(id);
            return View(model);
        }
    }
}
