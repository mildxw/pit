using System.Data.Entity;

namespace BankAccountNS.Data;

/// <summary>
/// Контекст БД (EF 6 + провайдер SQL Server через EntityFramework.SqlServer.dll).
/// </summary>
public sealed class BankDbContext : DbContext
{
    /// <summary>Создаёт контекст по имени строки подключения <c>BankDb</c> из App.config.</summary>
    public BankDbContext()
        : base("name=BankDb")
    {
    }

    /// <summary>Набор сохранённых счетов.</summary>
    public DbSet<BankAccountRecord> Accounts { get; set; } = null!;
}
