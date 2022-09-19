using Reconciliation.Core.Entities;
using Reconciliation.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Entities.DTOs
{
    public class UserCompanyDto : User, IDto
    {
        public int CompanyId { get; set; }
    }
}
