using ManagementDocument.Database.Entities;
using System.Collections.ObjectModel;

namespace ManagementDocument.API.Services
{
    /// <summary>
    /// Интерфейс для работы с типами документов.
    /// </summary>
    public interface IDocumentTypeService
    {
        /// <summary>
        /// Получает коллекцию всех типов документов.
        /// </summary>
        /// <returns>Коллекция типов документов.</returns>
        Task<ObservableCollection<DocumentType>> GetTypes();

        /// <summary>
        /// Проверяет существование типа документа по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор типа документа.</param>
        /// <returns>True, если тип документа существует, иначе false.</returns>
        public bool IsExistById(int id);
    }
}