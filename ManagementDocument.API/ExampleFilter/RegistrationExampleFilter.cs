using ManagementDocument.API.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace ManagementDocument.API.ExampleFilter
{
    /// <summary>
    /// Предоставляет пример данных для <see cref="CreateUserCommand"/> с использованием Swashbuckle.AspNetCore.Filters.
    /// </summary>
    public class RegistrationExampleFilter : IExamplesProvider<CreateUserCommand>
    {
        /// <summary>
        /// Получает пример экземпляра <see cref="CreateUserCommand"/>.
        /// </summary>
        /// <returns>Пример экземпляра <see cref="CreateUserCommand"/>.</returns>
        public CreateUserCommand GetExamples()
        {
            return new CreateUserCommand
            {
                Login = "Ivan",
                Password = "123"
            };
        }
    }
}
