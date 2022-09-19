using Reconciliation.Core.DataAccess.EntityFramework;
using Reconciliation.Core.Entities.Concrete;
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
    public class EfCompanyDal : EfEntityRepositoryBase<Company, ContextDb>, ICompanyDal
    {
        public UserCompany GetUserCompany(int userId)
        {
            using (var context = new ContextDb())
            {
                var result = context.UserCompanies.Where(x => x.UserId == userId).FirstOrDefault();
                return result;
            }
        }

        public void UserCompanyAdd(int userId, int companyId)
        {
            using (var context = new ContextDb())
            {
                UserCompany userCompany = new UserCompany()
                {
                    UserId = userId,
                    CompanyId = companyId,
                    AddedAt = DateTime.Now,
                    IsActive = true
                };
                context.UserCompanies.Add(userCompany);
                context.SaveChanges();
            }
        }
    }
}
