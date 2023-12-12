using ManagementDocument.Database.Context;
using ManagementDocument.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace ManagementDocument.API.Services
{
    /// <summary>
    /// Сервис для работы с типами документов.
    /// </summary>
    public class DocumentTypeService : IDocumentTypeService
    {
        #region Fields
        private readonly ManagementDocumentDbContext _context;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DocumentTypeService"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public DocumentTypeService(ManagementDocumentDbContext context) => _context = context;

        /// <summary>
        /// Получает коллекцию всех типов документов.
        /// </summary>
        /// <returns>Коллекция типов документов.</returns>
        public async Task<ObservableCollection<DocumentType>> GetTypes() => new(await _context.DocumentTypes.ToListAsync());

        /// <summary>
        /// Проверяет существование типа документа по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор типа документа.</param>
        /// <returns>True, если тип документа существует, иначе false.</returns>
        public bool IsExistById(int id)
        {
            return _context.DocumentTypes.Any(x => x.Id == id);
        }
    }
}
