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
        private IDataManager dataManager;
        private static string login, password;
        private static int MinLoginLen = 4, MaxLoginLen = 9;
        private static int MinPsdLen = 8, MaxPsdLen = 16;

        enum ErrorType
        {
            LoginSize,
            PasswordSize,
            LoginIncorrect,
            PasswordIncorrect,
            LoginExsist,
            UnMatching
        }

        public MainWindow()
        {
            InitializeComponent();

            dataManager = new LocalDataManager();


            this.Closing += SaveData;
        }

        private void SaveData(object sender, CancelEventArgs e)
        {
            dataManager.SaveData();
        }
        

        /// <summary>
        /// Обработка клика мыши по кнопке "Войти".
        /// </summary>
        private void btnSignInClick(object sender, RoutedEventArgs e)
        {
            MyMessageBox.Text = "";
            GetTextFromTextBox(out login, out password);

            if (!CheckData(login, password))
                return;

            ulong loginHash = Hashing.GetHash(login);
            Account accountValue = null;

            if(dataManager.SearchUser(loginHash, out accountValue))
            {
                MyMessageBox.Text = "";
                uint[] hashUser = Hashing.GetShaHash(password + accountValue.Salt);

                bool check = true;

                for (int i = 0; i < hashUser.Length && i < accountValue.HashedPassword.Length; i++)
                {
                    if (hashUser[i] != accountValue.HashedPassword[i])
                    {
                        check = false;
                        break;
                    }
                }

                if (check)
                {
                    DisplayMessage("Доступ разрешен.", "", MessageBoxImage.Information);
                    MainFrame.Content = new AccountPage(MainFrame, accountValue);
                }
                    
                else
                    DisplayError(ErrorType.UnMatching);
            }
            else
                DisplayError(ErrorType.UnMatching);
        }


        /// <summary>
        /// Обработка клика мыши по кнопке "Зарегистрироваться".
        /// </summary>
        private void btnReistrationClick(object sender, RoutedEventArgs e)
        {
            MyMessageBox.Text = "";
            GetTextFromTextBox(out login, out password);

            if (!CheckData(login, password))
                return;

            ulong loginHash = Hashing.GetHash(login);
            Account accountValue;

            if (dataManager.SearchUser(loginHash, out accountValue))
                DisplayError(ErrorType.LoginExsist);
            else
            {
                MyMessageBox.Text = "";
                try
                {
                    dataManager.AddData(login, password);
                }
                catch (OverflowException)
                {
                    DisplayMessage("Случилось переполнение хеш-таблицы.\n" +
                                "Программа сохранила данные в файл и записала нового пользователя.", "Внимание", MessageBoxImage.Information);
                }
            }
        }

        private void GetTextFromTextBox(out string login, out string password)
        {
            login = this.loginTextBox.Text;
            password = this.passwordTextBox.Text;
        }

        /// <summary>
        /// Проверка корректности ввода
        /// </summary>
        /// <returns> True, если данные корректны </returns>
        private bool CheckData(string login, string psd)
        {
            if (!(MinLoginLen <= login.Length && login.Length <= MaxLoginLen))
            {
                DisplayError(ErrorType.LoginSize);
                return false;
            }

            for (int i = 0; i < login.Length; i++)
                if (!('a' <= login[i] && login[i] <= 'z') &&
                    !('A' <= login[i] && login[i] <= 'Z') &&
                    !('0' <= login[i] && login[i] <= '9'))
                {
                    DisplayError( ErrorType.LoginIncorrect);
                    return false;
                }

            if (!(MinPsdLen <= password.Length && password.Length <= MaxPsdLen))
            {
                DisplayError(ErrorType.PasswordSize);
                return false;
            }

            for (int i = 0; i < password.Length; i++)
                if (!('a' <= password[i] && password[i] <= 'z') &&
                    !('A' <= password[i] && password[i] <= 'Z') &&
                    !('0' <= password[i] && password[i] <= '9'))
                {
                    DisplayError(ErrorType.PasswordIncorrect);
                    return false;
                }

            return true;
        }
        
        private void DisplayMessage(string message, string tittle, MessageBoxImage Type)
        {
            MessageBox.Show(message, tittle, MessageBoxButton.OK, Type);
        }

        private void DisplayError(ErrorType Type)
        {
            switch(Type)
            {
                case ErrorType.LoginSize:
                    MyMessageBox.Text = $"* Длина логина должна быть от {MinLoginLen} до {MaxLoginLen} символов";
                    break;
                case ErrorType.PasswordSize:
                    MyMessageBox.Text = $"* Длина пароля должна быть от {MinPsdLen} до {MaxPsdLen} символов";
                    break;
                case ErrorType.LoginIncorrect:
                    MyMessageBox.Text = "* Логин может содержать только цифры и латинские буквы";
                    break;
                case ErrorType.PasswordIncorrect:
                    MyMessageBox.Text = "* Пароль может содержать только цифры и латинские буквы";
                    break;
                case ErrorType.LoginExsist:
                    MyMessageBox.Text = "* Такой пользователь уже существует";
                    break;
                case ErrorType.UnMatching:
                    MyMessageBox.Text = "* Неправильный логин или пароль";
                    break;
            }
        }
    }
}
