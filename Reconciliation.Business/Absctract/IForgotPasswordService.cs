using Reconciliation.Core.Entities.Concrete;
using Reconciliation.Core.Utilities.Results.Abstract;
using Reconciliation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Business.Absctract
{
    public interface IForgotPasswordService
    {
        IDataResult<ForgotPassword> CreateForgotPassword(User user);

        IDataResult<List<ForgotPassword>> GetListByUserId(int userId);

        void Update(ForgotPassword forgotPassword);

        ForgotPassword GetForgotPassword(string value);
    }
}
