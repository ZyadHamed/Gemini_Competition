using ClearMindWindows.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClearMindWindows.Controls
{
    public class TaskContainer: Button
    {
        TranscribedTask task;
        public TaskContainer(TranscribedTask _task)
        {
            task = _task;
            Padding = new Thickness(0);
            Margin = new Thickness(5);
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
            VerticalContentAlignment = VerticalAlignment.Stretch;
            Border border = new Border() 
            { 
                CornerRadius = new CornerRadius(2),
                BorderBrush = new SolidColorBrush(Color.FromRgb(144, 164, 174)),
                BorderThickness = new Thickness(2),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };
            StackPanel borderChild = new StackPanel()
            {
                Background = new SolidColorBrush(Color.FromRgb(239, 245, 251))
            };
            TextBlock taskTitleTextBlock = new TextBlock()
            {
                FontFamily = new FontFamily("Arial"),
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                Text = task.Title,
                Margin = new Thickness(20, 10, 0, 0)
            };
            DateTime assignmentDate = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Convert.ToDouble(task.Timestamp)).ToLocalTime();
            string assignmentDateText = "Assigned on: " + assignmentDate.ToString("dd-MM-yyyy");
            TextBlock assignmentDateTextBlock = new TextBlock()
            {
                FontFamily = new FontFamily("Georgia"),
                FontSize = 16,
                Foreground = new SolidColorBrush(Color.FromRgb(117, 117, 117)),
                Text = assignmentDateText,
                Margin = new Thickness(20, 10, 0, 0)
            };
            borderChild.Children.Add(taskTitleTextBlock);
            borderChild.Children.Add(assignmentDateTextBlock);
            border.Child = borderChild;
            Content = border;
            Click += TaskContainer_Click;
        }

        private void TaskContainer_Click(object sender, RoutedEventArgs e)
        {
            var taskDetailsContainer = (((Parent as StackPanel).Parent as ScrollViewer).Parent as Grid).Children[2] as Grid;
            StackPanel taskNameAndDetailsContainer = taskDetailsContainer.Children[0] as StackPanel;
            TextBlock lbTaskName = taskNameAndDetailsContainer.Children[0] as TextBlock;
            TextBlock lbTaskDescription = taskNameAndDetailsContainer.Children[1] as TextBlock;
            TextBlock lbTaskAssignmentDate = taskNameAndDetailsContainer.Children[2] as TextBlock;
            lbTaskName.Text = task.Title;
            lbTaskDescription.Text = task.Description;
            DateTime assignmentDate = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Convert.ToDouble(task.Timestamp)).ToLocalTime();
            string assignmentDateText = "Assigned on: " + assignmentDate.ToString();
            lbTaskAssignmentDate.Text = assignmentDateText;
        }

    }
}
