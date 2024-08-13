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
using ClearMindWindows.Controls;
using ClearMindWindows.RequestModels;

namespace ClearMindWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private long ConvertToTimestamp(DateTime value)
        {
            long epoch = (value.Ticks - 621355968000000000) / 10000000;
            return epoch;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(Properties.Settings.Default.Name == "")
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                Close();
            }
            lbName.Text = Properties.Settings.Default.Name;
        }

        private async void btnGmail_Click(object sender, RoutedEventArgs e)
        {
            TasksModel model = new TasksModel();
            //await model.SyncTasks();
            List<TaskContainer> tasks = await model.GetTasks();
            foreach (TaskContainer task in tasks) 
            {
                tasksContainer.Children.Add(task);
            }
        }
    }
}