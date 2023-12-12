using ManagementDocument.API.Commands;
using ManagementDocument.API.Services;
using MediatR;

namespace ManagementDocument.API.Handlers
{
    /// <summary>
    /// Обработчик команды обновления документа.
    /// </summary>
    public class UpdateDocumentHandler : IRequestHandler<UpdateDocumentCommand>
    {
        #region Fields
        private readonly IDocumentService _documentService;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UpdateDocumentHandler"/>.
        /// </summary>
        /// <param name="documentService">Сервис для работы с документами.</param>
        public UpdateDocumentHandler(IDocumentService documentService) => _documentService = documentService;

        /// <summary>
        /// Обрабатывает запрос на обновление документа.
        /// </summary>
        /// <param name="request">Запрос на обновление документа.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        public async Task Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            var doc = await _documentService.GetDocument(request.Num ?? throw new ArgumentNullException(nameof(request.Num), "Номер документа не указан"), request.DocType);

            doc.Org = request.Org;
            doc.Codeorg = request.CodeOrg;
            doc.Date = request.Date;
            doc.Birthdate = request.BirthDate;

            await _documentService.UpdateDocument(doc);
        }
    }
}
