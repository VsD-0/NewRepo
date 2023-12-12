using ManagementDocument.Domain.Models;
using MediatR;

namespace ManagementDocument.API.Commands
{
    /// <summary>
    /// Команда обновления пользователя
    /// </summary>
    public class UpdateUserCommand : IRequest<UserDto>
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string? Login { get; set; } = null!;

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        public int Role { get; set; }
    }
}
