using Reconciliation.Core.Utilities.Results.Abstract;
using Reconciliation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Business.Absctract
{
    public interface ITermsAndConditionService
    {
        IResult Update(TermsAndCondition termsAndCondition);
        IDataResult<TermsAndCondition> Get();
    }
}
