using FinalProject.Models.ViewModels;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            var billsrank = bills.GroupBy(b => b.CustomerId).Select(x => new CustomerRankViewModel
            {
                CustomerId = x.First().CustomerId,
                CustomerName = x.First().CustomerName,
                MoneySpent = x.Sum(x => x.Total)
            }).OrderByDescending(x => x.MoneySpent).ToList();
            return View(billsrank);
        }
        [HttpGet]
        public async Task<IActionResult> ExportExcel()
        {
            var customers = await context.Customers.ToListAsync();
            var payments = await context.Payments.ToListAsync();
            var items = await context.Items.Where(x => x.State == true).ToListAsync();
            var bills = await context.Bills.Where(x => x.State == true).ToListAsync();

            IWorkbook woorkbook = new XSSFWorkbook();

            ISheet sheetCustomers = woorkbook.CreateSheet("Customers");
            ISheet sheetPayments = woorkbook.CreateSheet("Payments");
            ISheet sheetItems = woorkbook.CreateSheet("Items");
            ISheet sheetBills = woorkbook.CreateSheet("Bills");
            var title = woorkbook.CreateCellStyle();
            var font = woorkbook.CreateFont();
            font.IsBold = true;
            title.SetFont(font);

            IRow row = sheetCustomers.CreateRow(0);
            var cell = row.CreateCell(0);
            cell.SetCellValue("Id");
            cell.CellStyle = title;
            cell = row.CreateCell(1);
            cell.SetCellValue("Name");
            cell.CellStyle = title;
            cell = row.CreateCell(2);
            cell.SetCellValue("Email");
            cell.CellStyle = title;

            int rowNo = 1;
            foreach (var item in customers)
            {
                row = sheetCustomers.CreateRow(rowNo);
                row.CreateCell(0).SetCellValue(item.Id);
                row.CreateCell(1).SetCellValue(item.Name);
                row.CreateCell(2).SetCellValue(item.Email);
                ++rowNo;
                sheetCustomers.AutoSizeColumn(0);
                sheetCustomers.AutoSizeColumn(1);
                sheetCustomers.AutoSizeColumn(2);
            }

            IRow row1 = sheetPayments.CreateRow(0);
            var cell1 = row1.CreateCell(0);
            cell1.SetCellValue("Id");
            cell1.CellStyle = title;
            cell1 = row1.CreateCell(1);
            cell1.SetCellValue("Customer Id");
            cell1.CellStyle = title;
            cell1 = row1.CreateCell(2);
            cell1.SetCellValue("Total");
            cell1.CellStyle = title;

            int rowNo1 = 1;
            foreach (var item in payments)
            {
                row1 = sheetPayments.CreateRow(rowNo1);
                row1.CreateCell(0).SetCellValue(item.Id);
                row1.CreateCell(1).SetCellValue(item.CustomerId);
                row1.CreateCell(2).SetCellValue(item.Amount);
                ++rowNo1;
                sheetPayments.AutoSizeColumn(0);
                sheetPayments.AutoSizeColumn(1);
                sheetPayments.AutoSizeColumn(2);
            }

            IRow row2 = sheetItems.CreateRow(0);
            var cell2 = row2.CreateCell(0);
            cell2.SetCellValue("Id");
            cell2.CellStyle = title;
            cell2 = row2.CreateCell(1);
            cell2.SetCellValue("Name");
            cell2.CellStyle = title;
            cell2 = row2.CreateCell(2);
            cell2.SetCellValue("Price");
            cell2.CellStyle = title;

            int rowNo2 = 1;
            foreach (var item in items)
            {
                row2 = sheetItems.CreateRow(rowNo2);
                row2.CreateCell(0).SetCellValue(item.Id);
                row2.CreateCell(1).SetCellValue(item.Name);
                row2.CreateCell(2).SetCellValue(item.Price);
                ++rowNo2;
                sheetItems.AutoSizeColumn(0);
                sheetItems.AutoSizeColumn(1);
                sheetItems.AutoSizeColumn(2);
            }

            IRow row3 = sheetBills.CreateRow(0);
            var cell3 = row3.CreateCell(0);
            cell3.SetCellValue("Id");
            cell3.CellStyle = title;
            cell3 = row3.CreateCell(1);
            cell3.SetCellValue("Customer Name");
            cell3.CellStyle = title;
            cell3 = row3.CreateCell(2);
            cell3.SetCellValue("Total");
            cell3.CellStyle = title;
            cell3 = row3.CreateCell(3);
            cell3.SetCellValue("Date");
            cell3.CellStyle = title;

            int rowNo3 = 1;
            foreach (var item in bills)
            {
                row3 = sheetBills.CreateRow(rowNo3);
                row3.CreateCell(0).SetCellValue(item.Id);
                row3.CreateCell(1).SetCellValue(item.CustomerName);
                row3.CreateCell(2).SetCellValue(item.Total);
                row3.CreateCell(3).SetCellValue(item.DatePurchase.ToShortDateString());
                ++rowNo3;
                sheetBills.AutoSizeColumn(0);
                sheetBills.AutoSizeColumn(1);
                sheetBills.AutoSizeColumn(2);
                sheetBills.AutoSizeColumn(3);
            }

            var st = new MemoryStream();
            woorkbook.Write(st);
            var st2 = new MemoryStream();
            var array = st.ToArray();
            st2.Write(array, 0, array.Length);
            st2.Position = 0;
            return File(st2, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.", "Report.xlsx");
        }

        [HttpGet]
        public async Task<IActionResult> ExportPdf()
        {
            var payments = await context.Payments.ToListAsync();
            var bills = await context.Bills.ToListAsync();
            var customers = await context.Customers.ToListAsync();
            var billsrank = bills.GroupBy(b => b.CustomerId).Select(x => new CustomerRankViewModel
            {
                CustomerId = x.First().CustomerId,
                CustomerName = x.First().CustomerName,
                MoneySpent = x.Sum(x => x.Total)
            }).OrderByDescending(x => x.MoneySpent).ToList();

            var st = new MemoryStream();
            PdfWriter writer = new PdfWriter(st);
            PdfDocument pdf = new PdfDocument(writer);
            Document report = new Document(pdf, PageSize.A4);
            Paragraph text = new Paragraph("Customers Ranking")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(15)
                .SetMarginBottom(3);
            report.Add(text);


            Table table = new Table(4, false);
            table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);

            Cell cell = new Cell()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .Add(new Paragraph("Customer"));
            table.AddCell(cell);

            cell = new Cell();
            cell.SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .Add(new Paragraph("Customer Id"));
            table.AddCell(cell);

            cell = new Cell();
            cell.SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .Add(new Paragraph("Money Spent"));
            table.AddCell(cell);

            cell = new Cell();
            cell.SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .Add(new Paragraph("Rank"));
            table.AddCell(cell);

            var i = 1;
            foreach (var item in billsrank)
            {
                cell = new Cell()
                .SetBackgroundColor(ColorConstants.WHITE)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .Add(new Paragraph(item.CustomerName));
                table.AddCell(cell);

                cell = new Cell()
                .SetBackgroundColor(ColorConstants.WHITE)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .Add(new Paragraph(item.CustomerId.ToString()));
                table.AddCell(cell);

                cell = new Cell()
                .SetBackgroundColor(ColorConstants.WHITE)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .Add(new Paragraph(item.MoneySpent.ToString() + "$"));
                table.AddCell(cell);

                cell = new Cell()
                .SetBackgroundColor(ColorConstants.WHITE)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .Add(new Paragraph(i++.ToString()));
                table.AddCell(cell);
            }

            report.Add(table);

            text = new Paragraph("Current Account")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(15)
                .SetMarginBottom(3);
            report.Add(text);

            table = new Table(3, false);
            table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);

            cell = new Cell()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .Add(new Paragraph("Customer Id"));
            table.AddCell(cell);

            cell = new Cell();
            cell.SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .Add(new Paragraph("Customer Name"));
            table.AddCell(cell);

            cell = new Cell();
            cell.SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .Add(new Paragraph("Balance"));
            table.AddCell(cell);

            var customersGrouped = customers.GroupBy(x=>x.Id);
            var billsGrouped = bills.GroupBy(x => x.CustomerId).Select(y=> new Bill
            {
                Total = y.Sum(x=>x.Total),
                CustomerId = y.FirstOrDefault().CustomerId,               
            }).ToList();

            var current = (from pay in payments
                           group pay by pay.CustomerId into pb
                           join bill in billsGrouped on pb.FirstOrDefault().CustomerId equals bill.CustomerId
                           join customer in customersGrouped on bill.CustomerId equals customer.FirstOrDefault().Id
                           select new CustomerRankViewModel
                           {
                               CustomerId = pb.FirstOrDefault().CustomerId,
                               MoneySpent = bill.Total,
                               Payments = pb.Sum(x=>x.Amount),
                               CustomerName = customer.FirstOrDefault().Name,
                           }).ToList();

            foreach(var item in current)
            {
                cell = new Cell()
                .SetBackgroundColor(ColorConstants.WHITE)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .Add(new Paragraph(item.CustomerId.ToString()));
                table.AddCell(cell);

                cell = new Cell();
                cell.SetBackgroundColor(ColorConstants.WHITE)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .Add(new Paragraph(item.CustomerName));
                table.AddCell(cell);

                cell = new Cell();
                cell.SetBackgroundColor(ColorConstants.WHITE)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .Add(new Paragraph((item.Payments - item.MoneySpent).ToString()));
                table.AddCell(cell);
            }
            report.Add(table);

            report.Close();

            var st2 = new MemoryStream();
            var array = st.ToArray();
            st2.Write(array, 0, array.Length);
            st2.Position = 0;
            return File(st2, "application/pdf", "Report.pdf");
        }
    }
}
