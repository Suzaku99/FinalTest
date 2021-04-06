using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Test.ViewModels
{
    public class PaymentsToReturn
    {
        public string TransactionId { get; set; }
        public string Payment { get; set; }
        [JsonIgnore]
        public string Status { get; set; }
        public string StatusCode
        {
            get
            {
                if (Status == "Approved")
                    return "A";
                else if (Status == "Failed" || Status == "Rejected")
                    return "R";
                else if (Status == "Finished" || Status == "Done")
                    return "D";
                else
                    return default;
            }
        }
    }
}
