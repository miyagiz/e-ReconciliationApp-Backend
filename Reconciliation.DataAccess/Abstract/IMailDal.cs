using Reconciliation.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.DataAccess.Abstract
{
    public interface IMailDal
    {
        void SendMail(SendMailDto sendMailDto);
    }
}
