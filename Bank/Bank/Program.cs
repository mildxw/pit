using System.Data.Entity;
using BankAccountNS;
using BankAccountNS.Data;

Database.SetInitializer(new CreateDatabaseIfNotExists<BankDbContext>());

var account = new BankAccount("Mr. Roman Abramovich", 11.99);
account.Credit(5.77);
account.Debit(11.22);
Console.WriteLine("Current balance is ${0:F2}", account.Balance);

try
{
    using var db = new BankDbContext();
    db.Database.CreateIfNotExists();
    db.Accounts.Add(new BankAccountRecord
    {
        CustomerName = account.CustomerName,
        Balance = account.Balance
    });
    db.SaveChanges();
    Console.WriteLine("EF 6 (SQL Server / LocalDB): записей в таблице — {0}.", db.Accounts.Count());
}
catch (Exception ex)
{
    Console.WriteLine("EF 6: ошибка БД (нужен LocalDB / SQL Server): {0}", ex.Message);
}
