using Reconciliation.Core.DataAccess;
using Reconciliation.Entities.Concrete;
using Reconciliation.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.DataAccess.Abstract
{
    public interface IAccountReconciliationDal : IEntityRepository<AccountReconciliation>
    {
        List<AccountReconciliationDto> GetAllDto(int companyId);
    }
}
