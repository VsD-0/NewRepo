using FluentValidation;
using ManagementDocument.API.Commands;
using ManagementDocument.API.Services;

namespace ManagementDocument.API.Validations
{
    /// <summary>
    /// Валидатор для команды обновления информации о документе.
    /// </summary>
    public class UpdateDocumentValidator : AbstractValidator<UpdateDocumentCommand>
    {
        #region Fields
        private readonly IDocumentService _documentService;
        private readonly IDocumentTypeService _documentTypeService;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UpdateDocumentValidator"/>.
        /// </summary>
        /// <param name="documentService">Сервис для работы с документами.</param>
        /// <param name="documentTypeService">Сервис для работы с типами документов.</param>
        public UpdateDocumentValidator(IDocumentService documentService, IDocumentTypeService documentTypeService)
        {
            _documentService = documentService;
            _documentTypeService = documentTypeService;

            // Общие проверки
            RuleFor(x => x.DocType)
                .NotNull()
                .WithMessage("Это обязательное поле")
                .Must(IsExistDocType)
                .WithMessage("Этот тип документа не существует");

            // Для паспорта РФ
            When(x => x.DocType == 1, () =>
            {
                RuleFor(x => x)
                .NotEmpty()
                .WithMessage("Это обязательное поле")
                .Must(NumValidation)
                .WithName("Num")
                .WithMessage("Документ с таким номером не существует");

                RuleFor(x => x)
                    .NotEmpty()
                    .WithName("BirthDate")
                    .WithMessage("Это обязательное поле")
                    .WithName("BirthDate")
                    .Must(BirthDateValidation)
                    .WithName("BirthDate")
                    .WithMessage("Дата выдачи > 14 лет, или обновление в 20 / 45 лет");

                RuleFor(x => x.CodeOrg)
                    .NotEmpty()
                    .WithMessage("Это обязательное поле")
                    .Matches(@"^\d{3}-\d{3}$")
                    .WithMessage("Неправильный формат");

                RuleFor(x => x.Num)
                    .NotEmpty()
                    .WithMessage("Это обязательное поле")
                    .Matches(@"^\d{4} \d{6}$")
                    .WithMessage("Неправильный формат");
            });

            // Для загранпаспорт гражданина РФ
            When(x => x.DocType == 2, () =>
            {
                RuleFor(x => x)
                .NotEmpty()
                .WithName("Num")
                .WithMessage("Это обязательное поле")
                .Must(NumValidation)
                .WithName("Num")
                .WithMessage("Документ с таким номером не существует");
            });

            // Для военного билета
            When(x => x.DocType == 3, () =>
            {
                RuleFor(x => x)
                .NotEmpty()
                .WithName("Num")
                .WithMessage("Это обязательное поле")
                .Must(NumValidation)
                .WithName("Num")
                .WithMessage("Документ с таким номером не существует");
            });

            // Для остальных документов
            RuleFor(x => x.Date)
                    .NotEmpty()
                    .WithMessage("Это обязательное поле")
                    .Must(DateValidation)
                    .WithMessage("Дата выдачи раньше текущей");

            RuleFor(x => x.BirthDate)
                    .NotEmpty()
                    .WithMessage("Это обязательное поле")
                    .Must(DateValidation)
                    .WithMessage("Дата выдачи раньше текущей");

            RuleFor(x => x.CodeOrg)
                    .NotEmpty()
                    .WithMessage("Это обязательное поле");

            RuleFor(x => x.Org)
                    .NotEmpty()
                    .WithMessage("Это обязательное поле");

        }
        bool NumValidation(UpdateDocumentCommand doc) => _documentService.IsExist(doc.Num ?? throw new ArgumentNullException(nameof(doc.Num), "Номер документа является null"), doc.DocType);
        bool IsExistDocType(int type) => _documentTypeService.IsExistById(type);

        bool BirthDateValidation(UpdateDocumentCommand doc)
        {
            if (string.IsNullOrEmpty(doc.BirthDate.ToString())) return false;

            DateOnly date = doc.Date;
            DateOnly birthDate = doc.BirthDate;
            int age = date.Year - birthDate.Year;
            if (date.Month < birthDate.Month || (date.Month == birthDate.Month && date.Day < birthDate.Day)) age--;
            return age >= 14;
        }

        bool DateValidation(DateOnly d)
        {
            if (string.IsNullOrEmpty(d.ToString()))
                return false;

            DateOnly date = d;
            return date < DateOnly.FromDateTime(DateTime.Today);
        }

    }
}
