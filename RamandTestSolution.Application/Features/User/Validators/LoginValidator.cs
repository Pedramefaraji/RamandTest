using FluentValidation;
using RamandTestSolution.Application.Features.User.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamandTestSolution.Application.Features.User.Validators
{
    public class LoginValidator : AbstractValidator<Login>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("نام کاربری نمیتواند خالی باشد");
            RuleFor(x => x.Password).NotEmpty().WithMessage("رمز عبور نمیتواند خالی باشد");
        }
    }
}
