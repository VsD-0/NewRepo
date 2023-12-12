namespace ManagementDocument.API.Models
{
    /// <summary>
    /// Настройки для JSON Web Token (JWT).
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Секретный ключ для подписи JWT.
        /// </summary>
        public string? SecretKey { get; set; }

        /// <summary>
        /// Эмитент JWT.
        /// </summary>
        public string? Issuer { get; set; }

        /// <summary>
        /// Аудитория JWT.
        /// </summary>
        public string? Audience { get; set; }
    }
}
