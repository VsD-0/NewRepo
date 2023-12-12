using ManagementDocument.API.Commands;
using ManagementDocument.Database.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ManagementDocument.API.Controllers
{
    /// <summary>
    /// Контроллер регистрации
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        #region Fields
        private readonly IMediator _mediator;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RegistrationController"/>.
        /// </summary>
        /// <param name="mediator">Экземпляр интерфейса IMediator.</param>
        public RegistrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <param name="id">Id пользователя</param>
        /// <returns>Пользователь</returns>
        [HttpGet("GetUserById/{id}")]
        [SwaggerOperation(Summary = "Возвращает пользователя по Id",
                          OperationId = "GetUserById_Reg")]
        [SwaggerResponse(200, "Успешное выполнение", typeof(User))]
        [SwaggerResponse(404, "Пользователь не найден")]
        public async Task<ActionResult> GetUserById(int id)
        {
            var user = await _mediator.Send(new GetUserByIdCommand { Id = id });
            return user is not null ? Ok(user) : NotFound();
        }

        /// <param name="createUserCommand">Пользователь</param>
        /// <returns>Пользователь</returns>
        [HttpPost("CreateUser")]
        [SwaggerOperation(Summary = "Создаёт нового пользователя",
                          Description = "Создаёт нового пользователя и возвращает его",
                          OperationId = "CreateUser_Reg")]
        [SwaggerResponse(201, "Успешное выполнение", typeof(User))]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand createUserCommand)
        {
            var result = await _mediator.Send(createUserCommand);
            return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
        }
    }
}
