using Reconciliation.Core.DataAccess;
using Reconciliation.Core.Entities.Concrete;
using Reconciliation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.DataAccess.Abstract
{
    public interface ICompanyDal : IEntityRepository<Company>
    {
        void UserCompanyAdd(int userId, int companyId);

        UserCompany GetUserCompany(int userId);
    }
}
