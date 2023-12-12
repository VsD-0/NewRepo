using ManagementDocument.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementDocument.Database.Context;

/// <summary>
/// Контекст базы данных для управления документами.
/// </summary>
public partial class ManagementDocumentDbContext : DbContext
{
    /// <summary>
    /// Конструктор контекста базы данных.
    /// </summary>
    /// <param name="options">Параметры контекста базы данных.</param>
    public ManagementDocumentDbContext(DbContextOptions<ManagementDocumentDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Таблица документов
    /// </summary>
    public virtual DbSet<Document> Documents { get; set; }

    /// <summary>
    /// Таблица типов документа
    /// </summary>
    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    /// <summary>
    /// Таблица пользователей
    /// </summary>
    public virtual DbSet<User> Users { get; set; }

    /// <summary>
    /// Метод, вызываемый при создании модели базы данных.
    /// </summary>
    /// <param name="modelBuilder">Построитель модели базы данных.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("documents_pkey");

            entity.ToTable("documents");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Codeorg)
                .HasMaxLength(7)
                .HasColumnName("codeorg");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Doctype).HasColumnName("doctype");
            entity.Property(e => e.Num)
                .HasMaxLength(80)
                .HasColumnName("num");
            entity.Property(e => e.Org).HasColumnName("org");

            entity.HasOne(d => d.DoctypeNavigation).WithMany(p => p.Documents)
                .HasForeignKey(d => d.Doctype)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("documents_doctype_fkey");
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("document_types_pkey");

            entity.ToTable("document_types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(80)
                .HasColumnName("type");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Login)
                .HasMaxLength(90)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(90)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(u => u.RoleNavigation).WithMany(r => r.Users)
                .HasForeignKey(u => u.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_fk");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_roles_pkey");

            entity.ToTable("user_roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Role).HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
