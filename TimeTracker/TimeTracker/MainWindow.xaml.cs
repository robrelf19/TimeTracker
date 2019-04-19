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

namespace TimeTracker
{
    public partial class MainWindow : Window
    {
        public static MainWindow AppWindow;

        public List<Window> windowList = new List<Window>();

        public MainWindow()
        {
            InitializeComponent();
            Left = SystemParameters.WorkArea.Right - 50;
            Top = SystemParameters.WorkArea.Bottom -Height;
            this.Topmost = true;
            AppWindow = this;
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            TaskWindow taskWindow = new TaskWindow();
            windowList.Add(taskWindow);
        }

        public void ResetTaskWindowPositions()
        {
            int counter = 1;
            foreach (TaskWindow item in windowList)
            {
                item.Left = SystemParameters.WorkArea.Right - (Width + (100 * counter));
                counter++;
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            if (windowList.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Close all tasks without Saving ?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
            
        }
    }
}
