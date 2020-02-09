using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StepShop.IdentityApi.DTO.Validators
{
    public class LoginCredentialsDtoValidator : AbstractValidator<LoginCredentialsDto>
    {
        public LoginCredentialsDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
