using ManagementDocument.API.Commands;
using ManagementDocument.API.Services;
using ManagementDocument.Database.Entities;
using MediatR;

namespace ManagementDocument.API.Handlers
{
    /// <summary>
    /// Обработчик для получения документа по его идентификатору.
    /// </summary>
    public class GetByIdHandler : IRequestHandler<GetByIdCommand, Document?>
    {
        #region Fields
        private readonly IDocumentService _documentService;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="GetByIdHandler"/>.
        /// </summary>
        /// <param name="documentService">Сервис документов.</param>
        public GetByIdHandler(IDocumentService documentService) => _documentService = documentService;

        /// <summary>
        /// Обрабатывает команду получения документа по его идентификатору.
        /// </summary>
        /// <param name="request">Команда получения документа.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        public async Task<Document?> Handle(GetByIdCommand request, CancellationToken cancellationToken)
        {
            var docs = await _documentService.GetDocuments();

            return docs.FirstOrDefault(d => d.Id == request.Id);
        }
    }
}
