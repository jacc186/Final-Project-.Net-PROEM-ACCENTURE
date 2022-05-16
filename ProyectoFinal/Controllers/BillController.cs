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
        public IActionResult Index(BillListClassModel model)
        {
            if (model.Bill != null && (model.Bill.Id == 0) && (model.Bill.CustomerId != 0))
            {
                if (model.Bill.BillDetails == null)
                {
                    model.Bill.BillDetails = new List<BillDetail>();
                }
                model.Bill.BillDetails.Add(model.BillDetail);

                foreach (var item in model.Bill.BillDetails)
                {
                    item.ItemName = context.Items.Find(item.ItemId).Name;
                }
                var name = context.Customers.Find(model.Bill.CustomerId).Name;
                var total = 0.0f;
                foreach(var item in model.Bill.BillDetails)
                {
                    total += (item.Amount * context.Items.Find(item.ItemId).Price);
                }
                context.Add(new Bill { State = true,CustomerId = model.Bill.CustomerId, Total = total, CustomerName = name, BillDetails = model.Bill.BillDetails, DatePurchase = DateTime.Now });
                context.SaveChanges();
                model.Bill = new Bill();
            }
            else
            {
                model.Bill = new Bill();
            }
            if (model.Bill.BillDetails == null)
            {
                model.Bill.BillDetails = new List<BillDetail>();
            }
            if (model == null)
                model = new BillListClassModel();
            model.Bills = context.Bills.Where(x => x.State == true).ToList();
            model.Customers = context.Customers.Where(x => x.Active == true).ToList();
            model.Items = context.Items.Where(x => x.State == true).ToList();
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var model = new Bill();
            model = context.Bills.Find(id);
            model.State = false;
            context.Attach(model);
            context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Bill bill)
        {
            bill.State = true;
            foreach (var item in bill.BillDetails)
            {
                bill.Total += (item.Amount * context.Items.Find(item.ItemId).Price);
                item.ItemName = context.Items.Find(item.ItemId).Name;
                item.BillId = bill.Id;
            }
            context.Attach(bill);
            context.Entry(bill).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var model = context.Bills.Find(id);
            BillListClassModel billList = new BillListClassModel();
            billList.Bill = model;
            billList.Bill.BillDetails = context.BillDetails.Where(x => x.BillId == id && x.ItemName != null).ToList();
            billList.Items = context.Items.ToList();
            return View(billList);
        }

        public IActionResult AddItem(BillListClassModel model)
        {
            if (model.Bill.BillDetails == null)
            {
                model.Bill.BillDetails = new List<BillDetail>();
            }

            model.Bill.BillDetails.Add(model.BillDetail);

            foreach (var item in model.Bill.BillDetails)
            {
                item.ItemName = context.Items.Find(item.ItemId).Name;
            }

            if (model == null)
                model = new BillListClassModel();
            model.Bills = context.Bills.Where(x => x.State == true).ToList();
            model.Customers = context.Customers.Where(x => x.Active == true).ToList();
            model.Items = context.Items.Where(x => x.State == true).ToList();
            return View("Index", model);
        }
    }
}
