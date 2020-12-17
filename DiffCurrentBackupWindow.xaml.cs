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
using System.IO;



namespace EasySaveG5Graphic
{
    /// <summary>
    /// Logique d'interaction pour DiffCurrentBackupWindow.xaml
    /// </summary>
    public partial class DiffCurrentBackupWindow : Window
    {
        public Controller Controller { get; set; }


        public Thread thread { get; set; }

        public string SourceDir { get; set; }
        public string TargetDir { get; set; }
        public string LastFullDir { get; set; }

        public DiffCurrentBackupWindow(Controller controller, string LastFullBackup, string Source, string Target)
        {
            this.Controller = controller;
            InitializeComponent();

            SourceDir = Source;
            TargetDir = Target;
            LastFullDir = LastFullBackup;


            //create a thread for the backup
            thread = new Thread(this.ThreadBackup);

            SourceTextBox.Text = SourceDir;
            TargetTextBox.Text = TargetDir;
            FullBackupUsedTextBox.Text = LastFullDir;

            //launch the thread
            thread.Start(new List<string>() { LastFullBackup, Source, Target });


        }

        public void ThreadBackup(object param)
        {
            var list = (List<string>)param;

            Application.Current.Dispatcher.Invoke(() =>
            {
                PercentageTextBox.Text = "Full Backup started";
            });

            Barrier barrier = new Barrier(participantCount: 0);
            Controller.Barrier = barrier;
            Controller.Barrier.AddParticipant();
            //launch the backup
            Controller.doDiffSave(list[0], list[1], list[2]);

            Application.Current.Dispatcher.Invoke(() =>
            {
                PercentageTextBox.Text = "Saved successfully";
            });


        }

        private void PauseBackupButton(object sender, RoutedEventArgs e)
        {
            //Feature to add
        }

        private void PlayBackupButton(object sender, RoutedEventArgs e)
        {
            //Feature to add
        }

        private void StopBackupButton(object sender, RoutedEventArgs e)
        {

            thread.Abort();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
