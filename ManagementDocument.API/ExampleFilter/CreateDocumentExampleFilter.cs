using ManagementDocument.API.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace ManagementDocument.API.ExampleFilter
{
    /// <summary>
    /// Предоставляет пример данных для <see cref="CreateDocumentCommand"/> с использованием Swashbuckle.AspNetCore.Filters.
    /// </summary>
    public class CreateDocumentExampleFilter : IExamplesProvider<CreateDocumentCommand>
    {
        /// <summary>
        /// Получает пример экземпляра <see cref="CreateDocumentCommand"/>.
        /// </summary>
        /// <returns>Пример экземпляра <see cref="CreateDocumentCommand"/>.</returns>
        public CreateDocumentCommand GetExamples()
        {
            return new CreateDocumentCommand
            {
                DocType = 1,
                Num = "1234 567890",
                CodeOrg = "123-456",
                Org = "Organization",
                Date = DateOnly.FromDateTime(new DateTime(2020, 1, 1)),
                BirthDate = DateOnly.FromDateTime(new DateTime(2000, 1, 1)),
            };
        }
    }


}
