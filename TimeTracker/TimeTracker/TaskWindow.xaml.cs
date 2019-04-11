using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;



namespace TimeTracker
{
    public partial class TaskWindow : Window
    {
        private Timer timer;
        private DateTime startTime = DateTime.MinValue;
        private TimeSpan totalElapsedTime = TimeSpan.Zero;
        private bool timerRunning = false;
        private TimeSpan elapsedTimeAtPause = TimeSpan.Zero;
        private bool timePause = false;
        
        public TaskWindow()
        {
            InitializeComponent();
            timer = new System.Timers.Timer();
            
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (timePause)
            {
                startTime = DateTime.Now - elapsedTimeAtPause; //if starting from a paused state, add back the time saved at paused
                elapsedTimeAtPause = TimeSpan.Zero;
            }
            else
            {
                startTime = DateTime.Now;
            }


        }
    }
}

//Wrote this in the btnStart method by mistake.
//TimeTracker.MainWindow.AppWindow.counter++;
//            TimeTracker.MainWindow.AppWindow.windowList.Add(this);
//            this.Show();
//Left = System.Windows.SystemParameters.WorkArea.Right - (Width * TimeTracker.MainWindow.AppWindow.counter);
//Top = System.Windows.SystemParameters.WorkArea.Bottom - Height;
//            this.Topmost = true;
