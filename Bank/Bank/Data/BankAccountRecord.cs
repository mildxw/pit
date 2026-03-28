using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccountNS.Data;

/// <summary>
/// Сущность для сохранения счёта в базе (Entity Framework 6, части 2–3).
/// </summary>
[Table("BankAccountRecords")]
public sealed class BankAccountRecord
{
    /// <summary>Идентификатор записи.</summary>
    [Key]
    public int Id { get; set; }

    /// <summary>Имя владельца.</summary>
    [Required]
    [MaxLength(256)]
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>Баланс на момент сохранения.</summary>
    public double Balance { get; set; }
}
