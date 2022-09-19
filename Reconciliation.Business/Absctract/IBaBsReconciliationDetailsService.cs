using Reconciliation.Core.Utilities.Results.Abstract;
using Reconciliation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Business.Absctract
{
    public interface IBaBsReconciliationDetailsService
    {
        IResult Add(BaBsReconciliationDetail babsReconciliationDetail);
        IResult AddToExcel(string filePath, int babsReconciliationId);
        IResult Update(BaBsReconciliationDetail babsReconciliationDetail);
        IResult Delete(BaBsReconciliationDetail babsReconciliationDetail);
        IDataResult<BaBsReconciliationDetail> GetById(int id);
        IDataResult<List<BaBsReconciliationDetail>> GetList(int babsReconciliationId);
    }
}
