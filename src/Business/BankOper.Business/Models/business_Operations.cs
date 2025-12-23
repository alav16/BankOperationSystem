using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOper.Business.Models
{
    public class business_Operations
    {
        public int OperaOperationsId { get; set; }
        public int CustomerId { get; set; }
        public string OperatonType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
