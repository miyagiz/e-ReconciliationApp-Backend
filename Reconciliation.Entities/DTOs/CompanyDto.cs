using Reconciliation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Entities.DTOs
{
    public class CompanyDto
    {
        public int UserId { get; set; }
        public Company Company { get; set; }
    }
}
