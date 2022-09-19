using Reconciliation.Core.Utilities.Results.Abstract;
using Reconciliation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Business.Absctract
{
    public interface IAccountReconciliationDetailService
    {
        IResult Add(AccountReconciliationDetail accountReconciliationDetail);
        IResult AddToExcel(string filePath, int accountReconciliationId);
        IResult Update(AccountReconciliationDetail accountReconciliationDetail);
        IResult Delete(AccountReconciliationDetail accountReconciliationDetail);
        IDataResult<AccountReconciliationDetail> GetById(int id);
        IDataResult<List<AccountReconciliationDetail>> GetList(int accountReconciliationId);
    }
}
