﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Core.Utilities.Results.Concrete
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data) : base(data, true)
        {
        }

        public SuccessDataResult(T data, string message) : base(data, true, message)
        {
        }

        public SuccessDataResult(): base(default,true)
        {

        }
    }
}
