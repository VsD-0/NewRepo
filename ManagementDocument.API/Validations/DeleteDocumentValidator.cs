using FluentValidation;
using ManagementDocument.API.Commands;
using ManagementDocument.API.Services;

namespace ManagementDocument.API.Validations
{
    /// <summary>
    /// Валидатор для команды удаления документа.
    /// </summary>
    public class DeleteDocumentValidator : AbstractValidator<DeleteDocumentCommand>
    {
        #region Fields
        private readonly IDocumentService _documentService;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DeleteDocumentValidator"/>.
        /// </summary>
        /// <param name="documentService">Сервис для работы с документами.</param>

        public DeleteDocumentValidator(IDocumentService documentService)
        {
            _documentService = documentService;

            // Правило валидации для идентификатора документа
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Это обязательное поле")
                .Must(IdValidation)
                .WithMessage("Документ не существует");
        }

        bool IdValidation(int id) => _documentService.IsExistById(id);
    }
}
