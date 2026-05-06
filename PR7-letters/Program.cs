using System;

/// <summary>
/// Основной класс программы.
/// </summary>
class ArrayExample
{
    /// <summary>
    /// Точка входа в программу.
    /// </summary>
    static void Main()
    {
        char[] letters = { 'f', 'r', 'e', 'd', ' ', 's', 'm', 'i', 't', 'h' };
        string name = "";
        int[] a = new int[10];

        for (int i = 0; i < letters.Length; i++)
        {
            name += letters[i];
            a[i] = i + 1;
            SendMessage(name, a[i]);
        }

        Console.ReadKey();
    }

    /// <summary>
    /// Выводит сообщение в консоль.
    /// </summary>
    /// <param name="name">Имя пользователя.</param>
    /// <param name="msg">Номер сообщения.</param>
    static void SendMessage(string name, int msg)
    {
        Console.WriteLine("Hello, " + name + "! Count to " + msg);
    }
}
