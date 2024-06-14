using Domain.Validation;
using FluentValidation;
using FluentValidation.Results;

namespace Domain.Entities
{
    public class User : Entity<User>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public int Age { get; private set; }

        public User()
        {
            ConfigureValidationRules();
        }

        public User(string firstName, string lastName, string login, string password, string email, int age) : this()
        {
            ValidateNull(firstName, lastName, email, password, login);
            ValidateEmail(email);
            ValidateAge(age);
        }

        public void Update(int id, string firstName, string lastName, string login, string password, string email, int age)
        {
            Id = id;
            ValidateNull(firstName, lastName, email, password, login);
            ValidateEmail(email);
            ValidateAge(age);
        }

        private void ConfigureValidationRules()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage("Nome inválido.");

            RuleFor(user => user.LastName)
                .NotEmpty().WithMessage("Sobrenome inválido.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("E-mail inválido.")
                .Must(EmailValidation.ValidateEmail).WithMessage("Formato de e-mail inválido.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Senha inválida.");

            RuleFor(user => user.Login)
                .NotEmpty().WithMessage("Login inválido.");

            RuleFor(user => user.Age)
                .GreaterThan(0).WithMessage("Idade inválida.")
                .LessThan(100).WithMessage("Idade inválida.");
        }

        public void ValidateNull(string firstName, string lastName, string login, string password, string email)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(firstName), "Nome inválido.");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(lastName), "Sobrenome inválido.");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(email), "E-mail inválido.");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(password), "Senha inválida.");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(login), "Login inválida.");

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Login = login;
        }

        public void ValidateEmail(string email)
        {
            DomainExceptionValidation.When(!EmailValidation.ValidateEmail(email), "Formato de e-mail inválido.");
            Email = email;
        }

        public void ValidateAge(int age)
        {
            DomainExceptionValidation.When(age <= 0, "Idade inválida.");
            DomainExceptionValidation.When(age >= 100, "Idade inválida.");

            Age = age;
        }

        public override bool IsValid()
        {
            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
