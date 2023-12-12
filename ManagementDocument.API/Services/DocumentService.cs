using ManagementDocument.Database.Context;
using ManagementDocument.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementDocument.API.Services
{
    /// <summary>
    /// Сервис для работы с документами.
    /// </summary>
    public class DocumentService : IDocumentService
    {
        #region Fields
        private readonly ManagementDocumentDbContext _context;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DocumentService"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public DocumentService(ManagementDocumentDbContext context) => _context = context;


        /// <summary>
        /// Получает список всех документов.
        /// </summary>
        /// <returns>Список документов.</returns>
        public async Task<List<Document>> GetDocuments()
        {
            return await _context.Documents.ToListAsync();
        }

        /// <summary>
        /// Получает документ по номеру и типу.
        /// </summary>
        /// <param name="num">Номер документа.</param>
        /// <param name="type">Тип документа.</param>
        /// <returns>Документ.</returns>
        public async Task<Document> GetDocument(string num, int type)
        {
            return await _context.Documents.Where(d => d.Doctype == type).FirstOrDefaultAsync(d => d.Num == num) ?? throw new ArgumentNullException(nameof(num), "Документ не найден");
        }

        /// <summary>
        /// Добавляет документ в базу данных.
        /// </summary>
        /// <param name="doc">Документ для добавления.</param>
        public async Task AddDocument(Document doc)
        {
            await _context.Documents.AddAsync(doc);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет документ из базы данных.
        /// </summary>
        /// <param name="doc">Документ для удаления.</param>
        public async Task DeleteDocument(Document doc)
        {
            _context.Documents.Remove(doc);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновляет информацию о документе в базе данных.
        /// </summary>
        /// <param name="doc">Документ для обновления.</param>
        public async Task UpdateDocument(Document doc)
        {
            _context.Documents.Update(doc);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Проверяет существование документа по номеру и типу.
        /// </summary>
        /// <param name="num">Номер документа.</param>
        /// <param name="type">Тип документа.</param>
        /// <returns>True, если документ существует, иначе false.</returns>
        public bool IsExist(string num, int type)
        {
            return _context.Documents.Where(d => d.Doctype == type)
                                     .Any(x => x.Num == num);
        }

        /// <summary>
        /// Проверяет существование документа по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор документа.</param>
        /// <returns>True, если документ существует, иначе false.</returns>
        public bool IsExistById(int id)
        {
            return _context.Documents.Any(x => x.Id == id);
        }
    }
}
