using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reconciliation.Core.Entities.Concrete
{
    public class UserCompany : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public DateTime AddedAt { get; set; }
        public bool IsActive { get; set; }
    }
}