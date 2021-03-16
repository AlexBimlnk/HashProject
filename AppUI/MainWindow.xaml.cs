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

namespace AppUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static AuthorizationPage AuthorizationPage = new AuthorizationPage();
        public static RegistrationPage RegistrationPage = new RegistrationPage();
        private static Frame MainFrame;
        public MainWindow()
        {
            InitializeComponent();
            MainFrame = Main;
            Main.Content = new AuthorizationPage();
        }

        public static void SetFrame(Page page)
        {
            MainFrame.Content = page;
        }
    }
}
