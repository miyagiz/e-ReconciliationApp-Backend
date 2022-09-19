using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reconciliation.Core.Entities;

namespace Reconciliation.Entities.Concrete
{
    public class Currency : IEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}