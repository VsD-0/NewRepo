using ManagementDocument.API.Commands;
using ManagementDocument.API.Services;
using ManagementDocument.Database.Entities;
using ManagementDocument.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ManagementDocument.API.Controllers
{
    /// <summary>
    /// Контроллер управления пользователями
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly IUserService _userService;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UserController"/>.
        /// </summary>
        /// <param name="mediator">Экземпляр интерфейса IMediator.</param>
        /// <param name="userService">Сервис пользователей.</param>
        public UsersController(IMediator mediator, IUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
        }

        /// <param name="role">Роль пользователя</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Количество элементов на странице</param>
        /// <returns>Страница с пользователями.</returns>
        [HttpGet("GetUsers")]
        [SwaggerOperation(Summary = "Получение страницы с пользователями",
                          Description = "Фильтрация списка пользователей по роли. Возвращяет страницу с пользователями.",
                          OperationId = "GetUsers")]
        [SwaggerResponse(200, "Успешное выполнение", typeof(PaginationResult<UserDto>))]
        public async Task<ActionResult<PaginatedResult<UserDto>>> GetUsers(int role, int pageNumber, int pageSize)
        {
            // Возвращение указанной страницы
            return Ok(await _mediator.Send(new GetUsersCommand { PageNumber = pageNumber, PageSize = pageSize, Role = role }));
        }

        /// <param name="id"></param>
        /// <returns>Пользователь</returns>
        [HttpGet("GetUserById/{id}")]
        [SwaggerOperation(Summary = "Возвращает пользователя по Id",
                          OperationId = "GetUserById")]
        [SwaggerResponse(200, "Успешное выполнение", typeof(User))]
        [SwaggerResponse(404, "Не удалось найти пользователя")]
        public async Task<ActionResult> GetUserById(int id)
        {
            var user = await _mediator.Send(new GetUserByIdCommand { Id = id });
            return user is not null ? Ok(user) : NotFound();
        }

        /// <param name="createUserCommand">Пользователь для добавления</param>
        /// <returns>Пользователь</returns>
        [HttpPost("CreateUser")]
        [SwaggerOperation(Summary = "Создаёт нового пользователя",
                          Description = "Создаёт нового пользователя и возвращает его",
                          OperationId = "CreateUser")]
        [SwaggerResponse(201, "Успешное выполнение", typeof(User))]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserCommand createUserCommand)
        {
            var result = await _mediator.Send(createUserCommand);
            return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
        }

        /// <param name="updateUserCommand">Пользователь</param>
        [HttpPut("UpdateUser")]
        [SwaggerOperation(Summary = "Обновляет существующего пользователя",
                          OperationId = "UpdateUser")]
        [SwaggerResponse(204, "Успешное выполнение")]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserCommand updateUserCommand)
        {
            await _mediator.Send(updateUserCommand);
            return NoContent();
        }

        /// <param name="deleteUserCommand">Id пользователя для удаления</param>
        [HttpDelete("DeleteUser/{id}")]
        [SwaggerOperation(Summary = "Удаляет существующего пользователя",
                          OperationId = "DeleteUser")]
        [SwaggerResponse(204, "Успешное выполнение")]
        public async Task<ActionResult> DeleteUser([FromRoute] DeleteUserCommand deleteUserCommand)
        {
            await _mediator.Send(deleteUserCommand);
            return NoContent();
        }
    }
}
