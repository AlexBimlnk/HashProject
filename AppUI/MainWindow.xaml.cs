using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using HashBL;

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
        /// Проверяет записан ли уже данный пользователь.
        /// </summary>
        /// <param name="key"> Хеш-ключ пользователя. </param>
        /// <param name="value"> Значение у ключа, если пользователь будет найден </param>
        /// <returns> True если пользователь уже записан. </returns>    
        /// <summary>
        private bool SearchUser(ulong key, out Tuple<uint[], string> value)
        {
            value = hashMap.Search(key);

            //Если в ОЗУ нет хеша
            if (value == null)
            {
                HashMap tempHashMap = new HashMap();
                foreach (var i in Directory.GetFiles(folderName, "*.data"))
                {
                    tempHashMap.Deserialize(i);
                    value = tempHashMap.Search(key);

                    //Если нашли пользователя
                    if(value != null)
                        return true;
                }
            }

            return value == null ? false : true;
        }

        /// <summary>
        /// Обработка клика мыши по кнопке "Войти".
        /// </summary>
        private void btnSignInClick(object sender, RoutedEventArgs e)
        {
            GetTextFromTextBox(out login, out password);

            ulong loginHash = Hashing.GetHash(login);
            Tuple<uint[], string> hashValue = null;

            if(SearchUser(loginHash, out hashValue))
            {
                uint[] hashUser = Hashing.GetPasswordHash(password + hashValue.Item2);

                bool check = true;

                for (int i = 0; i < hashUser.Length && i < hashValue.Item1.Length; i++)
                {
                    if (hashUser[i] != hashValue.Item1[i])
                    {
                        check = false;
                        break;
                    }
                }

                if (check)
                    MessageBox.Show("Доступ разрешен.", "",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Неправильный пароль.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Такого пользователя не существует.", "Ошибка",
                                    MessageBoxButton.OK, MessageBoxImage.Error);       
        }


        /// <summary>
        /// Обработка клика мыши по кнопке "Зарегистрироваться".
        /// </summary>
        private void btnReistrationClick(object sender, RoutedEventArgs e)
        {
            GetTextFromTextBox(out login, out password);

            ulong loginHash = Hashing.GetHash(login);
            Tuple<uint[], string> hashValue;

            if (SearchUser(loginHash, out hashValue))
                MessageBox.Show("Такой пользователь уже существет.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                string salt = Hashing.GetSalt();
                try
                {   
                    hashMap.AddHash(loginHash, Hashing.GetPasswordHash(password + salt), salt);
                }
                catch (OverflowException)
                {
                    hashMap.Serealize(nowFileName);
                    nowFileName = $@"{folderName}\file{Directory.GetFiles(folderName, "*.data").Length}.data";
                    hashMap.AddHash(loginHash, Hashing.GetPasswordHash(password + salt), salt);
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
