using ClearMindWindows.Data;
using ClearMindWindows.RequestModels;
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
using System.Windows.Shapes;

namespace ClearMindWindows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.Show();
            Close();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            UsersModel usersModel = new UsersModel();
            bool loginSuccess = await usersModel.CheckUserLogin(txtEmail.Text, txtPassword.Password);
            if (loginSuccess)
            {
                User user = await usersModel.GetUser(txtEmail.Text);
                Properties.Settings.Default.Name = user.Name;
                Properties.Settings.Default.Email = user.Email;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Invalid Credientials! Please try again.");
            }
        }
    }
}
