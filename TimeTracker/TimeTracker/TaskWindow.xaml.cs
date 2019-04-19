using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;
using System.Timers;
using System.ComponentModel;

namespace TimeTracker
{
    public partial class TaskWindow : Window
    {
        public static TaskWindow taskWindow;

        DispatcherTimer timer = new DispatcherTimer();
        private DateTime startTime = DateTime.MinValue;
        public TimeSpan totalElapseTime = TimeSpan.Zero;
        private TimeSpan elapsedTimeAtPause = TimeSpan.Zero;
        private bool timePause = false;
        
        public TaskWindow()
        {
            InitializeComponent();
            this.Show();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            Left = SystemParameters.WorkArea.Right - (MainWindow.AppWindow.Width + (Width * (MainWindow.AppWindow.windowList.Count + 1)));
            Top = SystemParameters.WorkArea.Bottom - Height;
            this.Topmost = true;

            taskWindow = this;

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTick;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            totalElapseTime = DateTime.Now - startTime + elapsedTimeAtPause;
            txtTime.Text = totalElapseTime.ToString("hh':'mm':'ss");
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (timePause)
            {
                startTime = DateTime.Now - elapsedTimeAtPause; //if starting from a paused state, add back the elapsed time saved during the pause
                elapsedTimeAtPause = TimeSpan.Zero;
            }
            else
            {
                startTime = DateTime.Now;
            }

            timer.Start();
            timePause = false;

            btnStop.IsEnabled = true;
            btnReset.IsEnabled = false;
            btnStart.IsEnabled = false;
            btnSave.IsEnabled = false;
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            elapsedTimeAtPause = totalElapseTime; //store the amount of time that has been accrued
            timer.Stop();
            timePause = true;

            btnStop.IsEnabled = false;
            btnStart.IsEnabled = true;
            btnReset.IsEnabled = true;
            btnSave.IsEnabled = true;
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            elapsedTimeAtPause = TimeSpan.Zero;
            totalElapseTime = TimeSpan.Zero;
            timePause = false;
            txtTime.Text = totalElapseTime.ToString("h':'m':'s");
        }

        public void TaskWindow_Closing(object sender, CancelEventArgs e)
        {
            MainWindow.AppWindow.windowList.Remove(this);
            MainWindow.AppWindow.ResetTaskWindowPositions();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            NotesWindow notesWindow = new NotesWindow();
        }
    }
}




