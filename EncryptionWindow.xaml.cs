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
using System.IO;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace EasySaveG5Graphic
{
    /// <summary>
    /// Logique d'interaction pour EncryptionWindow.xaml
    /// </summary>
    public partial class EncryptionWindow : Window
    {
        public Controller Controller { get; set; }
        public EncryptionWindow(Controller controller)
        {
            this.Controller = controller;
            InitializeComponent();

            if (!File.Exists("ExtensionFile.txt"))
            {
                File.CreateText("ExtensionFile.txt");
            }
            using (StreamReader r = new StreamReader("ExtensionFile.txt"))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    extensionListBox.Items.Add(line);
                }
            }
        }

        private void AddExtensionButtonClicked(object sender, RoutedEventArgs e)
        {
            extensionListBox.Items.Add("." + extensionTextBox.Text);
        }

        private void RemoveExtensionButtonClicked(object sender, RoutedEventArgs e)
        {
            if (extensionListBox.SelectedIndex != -1)
            {
                extensionListBox.Items.RemoveAt(extensionListBox.SelectedIndex);
            }
        }

        private void SubmitExtensionButtonClicked(object sender, RoutedEventArgs e)
        {
            EasySaveG5Graphic.MessageBoxManager msgbox = new EasySaveG5Graphic.MessageBoxManager();
           
            if (!File.Exists("ExtensionFile.txt"))
            {
                File.CreateText("ExtensionFile.txt");
            }

            StreamWriter writer = new StreamWriter("ExtensionFile.txt");

            for (int i = 0; i < extensionListBox.Items.Count; i++)
            {
                writer.WriteLine((string)extensionListBox.Items[i]);
            }
            writer.Close();
            //if(Controller.cmb ==0)
            //{
            //    //Properties.Settings.Default.LangueCode = "fr-FR";
            //    MessageBoxManager.OK = "OK";
            //    MessageBoxManager.Register();
            //    MessageBox.Show("Fichier validé!","EasySave", MessageBoxButtons.OK);
            //    MessageBoxManager.Unregister();
            //}
           //else
           // {
                MessageBoxManager.OK = "OK";
                MessageBoxManager.Register();
                MessageBox.Show("Ok!", "EasySave", MessageBoxButtons.OK);
                MessageBoxManager.Unregister();
            //}
            

        }

        private void GoBackButtonClicked(object sender, RoutedEventArgs e)
        {
            SettingWindow objFullBackupWindow = new SettingWindow(Controller);
            objFullBackupWindow.Show();
            this.Close();
        }

        private void extensionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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