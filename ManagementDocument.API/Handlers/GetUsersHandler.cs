using ManagementDocument.API.Commands;
using ManagementDocument.API.Services;
using ManagementDocument.Domain.Models;
using MediatR;

namespace ManagementDocument.API.Handlers
{
    /// <summary>
    /// Обработчик команды получения пользователей с пагинацией.
    /// </summary>
    public class GetUsersHandler : IRequestHandler<GetUsersCommand, PaginatedResult<UserDto>>
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="GetUsersHandler"/>.
        /// </summary>
        /// <param name="userService">Сервис пользователей.</param>
        public GetUsersHandler(IUserService userService) => _userService = userService;

        /// <summary>
        /// Обрабатывает запрос на получение пользователей с пагинацией.
        /// </summary>
        /// <param name="request">Запрос на получение пользователей.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат пагинированной выборки пользователей.</returns>
        public async Task<PaginatedResult<UserDto>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
        {
            List<UserDto> users;

            if (request.Role != -1)
                users = (await _userService.GetUsersDto()).Where(u => u.Role == request.Role).ToList();
            else
                users = await _userService.GetUsersDto();

            int totalItems = users.Count;

            int skip = (request.PageNumber - 1) * request.PageSize;

            users = new(users.Skip(skip).Take(request.PageSize));

            var pagination = new PaginationResult<UserDto>
            {
                CurrentPage = request.PageNumber,
                PageSize = request.PageSize,
                TotalItems = totalItems
            };

            return new PaginatedResult<UserDto>
            {
                Pagination = pagination,
                Items = users
            };
        }
    }
}
