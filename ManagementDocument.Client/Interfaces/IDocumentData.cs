using ManagementDocument.Database.Entities;
using ManagementDocument.Domain.Models;
using Refit;

namespace ManagementDocument.Client.Interfaces
{
    public interface IDocumentData
    {
        [Get("/Documents/GetSearchedDocuments")]
        Task<PaginatedResult<Document>> GetSearchedDocuments(int type, string param, string paramValue, int pageNumber, int pageSize);

        [Post("/Documents/CreateDocument")]
        Task<Document> CreateDocument([Body] Document doc);


        [Put("/Documents/UpdateDocument")]
        Task UpdateDocument([Body] Document doc);


        [Delete("/Documents/DeleteDocument/{id}")]
        Task DeleteDocument(int id);
    }
}
