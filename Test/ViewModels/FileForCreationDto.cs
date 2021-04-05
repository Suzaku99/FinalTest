using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Test.ViewModels
{
    public class FileForCreationDto
    {
        public IFormFile File { get; set; }
        public string TransactionId { get; set; }
        public Decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }
    }

	[XmlRoot(ElementName = "PaymentDetails")]
	public class PaymentDetails
	{
		[XmlElement(ElementName = "Amount")]
		public string Amount { get; set; }
		[XmlElement(ElementName = "CurrencyCode")]
		public string CurrencyCode { get; set; }
	}

	[XmlRoot(ElementName = "Transaction")]
	public class Transaction
	{
		[XmlElement(ElementName = "TransactionDate")]
		public string TransactionDate { get; set; }
		[XmlElement(ElementName = "PaymentDetails")]
		public PaymentDetails PaymentDetails { get; set; }
		[XmlElement(ElementName = "Status")]
		public string Status { get; set; }
		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }
	}

	[XmlRoot(ElementName = "Transactions")]
	public class Transactions
	{
		[XmlElement(ElementName = "Transaction")]
		public List<Transaction> Transaction { get; set; }
	}
}