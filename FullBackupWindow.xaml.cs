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
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace EasySaveG5Graphic
{
    /// <summary>
    /// Logique d'interaction pour FullBackupWindow.xaml
    /// </summary>
    public partial class FullBackupWindow : Window
    {
        public Controller Controller { get; set; }

        public FullBackupWindow(Controller controller)
        {
            this.Controller = controller;
            InitializeComponent();
        }


        private void SourceFolderButtonClicked(object sender, RoutedEventArgs e)
        {
            var sourceDialog = new System.Windows.Forms.FolderBrowserDialog();
            sourceDialog.Description = "Please select the directory to back up.";
            sourceDialog.ShowNewFolderButton = false;

            if (sourceDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                sourceTextBox.Text = sourceDialog.SelectedPath;

            }
        }

        private void TargetFolderButtonClicked(object sender, RoutedEventArgs e)
        {
            var targetDialog = new System.Windows.Forms.FolderBrowserDialog();
            targetDialog.Description = "Please select the directory to back up.";
            targetDialog.ShowNewFolderButton = false;

            if (targetDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                targetTextBox.Text = targetDialog.SelectedPath;
            }
        }
        private void RunFullBackUpButtonClicked(object sender, RoutedEventArgs e)
        {
            var runDialog = new System.Windows.Forms.FolderBrowserDialog();
            runDialog.Description = "Please select source and target folders to back up.";

            if (Directory.Exists(sourceTextBox.Text) && Directory.Exists(targetTextBox.Text))
            {
                if (Process.GetProcessesByName("Calculator").Length > 0)
                {
                    System.Windows.MessageBox.Show("Please, close your enterprise software before running a backup");
                }
                else
                {

                    CurrentBackupWindow objcurrentBackupWindow = new CurrentBackupWindow(Controller, sourceTextBox.Text, targetTextBox.Text);
                    objcurrentBackupWindow.Show();


                    //Create a thread
                    /*  Thread thread = new Thread(new ThreadStart(() =>
                     {
                         Controller.doFullSave(sourceTextBox.Text, targetTextBox.Text);
                         System.Windows.MessageBox.Show("Ok!");

                         //create the window
                         CurrentBackupWindow objCurrentBackupWindow = new CurrentBackupWindow(Controller);

                         //show the window
                         objCurrentBackupWindow.Show();

                         //start the Dispatcher Processing
                         System.Windows.Threading.Dispatcher.Run();
                }));
                         //set the apartment state
                        thread.SetApartmentState(ApartmentState.STA);
                        //make the thread a background thread
                        thread.IsBackground = true;
                    //start the thread
                    thread.Start(); */
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please, select a target directory and a source directory");
            }
        }



        private void GoBackButtonClicked(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            objMainWindow.Show();
            this.Close();
        }
        private void ListViewItem_MouseEnter(object sender, EventArgs e)
        {
            // Set tooltip visibility

            if (Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_DiffBackup.Visibility = Visibility.Collapsed;
                tt_FullBackup.Visibility = Visibility.Collapsed;
                tt_Parameters.Visibility = Visibility.Collapsed;

            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_DiffBackup.Visibility = Visibility.Visible;
                tt_FullBackup.Visibility = Visibility.Visible;
                tt_Parameters.Visibility = Visibility.Visible;

            }
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 0.3;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void OpenFullBackupWindow(object sender, RoutedEventArgs e)
        {
            FullBackupWindow objFullBackupWindow = new FullBackupWindow(Controller);
            objFullBackupWindow.Show();
            this.Close();
        }

        private void OpenDifferentialBackupWindow(object sender, RoutedEventArgs e)
        {
            DifferentialBackupWindow objFullBackupWindow = new DifferentialBackupWindow(Controller);

            objFullBackupWindow.Show();
            this.Close();
        }

        private void OpenSettingWindow(object sender, RoutedEventArgs e)
        {
            SettingWindow objFullBackupWindow = new SettingWindow(Controller);
            objFullBackupWindow.Show();
            this.Close();
        }
    }
}
