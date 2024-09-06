using Gatherly.Domain.Primitives;
using Gatherly.Domain.ValueObjects;

namespace Gatherly.Domain.Entities;

public sealed class Member : Entity
{
    public Member(Guid id, FirstName firstName, string lastName, string email) : base(id)
    {
        FirstName=firstName;
        LastName=lastName;
        Email=email;
    }

    public FirstName FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public static Member Create(Guid id, FirstName firstName, string lastName, string email)
    {
        var member = new Member(id, firstName, lastName, email);
        return member;
    }
}