using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ManagementDocument.API.Commands
{
    /// <summary>
    /// Команда обновления документа
    /// </summary>
    public class UpdateDocumentCommand : IRequest
    {
        /// <summary>
        /// Тип документа
        /// </summary>
        /// <example>1</example>
        [FromBody]
        [BindRequired]
        public int DocType { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        /// <example>1234 567890</example>
        [FromBody]
        [BindRequired]
        public string? Num { get; set; }

        /// <summary>
        /// Дата получения документа
        /// </summary>
        /// <example>2020-01-01</example>
        [FromBody]
        [BindRequired]
        public DateOnly Date { get; set; }

        /// <summary>
        /// Код организации
        /// </summary>
        /// <example>123-456</example>
        [FromBody]
        [BindRequired]
        public string? CodeOrg { get; set; }

        /// <summary>
        /// Организация
        /// </summary>
        /// <example>Организация 1</example>
        [FromBody]
        [BindRequired]
        public string? Org { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        /// <example>2000-01-01</example>
        [FromBody]
        [BindRequired]
        public DateOnly BirthDate { get; set; }
    }
}
