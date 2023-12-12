using ManagementDocument.API.Enums;
using ManagementDocument.Database.Entities;
using MediatR;

namespace ManagementDocument.API.Commands
{
    /// <summary>
    /// Команда сортировки списка документов
    /// </summary>
    public class GetSortDocumentsCommand : IRequest<List<Document>>
    {
        /// <summary>
        /// Список документов
        /// </summary>
        /// <example>
        /// {
        ///     {
        ///         DocType: 1,
        ///         ...
        ///     },
        ///     ...
        /// }
        /// </example>
        public List<Document>? Documents { get; set; }

        /// <summary>
        /// Список параметров
        /// </summary>
        /// <example>
        /// {
        ///     "Doctype",
        ///     "Num"
        /// }
        /// </example>
        public DocumentSortField[] Params { get; set; }
    }
}
