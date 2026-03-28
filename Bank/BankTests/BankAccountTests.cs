using BankAccountNS;

namespace BankTests;

[TestClass]
public sealed class BankAccountTests
{
    [TestMethod]
    public void Debit_WithValidAmount_UpdatesBalance()
    {
        const double beginningBalance = 11.99;
        const double debitAmount = 4.55;
        const double expected = 7.44;
        var account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

        account.Debit(debitAmount);

        Assert.AreEqual(expected, account.Balance, 0.001, "Account not debited correctly");
    }

    [TestMethod]
    public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
    {
        const double beginningBalance = 11.99;
        const double debitAmount = -100.00;
        var account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

        try
        {
            account.Debit(debitAmount);
        }
        catch (ArgumentOutOfRangeException e)
        {
            StringAssert.Contains(e.Message, BankAccount.DebitAmountLessThanZeroMessage);
            return;
        }

        Assert.Fail("The expected exception was not thrown.");
    }

    [TestMethod]
    public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
    {
        const double beginningBalance = 11.99;
        const double debitAmount = 20.0;
        var account = new BankAccount("Mr. Bryan Walton", beginningBalance);

        try
        {
            account.Debit(debitAmount);
        }
        catch (ArgumentOutOfRangeException e)
        {
            StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
            return;
        }

        Assert.Fail("The expected exception was not thrown.");
    }

    [TestMethod]
    public void Credit_WithValidAmount_UpdatesBalance()
    {
        const double beginningBalance = 11.99;
        const double creditAmount = 5.01;
        const double expected = 17.00;
        var account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

        account.Credit(creditAmount);

        Assert.AreEqual(expected, account.Balance, 0.001, "Account not credited correctly");
    }

    [TestMethod]
    public void Credit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
    {
        const double beginningBalance = 11.99;
        const double creditAmount = -5.50;
        var account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

        try
        {
            account.Credit(creditAmount);
        }
        catch (ArgumentOutOfRangeException e)
        {
            StringAssert.Contains(e.Message, BankAccount.CreditAmountLessThanZeroMessage);
            return;
        }

        Assert.Fail("The expected exception was not thrown.");
    }
}
