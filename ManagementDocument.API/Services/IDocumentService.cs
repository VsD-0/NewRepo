using ManagementDocument.Database.Entities;

namespace ManagementDocument.API.Services
{
    /// <summary>
    /// Интерфейс для работы с документами.
    /// </summary>
    public interface IDocumentService
    {
        /// <summary>
        /// Получает список всех документов.
        /// </summary>
        /// <returns>Список документов.</returns>
        public Task<List<Document>> GetDocuments();

        /// <summary>
        /// Получает документ по его номеру и типу.
        /// </summary>
        /// <param name="num">Номер документа.</param>
        /// <param name="type">Тип документа.</param>
        /// <returns>Документ.</returns>
        public Task<Document> GetDocument(string num, int type);

        /// <summary>
        /// Добавляет новый документ.
        /// </summary>
        /// <param name="doc">Документ для добавления.</param>
        public Task AddDocument(Document doc);

        /// <summary>
        /// Удаляет документ.
        /// </summary>
        /// <param name="doc">Документ для удаления.</param>
        public Task DeleteDocument(Document doc);

        /// <summary>
        /// Обновляет информацию о документе.
        /// </summary>
        /// <param name="doc">Документ для обновления.</param>
        public Task UpdateDocument(Document doc);

        /// <summary>
        /// Проверяет существование документа по номеру и типу.
        /// </summary>
        /// <param name="num">Номер документа.</param>
        /// <param name="type">Тип документа.</param>
        /// <returns>True, если документ существует, иначе false.</returns>
        public bool IsExist(string num, int type);

        /// <summary>
        /// Проверяет существование документа по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор документа.</param>
        /// <returns>True, если документ существует, иначе false.</returns>
        public bool IsExistById(int id);
    }
}
