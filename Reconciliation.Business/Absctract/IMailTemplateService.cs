using Reconciliation.Core.Utilities.Results.Abstract;
using Reconciliation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Business.Absctract
{
    public interface IMailTemplateService
    {
        IResult Add(MailTemplate mailTemplate);
        IResult Update(MailTemplate mailTemplate);
        IResult Delete(MailTemplate mailTemplate);
        IDataResult<MailTemplate> Get(int id);
        IDataResult<MailTemplate> GetByTemplateName(int companyId, string name);
        IDataResult<List<MailTemplate>> GetAll(int companyId);

    }
}
