using FluentValidation;
using ManagementDocument.API.Commands;
using ManagementDocument.API.Services;

namespace ManagementDocument.API.Validations
{
    /// <summary>
    /// Валидатор для команды авторизации.
    /// </summary>
    public class AuthValidator : AbstractValidator<AuthCommand>
    {
        #region Fields
        private readonly IUserService _userService;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AuthValidator"/>.
        /// </summary>
        /// <param name="userService">Сервис для работы с пользователями.</param>
        public AuthValidator(IUserService userService)
        {
            _userService = userService;

            // Проверка логина
            RuleFor(x => x.Login)
               .NotNull()
               .WithMessage("Это обязательное поле")
               .Must(IsExistLogin)
               .WithMessage("Пользователя с таким именем не существует");

            // Проверка пароля
            RuleFor(x => x)
               .NotNull()
               .WithName("Password")
               .WithMessage("Это обязательное поле")
               .Must(IsValidPassword)
               .WithName("Password")
               .WithMessage("Неверный пароль");
        }

        private bool IsExistLogin(string? login) => _userService.IsExistLogin(login ?? throw new ArgumentNullException(nameof(login), "Логин является null"));
        private bool IsValidPassword(AuthCommand user) => _userService.IsValidPassword(user.Login ?? throw new ArgumentNullException(nameof(user.Login), "Логин является null"),
                                                                                       user.Password ?? throw new ArgumentNullException(nameof(user.Password), "Пароль является null"));
    }
}
