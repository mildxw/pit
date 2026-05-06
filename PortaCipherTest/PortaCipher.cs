using System;
using System.Collections.Generic;
using System.Linq;

namespace PR7_PortaCipher
{
    /// <summary>
    /// Выполняет шифрование и дешифрование текста с помощью таблицы замен слов.
    /// </summary>
    public class PortaCipher
    {
        private readonly Dictionary<string, string> _codeBook;

        /// <summary>
        /// Создаёт объект шифра с заданной таблицей кодов.
        /// </summary>
        /// <param name="codeBook">Таблица замен слов.</param>
        public PortaCipher(Dictionary<string, string> codeBook)
        {
            if (codeBook == null || codeBook.Count == 0)
                throw new ArgumentException("Таблица кодов не может быть пустой.");

            _codeBook = codeBook;
        }

        /// <summary>
        /// Шифрует текст, заменяя известные слова кодами.
        /// </summary>
        /// <param name="text">Исходный текст.</param>
        /// <returns>Зашифрованный текст.</returns>
        public string Encrypt(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Текст не может быть пустым.");

            string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return string.Join(" ", words.Select(word =>
            {
                string lowerWord = word.ToLower();

                if (!_codeBook.ContainsKey(lowerWord))
                    throw new KeyNotFoundException($"Слово '{word}' отсутствует в таблице кодов.");

                return _codeBook[lowerWord];
            }));
        }

        /// <summary>
        /// Дешифрует текст, заменяя коды исходными словами.
        /// </summary>
        /// <param name="encryptedText">Зашифрованный текст.</param>
        /// <returns>Расшифрованный текст.</returns>
        public string Decrypt(string encryptedText)
        {
            if (string.IsNullOrWhiteSpace(encryptedText))
                throw new ArgumentException("Текст не может быть пустым.");

            Dictionary<string, string> reverseBook = _codeBook.ToDictionary(item => item.Value, item => item.Key);

            string[] codes = encryptedText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return string.Join(" ", codes.Select(code =>
            {
                if (!reverseBook.ContainsKey(code))
                    throw new KeyNotFoundException($"Код '{code}' отсутствует в таблице кодов.");

                return reverseBook[code];
            }));
        }
    }
}