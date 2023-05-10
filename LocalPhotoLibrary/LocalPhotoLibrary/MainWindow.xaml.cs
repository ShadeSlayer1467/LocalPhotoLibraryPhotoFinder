using MyPhoto;
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

namespace LocalPhotoLibrary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void lstPhotos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Photo photo = lstPhotos.SelectedItem as Photo;

            // open photo URL in explorer window with photo selected (highlighted)
            System.Diagnostics.Process.Start("explorer.exe", "/select, " + photo.URL);
        }
    }
}
