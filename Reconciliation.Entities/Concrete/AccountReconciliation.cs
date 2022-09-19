using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reconciliation.Core.Entities;

namespace Reconciliation.Entities.Concrete
{
    public class AccountReconciliation : IEntity
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int CurrencyAccountId { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public int CurrencyId { get; set; }
        public Decimal CurrencyDebit { get; set; }
        public Decimal CurrencyCredit { get; set; }
        public bool IsSendMail { get; set; }
        public DateTime? SendEmailDate { get; set; }
        public bool? IsEmailRead { get; set; }
        public DateTime? EmailReadDate { get; set; }
        public bool? IsResultSucceed { get; set; }
        public DateTime? ResultDate { get; set; }
        public string? ResultNote { get; set; }
        public string? Guid { get; set; }
    }
}