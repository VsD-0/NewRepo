using MediatR;

namespace ManagementDocument.API.Commands
{
    /// <summary>
    /// Команда удаления документа
    /// </summary>
    public class DeleteDocumentCommand : IRequest
    {
        /// <summary>
        /// Id документа
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
    }
}
