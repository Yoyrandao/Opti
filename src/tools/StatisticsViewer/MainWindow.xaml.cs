using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

using BackgroundAgent.Configuration;
using BackgroundAgent.Constants;
using BackgroundAgent.Requests;

using CommonTypes.Configuration;

using FtpDataAccess.Factories;
using FtpDataAccess.Helpers;
using FtpDataAccess.Repositories;

using Microsoft.Extensions.Configuration;

using Utils.Certificates;
using Utils.Http;

namespace StatisticsViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;
            
            InitializeComponent();
            LoadFiles();
        }

        private async void LoadFiles()
        {
            await Task.Run(() =>
            {
                var configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", false, true)
                   .Build();

                var options = new FtpConnectionOptions();
                var certificateOptions = new CertificateConfiguration();
                configuration.Bind("FtpConnection", options);
                configuration.Bind("Certificate", certificateOptions);

                var certificateProvider = new CertificateProvider();
                var restFactoryResolver = new RestClientFactoryResolver(new EndpointConfiguration()
                {
                    Backend = "https://localhost:5001/"
                }, certificateProvider, certificateOptions);

                var restFactory = restFactoryResolver.Resolve(Endpoint.SyncGateway);
                var requestFactory = new RequestFactory();
            
                var client = restFactory.Create();
            
                var directoryFiles = Directory.GetFiles(FsLocation.ApplicationData).Select(x => new FileInfo(x).Name);
            
                foreach (var file in directoryFiles)
                {
                    var response = client.Execute<ApiResponse>(requestFactory.CreateGetFileInfoRequest(file));
                    var size = response.Data.Data is long data ? data : 0;

                    App.Current.Dispatcher.Invoke(() =>
                    {
                        _fileCollection.Add(new FileModel
                        {
                            Name = file,
                            Size = $"{((long)(size / 1024)).ToString()} КБ"
                        });
                    });
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<FileModel> _fileCollection = new();
        public ObservableCollection<FileModel> FileCollection
        {
            get => _fileCollection;
            set
            {
                _fileCollection = value;
                OnPropertyChanged(nameof(FileCollection));
            }
        }
    }
}