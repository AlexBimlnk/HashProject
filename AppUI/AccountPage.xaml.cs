﻿using System;
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
    }
}
