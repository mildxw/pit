using NUnit.Framework;
using PR7_PortaCipher;
using System;
using System.Collections.Generic;

namespace PR7_PortaCipher
{
    /// <summary>
    /// Автоматизированные тесты для проверки работы шифра Порта.
    /// </summary>
    public class Tests
    {
        private Dictionary<string, string> _defaultTable;

        /// <summary>
        /// Создаёт таблицу кодов перед каждым тестом.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _defaultTable = new Dictionary<string, string>
            {
                { "мир", "A1" },
                { "труд", "B2" },
                { "май", "C3" },
                { "кот", "D4" },
                { "шпион", "E5" },
                { "план", "F6" },
                { "данные", "G7" },
                { "слон", "H8" }
            };
        }

        /// <summary>
        /// Проверяет шифрование известных слов.
        /// </summary>
        [Test]
        public void Encrypt_ValidWords_ReturnsCodes()
        {
            var cipher = new PortaCipher(_defaultTable);

            string result = cipher.Encrypt("кот дом мир");

            Assert.That(result, Is.EqualTo("A1 B2 C3"));
        }

        /// <summary>
        /// Проверяет дешифрование известных кодов.
        /// </summary>
        [Test]
        public void Decrypt_ValidCodes_ReturnsWords()
        {
            var cipher = new PortaCipher(_defaultTable);

            string result = cipher.Decrypt("A1 B2 C3");

            Assert.That(result, Is.EqualTo("кот дом мир"));
        }

        /// <summary>
        /// Проверяет, что после шифрования и дешифрования получается исходный текст.
        /// </summary>
        [Test]
        public void EncryptDecrypt_ValidText_ReturnsOriginalText()
        {
            var cipher = new PortaCipher(_defaultTable);

            string original = "кот дом";
            string encrypted = cipher.Encrypt(original);
            string decrypted = cipher.Decrypt(encrypted);

            Assert.That(decrypted, Is.EqualTo(original));
            Assert.That(encrypted, Is.Not.EqualTo(original));
        }

        /// <summary>
        /// Проверяет работу шифрования с другой таблицей кодов.
        /// </summary>
        [Test]
        public void Encrypt_WithAnotherTable_ReturnsCodesFromAnotherTable()
        {
            var table = new Dictionary<string, string>
            {
                { "солнце", "X1" },
                { "луна", "X2" }
            };

            var cipher = new PortaCipher(table);

            string result = cipher.Encrypt("солнце луна");

            Assert.That(result, Is.EqualTo("X1 X2"));
        }

        /// <summary>
        /// Проверяет работу дешифрования с другой таблицей кодов.
        /// </summary>
        [Test]
        public void Decrypt_WithAnotherTable_ReturnsWordsFromAnotherTable()
        {
            var table = new Dictionary<string, string>
            {
                { "река", "R1" },
                { "лес", "L2" }
            };

            var cipher = new PortaCipher(table);

            string result = cipher.Decrypt("R1 L2");

            Assert.That(result, Is.EqualTo("река лес"));
        }

        /// <summary>
        /// Проверяет ошибку при шифровании отсутствующего слова.
        /// </summary>
        [Test]
        public void Encrypt_UnknownWord_ThrowsException()
        {
            var cipher = new PortaCipher(_defaultTable);

            Assert.Throws<KeyNotFoundException>(() => cipher.Encrypt("собака"));
        }

        /// <summary>
        /// Проверяет ошибку при дешифровании отсутствующего кода.
        /// </summary>
        [Test]
        public void Decrypt_UnknownCode_ThrowsException()
        {
            var cipher = new PortaCipher(_defaultTable);

            Assert.Throws<KeyNotFoundException>(() => cipher.Decrypt("Z9"));
        }

        /// <summary>
        /// Проверяет ошибку при шифровании пустой строки.
        /// </summary>
        [Test]
        public void Encrypt_EmptyText_ThrowsException()
        {
            var cipher = new PortaCipher(_defaultTable);

            Assert.Throws<ArgumentException>(() => cipher.Encrypt(""));
        }

        /// <summary>
        /// Проверяет ошибку при дешифровании пустой строки.
        /// </summary>
        [Test]
        public void Decrypt_EmptyText_ThrowsException()
        {
            var cipher = new PortaCipher(_defaultTable);

            Assert.Throws<ArgumentException>(() => cipher.Decrypt(""));
        }

        /// <summary>
        /// Проверяет ошибку при создании шифра с пустой таблицей.
        /// </summary>
        [Test]
        public void Constructor_EmptyTable_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new PortaCipher(new Dictionary<string, string>()));
        }

        /// <summary>
        /// Проверяет ошибку при создании шифра с таблицей null.
        /// </summary>
        [Test]
        public void Constructor_NullTable_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new PortaCipher(null));
        }
    }
}