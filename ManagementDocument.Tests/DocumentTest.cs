using FluentValidation.TestHelper;
using ManagementDocument.API.Commands;
using ManagementDocument.API.Services;
using ManagementDocument.API.Validations;
using ManagementDocument.Database.Context;
using ManagementDocument.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace ManagementDocument.Tests
{
    /// <summary>
    /// Тесты для операций с документами.
    /// </summary>
    public class DocumentTest : IDisposable
    {
        #region Fields
        private readonly ManagementDocumentDbContext _context;
        private readonly IDocumentService _documentService;
        private readonly IDocumentTypeService _documentTypeService;
        private readonly CreateDocumentValidator _createDocumentValidator;
        #endregion Fields

        /// <summary>
        /// Инициализация контекста и сервисов перед запуском каждого теста.
        /// </summary>
        public DocumentTest()
        {
            var options = new DbContextOptionsBuilder<ManagementDocumentDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _context = new ManagementDocumentDbContext(options);

            _context.DocumentTypes.AddRange(new ObservableCollection<DocumentType>
            {
                new DocumentType
                {
                   Id = 1,
                   Type = "Паспорт гражданина РФ"
                },
                new DocumentType
                {
                    Id = 2,
                    Type = "Загранпаспорт гражданина РФ"
                },
                new DocumentType
                {
                    Id = 3,
                    Type = "Военный билет"
                }
            });

            _context.Documents.AddRange(new ObservableCollection<Document>
            {
                new Document
                {
                    Id = 1,
                    Doctype = 1,
                    Num = "1234 567891",
                    Date = new DateOnly(2021, 01, 01),
                    Codeorg = "111-111",
                    Org = "Org1",
                    Birthdate = new DateOnly(2001, 01, 01)
                },
                new Document
                {
                    Id = 2,
                    Doctype = 2,
                    Num = "1234 567892",
                    Date = new DateOnly(2022, 02, 02),
                    Codeorg = "222-222",
                    Org = "Org2",
                    Birthdate = new DateOnly(2002, 02, 02)
                },
                new Document
                {
                    Id = 3,
                    Doctype = 3,
                    Num = "1234 567890",
                    Date = new DateOnly(2023, 03, 03),
                    Codeorg = "333-333",
                    Org = "Org3",
                    Birthdate = new DateOnly(2003, 03, 03)
                }
            });

            _context.SaveChanges();

            _documentService = new DocumentService(_context);
            _documentTypeService = new DocumentTypeService(_context);
            _createDocumentValidator = new(_documentService, _documentTypeService);
        }

        #region Tests
        /// <summary>
        /// Тест на получение всех документов.
        /// </summary>
        [Fact]
        public async Task GetDocuments_ShouldReturnAllDocuments()
        {
            var documentService = new DocumentService(_context);

            var documents = await documentService.GetDocuments();

            Assert.Equal(3, documents.Count);
        }

        /// <summary>
        /// Тест на получение документа по номеру и типу.
        /// </summary>
        [Fact]
        public async Task GetDocument_ShouldReturnDocumentByNumAndType()
        {
            var documentService = new DocumentService(_context);

            var document = await documentService.GetDocument("1234 567891", 1);

            Assert.NotNull(document);
            Assert.Equal("1234 567891", document.Num);
            Assert.Equal(1, document.Doctype);
        }

        /// <summary>
        /// Тест на добавление нового документа.
        /// </summary>
        [Fact]
        public async Task AddDocument_ShouldAddNewDocument()
        {
            var documentService = new DocumentService(_context);
            var newDocument = new Document
            {
                Id = 4,
                Doctype = 1,
                Num = "9999 888888",
                Date = new DateOnly(2023, 07, 26),
                Codeorg = "444-444",
                Org = "Org4",
                Birthdate = new DateOnly(2009, 07, 26)
            };

            await documentService.AddDocument(newDocument);
            var addedDocument = await documentService.GetDocument("9999 888888", 1);

            Assert.NotNull(addedDocument);
            Assert.Equal(4, addedDocument.Id);
        }

        /// <summary>
        /// Тест на удаление документа из базы данных.
        /// </summary>
        [Fact]
        public async Task DeleteDocument_ShouldRemoveDocumentFromDatabase()
        {
            var documentService = new DocumentService(_context);
            var documentToDelete = await documentService.GetDocument("1234 567891", 1);

            await documentService.DeleteDocument(documentToDelete);
            var deletedDocument = await documentService.GetDocument("1234 567891", 1);

            Assert.Null(deletedDocument);
        }

        /// <summary>
        /// Тест на проверку валидации поля DocType при значении null.
        /// </summary>
        [Fact]
        public async void DocType_Null_ShouldHaveValidationError()
        {
            var command = new CreateDocumentCommand
            {
                Num = "1234 567890",
                Date = DateOnly.Parse("2023.05.05"),
                CodeOrg = "555-555",
                Org = "Org5",
                BirthDate = DateOnly.Parse("2003.05.05")
            };

            var result = await _createDocumentValidator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.DocType);
        }

        /// <summary>
        /// Тест на проверку валидации поля DocType при недопустимом значении.
        /// </summary>
        [Fact]
        public async Task DocType_InvalidValue_ShouldHaveValidationErrorAsync()
        {
            var command = new CreateDocumentCommand
            {
                DocType = 99,
                Num = "1234 567890",
                Date = DateOnly.Parse("2023.05.05"),
                CodeOrg = "555-555",
                Org = "Org5",
                BirthDate = DateOnly.Parse("2003.05.05")
            };

            var result = await _createDocumentValidator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.DocType);
        }

        /// <summary>
        /// Тест на проверку валидации поля Num при значении null.
        /// </summary>
        [Fact]
        public async Task Num_Null_ShouldHaveValidationErrorAsync()
        {
            var command = new CreateDocumentCommand
            {
                DocType = 1,
                Num = null,
                Date = DateOnly.Parse("2023.05.05"),
                CodeOrg = "555-555",
                Org = "Org5",
                BirthDate = DateOnly.Parse("2003.05.05")
            };

            var result = await _createDocumentValidator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.Num);
        }

        /// <summary>
        /// Тест на проверку валидации поля Num при наличии уже существующего документа с таким номером.
        /// </summary>
        [Fact]
        public async Task NumValidation_DocumentAlreadyExists_ShouldHaveValidationErrorAsync()
        {
            var command = new CreateDocumentCommand
            {
                DocType = 1,
                Num = "1234 567891",
                Date = DateOnly.Parse("2023.05.05"),
                CodeOrg = "555-555",
                Org = "Org5",
                BirthDate = DateOnly.Parse("2003.05.05")
            };

            var result = await _createDocumentValidator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.Num);
        }

        /// <summary>
        /// Тест на проверку валидации поля BirthDate при указании некорректной даты.
        /// </summary>
        [Fact]
        public async Task BirthDateValidation_DateError_ShouldHaveValidationErrorAsync()
        {
            var command = new CreateDocumentCommand
            {
                DocType = 1,
                Num = "1234 567946",
                Date = DateOnly.Parse("2023.05.05"),
                CodeOrg = "555-555",
                Org = "Org5",
                BirthDate = DateOnly.Parse("2020.05.05")
            };

            var result = await _createDocumentValidator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.BirthDate);
        }

        /// <summary>
        /// Тест на проверку валидации поля CodeOrg при значении null.
        /// </summary>
        [Fact]
        public async Task CodeOrg_Null_ShouldHaveValidationErrorAsync()
        {
            var command = new CreateDocumentCommand
            {
                DocType = 3,
                Num = "1234 567882",
                Date = DateOnly.Parse("2023.05.05"),
                CodeOrg = null,
                Org = "Org5",
                BirthDate = DateOnly.Parse("2003.05.05")
            };

            var result = await _createDocumentValidator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.CodeOrg);
        }

        /// <summary>
        /// Тест на проверку валидации поля Org при значении null.
        /// </summary>
        [Fact]
        public async Task Org_Null_ShouldHaveValidationErrorAsync()
        {
            var command = new CreateDocumentCommand
            {
                DocType = 2,
                Num = "1234 567865",
                Date = DateOnly.Parse("2023.05.05"),
                CodeOrg = "555-555",
                Org = null,
                BirthDate = DateOnly.Parse("2003.05.05")
            };

            var result = await _createDocumentValidator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(x => x.Org);
        }
        #endregion Tests

        /// <summary>
        /// Освобождение ресурсов после выполнения тестов.
        /// </summary>
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}