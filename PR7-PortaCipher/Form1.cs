using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR7_PortaCipher
{
    /// <summary>
    /// Главная форма приложения для шифрования и дешифрования текста.
    /// </summary>
    public partial class Form1 : Form
    {
        private readonly PortaCipher _cipher;

        /// <summary>
        /// Создаёт главную форму и инициализирует таблицу кодов.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            var codeBook = new Dictionary<string, string>
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

            _cipher = new PortaCipher(codeBook);

            encryptButton.Click += EncryptButton_Click;
            decryptButton.Click += DecryptButton_Click;
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки шифрования.
        /// </summary>
        private void EncryptButton_Click(object sender, EventArgs e)
        {
            try
            {
                resultTextBox.Text = _cipher.Encrypt(inputTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки дешифрования.
        /// </summary>
        private void DecryptButton_Click(object sender, EventArgs e)
        {
            try
            {
                resultTextBox.Text = _cipher.Decrypt(inputTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}
