using ManagementDocument.Database.Entities;
using ManagementDocument.Domain.Models;
using MediatR;

namespace ManagementDocument.API.Commands
{
    /// <summary>
    /// Команда получения списка документов на странице
    /// </summary>
    public class GetDocumentsCommand : IRequest<PaginatedResult<Document>>
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
        /// Список документов
        /// </summary>
        /// <example>5</example>
        public List<Document>? Documents { get; set; }
    }
}
