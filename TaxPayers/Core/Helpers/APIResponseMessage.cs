using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxPayers.Core.Helpers
{
    public class APIResponseMessage
    {
        public int ResultCode { get; set; }

        public string Remark { get; set; }

        public string Data { get; set; }

        public Exception Exception { get; set; }
        public string ResultString { get; set; }
    }
}
