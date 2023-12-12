using ManagementDocument.API.Commands;
using ManagementDocument.API.Enums;
using ManagementDocument.API.Services;
using ManagementDocument.Database.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ManagementDocument.API.Handlers
{
    public class GetSortDocumentsHandler : IRequestHandler<GetSortDocumentsCommand, List<Document>>
    {
        private readonly IDocumentService _documentService;

        public GetSortDocumentsHandler(IDocumentService documentService) => _documentService = documentService;

        public Task<List<Document>> Handle(GetSortDocumentsCommand request, CancellationToken cancellationToken)
        {
            // Список документов после фильтрации
            List<Document> docs = request.Documents;
            // Список параметров для сортировки
            DocumentSortField[] sorts = request.Params;

            sorts.Reverse();

            foreach (var sort in sorts)
            {
                string sortField = sort.ToString();
                bool descending = sortField.StartsWith("_");

                if (descending)
                    sortField = sortField.Substring(1);

                docs = ApplySort(docs, sortField, descending);
            }

            return Task.FromResult(docs);
        }

        private List<Document> ApplySort(List<Document> documents, string sortField, bool descending)
        {
            var property = typeof(Document).GetProperty(sortField);
            if (property == null)
            {
                throw new ArgumentException($"Неверное поле сортировки: {sortField}");
            }

            if (descending)
            {
                return documents.OrderByDescending(doc => property.GetValue(doc)).ToList();
            }
            else
            {
                return documents.OrderBy(doc => property.GetValue(doc)).ToList();
            }
        }
    }
}
