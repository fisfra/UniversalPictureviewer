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
using UniversalPictureViewer;

namespace UPWTester
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

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Settings.WindowTitle = "My Viewer";
            Settings.ViewerGroupBoxHeader = "Image";
            Settings.PictureDirectory = @"C:\Users\d040841\Pictures\Saved Pictures";
            UPWWindow viewer = new UPWWindow(UPWWindow.EMode.directory_mode);

            viewer.ShowDialog();
        }
    }
}
