using FluentValidation;
using Reconciliation.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Kullanıcı Adı Alanı Boş Geçilemez.");
            RuleFor(p => p.Name).MinimumLength(4).WithMessage("Kullanıcı Adınız En Az 4 Karakter Olmalıdır.");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Mail Alanı Boş Geçilemez.");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Lütfen Geçerli Bir Mail Giriniz.");
        }
    }
}
