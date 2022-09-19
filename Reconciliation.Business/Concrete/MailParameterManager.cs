using Reconciliation.Business.Absctract;
using Reconciliation.Business.BusinessAspects;
using Reconciliation.Business.Constans;
using Reconciliation.Core.Aspects.Caching;
using Reconciliation.Core.Aspects.Performance;
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
    public class MailParameterManager : IMailParameterService
    {
        private readonly IMailParameterDal _mailParameterDal;

        public MailParameterManager(IMailParameterDal mailParameterDal)
        {
            _mailParameterDal = mailParameterDal;
        }

        [CacheAspect(60)]
        public IDataResult<MailParameter> Get(int companyId)
        {
            return new SuccessDataResult<MailParameter>(_mailParameterDal.Get(m => m.CompanyId == companyId));
        }

        [PerformanceAspect(3)]
        [SecuredOperation("MailParameter.Update,Admin")]
        [CacheRemoveAspect("IMailParameterService.Get")]
        public IResult Update(MailParameter mailParameter)
        {
            var result = Get(mailParameter.CompanyId);
            if (result.Data == null)
            {
                _mailParameterDal.Add(mailParameter);
            }
            else
            {
                result.Data.SMTP = mailParameter.SMTP;
                result.Data.Port = mailParameter.Port;
                result.Data.SSL = mailParameter.SSL;
                result.Data.Email = mailParameter.Email;
                result.Data.Password = mailParameter.Password;
                _mailParameterDal.Update(result.Data);
            }
            return new SuccessResult(Messages.MailParameterUpdated);
        }
    }
}
