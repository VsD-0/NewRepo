using ManagementDocument.API.Commands;
using ManagementDocument.API.Services;
using MediatR;

namespace ManagementDocument.API.Handlers
{
    /// <summary>
    /// Обработчик для удаления документа по команде <see cref="DeleteDocumentCommand"/>.
    /// </summary>
    public class DeleteDocumentHandler : IRequestHandler<DeleteDocumentCommand>
    {
        #region Fields
        private readonly IDocumentService _documentService;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DeleteDocumentHandler"/>.
        /// </summary>
        /// <param name="documentService">Сервис документов.</param>
        public DeleteDocumentHandler(IDocumentService documentService) => _documentService = documentService;

        /// <summary>
        /// Обрабатывает команду удаления документа.
        /// </summary>
        /// <param name="request">Команда удаления документа.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        public async Task Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            var docs = await _documentService.GetDocuments();
            var doc = docs.FirstOrDefault(d => d.Id == request.Id) ?? throw new ArgumentNullException(nameof(request.Id), "Документ не найден");
            await _documentService.DeleteDocument(doc);
        }
    }
}
