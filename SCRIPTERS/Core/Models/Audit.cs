using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCRIPTERS.Core.Models
{
    public class Audit
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime TransactionTime { get; set; }
        public string User { get; set; }
        public string TransactionType { get; set; }
        public string TransactionDetails { get; set; }


    }
}