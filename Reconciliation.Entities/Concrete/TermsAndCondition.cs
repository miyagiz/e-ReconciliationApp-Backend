using Reconciliation.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Entities.Concrete
{
    public class TermsAndCondition:IEntity
    {
        public int Id { get; set; }

        public string Desription { get; set; }
    }
}
