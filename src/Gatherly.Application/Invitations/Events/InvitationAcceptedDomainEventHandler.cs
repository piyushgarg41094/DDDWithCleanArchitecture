using Gatherly.Application.Abstractions;
using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Enums;
using Gatherly.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gatherly.Domain.Errors.DomainErrors;

namespace Gatherly.Application.Invitations.Events
{
    public sealed class InvitationAcceptedDomainEventHandler : INotificationHandler<InvitationAcceptedDomainEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IGatheringRepository _gatheringRepository;
        public async Task Handle(InvitationAcceptedDomainEvent notification, CancellationToken cancellationToken)
        {
            var gathering = await _gatheringRepository.GetByIdWithCreatorAsync(notification.GatheringId, cancellationToken);
            if(gathering is null)
            {
                return;
            }
            await _emailService.SendInvitationAcceptedEmailAsync(gathering, cancellationToken);
        }
    }
}
