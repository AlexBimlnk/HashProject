using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppUI
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void btnRegistrationClick(object sender, RoutedEventArgs e)
        {
            //TODO Здесь нужно хешировать и возможно сериализовывать
            //Хотя сериализовывать можно когда приложение закрывают, подумаем

            MainWindow.SetFrame(MainWindow.AuthorizationPage);
        }
    }
}
