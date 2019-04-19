using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace TimeTracker
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        public NotesWindow()
        {
            InitializeComponent();

            SetupNotesBox();
            
            this.Show();
        }

        private void SetupNotesBox()
        {
            //string notesTime = TaskWindow.taskWindow.totalElapseTime.ToString("hh':'mm':'ss");
            string notesTime = TaskWindow.taskWindow.totalElapseTime.ToString("hh':'mm");
            //string notesDay = System.DateTime.Now.DayOfWeek.ToString();
            string notesDay = System.DateTime.Now.Date.ToString("dd/M/yyyy");
            string notesTitle = TaskWindow.taskWindow.txtTaskName.Text;
            txtNotes.Text = $"Task: {notesTitle}, Date/Time: {notesDay}, {notesTime}\r\n";

            txtNotes.GotFocus += delegate
            {
                txtNotes.SelectAll();
            };
        }

        private void BtnNotesSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialoge = new SaveFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };

            if (saveDialoge.ShowDialog() == true)
            {
                File.WriteAllText(saveDialoge.FileName, txtNotes.Text);
            }

            TaskWindow.taskWindow.Close();
            this.Close();
        }
    }
}
