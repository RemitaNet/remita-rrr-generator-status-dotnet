using System;
using System.Collections.Generic;
using System.Text;

namespace RemitaGenRRRStatus
{
    public class GenerateRRRRequest
    {
        public GenerateRRRRequest() { }

        public string serviceTypeId;
        
        public string amount;

        public string orderId;

        public string payerName;

        public string payerEmail;

        public string payerPhone;

        public string description;

        public List<CustomField> customFields { get; set; }
    }
}
