﻿using System;
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
using CleanShot.Models;

namespace CleanShot.Windows
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class Options : Window
    {
        public Options()
        {
            InitializeComponent();
            DataContext = Settings.Current;
        }

        private void buttonBrowse_Click(object sender, RoutedEventArgs e)
        {
            var browser = new System.Windows.Forms.FolderBrowserDialog();
            browser.ShowDialog();
            if (Directory.Exists(browser.SelectedPath))
            {
                textSaveFolder.Text = browser.SelectedPath;
                Settings.Current.SaveFolder = browser.SelectedPath;
            }
        }

        private void buttonUninstall_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("This will remove the settings and log files in AppData.  Proceed?", "Remove Data", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\CleanShot", true);
                Settings.Current.Uninstalled = true;
                this.Close();
            }
        }

        private void textSaveFolder_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Directory.CreateDirectory(textSaveFolder.Text);
            }
            catch
            {
                textSaveFolder.Text = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"\CleanShot\Images\");
                MessageBox.Show("Unable to create the specified directory.", "Invalid Directory", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.Save();
        }
    }
}