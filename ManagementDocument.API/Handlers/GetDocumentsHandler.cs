using ManagementDocument.API.Commands;
using ManagementDocument.Database.Entities;
using ManagementDocument.Domain.Models;
using MediatR;

namespace ManagementDocument.API.Handlers
{
    /// <summary>
    /// Обработчик команды получения документов с пагинацией.
    /// </summary>
    public class GetDocumentsHandler : IRequestHandler<GetDocumentsCommand, PaginatedResult<Document>>
    {
        /// <summary>
        /// Обрабатывает запрос на получение документов с пагинацией.
        /// </summary>
        /// <param name="request">Запрос на получение документов.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат пагинированной выборки документов.</returns>
        public Task<PaginatedResult<Document>> Handle(GetDocumentsCommand request, CancellationToken cancellationToken)
        {
            var docs = request.Documents ?? throw new ArgumentNullException(nameof(request.Documents), "Список документов не существует");

            int totalItems = docs.Count;

            int skip = (request.PageNumber - 1) * request.PageSize;

            List<Document> documents = new(docs.Skip(skip).Take(request.PageSize));

            var pagination = new PaginationResult<Document>
            {
                CurrentPage = request.PageNumber,
                PageSize = request.PageSize,
                TotalItems = totalItems
            };

            return Task.FromResult(new PaginatedResult<Document>
            {
                Pagination = pagination,
                Items = documents
            });
        }
    }
}
