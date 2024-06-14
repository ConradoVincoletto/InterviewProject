using Domain.Events;

namespace Domain.Entities
{
    public class UserRegisteredEvent : DomainEvent
    {
        public string Email { get; }
        public string FirstName { get; }

        public UserRegisteredEvent(string email, string firstName)
        {
            Email = email;
            FirstName = firstName;
        }
    }
}
