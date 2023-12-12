using ManagementDocument.API.Commands;
using ManagementDocument.API.Models;
using ManagementDocument.API.Services;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManagementDocument.API.Handlers
{
    /// <summary>
    /// Обработчик авторизации
    /// </summary>
    public class AuthHandler : IRequestHandler<AuthCommand, string>
    {
        #region Fields
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AuthHandler"/>.
        /// </summary>
        /// <param name="configuration">Конфигурация приложения.</param>
        /// <param name="userService">Сервис пользователей.</param>
        public AuthHandler(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        /// <summary>
        /// Обрабатывает команду авторизации и генерирует JWT-токен.
        /// </summary>
        /// <param name="request">Запрос на авторизацию.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Сгенерированный JWT-токен.</returns>
        public async Task<string> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            // Получение пользователя по его логину
            var user = await _userService.GetUserByLogin(request.Login ?? throw new ArgumentNullException(nameof(request.Login), "Пользователь не найден"));

            var jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtSettings>();

            var tokenHandler = new JwtSecurityTokenHandler();

            var secretKey = Encoding.UTF8.GetBytes(jwtSettings?.SecretKey ?? throw new ArgumentNullException(nameof(jwtSettings), "Настройки токена являются null"));

            // Описание токена
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = jwtSettings.Issuer,
                Audience = jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            // Генерация токена
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
