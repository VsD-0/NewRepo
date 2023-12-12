using ManagementDocument.Database.Entities;
using MediatR;

namespace ManagementDocument.API.Commands
{
    /// <summary>
    /// Команда получения пользователя по его Id
    /// </summary>
    public class GetUserByIdCommand : IRequest<User?>
    {
        /// <summary>
        /// Id документа
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
    }
}
