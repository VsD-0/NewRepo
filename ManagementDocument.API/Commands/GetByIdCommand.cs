using ManagementDocument.Database.Entities;
using MediatR;

namespace ManagementDocument.API.Commands
{
    /// <summary>
    /// Команда получения документа по его Id
    /// </summary>
    public class GetByIdCommand : IRequest<Document?>
    {
        /// <summary>
        /// Id документа
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
    }
}
