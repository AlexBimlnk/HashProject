using System;
using System.Collections.Generic;
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

namespace AppUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string login, password;
        private static HashStructure hashStructure = new HashStructure();

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработка клика мыши по кнопке "Войти"
        /// </summary>
        private void btnSignInClick(object sender, RoutedEventArgs e)
        {
            GetTextFromTextBox(out login, out password);

            //TODO
        }

        /// <summary>
        /// Обработка клика мыши по кнопке "Зарегистрироваться"
        /// </summary>
        private void btnReistrationClick(object sender, RoutedEventArgs e)
        {
            GetTextFromTextBox(out login, out password);

            //TODO

            //Например
            hashStructure.AddHash(hashStructure.HashData(password));
        }

        private void GetTextFromTextBox(out string login, out string password)
        {
            login = this.loginTextBox.Text;
            password = this.passwordTextBox.Text;
        }
    }
}
