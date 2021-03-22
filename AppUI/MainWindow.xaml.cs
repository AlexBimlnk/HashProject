using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HashBL;
using System.Diagnostics;

namespace AppUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string folderName = "HashData";
        private static string nowFileName;
        private static string login, password;
        private static HashMap hashMap = new HashMap();

        public MainWindow()
        {
            InitializeComponent();

            // Создаем папку для хранения данных, если таковая отсутствует 
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);

            string[] fileNames = Directory.GetFiles(folderName, "*.data");
            if (fileNames.Length > 0)
            {
                hashMap.Deserialize(fileNames[fileNames.Length - 1]);
                nowFileName = $@"{folderName}\file{fileNames.Length - 1}.data";
            }
            else
                nowFileName = $@"{folderName}\file{fileNames.Length}.data";


            this.Closing += SaveData;
        }

        private void SaveData(object sender, CancelEventArgs e)
        {
            hashMap.Serealize(nowFileName);
        }


        /// <summary>
        /// Проверяет записан ли уже данный пользователь
        /// </summary>
        /// <param name="loginHash"> Хеш-ключ пользователя </param>
        /// <returns> true если пользователь уже записан </returns>
        private bool SearchUsers(ulong loginHash)
        {
            bool find = hashMap.Search(loginHash);

            //Если в ОЗУ хеша нет
            if (!find)
            {
                HashMap tempHashMap = new HashMap();
                foreach (var i in Directory.GetFiles(folderName, "*.data"))
                {
                    tempHashMap.Deserialize(i);
                    if (tempHashMap.Search(loginHash))
                    {
                        find = true;
                        break;
                    }
                }
            }

            return find;
        }


        /// <summary>
        /// Обработка клика мыши по кнопке "Войти"
        /// </summary>
        private void btnSignInClick(object sender, RoutedEventArgs e)
        {
            GetTextFromTextBox(out login, out password);

            if(SearchUsers(Hashing.GetHash(login)))
                MessageBox.Show("Доступ разрешен.", "",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Такого пользователя не существует.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
        }


        /// <summary>
        /// Обработка клика мыши по кнопке "Зарегистрироваться"
        /// </summary>
        private void btnReistrationClick(object sender, RoutedEventArgs e)
        {
            GetTextFromTextBox(out login, out password);

            ulong loginHash = Hashing.GetHash(login);

            bool find = SearchUsers(loginHash);

            if (find)
                MessageBox.Show("Такой пользователь уже существет.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                try
                {
                    hashMap.AddHash(loginHash, Hashing.GetPasswordHash(password));
                }
                catch (OverflowException)
                {
                    hashMap.Serealize(nowFileName);
                    nowFileName = $@"{folderName}\file{Directory.GetFiles(folderName, "*.data").Length}.data";
                    hashMap.AddHash(loginHash, Hashing.GetPasswordHash(password));
                    MessageBox.Show("Случилось переполнение хеш-таблицы.\n" +
                                "Программа сохранила данные в файл и записала нового пользователя.", "Внимание",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void GetTextFromTextBox(out string login, out string password)
        {
            login = this.loginTextBox.Text;
            password = this.passwordTextBox.Text;
        }
    }
}
