using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Test.ViewModels
{
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