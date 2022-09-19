using Reconciliation.Core.DataAccess.EntityFramework;
using Reconciliation.DataAccess.Abstract;
using Reconciliation.DataAccess.Concrete.EntityFramework.Context;
using Reconciliation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.DataAccess.Concrete.EntityFramework
{
    public class EfCurrencyAccountDal : EfEntityRepositoryBase<CurrencyAccount, ContextDb>, ICurrencyAccountDal
    {
    }
}
