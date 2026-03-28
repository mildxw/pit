using System;

namespace BankAccountNS;

/// <summary>
/// Учебная модель банковского счёта с операциями дебета и кредита (практика «белый ящик»).
/// </summary>
public class BankAccount
{
    /// <summary>Сообщение при попытке списать больше, чем есть на счёте.</summary>
    public const string DebitAmountExceedsBalanceMessage = "Debit amount exceeds balance";

    /// <summary>Сообщение при отрицательной сумме списания.</summary>
    public const string DebitAmountLessThanZeroMessage = "Debit amount is less than zero";

    /// <summary>Сообщение при отрицательной сумме зачисления.</summary>
    public const string CreditAmountLessThanZeroMessage = "Credit amount is less than zero";

    private readonly string m_customerName;
    private double m_balance;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="BankAccount"/>.
    /// </summary>
    /// <param name="customerName">Имя владельца счёта.</param>
    /// <param name="balance">Начальный баланс.</param>
    public BankAccount(string customerName, double balance)
    {
        m_customerName = customerName;
        m_balance = balance;
    }

    /// <summary>
    /// Получает имя владельца счёта.
    /// </summary>
    /// <value>Строка с именем клиента.</value>
    public string CustomerName => m_customerName;

    /// <summary>
    /// Получает текущий баланс счёта.
    /// </summary>
    /// <value>Текущая сумма на счёте.</value>
    public double Balance => m_balance;

    /// <summary>
    /// Списывает указанную сумму со счёта (дебет).
    /// </summary>
    /// <param name="amount">Сумма списания; должна быть неотрицательной и не больше баланса.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Сумма отрицательна или превышает текущий баланс.
    /// </exception>
    public void Debit(double amount)
    {
        if (amount > m_balance)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), amount, DebitAmountExceedsBalanceMessage);
        }

        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), amount, DebitAmountLessThanZeroMessage);
        }

        m_balance -= amount;
    }

    /// <summary>
    /// Зачисляет указанную сумму на счёт (кредит).
    /// </summary>
    /// <param name="amount">Сумма зачисления; должна быть неотрицательной.</param>
    /// <exception cref="ArgumentOutOfRangeException">Сумма отрицательна.</exception>
    public void Credit(double amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), amount, CreditAmountLessThanZeroMessage);
        }

        m_balance += amount;
    }
}
