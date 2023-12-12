using System.Text.Json.Serialization;

namespace ManagementDocument.Database.Entities;

/// <summary>
/// Тип документа.
/// </summary>
public partial class DocumentType
{
    /// <summary>
    /// Идентификатор типа документа.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование типа документа.
    /// </summary>
    public string Type { get; set; } = null!;

    /// <summary>
    /// Навигационное свойство для доступа к списку документов данного типа.
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
}
