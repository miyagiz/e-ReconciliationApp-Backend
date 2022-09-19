using Reconciliation.Core.Utilities.Results.Abstract;
using Reconciliation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Business.Absctract
{
    public interface IMailParameterService
    {
        IResult Update(MailParameter mailParameter);
        IDataResult<MailParameter> Get(int companyId);
    }
}
