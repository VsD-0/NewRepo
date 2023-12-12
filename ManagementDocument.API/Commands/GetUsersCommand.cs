using ManagementDocument.Domain.Models;
using MediatR;

namespace ManagementDocument.API.Commands
{
    /// <summary>
    /// Команда получения списка пользователей на странице
    /// </summary>
    public class GetUsersCommand : IRequest<PaginatedResult<UserDto>>
    {
        /// <summary>
        /// Номер страницы
        /// </summary>
        /// <example>1</example>
        public int PageNumber { get; set; }

        /// <summary>
        /// Количество элементов на странице
        /// </summary>
        /// <example>5</example>
        public int PageSize { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        /// <example>-1</example>
        public int Role { get; set; }
    }
}
