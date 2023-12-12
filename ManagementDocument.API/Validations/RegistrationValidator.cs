using FluentValidation;
using ManagementDocument.API.Commands;
using ManagementDocument.API.Services;

namespace ManagementDocument.API.Validations
{
    /// <summary>
    /// Валидатор для команды регистрации нового пользователя.
    /// </summary>
    public class RegistrationValidator : AbstractValidator<CreateUserCommand>
    {
        #region Fields
        private readonly IUserService _userService;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RegistrationValidator"/>.
        /// </summary>
        /// <param name="userService">Сервис для работы с пользователями.</param>
        public RegistrationValidator(IUserService userService)
        {
            _userService = userService;

            // Правило валидации для логина пользователя
            RuleFor(x => x.Login)
               .NotNull()
               .WithMessage("Это обязательное поле")
               .Must(IsExistLogin)
               .WithMessage("Пользователя с таким именем уже существует");

            // Правило валидации для пароля пользователя
            RuleFor(x => x)
               .NotNull()
               .WithName("Password")
               .WithMessage("Это обязательное поле");
        }

        bool IsExistLogin(string? login) => !_userService.IsExistLogin(login ?? throw new ArgumentNullException(nameof(login), "Логин является null"));
    }
}
