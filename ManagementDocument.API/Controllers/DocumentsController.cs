using ManagementDocument.API.Commands;
using ManagementDocument.API.Enums;
using ManagementDocument.API.Exceptions;
using ManagementDocument.API.ModelBinders;
using ManagementDocument.API.Services;
using ManagementDocument.Database.Entities;
using ManagementDocument.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace ManagementDocument.API.Controllers
{
    /// <summary>
    /// Контроллер управления документами
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly IDocumentService _documentService;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DocumentsController"/>.
        /// </summary>
        /// <param name="mediator">Экземпляр интерфейса IMediator.</param>
        /// <param name="documentService">Сервис документов.</param>
        public DocumentsController(IMediator mediator, IDocumentService documentService)
        {
            _mediator = mediator;
            _documentService = documentService;
        }

        /// <param name="type">Тип документа</param>
        /// <param name="param">Параметр для фильтра</param>
        /// <param name="paramValue">Значение параметра</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Количество элементов на странице</param>
        /// <param name="sort">Параметры сортировки</param>
        /// <returns>Страница с документами.</returns>
        [HttpGet("GetSearchedDocuments")]
        [Authorize(Roles = "1, 2")]
        [SwaggerOperation(Summary = "Получение страницы с документами",
                          Description = "Фильтрация списка документов по типу и по выбранному параметру. Возвращяет страницу с документами.",
                          OperationId = "GetSearchedDocuments")]
        [SwaggerResponse(200, "Успешное выполнение", typeof(PaginationResult<Document>))]
        public async Task<ActionResult<PaginatedResult<Document>>> GetSearchedDocuments(int type, string param, string paramValue, int pageNumber, int pageSize)//, [FromQuery][ModelBinder(BinderType = typeof(DocumentSortModelBinder))] DocumentSortField[] sort)
        {
            // Получение всего списка документов
            var documents = await _documentService.GetDocuments();

            // если тип документа не равен -1, то провести фильтрацию по типу
            if (type != -1)
                documents = await _mediator.Send(new GetByParamCommand { Param = "DocType", Value = type.ToString(), Documents = documents });

            // если параметр документа не является "noparam", то провести фильтрацию по параметру
            if (param != "noparam")
                documents = await _mediator.Send(new GetByParamCommand { Param = param, Value = paramValue, Documents = documents });

            // Сортировка по параметрам
            //if (sort.Length > 0)
            //    documents = await _mediator.Send(new GetSortDocumentsCommand { Documents = documents, Params = sort });

            // Возвращение указанной страницы
            return Ok(await _mediator.Send(new GetDocumentsCommand { PageNumber = pageNumber, PageSize = pageSize, Documents = documents }));
        }

        /// <param name="id">Id документа</param>
        /// <returns>Документ</returns>
        [HttpGet("GetById/{id}")]
        [Authorize(Roles = "1, 2")]
        [SwaggerOperation(Summary = "Возвращает документ по Id",
                          OperationId = "GetDocumentById")]
        [SwaggerResponse(200, "Успешное выполнение", typeof(Document))]
        [SwaggerResponse(404, "Не удалось найти документ")]
        public async Task<ActionResult> GetById(int id)
        {
            var doc = await _mediator.Send(new GetByIdCommand { Id = id });
            return doc is not null ? Ok(doc) : NotFound();
        }

        /// <param name="createDocumentCommand">Документ</param>
        /// <returns>Документ</returns>
        [HttpPost("CreateDocument")]
        [Authorize(Roles = "2")]
        [SwaggerOperation(Summary = "Создаёт новый документ",
                          Description = "Создаёт новый документ и возвращает этот документ",
                          OperationId = "CreateDocument")]
        [SwaggerResponse(201, "Успешное выполнение", typeof(Document))]
        [SwaggerResponse(400, "Произошла ошибка", typeof(CustomException))]
        public async Task<ActionResult> CreateDocument([FromBody] CreateDocumentCommand createDocumentCommand)
        {
            try
            {
                var result = await _mediator.Send(createDocumentCommand);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        /// <param name="updateDocumentCommand">Документ</param>
        [HttpPut("UpdateDocument")]
        [Authorize(Roles = "2")]
        [SwaggerOperation(Summary = "Обновляет существующий документ",
                          OperationId = "UpdateDocument")]
        [SwaggerResponse(204, "Успешное выполнение")]
        public async Task<ActionResult> UpdateDocument([FromBody] UpdateDocumentCommand updateDocumentCommand)
        {
            await _mediator.Send(updateDocumentCommand);
            return NoContent();
        }

        /// <param name="deleteDocumentCommand">Id документа</param>
        [HttpDelete("DeleteDocument/{id}")]
        [Authorize(Roles = "2")]
        [SwaggerOperation(Summary = "Удаляет существующий документ",
                          OperationId = "DeleteDocument")]
        [SwaggerResponse(204, "Успешное выполнение")]
        public async Task<ActionResult> DeleteDocument([FromRoute] DeleteDocumentCommand deleteDocumentCommand)
        {
            await _mediator.Send(deleteDocumentCommand);
            return NoContent();
        }
    }
}
