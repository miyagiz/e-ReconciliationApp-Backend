using Reconciliation.Business.Absctract;
using Reconciliation.Business.BusinessAspects;
using Reconciliation.Business.Constans;
using Reconciliation.Core.Entities.Concrete;
using Reconciliation.Core.Utilities.Results.Abstract;
using Reconciliation.Core.Utilities.Results.Concrete;
using Reconciliation.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

        [SecuredOperation("UserOperationClaim.Add,Admin")]
        public IResult Add(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult(Messages.AddedUserOperationClaim);
        }

        [SecuredOperation("UserOperationClaim.Delete,Admin")]
        public IResult Delete(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Delete(userOperationClaim);
            return new SuccessResult(Messages.DeletedUserOperationClaim);
        }

        [SecuredOperation("UserOperationClaim.Get,Admin")]
        public IDataResult<UserOperationClaim> GetById(int id)
        {
            return new SuccessDataResult<UserOperationClaim>(_userOperationClaimDal.Get(x => x.Id == id));
        }

        [SecuredOperation("UserOperationClaim.GetList,Admin")]
        public IDataResult<List<UserOperationClaim>> GetList(int userId, int companyId)
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetList(x => x.UserId == userId && x.CompanyId == companyId));
        }

        [SecuredOperation("UserOperationClaim.Update,Admin")]
        public IResult Update(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult(Messages.UpdatedUserOperationClaim);
        }
    }
}
