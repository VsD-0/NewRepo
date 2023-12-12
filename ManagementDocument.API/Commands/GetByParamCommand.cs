using ManagementDocument.Database.Entities;
using MediatR;

namespace ManagementDocument.API.Commands
{
    /// <summary>
    /// Команда получения документа по его параметру
    /// </summary>
    public class GetByParamCommand : IRequest<List<Document>>
    {
        /// <summary>
        /// Имя параметра
        /// </summary>
        /// <example>Codeorg</example>
        public string? Param { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        /// <example>123-456</example>
        public string? Value { get; set; }

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
    }
}
