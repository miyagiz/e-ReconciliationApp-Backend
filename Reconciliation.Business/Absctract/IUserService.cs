using Reconciliation.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Business.Absctract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user, int companyId);
        void Add(User user);
        void Update(User user);
        User GetByMail(string mail);
        User GetById(int id);
        User GetByMailConfirmValue(string value);
    }
}
