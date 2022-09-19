using Reconciliation.Core.Entities.Concrete;
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
    public interface ICompanyService
    {
        //CRUD
        IResult Add(Company company);

        IResult Update(Company company);

        IDataResult<Company> GetById(int companyId);

        IResult AddCompanyAndUserCompany(CompanyDto company);

        IDataResult<List<Company>> GetList();

        IDataResult<UserCompany> GetCompany(int userId);

        IResult CompanyExist(Company company);

        IResult UserCompanyAdd(int userId, int companyId);
    }
}
