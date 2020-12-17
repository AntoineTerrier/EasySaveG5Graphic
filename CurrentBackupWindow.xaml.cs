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
    /// Logique d'interaction pour CurrentBackupWindow.xaml
    /// </summary>
    public partial class CurrentBackupWindow : Window
    {

        public Controller Controller { get; set; }


        public Thread thread { get; set; }

        public string SourceDir { get; set; }
        public string TargetDir { get; set; }



        public CurrentBackupWindow(Controller controller, string Source, string Target)
        {
            this.Controller = controller;

            InitializeComponent();

            SourceDir = Source;
            TargetDir = Target;

            //create a thread for the backup
            thread = new Thread(this.ThreadBackup);

            SourceTextBox.Text = SourceDir;
            TargetTextBox.Text = TargetDir;

            //launch the thread
            thread.Start(new List<string>() { Source, Target });


        }

        public void ThreadBackup(object param)
        {
            var list = (List<string>)param;

            Application.Current.Dispatcher.Invoke(() =>
            {
                PercentageTextBox.Text = "Full Backup started";
            });

            //launch the backup
            //  Dispatcher.Invoke(() =>
            //   {
            Barrier barrier = new Barrier(participantCount: 0);
            Controller.Barrier = barrier;
            Controller.Barrier.AddParticipant();
            Controller.doFullSave(list[0], list[1]);
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





    }
}

