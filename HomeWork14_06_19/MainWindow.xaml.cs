using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HomeWork14_06_19
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Thread backgroundMusicThread;
        private Thread fileSaveThread;

        public MainWindow()
        {
            InitializeComponent();

            this.Closed += MainWindowClosed;

            backgroundMusicMediaElement.MediaEnded += BackgroundMusicMediaElementMediaEnded;
            backgroundMusicMediaElement.Source = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}\music.mp3");

            fileSaveThread = new Thread(SaveTextToFile) { IsBackground = false };

            backgroundMusicThread = new Thread(backgroundMusicMediaElement.Play) { IsBackground = true };
            backgroundMusicThread.Start();
        }

        private void BackgroundMusicMediaElementMediaEnded(object sender, RoutedEventArgs e)
        {
            backgroundMusicThread.Start();
        }

        private void MainWindowClosed(object sender, EventArgs e)
        {
            fileSaveThread.Start();
        }

        private void SaveTextToFile()
        {
            string text = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;

            using (var streamWriter = new StreamWriter(File.Open("Text.txt", FileMode.Create)))
            {
                streamWriter.Write(text);
            }
        }
    }
}
