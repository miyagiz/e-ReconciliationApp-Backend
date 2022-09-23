﻿using Reconciliation.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Entities.DTOs
{
    public class ForgotPasswordDto:IDto
    {
        public string Value { get; set; }

        public string Password { get; set; }
    }
}
