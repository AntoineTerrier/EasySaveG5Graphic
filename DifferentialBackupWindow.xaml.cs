using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.ComponentModel;
namespace EasySaveG5Graphic
{
    /// <summary>
    /// Logique d'interaction pour DifferentialBackupWindow.xaml
    /// </summary>
    public partial class DifferentialBackupWindow : Window, INotifyPropertyChanged
    {
        
        EasySaveG5Graphic.MessageBoxManager msgbox = new EasySaveG5Graphic.MessageBoxManager();
        public Controller Controller { get; set; }
        public DifferentialBackupWindow(Controller controller)
        {
            this.Controller = controller;
            InitializeComponent();
            DataContext = this;

        }
        
        #region INotifyPropertyChanged Member

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void SourceFolderButtonClicked(object sender, RoutedEventArgs e)
        {
            MessageBoxManager.Retry = "Please select the directory to back up";
            var sourceDialog = new System.Windows.Forms.FolderBrowserDialog
            {
               // Description = "Please select the directory to back up.",
                ShowNewFolderButton = false
            };

            if (sourceDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sourceTextBox.Text = sourceDialog.SelectedPath;
            }
        }

        private void TargetFolderButtonClicked(object sender, RoutedEventArgs e)
        {
            var targetDialog = new System.Windows.Forms.FolderBrowserDialog();
            //targetDialog.Description = "Please select the directory to back up.";
            targetDialog.ShowNewFolderButton = false;

            if (targetDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                targetTextBox.Text = targetDialog.SelectedPath;
            }
        }


        private void FullBackupFolderButtonClicked(object sender, RoutedEventArgs e)
        {
            var fullBackupDialog = new System.Windows.Forms.FolderBrowserDialog();
            //fullBackupDialog.Description = "Please select the directory to back up.";
            fullBackupDialog.ShowNewFolderButton = false;

            if (fullBackupDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fullBackupTextBox.Text = fullBackupDialog.SelectedPath;
            }
        }
        private void RunDiffBackUpButtonClicked(object sender, RoutedEventArgs e)
        {
            var runDialog = new System.Windows.Forms.FolderBrowserDialog();
            //runDialog.Description = "Please select source and target folders to back up.";
            if (Directory.Exists(sourceTextBox.Text) && Directory.Exists(targetTextBox.Text) && Directory.Exists(fullBackupTextBox.Text))
            {
                if (Process.GetProcessesByName("Calculator").Length > 0)
                {
                    System.Windows.MessageBox.Show("Please, close your enterprise software before running a backup");
                }
                else
                {

                    DiffCurrentBackupWindow objDiffCurrentBackupWindow = new DiffCurrentBackupWindow(Controller,fullBackupTextBox.Text, sourceTextBox.Text, targetTextBox.Text);
                    objDiffCurrentBackupWindow.Show();

                  //  Controller.doDiffSave(fullBackupTextBox.Text, sourceTextBox.Text, targetTextBox.Text);
                  //  System.Windows.MessageBox.Show("Ok!");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please, select a target directory, a source directory and your last full save directory");
            }

        }
        

        private void GoBackButtonClicked(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            objMainWindow.Show();
            this.Close();
        }
        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
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

 