using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public class Payment
    {
        [Key]
        public string TransactionId { get; set; }
        public decimal Amonnt { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Status{ get; set; }
    }
}
