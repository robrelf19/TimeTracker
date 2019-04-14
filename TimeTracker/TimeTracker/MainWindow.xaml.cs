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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow AppWindow;

        public int counter = -1;
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
            counter++;
            TaskWindow taskWindow = new TaskWindow();
            windowList.Add(taskWindow);
        }

        public void ResetTaskWindowPositions()
        {
            if(counter > 0)
            {
                for (int i = 0; i < counter; i++)
                {
                    windowList[i].Left = SystemParameters.WorkArea.Right - 50 - (150 * counter);
                }
            }
            else
            {
                return;
            }
        }
    }
}
