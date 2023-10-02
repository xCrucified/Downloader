using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Downloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WebClient client;

        public MainWindow()
        {
            InitializeComponent();
            client = new WebClient();

            client.DownloadProgressChanged += Client_DownloadProgressChanged;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (client.IsBusy)
            {
                MessageBox.Show("Web Client is busy! Try agin later...");
                return;
            }

            string pathUrl = $"https://source.unsplash.com/random/{widthTxtBox.Text}x{heightTxtBox.Text}/?{categoryTxtBox.Text}&1";
            DownloadFileAsync(pathUrl);
        }

        private async void DownloadFileAsync(string address)
        {
            string dest = destTxtBox.Text;
            string fileName = new Random().Next(1,1234).ToString() + ".png";
            string destination = Path.Combine(dest, fileName);

            try
            {
                await client.DownloadFileTaskAsync(address, destination);
                MessageBox.Show("Download complete!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading file: {ex.Message}");
            }

            try
            {
                Process.Start(dest);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening file: {ex.Message}");
            }
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
