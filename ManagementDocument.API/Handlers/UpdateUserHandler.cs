using ManagementDocument.API.Commands;
using ManagementDocument.API.Services;
using ManagementDocument.Domain.Models;
using MediatR;

namespace ManagementDocument.API.Handlers
{
    /// <summary>
    /// Обработчик команды обновления пользователя.
    /// </summary>
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        #region Fields
        private readonly IUserService _userService;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UpdateDocumentHandler"/>.
        /// </summary>
        /// <param name="userService">Сервис для работы с пользователями.</param>
        public UpdateUserHandler(IUserService userService) => _userService = userService;

        /// <summary>
        /// Обрабатывает запрос на обновление пользователя.
        /// </summary>
        /// <param name="request">Запрос на обновление пользователя.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserById(request.Id);

            user.Login = request.Login;
            user.Role = request.Role;

            await _userService.UpdateUser(user);

            return new UserDto
            {
                Id = request.Id,
                Login = user.Login,
                Role = user.Role
            };
        }
    }
}
