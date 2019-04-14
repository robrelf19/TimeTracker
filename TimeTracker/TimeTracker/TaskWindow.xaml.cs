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

        DispatcherTimer timer = new DispatcherTimer();
        private DateTime startTime = DateTime.MinValue;
        private TimeSpan totalElapseTime = TimeSpan.Zero;
        private bool timerRunning = false;
        private TimeSpan elapsedTimeAtPause = TimeSpan.Zero;
        private bool timePause = false;
        private int instanceIndex;


        public TaskWindow()
        {
            InitializeComponent();
            this.Show();
            Left = SystemParameters.WorkArea.Right - 50 - (Width * (TimeTracker.MainWindow.AppWindow.counter + 1));
            Top = SystemParameters.WorkArea.Bottom - Height;
            this.Topmost = true;

            instanceIndex = MainWindow.AppWindow.counter;

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timerTick;
        }



        private void timerTick(object sender, EventArgs e)
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

            timerRunning = true;
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
            timerRunning = false;
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

        private void TaskWindowClosing(object sender, CancelEventArgs e)
        {
            if (MainWindow.AppWindow.counter > 0)
            {

                MainWindow.AppWindow.windowList.RemoveAt(instanceIndex);
                MainWindow.AppWindow.counter--;
                MessageBox.Show($"Counter: {MainWindow.AppWindow.counter.ToString()}, Instance ID: {instanceIndex}");
                MainWindow.AppWindow.ResetTaskWindowPositions();
            }

        }
    }
}

//Wrote this in the btnStart method by mistake. Should go in the New button event on Main
//TimeTracker.MainWindow.AppWindow.counter++;
//            TimeTracker.MainWindow.AppWindow.windowList.Add(this);
//            this.Show();
//Left = System.Windows.SystemParameters.WorkArea.Right - (Width * TimeTracker.MainWindow.AppWindow.counter);
//Top = System.Windows.SystemParameters.WorkArea.Bottom - Height;
//            this.Topmost = true;

