using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Members.Commands
{
    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var firstNameResult = FirstName.Create(request.FirstName);
            if (firstNameResult.IsFailure)
            {
                //Log Error
                return Unit.Value;
            }
            var member = new Member(
                Guid.NewGuid(),
                firstNameResult.Value,
                request.LastName,
                request.Email);

            _memberRepository.Add(member);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
