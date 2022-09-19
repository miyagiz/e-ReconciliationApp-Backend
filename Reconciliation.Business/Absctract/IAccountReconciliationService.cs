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
    public interface IAccountReconciliationService
    {
        IResult Add(AccountReconciliation accountReconciliation);
        IResult AddToExcel(string filePath, int companyId);
        IResult Update(AccountReconciliation accountReconciliation);
        IResult Delete(AccountReconciliation accountReconciliation);
        IDataResult<AccountReconciliation> GetById(int id);
        IDataResult<AccountReconciliation> GetByCode(string code);
        IDataResult<List<AccountReconciliation>> GetList(int companyId);
        IDataResult<List<AccountReconciliationDto>> GetListDto(int companyId);
        IResult SendReconciliationMail(AccountReconciliationDto accountReconciliationDto);
    }
}
