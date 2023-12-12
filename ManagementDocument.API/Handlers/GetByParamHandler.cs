using ManagementDocument.API.Commands;
using ManagementDocument.Database.Entities;
using MediatR;

namespace ManagementDocument.API.Handlers
{
    /// <summary>
    /// Обработчик команды получения документов по параметру.
    /// </summary>
    public class GetByParamHandler : IRequestHandler<GetByParamCommand, List<Document>>
    {
        /// <summary>
        /// Обрабатывает запрос на получение документов по параметру.
        /// </summary>
        /// <param name="request">Запрос на получение документов по параметру.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список документов, соответствующих параметру.</returns>
        public Task<List<Document>> Handle(GetByParamCommand request, CancellationToken cancellationToken)
        {
            return request.Param switch
            {
                "DocType" => Task.FromResult(request.Documents
                                        ?.Where(d => d.Doctype.ToString() == request.Value)
                                        .ToList()
                                        ?? throw new ArgumentNullException(nameof(request.Param), "Произошла ошибка")),

                "Num" => Task.FromResult(request.Documents
                                        ?.Where(d => d.Num == request.Value)
                                        .ToList()
                                        ?? throw new ArgumentNullException(nameof(request.Param), "Произошла ошибка")),

                "Date" => Task.FromResult(request.Documents
                                        ?.Where(d => d.Date.ToString() == request.Value)
                                        .ToList()
                                        ?? throw new ArgumentNullException(nameof(request.Param), "Произошла ошибка")),

                "CodeOrg" => Task.FromResult(request.Documents
                                        ?.Where(d => d.Codeorg == request.Value)
                                        .ToList()
                                        ?? throw new ArgumentNullException(nameof(request.Param), "Произошла ошибка")),

                "Org" => Task.FromResult(request.Documents
                                        ?.Where(d => d.Org == request.Value)
                                        .ToList()
                                        ?? throw new ArgumentNullException(nameof(request.Param), "Произошла ошибка")),

                "BirthDate" => Task.FromResult(request.Documents
                                        ?.Where(d => d.Birthdate.ToString() == request.Value)
                                        .ToList()
                                        ?? throw new ArgumentNullException(nameof(request.Param), "Произошла ошибка")),

                _ => throw new ArgumentNullException(nameof(request.Param), "Этот параметр не существует"),
            };
        }
    }
}
