using Reconciliation.Core.Utilities.Results.Abstract;
using Reconciliation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Business.Absctract
{
    public interface ICurrencyAccountService
    {
        IResult Add(CurrencyAccount currencyAccount);

        IResult AddToExcel(string filePath, int companyId);

        IResult Update(CurrencyAccount currencyAccount);

        IResult Delete(CurrencyAccount currencyAccount);

        IDataResult<CurrencyAccount> Get(int id);

        IDataResult<CurrencyAccount> GetByCode(string code, int companyId);

        IDataResult<List<CurrencyAccount>> GetList(int companyId);
    }
}
