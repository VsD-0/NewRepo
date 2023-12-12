﻿using ManagementDocument.Client.Interfaces;
using ManagementDocument.Client.Models;
using ManagementDocument.Database.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace ManagementDocument.Client.Handlers
{
    /// <summary>
    /// Обработчик токена для добавления и обновления JWT-токена перед отправкой запросов.
    /// </summary>
    public class TokenHandler : DelegatingHandler
    {
        #region Fields
        private readonly IAuthData _authData;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TokenHandler"/>.
        /// </summary>
        /// <param name="authData">Сервис аутентификации для обновления токена.</param>
        public TokenHandler(IAuthData authData) => _authData = authData;

        /// <summary>
        /// Добавляет или обновляет JWT-токен перед отправкой запроса.
        /// </summary>
        /// <param name="request">Запрос, который будет отправлен.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Ответ от сервера.</returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(TokenStorage.Token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(TokenStorage.Token);

                if (DateTime.UtcNow > jwtToken.ValidTo)
                {
                    int userId = int.Parse(jwtToken.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value ?? throw new("Тип не был найден"));

                    User user = await _authData.GetUserById(userId);
                    var newToken = await _authData.Auth(user);
                    TokenStorage.Token = newToken;
                }

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TokenStorage.Token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
