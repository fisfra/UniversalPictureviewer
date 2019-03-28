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

namespace UniversalPictureViewer
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class UPWWindow : Window
    {
        public enum EMode {directory_mode, database_mode};

        private UPWControler _controler;

        public UPWWindow(EMode mode)
        {
            InitializeComponent();

            _controler = new UPWControler(this, mode);

            _controler.ApplySetting();
        }

        private void imgViewer_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                _controler.HandleClose();
            }
        }

        private void imgViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            //cbFavourite.IsChecked = !cbFavourite.IsChecked;
            //_controler.ToggleFavourites();

            Mouse.OverrideCursor = null;
        }

        private void imgViewer_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                _controler.HandleClose();
            }
        }

        private void MouseThroughFav_Click(object sender, RoutedEventArgs e)
        {
            //_controler.HandleMouseThroughFav_Click();
        }
    }
}
