using FluentValidation;
using Gatherly.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Members.Commands
{
    internal class CraeteMemberCommandValidator : AbstractValidator<CreateMemberCommand>
    {
        public CraeteMemberCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty();

            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(FirstName.MaxLength);

            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        }
    }
}
