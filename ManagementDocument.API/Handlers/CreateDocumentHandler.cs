using ManagementDocument.API.Commands;
using ManagementDocument.API.Exceptions;
using ManagementDocument.API.Services;
using ManagementDocument.Database.Entities;
using MediatR;

namespace ManagementDocument.API.Handlers
{
    /// <summary>
    /// Обработчик для создания документа с использованием <see cref="CreateDocumentCommand"/>.
    /// </summary>
    public class CreateDocumentHandler : IRequestHandler<CreateDocumentCommand, Document>
    {
        #region Fields
        private readonly IDocumentService _documentService;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CreateDocumentHandler"/>.
        /// </summary>
        /// <param name="documentService">Сервис документов.</param>
        public CreateDocumentHandler(IDocumentService documentService) => _documentService = documentService;

        /// <summary>
        /// Обрабатывает команду создания документа.
        /// </summary>
        /// <param name="request">Команда создания документа.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Созданный документ.</returns>
        public async Task<Document> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<CustomError>();

            errors.Add(new CustomError { Code = 20, Message = "Первая ошибка" });
            errors.Add(new CustomError { Code = 21, Message = "Вторая ошибка" });

            var doc = new Document
            {
                Doctype = request.DocType,
                Num = request.Num,
                Date = request.Date,
                Codeorg = request.CodeOrg,
                Org = request.Org,
                Birthdate = request.BirthDate
            };

            await _documentService.AddDocument(doc);

            return doc;
        }
    }
}
