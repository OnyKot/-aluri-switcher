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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Switcher.utils;
namespace Switcher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SSwitcher switcher;
        CertManager certsManager;
        public static bool cert_installed;
        public MainWindow()
        {
            InitializeComponent();
            switchButton.Content = "Ожидание...";
            certsManager = new CertManager();
            switcher = new SSwitcher(tr.misaneIP);
            switchButton.IsEnabled = false;
            init();
        }

        public async void init()
        {
            await CheckCertificate();

            await CheckServer();
        }

        private async Task CheckServer()
        {
            switchButton.IsEnabled = false;
            var currentServer = await switcher.getCurrsrvAsync();
            statusLabel.Content = (currentServer == SSwitcher.Servers.minase)
                ? tr.onminase : tr.onoff;
            switchButton.Content = (currentServer == SSwitcher.Servers.Off)
                ? tr.switchtominase : tr.switchtooff;
            switchButton.IsEnabled = true;
        }

        private async Task CheckCertificate()
        {
            var certificateStatus = await certsManager.StatusAsync();
            cert_installed = certificateStatus;
        }
  

        private void switchButton_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }



        private async void switchButton_Click_1(object sender, RoutedEventArgs e)
        {
            var srv = await switcher.getCurrsrvAsync();
            try
            {
                if (!cert_installed)
                {
                    certsManager.install();
                }
                if(srv == SSwitcher.Servers.Off)
                {
                    switcher.SwitchToMinase();

                }
                else
                {
                    switcher.SwitchToOff();
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Ошибка при переключении, У вас включен антивирус? пользователь имеет право на редактирование файлов windows? есть место на диске?\nПодробности:\n\n" + ex);
            }

            await CheckServer();
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void closeButton_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
    }
