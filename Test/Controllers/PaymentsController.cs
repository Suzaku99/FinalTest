using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using Test.Data;
using Test.Models;
using Test.ViewModels;

namespace Test.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly DataContext _context;
        private readonly CultureInfo cultureinfoTH = new CultureInfo("th-TH");
        private readonly CultureInfo cultureinfoEN = new CultureInfo("en-US");

        public PaymentsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayments(string currency, string start, string end, string statusCode)
        {
            var paymentsDb = _context.Payment.AsQueryable();

            if (!string.IsNullOrEmpty(currency))
            {
                paymentsDb = paymentsDb.Where(x => x.CurrencyCode == currency);
            }

            if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {
                paymentsDb = paymentsDb.Where(x => x.TransactionDate >= DateTime.Parse(start) && x.TransactionDate <= DateTime.Parse(end));
            }

            var payments = await paymentsDb.ToListAsync();

            List<PaymentsToReturn> Payments = new List<PaymentsToReturn>();
            foreach(var item in paymentsDb)
            {
                PaymentsToReturn Payment = new PaymentsToReturn
                {
                    TransactionId = item.TransactionId,
                    Payment = $"{item.Amonnt} {item.CurrencyCode}",
                    Status = item.Status
                };

                Payments.Add(Payment);
            }

            IEnumerable<PaymentsToReturn> result = Payments;
            if (!string.IsNullOrEmpty(statusCode))
            {
                result = Payments.Where(x => x.StatusCode == statusCode);
            }
                
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrency()
        {
            var currency = await _context.Payment.Select(x => x.CurrencyCode).Distinct().ToListAsync();
            return Ok(currency);
        }

        [HttpPost]
        public async Task<IActionResult> Uploadfile()
        {
            var file = Request.Form.Files[0];

            if (file.Length > 0)
            {
                if (file.FileName.EndsWith(".csv"))
                {
                    try
                    {
                        using (var sreader = new StreamReader(file.OpenReadStream()))
                        {
                            while (!sreader.EndOfStream)
                            {
                                string[] rows = sreader.ReadLine().Split(',');

                                Payment payment = new Payment
                                {
                                    TransactionId = rows[0].ToString().Replace("\"", string.Empty),
                                    Amonnt = decimal.Parse(rows[1].ToString().Replace("\"", string.Empty)),
                                    CurrencyCode = rows[2].ToString().Replace("\"", string.Empty),
                                    TransactionDate = DateTime.Parse(rows[3].ToString().Replace("\"", string.Empty), cultureinfoTH),
                                    Status = rows[4].ToString().Replace("\"", string.Empty),
                                };

                                _context.Add(payment);
                            }

                            await _context.SaveChangesAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else if (file.FileName.EndsWith(".xml"))
                {
                    try
                    {
                        var mySerializer = new XmlSerializer(typeof(Transactions));
                        var myObject = new Transactions();
                        using (var reader = new StreamReader(file.OpenReadStream()))
                        {
                           myObject = (Transactions)mySerializer.Deserialize(reader);
                        }

                        foreach(var item in myObject.Transaction)
                        {
                            Payment payment = new Payment
                            {
                                TransactionId = item.Id,
                                Amonnt = decimal.Parse(item.PaymentDetails.Amount),
                                CurrencyCode = item.PaymentDetails.CurrencyCode,
                                TransactionDate = DateTime.Parse(item.TransactionDate),
                                Status = item.Status,
                            };
                            _context.Add(payment);
                        }

                        await _context.SaveChangesAsync();

                    } catch(Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return BadRequest("Unknown format");
                }
            }
            return BadRequest("File is empty");
        }
    }
}
