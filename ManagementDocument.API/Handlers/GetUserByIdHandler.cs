using ManagementDocument.API.Commands;
using ManagementDocument.API.Services;
using ManagementDocument.Database.Entities;
using MediatR;

namespace ManagementDocument.API.Handlers
{
    /// <summary>
    /// Обработчик команды получения пользователя по идентификатору.
    /// </summary>
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdCommand, User?>
    {
        #region Fields
        private readonly IUserService _userService;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetUserByIdHandler"/>.
        /// </summary>
        /// <param name="userService">Сервис для работы с пользователями.</param>
        public GetUserByIdHandler(IUserService userService) => _userService = userService;

        /// <summary>
        /// Обрабатывает запрос на получение пользователя по идентификатору.
        /// </summary>
        /// <param name="request">Запрос на получение пользователя.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Найденный пользователь или null, если пользователь не найден.</returns>
        public async Task<User?> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserById(request.Id);
        }
    }
}
