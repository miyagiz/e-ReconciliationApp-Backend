using Reconciliation.Core.Entities.Concrete;
using Reconciliation.Core.Utilities.Results.Abstract;
using Reconciliation.Core.Utilities.Security.JWT;
using Reconciliation.Entities.Concrete;
using Reconciliation.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Business.Absctract
{
    public interface IAuthService
    {
        IDataResult<UserCompanyDto> Register(UserForRegister userForRegister, string password, Company company);
        IDataResult<User> RegisterSecondAccount(UserForRegister userForRegister, string password, int companyId);
        IDataResult<User> Login(UserForLogin userForLogin);
        IDataResult<User> GetById(int id);
        IDataResult<User> GetByMail(string email);
        IDataResult<User> GetByMailConfirmValue(string value);
        IResult UserExist(string email);
        IResult Update(User user);
        IResult ChangePassword(User user);
        IResult CompanyExists(Company company);
        IResult SendConfirmEmailAgain(User user);
        IDataResult<AccessToken> CreateAccessToken(User user, int companyId);
        IDataResult<UserCompany> GetUserCompany(int userId);
        IResult SendForgotPasswordEmail(User user, string value);
    }
}
