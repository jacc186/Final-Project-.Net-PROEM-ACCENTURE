using FinalProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using System.Linq;

namespace FinalProject.Controllers
{
    public class PaymentController : Controller
    {
        public PaymentController(ProjectContext context)
        {
            this.context = context;
        }
        public ProjectContext context { get; set; }
        public IActionResult Index(PaymentModelView paymentModelView)
       {
            if(paymentModelView.Payment != null)
            {
                context.Payments.Add(paymentModelView.Payment);
                context.SaveChanges();
            }
            PaymentModelView model = new PaymentModelView();
            model.PaymentsList = context.Payments.ToList();
            model.Customers = context.Customers.ToList();
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var model = context.Payments.Find(id);
            context.Payments.Remove(model);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var model = context.Payments.Find(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Payment payment)
        {
            context.Attach(payment);
            context.Entry(payment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
