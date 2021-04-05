using System;
using System.Collections.Generic;
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

        public PaymentsController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Uploadfile()
        {
            FileForCreationDto fileForCreationDto = new FileForCreationDto();
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
                                fileForCreationDto.TransactionId = rows[0].ToString().Replace("\"", string.Empty);
                                fileForCreationDto.Amount = decimal.Parse(rows[1].ToString().Replace("\"", string.Empty));
                                fileForCreationDto.CurrencyCode = rows[2].ToString().Replace("\"", string.Empty);
                                fileForCreationDto.TransactionDate = DateTime.Parse(rows[3].ToString().Replace("\"", string.Empty));
                                fileForCreationDto.Status = rows[4].ToString().Replace("\"", string.Empty);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest("csv invalid.");
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
                                Status = Enum.TryParse(item.Status, out PaymentStatus paymentStatus) ? paymentStatus : default,
                        };
                            _context.Add(payment);
                        }

                        await _context.SaveChangesAsync();

                    } catch(Exception ex)
                    {

                    }
                }
                else
                {

                }
            }
            return Ok();
        }
    }
}
