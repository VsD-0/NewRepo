using ManagementDocument.API.Commands;
using ManagementDocument.Database.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ManagementDocument.API.Controllers
{
    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        #region Fields
        private readonly IMediator _mediator;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AuthController"/>.
        /// </summary>
        /// <param name="mediator">Экземпляр интерфейса IMediator.</param>
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <param name="id">Id пользователя</param>
        /// <returns>Пользователь</returns>
        [HttpGet("GetUserById/{id}")]
        [SwaggerOperation(Summary = "Возвращает пользователя по Id",
                          OperationId = "GetUserById_Auth")]
        [SwaggerResponse(200, "Успешное выполнение", typeof(User))]
        [SwaggerResponse(404, "Пользователь не найден")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _mediator.Send(new GetUserByIdCommand { Id = id });
            return user is not null ? Ok(user) : NotFound();
        }

        /// <param name="authCommand">Данные пользователя (логин, пароль)</param>
        /// <returns>Токен</returns>
        [HttpPost("Auth")]
        [SwaggerOperation(Summary = "Авторизует пользователя",
                          Description = "Авторизация пользователя и отправляет jwt токен",
                          OperationId = "Auth")]
        [SwaggerResponse(200, "Успешное выполнение")]
        [SwaggerResponse(400, "Не удалось авторизовать пользователя")]
        public async Task<IActionResult> Auth([FromBody] AuthCommand authCommand)
        {
            var token = await _mediator.Send(authCommand);
            return token is not null ? Ok(token) : BadRequest();
        }
    }
}
