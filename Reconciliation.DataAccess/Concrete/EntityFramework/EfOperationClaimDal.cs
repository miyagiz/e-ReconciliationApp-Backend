using Reconciliation.Core.DataAccess.EntityFramework;
using Reconciliation.Core.Entities.Concrete;
using Reconciliation.DataAccess.Abstract;
using Reconciliation.DataAccess.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.DataAccess.Concrete.EntityFramework
{
    public class EfOperationClaimDal : EfEntityRepositoryBase<OperationClaim, ContextDb>, IOperationClaimDal
    {
    }
}
