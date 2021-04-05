using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public decimal Amonnt { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public PaymentStatus Status{ get; set; }
    }
}
