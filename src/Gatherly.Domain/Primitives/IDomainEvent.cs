using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Primitives
{
    public interface IDomainEvent : INotification
    {

    }

    //MemberCreatedDomainEvent
    //GatheringCreatedDomainEvents
    //InvitationAcceptedDomainEvent
   
}
