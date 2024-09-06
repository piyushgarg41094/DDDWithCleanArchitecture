using Gatherly.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Errors
{
    public static class DomainErrors
    {
        public static class Gathering
        {
            public static readonly Error InvitationCreator = new Error(
                "Gathering.InvitingCreator",
                "Can't send invitation to gathering creator.");

            public static readonly Error AlreadyPassed = new Error(
                "Gathering.AlreadyPassed",
                "Can't send invitation for gathering in the past.");
        }

        public static class Member
        {
            public static readonly Error EmailAlreadyInUse = new(
            "Member.EmailAlreadyInUse",
            "The specified email is already in use");
        }
    }
}
