using FluentValidation;
using Reconciliation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Business.ValidationRules.FluentValidation
{
    public class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Şirket Adı Alanı Boş Geçilemez.");
            RuleFor(p => p.Name).MinimumLength(4).WithMessage("Şirket Adı En Az 4 Karakter Olmalıdır.");
            RuleFor(p => p.Address).NotEmpty().WithMessage("Şirket Adresi Alanı Boş Geçilemez");
        }
    }
}
