using ManagementDocument.API.Commands;
using ManagementDocument.API.Services;
using ManagementDocument.Database.Entities;
using MediatR;

namespace ManagementDocument.API.Handlers
{
    /// <summary>
    /// Обработчик для создания нового пользователя с использованием <see cref="CreateUserCommand"/>.
    /// </summary>
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, User>
    {
        #region Fields
        private readonly IUserService _userService;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CreateUserHandler"/>.
        /// </summary>
        /// <param name="userService">Сервис пользователей.</param>
        public CreateUserHandler(IUserService userService) => _userService = userService;

        /// <summary>
        /// Обрабатывает команду создания нового пользователя.
        /// </summary>
        /// <param name="request">Команда создания пользователя.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Созданный пользователь.</returns>
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User newUser = new()
            {
                Login = request.Login,
                Password = request.Password,
                Role = request.Role
            };

            await _userService.AddUser(newUser);

            return newUser;
        }
    }
}
