using Reconciliation.Core.Entities;
using Reconciliation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Entities.DTOs
{
    public class UserAndCompanyRegisterDto : IDto
    {
        public UserForRegister UserForRegister { get; set; }
        public Company Company { get; set; }
    }
}
