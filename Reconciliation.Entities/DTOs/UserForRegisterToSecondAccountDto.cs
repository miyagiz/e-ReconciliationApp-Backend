using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Entities.DTOs
{
    public class UserForRegisterToSecondAccountDto : UserForRegister
    {
        public int CompanyId { get; set; }

    }
}
