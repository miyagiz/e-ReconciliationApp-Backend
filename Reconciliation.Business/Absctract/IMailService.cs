using Reconciliation.Core.Utilities.Results.Abstract;
using Reconciliation.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Business.Absctract
{
    public interface IMailService
    {
        IResult SendMail(SendMailDto sendMailDto);
    }
}
