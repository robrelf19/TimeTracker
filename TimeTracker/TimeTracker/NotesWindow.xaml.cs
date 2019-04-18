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
            string notesTime = TaskWindow.taskWindow.totalElapseTime.ToString("hh':'mm':'ss");
            //string notesDay = System.DateTime.Now.DayOfWeek.ToString();
            string notesDay = System.DateTime.Now.Date.ToString("dd/M/yyyy");
            string notesTitle = TaskWindow.taskWindow.txtTaskName.Text;
            txtNotes.Text = $"{notesTitle}, {notesDay}, {notesTime}\r\n";

            txtNotes.GotFocus += delegate
            {
                txtNotes.SelectAll();
            };
        }
    }
}
