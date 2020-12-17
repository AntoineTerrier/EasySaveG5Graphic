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
using System.Windows.Forms;
using System.IO;
using MessageBox = System.Windows.Forms.MessageBox;

namespace EasySaveG5Graphic
{
    /// <summary>
    /// Logique d'interaction pour EntepriseSoftwareWindow.xaml
    /// </summary>
    public partial class EntepriseSoftwareWindow : Window
    {
        public Controller Controller { get; set; }
        public EntepriseSoftwareWindow(Controller controller)
        {
            this.Controller = controller;
            InitializeComponent();
        }

        private void ChooseEnterpriseSoftwareButtonClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string softwarePath = openFileDialog.FileName;

                EnterpriseSoftwarePathTextBox.Text = softwarePath;

            }
        }

        private void SubmitSoftwarePathButtonClicked(object sender, RoutedEventArgs e)
        {
            EasySaveG5Graphic.MessageBoxManager msgbox = new EasySaveG5Graphic.MessageBoxManager();
            if (!File.Exists("EnterpriseSoftware.txt"))
            {
                File.CreateText("EnterpriseSoftware.txt");
            }
            StreamWriter writer = new StreamWriter("EnterpriseSoftware.txt");

            writer.WriteLine((string)EnterpriseSoftwarePathTextBox.Text);
            writer.Close();
            MessageBoxManager.OK = "OK";
            MessageBoxManager.Register();
            MessageBox.Show("Ok!", "EasySave", MessageBoxButtons.OK);
            MessageBoxManager.Unregister();
        }

        private void GoBackButtonClicked(object sender, RoutedEventArgs e)
        {
            SettingWindow objFullBackupWindow = new SettingWindow(Controller);
            objFullBackupWindow.Show();
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
