using Reconciliation.Business.Absctract;
using Reconciliation.Business.BusinessAspects;
using Reconciliation.Business.Constans;
using Reconciliation.Core.Utilities.Results.Abstract;
using Reconciliation.Core.Utilities.Results.Concrete;
using Reconciliation.DataAccess.Abstract;
using Reconciliation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Business.Concrete
{
    public class TermsAndConditionManager : ITermsAndConditionService
    {
        private readonly ITermsAndConditionDal _termsAndConditionDal;

        public TermsAndConditionManager(ITermsAndConditionDal termsAndConditionDal)
        {
            _termsAndConditionDal = termsAndConditionDal;
        }

        //[SecuredOperation("Admin")]
        public IDataResult<TermsAndCondition> Get()
        {
            return new SuccessDataResult<TermsAndCondition>(_termsAndConditionDal.GetList().FirstOrDefault());
        }

        [SecuredOperation("Admin")]
        public IResult Update(TermsAndCondition termsAndCondition)
        {
            var result = _termsAndConditionDal.GetList().FirstOrDefault();
            if (result != null)
            {
                result.Description = termsAndCondition.Description;
                _termsAndConditionDal.Update(termsAndCondition);
            }
            else
            {
                _termsAndConditionDal.Add(termsAndCondition);
            }
            return new SuccessResult(Messages.UpdatedTermsAndCondition);
        }
    }
}
