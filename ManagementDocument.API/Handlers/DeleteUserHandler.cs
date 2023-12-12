using ManagementDocument.API.Commands;
using ManagementDocument.API.Services;
using MediatR;

namespace ManagementDocument.API.Handlers
{
    /// <summary>
    /// Обработчик для удаления пользователя/>.
    /// </summary>
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
    {
        #region Fields
        private readonly IUserService _userService;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DeleteUserHandler"/>.
        /// </summary>
        /// <param name="userService">Сервис пользователей.</param>
        public DeleteUserHandler(IUserService userService) => _userService = userService;

        /// <summary>
        /// Обрабатывает команду удаления пользователя.
        /// </summary>
        /// <param name="request">Команда удаления пользователя.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetUsers();
            var user = users.FirstOrDefault(d => d.Id == request.Id) ?? throw new ArgumentNullException(nameof(request.Id), "Документ не найден");
            await _userService.DeleteUser(user);
        }
    }
}
