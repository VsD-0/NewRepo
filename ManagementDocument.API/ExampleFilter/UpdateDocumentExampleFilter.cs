using ManagementDocument.API.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace ManagementDocument.API.ExampleFilter
{
    /// <summary>
    /// Предоставляет пример данных для <see cref="UpdateDocumentCommand"/> с использованием Swashbuckle.AspNetCore.Filters.
    /// </summary>
    public class UpdateDocumentExampleFilter : IExamplesProvider<UpdateDocumentCommand>
    {
        /// <summary>
        /// Получает пример экземпляра <see cref="UpdateDocumentCommand"/>.
        /// </summary>
        /// <returns>Пример экземпляра <see cref="UpdateDocumentCommand"/>.</returns>
        public UpdateDocumentCommand GetExamples()
        {
            return new UpdateDocumentCommand
            {
                DocType = 1,
                Num = "1234 567890",
                CodeOrg = "123-456",
                Org = "UpdateOrganization",
                Date = DateOnly.FromDateTime(new DateTime(2020, 1, 1)),
                BirthDate = DateOnly.FromDateTime(new DateTime(2000, 1, 1))
            };
        }
    }
}
