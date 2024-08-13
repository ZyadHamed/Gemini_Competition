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
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        public SignUpWindow()
        {
            InitializeComponent();
        }

        void ValidateInput()
        {
            if(txtPassword.Password != txtRepeatPassword.Password)
            {
                MessageBox.Show("Passwords Don't Match!");
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
        
        private async void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            ValidateInput();
            UsersModel usersModel = new UsersModel();
            string result = await usersModel.AddUser(txtName.Text, txtEmail.Text, txtPassword.Password);
            MessageBox.Show(result);
        }
    }
}
