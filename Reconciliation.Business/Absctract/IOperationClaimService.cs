using Reconciliation.Core.Entities.Concrete;
using Reconciliation.Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Business.Absctract
{
    public interface IOperationClaimService
    {
        IResult Add(OperationClaim operationClaim);
        IResult Update(OperationClaim operationClaim);
        IResult Delete(OperationClaim operationClaim);
        IDataResult<OperationClaim> GetById(int id);
        IDataResult<List<OperationClaim>> GetList();
    }
}
