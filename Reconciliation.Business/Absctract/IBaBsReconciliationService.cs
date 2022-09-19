using Reconciliation.Core.Utilities.Results.Abstract;
using Reconciliation.Entities.Concrete;
using Reconciliation.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Business.Absctract
{
    public interface IBaBsReconciliationService
    {
        IResult Add(BaBsReconciliation baBsReconciliation);
        IResult AddToExcel(string filePath, int companyId);
        IResult Update(BaBsReconciliation baBsReconciliation);
        IResult Delete(BaBsReconciliation baBsReconciliation);
        IDataResult<BaBsReconciliation> GetByCode(string code);
        IDataResult<BaBsReconciliation> GetById(int id);
        IDataResult<List<BaBsReconciliation>> GetList(int companyId);
        IDataResult<List<BaBsReconciliationDto>> GetListDto(int companyId);
        IResult SendReconciliationMail(BaBsReconciliationDto accountReconciliationDto);

    }
}
