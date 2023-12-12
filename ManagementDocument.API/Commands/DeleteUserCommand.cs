using MediatR;

namespace ManagementDocument.API.Commands
{
    /// <summary>
    /// Команда удаления пользователя
    /// </summary>
    public class DeleteUserCommand : IRequest
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
    }
}
