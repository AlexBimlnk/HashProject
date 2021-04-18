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
    /// Логика взаимодействия для AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        Frame MainFrame;
        Account myAccount;

        public AccountPage(Frame frame, Account account)
        {
            InitializeComponent();
            myAccount = account;
            MainFrame = frame;
            UserNameLabel.Content = myAccount.Login;
            BalanceCountLabel.Content = myAccount.Balance.ToString();
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = null;
        }

        private void addMoneyBtn_Click(object sender, RoutedEventArgs e)
        {
            string money_s = moneyBox.Text;
            if (money_s.Length != 0)
            {
                for (int i = 0;i < money_s.Length;i++)
                    if (!('0' <= money_s[i] && money_s[i] <= '9'))
                    {
                        moneyBox.Clear();
                        return;
                    }

                int money = int.Parse(money_s);

                myAccount.AddMoney(money);
                BalanceCountLabel.Content = myAccount.Balance.ToString();

                moneyBox.Clear();
            }
        }
    }
}
