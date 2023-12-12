using ManagementDocument.API.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace ManagementDocument.API.ExampleFilter
{
    /// <summary>
    /// Предоставляет пример данных для <see cref="AuthCommand"/> с использованием Swashbuckle.AspNetCore.Filters.
    /// </summary>
    public class AuthExampleFilter : IExamplesProvider<AuthCommand>
    {
        /// <summary>
        /// Получает пример экземпляра <see cref="AuthCommand"/>.
        /// </summary>
        /// <returns>Пример экземпляра <see cref="AuthCommand"/>.</returns>
        public AuthCommand GetExamples()
        {
            return new AuthCommand
            {
                Login = "Oleg2",
                Password = "147"
            };
        }
    }
}
